using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RecycleGame : MonoBehaviour
{
    //functions for firebase tracking
    public TimeManager timeManager;
    public GameObject databaseManager;
    public int currentStreak = 0;
    public int savedStreak = 0;

    public List<GameObject> throwables;
    public static bool isGameActive = false;
    public static bool isOne = false;

    public List<GameObject> directControl;
    public List<GameObject> rayControl;
    public List<GameObject> previousControl;

    public GameObject gameOver;
    public GameObject intructions;
    public GameObject gameItems;

    public GameObject locked;
    public GameObject unlocked;
    public GameObject lastPanel;

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI wrong;

    public static int score = 0;
    public static int miss = 0;
    public static int throws = 0;

    public static bool isUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("DatabaseManager");
        //sets new timing from db
        TimeManager.timeRemaining = databaseManager.GetComponent<RealtimeDbManager>().startingTime;

        score = 0;
        if (isUnlocked == false)
        {
            locked.SetActive(true);
            unlocked.SetActive(false);
        }
        else
        {
            locked.SetActive(false);
            unlocked.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnThrowables();

        if (TimeManager.gameEnd == true)
        {
            GameOver();
        }

        UpdateScore();

        Debug.Log(throws);
        Debug.Log(score);
        Debug.Log(miss);
    }

    private void SpawnThrowables()
    {
        while (isGameActive && !isOne)
        {
            int index = Random.Range(0, throwables.Count);
            Instantiate(throwables[index].AddComponent<SelfDestruct>());
            isOne = true;
            throws += 1;
        }
    }

    //function runs when game is over
    public void GameOver()
    {
        Raycast();
        isGameActive = false;
        gameOver.SetActive(true);
        gameItems.SetActive(false);
        finalScore.text = scoreTxt.text;
        if (miss == 0)
        {
            wrong.text = "* No trash have been place into the wrong bins *";
        }
        else
        {
            wrong.text = string.Format("* {0} trash had been place into the wrong bins *", miss);
        }

        //functions to enter new minigame stats into realtimeDbManager
        int completion = databaseManager.GetComponent<RealtimeDbManager>().completion;
        if (completion <= 3)
        { 
            databaseManager.GetComponent<RealtimeDbManager>().completion = 4;
        }
        float roundTime = timeManager.GetTimeRecorded();
        int throwStreak = savedStreak;
        databaseManager.GetComponent<RealtimeDbManager>().NewMinigameStats(score, roundTime, throwStreak, score, miss, throws);
        databaseManager.GetComponent<RealtimeDbManager>().noOfMinigamesCompleted += 1;
        databaseManager.GetComponent<RealtimeDbManager>().noOfTaskCompleted += 1;
    }

    public void CloseGameOver()
    {
        gameOver.SetActive(false);
    }

    public void StartGame()
    {
        timeManager.RecordTimeStart();
        intructions.SetActive(false);
        gameItems.SetActive(true);
        isGameActive = true;
    }

    public void Restart()
    {
        gameOver.SetActive(false);
        score = 0;
        miss = 0;
        throws = 0;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        //Return to menu
        SceneManager.LoadScene("MainMenu");
    }

    private void Unlocked()
    {
        locked.SetActive(false);
        unlocked.SetActive(true);
    }

    private void UpdateScore()
    {
        scoreTxt.text = "Score: " + score;
    }

    public void TrackScoreStreak(bool track)
    {
        if (track)
        {
            currentStreak += 1;
        } else
        {
            savedStreak = currentStreak;
            currentStreak = 0;
        }
    }

    public void Raycast()
    {
        previousControl.Clear();
        StoreControl();

        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(true);
            rayControl[1].SetActive(true);
            directControl[0].SetActive(false);
            directControl[1].SetActive(false);
        }
    }

    public void Direct()
    {
        previousControl.Clear();
        StoreControl();

        if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(false);
            rayControl[1].SetActive(false);
            directControl[0].SetActive(true);
            directControl[1].SetActive(true);
        }
    }

    public void StoreControl()
    {
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            previousControl.Add(directControl[0]);
            previousControl.Add(directControl[1]);
        }
        else if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            previousControl.Add(rayControl[0]);
            previousControl.Add(rayControl[1]);
        }
    }

    public void RetoreControl()
    {
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            directControl[0].SetActive(false);
            directControl[1].SetActive(false);
            previousControl[0].SetActive(true);
            previousControl[1].SetActive(true);
        }
        else if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(false);
            rayControl[1].SetActive(false);
            previousControl[0].SetActive(true);
            previousControl[1].SetActive(true);
        }
    }
}
