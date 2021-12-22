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

    public void Appear()
    {
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
