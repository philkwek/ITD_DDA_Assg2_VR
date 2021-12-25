/******************************************************************************
Author: Donavan, Phil
Name of Class: RecycleGame
Description of Class: This script manage the RecycleGame
Date Created: 10/12/21
******************************************************************************/
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

    public Transform spawnPoint;

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

/*    public static bool isUnlocked = false;*/

    // Start is called before the first frame update
    void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("DatabaseManager");

        //sets new timing from db
        TimeManager.timeRemaining = databaseManager.GetComponent<RealtimeDbManager>().startingTime;

/*        if (isUnlocked == false)
        {
            locked.SetActive(true);
            unlocked.SetActive(false);
        }
        else
        {
            locked.SetActive(false);
            unlocked.SetActive(true);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //Calling the SpawnThrowables function
        SpawnThrowables();

        //Calling the UpdateScore
        UpdateScore();
    }

    //This script spawn the trashs when the game starts
    private void SpawnThrowables()
    {
        //While game is activated and there is no trash in the scene 
        while (isGameActive && !isOne)
        {
            //Randomise a value
            int index = Random.Range(0, throwables.Count);

            //Spawn the a random trash in the throwables list, adding the SelfDestruct script component
            Instantiate(throwables[index].AddComponent<SelfDestruct>(),spawnPoint.position,Quaternion.identity);

            //Setting isOne true so that trash will not be spawned before the current one is destroyed
            isOne = true;

            //Throws plus 1
            throws += 1;
        }
    }

    //Runs when the game is over
    public void GameOver()
    {
        int throwStreak = savedStreak;

        //Stopping the timer
        timeManager.StopStopWatch();

        //Changing the Raycast Controls
        Raycast();

        //Game is deactivated
        isGameActive = false;

        //Game Over menu is activated
        gameOver.SetActive(true);

        //Game related items are deactivated
        gameItems.SetActive(false);

        //Displaying the score that the player get
        finalScore.text = scoreTxt.text;

        //If the player has no misses
        if (miss == 0)
        {
            //Display the following text
            wrong.text = "* No trash have been place into the wrong bins *";
        }
        //If the player has misses
        else
        {
            //Display the following text
            wrong.text = string.Format("* {0} trash had been place into the wrong bins *", miss);
        }


        int completion = databaseManager.GetComponent<RealtimeDbManager>().completion;
        if (completion <= 3)
        { 
            databaseManager.GetComponent<RealtimeDbManager>().completion = 4;
        }
        float roundTime = timeManager.GetTimeRecorded();
        float tempAccuracy = (float)score/throws;
        databaseManager.GetComponent<RealtimeDbManager>().NewMinigameStats(score, roundTime, throwStreak, score, miss, throws, tempAccuracy);
        databaseManager.GetComponent<RealtimeDbManager>().noOfMinigamesCompleted += 1;
        databaseManager.GetComponent<RealtimeDbManager>().noOfTaskCompleted += 1;
    }

    //Run when the player starts the game
    public void StartGame()
    {
        //Starts timing how long did the player play the game still the game is over
        timeManager.RecordTimeStart();

        //Instruction menu is deactivated
        intructions.SetActive(false);

        //Game related items are activated
        gameItems.SetActive(true);

        //Game is activated
        isGameActive = true;
    }

    //Run when the player restarts the game
    public void Restart()
    {
        //Game Over menu is deactivated
        gameOver.SetActive(false);

        //Game stats are reset
        score = 0;
        miss = 0;
        throws = 0;
    }

    //Run when the player clicks the "Main Menu" button
    public void MainMenu()
    {
        //Time starts running
        Time.timeScale = 1;

        //Return to menu
        SceneManager.LoadScene("MainMenu");
    }

    private void Unlocked()
    {
        locked.SetActive(false);
        unlocked.SetActive(true);
    }

    //Update score
    private void UpdateScore()
    {
        //Showing the score the player gets
        scoreTxt.text = "Score: " + score;
    }

    public void TrackScoreStreak(bool track)
    {
        if (track)
        {
            currentStreak += 1;
        } else
        {
            if (savedStreak < currentStreak)
            {
                savedStreak = currentStreak;
            }
            currentStreak = 0;
        }
    }

    //Activating Raycast control
    public void Raycast()
    {
        //Clear the previousControl list
        previousControl.Clear();

        //Store the current type of controls in the previousControl list
        StoreControl();

        //Check if Direct Control is activated
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            //Activate Left and Right Raycast controls
            rayControl[0].SetActive(true);
            rayControl[1].SetActive(true);

            //Deactivate Left and Right Direct controls
            directControl[0].SetActive(false);
            directControl[1].SetActive(false);
        }
    }

    //Activating Direct control
    public void Direct()
    {
        //Clear the previousControl list
        previousControl.Clear();

        //Store the current type of controls in the previousControl list
        StoreControl();

        //Check if Raycast Control is activated
        if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            //Deactivate Left and Right Raycast controls
            rayControl[0].SetActive(false);
            rayControl[1].SetActive(false);

            //Activate Left and Right Direct controls
            directControl[0].SetActive(true);
            directControl[1].SetActive(true);
        }
    }

    //Store the current type of controls in the previousControl list
    public void StoreControl()
    {
        //Check if Direct Control is activated
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            //Adding Direct Controls in the previousControl list
            previousControl.Add(directControl[0]);
            previousControl.Add(directControl[1]);
        }
        //Check if Raycast Control is activated
        else if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            //Adding Raycast Controls in the previousControl list
            previousControl.Add(rayControl[0]);
            previousControl.Add(rayControl[1]);
        }
    }

    //Restore the current type of controls to the controls that was used before this one
    public void RetoreControl()
    {
        //Check if Direct Control is activated
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            //Deactivate Left and Right Direct controls
            directControl[0].SetActive(false);
            directControl[1].SetActive(false);

            //Activate previous controls
            previousControl[0].SetActive(true);
            previousControl[1].SetActive(true);
        }
        //Check if Raycast Control is activated
        else if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            //Deactivate Left and Right Raycast controls
            rayControl[0].SetActive(false);
            rayControl[1].SetActive(false);

            //Activate previous controls
            previousControl[0].SetActive(true);
            previousControl[1].SetActive(true);
        }
    }

    //Close the Game Over menu
    public void CloseGameOver()
    {
        //Deactivate the Game Over menu
        gameOver.SetActive(false);
    }
}
