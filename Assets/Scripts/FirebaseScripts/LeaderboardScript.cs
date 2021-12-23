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

public class LeaderboardScript : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference databaseRef;

    public TextMeshProUGUI[] minigameHighscoreTexts;
    public TextMeshProUGUI[] minigameUsernameTexts;

    public TextMeshProUGUI[] timeSpentTexts;
    public TextMeshProUGUI[] timeSpentUsernameTexts;

    public GameObject highscoreBoard;
    public GameObject timespentBoard;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetHighscoreUsers();
        GetTimeUsers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchBoard()
    {
        Debug.Log("Clicked");
        if (highscoreBoard.activeSelf == true)
        {
            highscoreBoard.SetActive(false);
            timespentBoard.SetActive(true);
        } else
        {
            highscoreBoard.SetActive(true);
            timespentBoard.SetActive(false);
        }
    }

    public void GetHighscoreUsers()
    {
        Query searchHighscorers = databaseRef.Child("playerProfileData").OrderByChild("minigameHighscore").LimitToLast(6);

        searchHighscorers.GetValueAsync().ContinueWithOnMainThread(task =>
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
                int i = -1;
                foreach (DataSnapshot snap in ds.Children.Reverse<DataSnapshot>())
                {
                    i += 1;
                    PlayerProfileData player = JsonUtility.FromJson<PlayerProfileData>(snap.GetRawJsonValue());
                    minigameHighscoreTexts[i].text = player.minigameHighscore.ToString();
                    minigameUsernameTexts[i].text = player.username;
                }
            }
        });
    }

    public void GetTimeUsers()
    {
        Query searchTime = databaseRef.Child("playerProfileData").OrderByChild("totalTimePlayed").LimitToLast(6);

        searchTime.GetValueAsync().ContinueWithOnMainThread(task =>
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
                int i = -1;
                foreach (DataSnapshot snap in ds.Children.Reverse<DataSnapshot>())
                {
                    i += 1;
                    PlayerProfileData player = JsonUtility.FromJson<PlayerProfileData>(snap.GetRawJsonValue());
                    timeSpentTexts[i].text = player.totalTimePlayed.ToString();
                    timeSpentUsernameTexts[i].text = player.username;
                }
            }
        });
    }
}
