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
using System.Linq;

public class RealtimeDbManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference databaseRef;

    public TextMeshProUGUI accountNameUI;
    public static RealtimeDbManager instance;

    public int weekOfYear;
    public string dayOfWeek;
    public string date;

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

        Invoke("GoOffline", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    //function adds user to realtimedb list of currently online users, runs once on startup
    public void CurrentlyActive()
    {
        CultureInfo myCI = new CultureInfo("en-US");
        Calendar myCal = myCI.Calendar;

        CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
        DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
        weekOfYear = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
        Debug.Log(weekOfYear);

        DateTime dt = System.DateTime.Now;
        dayOfWeek = dt.DayOfWeek.ToString();
        date = dt.ToString("yyyy-MM-dd");


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

                    if (newDay.currentlyActive != null)
                        {
                            bool firstLogin = true;

                            for (int i = 0; i < newDay.wasActive.Length; i++)
                            {
                                //checks wasActive list for user to see if this is first login
                                if (newDay.wasActive[i] == auth.CurrentUser.UserId)
                                {
                                    firstLogin = false;
                                }
                            }

                            List<string> currentlyActive = newDay.currentlyActive.ToList();
                            List<string> wasActive = newDay.wasActive.ToList();

                            currentlyActive.Add(auth.CurrentUser.UserId);
                            if (firstLogin)
                            {
                                wasActive.Add(auth.CurrentUser.UserId);
                            }

                            string[] currentActiveArray = currentlyActive.ToArray();
                            string[] wasActiveArray = wasActive.ToArray();

                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("wasActive").SetValueAsync(wasActiveArray);
                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("currentlyActive").SetValueAsync(currentActiveArray);
                            Debug.Log("Update success");
                        } else
                        {
                            bool firstLogin = true;

                            for (int i = 0; i < newDay.wasActive.Length; i++)
                            {
                                //checks wasActive list for user to see if this is first login
                                if (newDay.wasActive[i] == auth.CurrentUser.UserId)
                                {
                                    firstLogin = false;
                                }
                            }

                            List<string> wasActive = newDay.wasActive.ToList();


                            if (firstLogin)
                            {
                                wasActive.Add(auth.CurrentUser.UserId);
                            }

                            string[] currentActiveArray = { auth.CurrentUser.UserId };
                            string[] wasActiveArray = wasActive.ToArray();

                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("wasActive").SetValueAsync(wasActiveArray);
                            databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dt.DayOfWeek.ToString()).Child("currentlyActive").SetValueAsync(currentActiveArray);
                            Debug.Log("Update success");
                        }

                       
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
        float finalTime = float.Parse(twoDP, CultureInfo.InvariantCulture.NumberFormat);
        Debug.Log(finalTime);

        float currentTotalMin;
        string currentTime = DateTime.Now.ToString("HH:mm:ss");
        string dateTitle = date + "T" + currentTime;
        Debug.Log(dateTitle);

        //get current total time spent for the day (min)
        Query findDay = databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayOfWeek);
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

            DataSnapshot ds = task.Result;
            if (ds.Exists)
            {
                Day newDay = JsonUtility.FromJson<Day>(ds.GetRawJsonValue());
                currentTotalMin = newDay.totalPlaySession;
                currentTotalMin += finalTime;
                databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayOfWeek).Child("totalPlaySession").SetValueAsync(currentTotalMin);
                //
            }
        });

        Query findPlayerTime = databaseRef.Child("playerProfileData").Child(auth.CurrentUser.UserId);
        findPlayerTime.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                throw task.Exception;
            }

            if (!task.IsCompleted)
            {
                return;
            }

            DataSnapshot ds = task.Result;
            if (ds.Exists)
            {
                PlayerProfileData profile = JsonUtility.FromJson<PlayerProfileData>(ds.GetRawJsonValue());
                currentTotalMin = profile.totalTimePlayed;
                currentTotalMin += finalTime;
                databaseRef.Child("playerProfileData").Child(auth.CurrentUser.UserId).Child("totalTimePlayed").SetValueAsync(currentTotalMin);

                PlayerSessionTime newSession = new PlayerSessionTime(date, dayOfWeek, currentTime, finalTime, dateTitle);
                string sessionJson = JsonUtility.ToJson(newSession);
                databaseRef.Child("playerSessionTime").Child(auth.CurrentUser.UserId).Child(dateTitle).SetRawJsonValueAsync(sessionJson);
            }
        });

        //this function takes off user from list of currently active users;
        Query findDayTime = databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayOfWeek);
        findDayTime.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                throw task.Exception;
            }

            if (!task.IsCompleted)
            {
                return;
            }

            DataSnapshot ds = task.Result;
            if (ds.Exists)
            {
                Day newDay = JsonUtility.FromJson<Day>(ds.GetRawJsonValue());
                List<string> currentlyActive = newDay.currentlyActive.ToList();
                currentlyActive.Remove(auth.CurrentUser.UserId);
                string[] currentActiveArray = currentlyActive.ToArray();
                Debug.Log(currentActiveArray);
                databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayOfWeek).Child("currentlyActive").SetValueAsync(currentActiveArray);
            }
        });
    }
}