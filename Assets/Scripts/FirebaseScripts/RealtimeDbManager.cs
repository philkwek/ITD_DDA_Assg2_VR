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
        GoOffline();
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
        int weekOfYear = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
        Debug.Log(weekOfYear);

        DateTime dt = System.DateTime.Now;
        Debug.Log(dt.DayOfWeek);
        Debug.Log(dt.ToString("yyyy-MM-dd"));


        //First, checks if database has current week data
        Query searchWeek = databaseRef.Child("weeklyActive").OrderByChild("weekNumber").EqualTo(weekOfYear).LimitToFirst(1);
        searchWeek.GetValueAsync().ContinueWithOnMainThread(task =>
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
                    Weekly weekly = JsonUtility.FromJson<Weekly>(snap.GetRawJsonValue());
                    Debug.Log(weekly.weekNumber);
                }
            }
            else
            {
                Debug.Log("Does not exist");
                Debug.Log(auth.CurrentUser.UserId);
                string[] dayArray = { "Sunday", "Monday", "Tueday", "Wednesday", "Thursday", "Friday", "Saturday"};

                for(int i = 0; i<dayArray.Length; i++)
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
        Debug.Log(timeNow);
    }
}