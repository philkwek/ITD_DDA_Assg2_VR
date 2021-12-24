/******************************************************************************
Author: Kelly, Eileen, Elicia, Phil, Donavan
Name of Class: Toggle UI
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

    public void Appear()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        databaseManager.GetComponent<RealtimeDbManager>().GoOffline();
        Invoke("CloseGame", 3);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void Finish()
    {
        finishUI.SetActive(true);
    }

    public void CloseFinish()
    {
        finishUI.SetActive(false);
    }
}
