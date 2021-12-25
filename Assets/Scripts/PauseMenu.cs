/******************************************************************************
Author: Kelly, Eileen, Elicia, Phil, Donavan
Name of Class: Pause Menu
Description of Class: This script is to toggle the text menu for pausing game
Date Created: 23/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject pauseBtn;

    public GameObject finishUI;

    public GameObject databaseManager;

    private void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("DatabaseManager");
    }


    //When user clicks on the pause button, pause menu UI will be shown
    //The time would also stop if in Recycle scene
    public void Appear()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    //When user clicks on resume, pause menu UI will disappear
    //The time would continue to count in the Recycle scene
    public void Resume()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    //When user clicks on the Main Menu button, it will direct user back to Main Menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //When user clicks on the Quit game button, it will log the player out
    //Calls the CloseGame function
    public void QuitGame()
    {
        databaseManager.GetComponent<RealtimeDbManager>().GoOffline();
        Invoke("CloseGame", 3);
    }

    //Users will be logged out from the account, end of the game
    public void CloseGame()
    {
        Application.Quit();
    }

    //When users has completed the game, it will have a ending UI for the users to choose to continue or go back to Main Menu
    public void Finish()
    {
        finishUI.SetActive(true);
    }

    //Closes the Ending UI if users does not make a choice
    public void CloseFinish()
    {
        finishUI.SetActive(false);
    }
}
