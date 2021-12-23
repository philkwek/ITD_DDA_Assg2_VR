using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;
using System;
using System.Globalization;

public class RealtimeDbManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference databaseRef;

    public TextMeshProUGUI accountNameUI;
    public static RealtimeDbManager instance;

    public int weekOfYear;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentlyActive();

        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //GoOffline();
    }

    public void InsertUsername()
    {
        //Changes username in player child
        Query searchPlayer = databaseRef.Child("players").OrderByChild("userID").EqualTo(auth.CurrentUser.UserId).LimitToFirst(1);

        searchPlayer.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                throw task.Exception;
            }

            if (!task.IsCompleted)
            {
                return;
            }

            //Take snapshot of database
            DataSnapshot ds = task.Result;
            if (ds.Exists)
            {
                Debug.Log("Player found...");

                foreach (DataSnapshot snap in ds.Children)
                {
                    Player player = JsonUtility.FromJson<Player>(snap.GetRawJsonValue());
                    Debug.Log(player.username);
                    accountNameUI.text = "Account: " +  player.username;
                }
            }
            else
            {
                Debug.Log("Player does not exist");
            }
           });
    }

    //function adds user to realtimedb list of currently online users
    public void CurrentlyActive()
    {
        CultureInfo myCI = new CultureInfo("en-US");
        Calendar myCal = myCI.Calendar;

        CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
        DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
        weekOfYear = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
        Debug.Log(weekOfYear);

        DateTime dt = System.DateTime.Now;
        Debug.Log(dt.DayOfWeek);
        Debug.Log(dt.ToString("yyyy-MM-dd"));


        //First, checks if database has current week data
        Query findWeek = databaseRef.Child("weeklyActive").Child("week" + weekOfYear);
        findWeek.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                throw task.Exception;
            }

            if (!task.IsCompleted)
            {
                return;
            }

            //Take snapshot of database
            DataSnapshot ds = task.Result;
            if (ds.Exists)
            {
                Debug.Log("Player found...");
                string[] dayArray = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                Weekly weekly = JsonUtility.FromJson<Weekly>(ds.GetRawJsonValue());
                Debug.Log(weekly.weekNumber);

                Query findDay = databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString());
                findDay.GetValueAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.Exception != null)
                    {
                        throw task.Exception;
                    }

                    if (!task.IsCompleted)
                    {
                        return;
                    }
                    DataSnapshot day = task.Result;
                    if (day.Exists)
                    {
                        Day newDay = JsonUtility.FromJson<Day>(day.GetRawJsonValue());
                        Debug.Log(newDay.currentlyActive[0]);

                        bool firstLogin = true;

                        for (int i = 0; i < newDay.currentlyActive.Length; i++)
                        {
                            //checks wasActive list for user to see if this is first login
                            if (newDay.wasActive[i] == auth.CurrentUser.UserId)
                            {
                                firstLogin = false;
                            }
                        }
                        if (firstLogin)
                        {
                            //add user to current array for both currently and was
                            int newLength = newDay.currentlyActive.Length + 1;
                            string[] currentlyActive = new string[newLength];
                            string[] wasActive = new string[newLength];

                            for (int i = 0; i < newLength; i++)
                            {
                                //checks wasActive list for user to see if this is first login
                                if (newDay.wasActive[i] != null && newDay.currentlyActive != null)
                                {
                                    currentlyActive[i] = newDay.currentlyActive[i];
                                    wasActive[i] = newDay.wasActive[i];

                                } else if (newDay.wasActive[i] != null)
                                {
                                    wasActive[i] = newDay.wasActive[i];

                                } else if (newDay.wasActive[i] == null && newDay.currentlyActive == null)
                                {
                                    currentlyActive[i] = auth.CurrentUser.UserId;
                                    wasActive[i] = auth.CurrentUser.UserId;
                                }
                            }
                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("wasActive").SetValueAsync(wasActive);
                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("currentlyActive").SetValueAsync(currentlyActive);
                        }
                        else
                        {
                            //add user to current array for both currently and was
                            int newLength = newDay.currentlyActive.Length + 1;
                            string[] currentlyActive = new string[newLength];
                            string[] wasActive = new string[newLength];

                            for (int i = 0; i < newLength; i++)
                            {
                                //checks wasActive list for user to see if this is first login
                                if (newDay.wasActive[i] != null && newDay.currentlyActive != null)
                                {
                                    currentlyActive[i] = newDay.currentlyActive[i];
                                    wasActive[i] = newDay.wasActive[i];

                                }
                                else if (newDay.wasActive[i] != null)
                                {
                                    wasActive[i] = newDay.wasActive[i];

                                }
                                else if (newDay.wasActive[i] == null && newDay.currentlyActive == null)
                                {
                                    currentlyActive[i] = auth.CurrentUser.UserId;
                                }
                            }
                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("wasActive").SetValueAsync(wasActive);
                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("currentlyActive").SetValueAsync(currentlyActive);
                        }


                        
                        Debug.Log("Update success");
                    }
                    else
                    {
                        Debug.Log("Day not found! Creating new day...");
                        //Add day
                        string[] dayArray = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                        for (int i = 0; i < dayArray.Length; i++)
                        {
                            if (dayArray[i] == dt.DayOfWeek.ToString()) //dt.DayOfWeek.ToString()
                            {
                                //adds user to the currently active and was active
                                string[] currentActive = { auth.CurrentUser.UserId.ToString() };
                                string date = dt.ToString("yyyy-MM-dd");
                                float totalPlaySession = 0;
                                string[] wasActive = { auth.CurrentUser.UserId.ToString() };

                                Day createDay = new Day(currentActive, date, totalPlaySession, wasActive);
                                string dayJson = JsonUtility.ToJson(createDay);
                                databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayArray[i]).SetRawJsonValueAsync(dayJson);
                            }
                        }
                    }
                });

            }
            else
            {
                Debug.Log("Does not exist");
                Debug.Log(auth.CurrentUser.UserId);
                string[] dayArray = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

                for (int i = 0; i < dayArray.Length; i++)
                {
                    if (dayArray[i] == dt.DayOfWeek.ToString())
                    {
                        //adds user to the currently active and was active
                        string[] currentActive = { auth.CurrentUser.UserId.ToString() };
                        string date = dt.ToString("yyyy-MM-dd");
                        float totalPlaySession = 0;
                        string[] wasActive = { auth.CurrentUser.UserId.ToString() };

                        Day day = new Day(currentActive, date, totalPlaySession, wasActive);
                        string dayJson = JsonUtility.ToJson(day);

                        databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayArray[i]).SetRawJsonValueAsync(dayJson);
                        databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child("weekNumber").SetValueAsync(weekOfYear);
                    }
                }
            }
        });
    }

    public void GoOffline()
    {
        float timeNow = Time.realtimeSinceStartup;
        float timeMinute = timeNow / 60;
        string twoDP = timeMinute.ToString("F2");
        Debug.Log(twoDP);

        //get current total time spent for the day (min)
        Query getTime = databaseRef.Child("weeklyActive").OrderByChild("weekNumber").EqualTo(weekOfYear).LimitToFirst(1);
        //text
    }
}