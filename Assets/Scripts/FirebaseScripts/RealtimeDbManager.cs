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
using UnityEngine.SceneManagement;

public class RealtimeDbManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference databaseRef;

    public GameObject accountNameUI;
    public GameObject onlinePlayersUI;
    public static RealtimeDbManager instance;

    public int weekOfYear;
    public string dayOfWeek;
    public string date;

    //Player profile data
    public int completion;
    public int noOfMinigamesCompleted;
    public int noOfTaskCompleted;

    //Player game data to store
    public int objectsPicked;
    public int noOfCraftsMade;

    //Minigame Stats to store;
    public float minigameAccuracyPercentage;
    public int minigameHighscore;
    public float minigameLongestRound;
    public int minigameThrowStreak;
    public int minigameTotalHits;
    public int minigameTotalMiss;
    public int minigameTotalThrows;

    //Recycle Game Data
    public float startingTime;


    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        InvokeRepeating("GetNoOnline", 2.0f, 10.0f);
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        if (scene.name == "MainMenu")
        {
            onlinePlayersUI = GameObject.Find("PlayersOnline");
            accountNameUI = GameObject.Find("AccountName");
            Invoke("CurrentlyActive", 1);
            Invoke("GetGameData", 1);
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

                foreach (DataSnapshot snap in ds.Children)
                {
                    Player player = JsonUtility.FromJson<Player>(snap.GetRawJsonValue());
                    Debug.Log(player.username);
                    accountNameUI.GetComponent<TextMeshProUGUI>().text = "Account: " +  player.username;
                }
            }
            else
            {
                Debug.Log("Player does not exist");
            }
        });
    }

    public void GetGameData()
    {
        //This function is to check for game parameter updates from admin client (currently supports recycle game adjustment of starting time
        Query findRecycleData = databaseRef.Child("gameParameters").Child("recycle");
        findRecycleData.GetValueAsync().ContinueWithOnMainThread(task =>
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
                RecycleData data = JsonUtility.FromJson<RecycleData>(ds.GetRawJsonValue());
                startingTime = data.startingTime;
                Debug.Log("Currently set starting time for recycle: " + startingTime);
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

        //Then, gets current minigame stats
        Query findMinigame = databaseRef.Child("playerGameData").Child(auth.CurrentUser.UserId).Child("minigameStats");
        findMinigame.GetValueAsync().ContinueWithOnMainThread(task => {
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
                MinigameStats oldStats = JsonUtility.FromJson<MinigameStats>(ds.GetRawJsonValue());

                string accuracyString = oldStats.accuracyPercentage;
                string[] newString = accuracyString.Split("%"[0]);
                minigameAccuracyPercentage = float.Parse(newString[0]);
                minigameHighscore = oldStats.highscore;
                minigameLongestRound = oldStats.longestRoundMinutes;
                minigameThrowStreak = oldStats.longestThrowStreak;
                minigameTotalHits = oldStats.totalHits;
                minigameTotalMiss = oldStats.totalMiss;
                minigameTotalThrows = oldStats.totalThrows;
            }

        });

        //Lastly, gets current profile data stats
        Query findProfileData = databaseRef.Child("playerProfileData").Child(auth.CurrentUser.UserId);
        findProfileData.GetValueAsync().ContinueWithOnMainThread(task => {
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
                PlayerProfileData newProfile = JsonUtility.FromJson<PlayerProfileData>(ds.GetRawJsonValue());
                completion = newProfile.completion;
                noOfMinigamesCompleted = newProfile.noOfMinigamesCompleted;
                noOfTaskCompleted = newProfile.noOfTaskCompleted;
            }

        });
    }

    public void GoOffline()
    {
        float timeNow = Time.realtimeSinceStartup;
        float timeMinute = timeNow / 60;
        string twoDP = timeMinute.ToString("F2");
        float finalTime = float.Parse(twoDP, CultureInfo.InvariantCulture.NumberFormat);

        float currentTotalMin;
        string currentTime = DateTime.Now.ToString("HH:mm:ss");
        string dateTitle = date + "T" + currentTime;

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
        //updates player's own stat in firebase for totaltimespend ingame
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
                databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayOfWeek).Child("currentlyActive").SetValueAsync(currentActiveArray);
            }
        });

        //this function updates the totalObjPicked & noOfCrafts made stat for player
        Query findObjPicked = databaseRef.Child("playerGameData").Child(auth.CurrentUser.UserId);
        findObjPicked.GetValueAsync().ContinueWithOnMainThread(task =>
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
                PlayerGameData gameData = JsonUtility.FromJson<PlayerGameData>(ds.GetRawJsonValue());
                int oldObjPicked = gameData.totalObjPicked;
                int oldCraftsMade = gameData.noOfCraftsMade;

                int newObjPicked = oldObjPicked + objectsPicked;
                int newCraftsMade = oldCraftsMade + noOfCraftsMade;

                databaseRef.Child("playerGameData").Child(auth.CurrentUser.UserId).Child("totalObjPicked").SetValueAsync(newObjPicked);
                databaseRef.Child("playerGameData").Child(auth.CurrentUser.UserId).Child("noOfCraftsMade").SetValueAsync(newCraftsMade);
            }
        });

        //this function updates player's minigame data
        string minigameAccuracy = minigameAccuracyPercentage + "%";
        MinigameStats newStats = new MinigameStats(minigameAccuracy, minigameHighscore, minigameLongestRound,
            minigameThrowStreak, minigameTotalHits, minigameTotalMiss, minigameTotalThrows);
        string statsJson = JsonUtility.ToJson(newStats);
        databaseRef.Child("playerGameData").Child(auth.CurrentUser.UserId).Child("minigameStats").SetRawJsonValueAsync(statsJson);

        //this functions updates player's profile data
        databaseRef.Child("playerProfileData").Child(auth.CurrentUser.UserId).Child("completion").SetValueAsync(completion);
        databaseRef.Child("playerProfileData").Child(auth.CurrentUser.UserId).Child("noOfMinigamesCompleted").SetValueAsync(noOfMinigamesCompleted);
        databaseRef.Child("playerProfileData").Child(auth.CurrentUser.UserId).Child("noOfTaskCompleted").SetValueAsync(noOfTaskCompleted);
        databaseRef.Child("playerProfileData").Child(auth.CurrentUser.UserId).Child("minigameHighscore").SetValueAsync(minigameHighscore);
    }

    public void GetNoOnline()
    {
        Query searchOnline = databaseRef.Child("weeklyActive").Child("week" + weekOfYear).Child(dayOfWeek);
        searchOnline.GetValueAsync().ContinueWithOnMainThread(task =>
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
                Day newDay = JsonUtility.FromJson<Day>(ds.GetRawJsonValue());
                var noOfOnlinePlayers = newDay.currentlyActive.Length;
                onlinePlayersUI.GetComponent<TextMeshProUGUI>().text = "Players Online: " + noOfOnlinePlayers;
            }
            else
            {
                Debug.Log("error");
            }
        });
    }

    public void AddObjPicked()
    {
        objectsPicked += 1;
    }

    public void AddCraftsMade()
    {
        noOfCraftsMade += 1;
    }

    public void CompletedGame()
    {
        completion += 1;
    }

    public void AddTaskCompleted()
    {
        noOfMinigamesCompleted += 1;
    }

    public void NewMinigameStats(int highscore, float roundTime, int throwStreak, int totalHit, int totalMiss, int totalThrows)
    { //function is called when playeer finishes a minigame round, where stats are recorded for saving when game is closing.
        float tempAccuracy = totalHit / totalThrows * 100;

        noOfMinigamesCompleted += 1;

        if (tempAccuracy > minigameAccuracyPercentage)
        {
            minigameAccuracyPercentage = tempAccuracy;
        }
        if (highscore > minigameHighscore)
        {
            minigameHighscore = highscore;
        }
        if (roundTime > minigameLongestRound)
        {
            minigameLongestRound = roundTime;
        }
        if (throwStreak > minigameThrowStreak)
        {
            minigameThrowStreak = throwStreak;
        }
        minigameTotalHits += totalHit;
        minigameTotalMiss += totalMiss;
        minigameTotalThrows += totalMiss;
    }

    void OnApplicationQuit()
    {
        GoOffline();
    }

}