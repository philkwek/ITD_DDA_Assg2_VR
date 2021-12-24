/******************************************************************************
Author: Elicia, Phil, Donavan, Kelly, Eileen
Name of Class: Toggle UI
Description of Class: This script is to toggle the text menu in the reuse scene
Date Created: 20/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ToggleUI : MonoBehaviour
{
    public GameObject startText;
    public GameObject instructions;
    public GameObject endReuseScene;
    public PauseMenu pauseMenu;

    public List<GameObject> directControl;
    public List<GameObject> rayControl;
    public List<GameObject> previousControl;

    //Clears the UI
    public void ResetUI()
    {
        startText.SetActive(false);
        instructions.SetActive(false);
        endReuseScene.SetActive(false);
    }

    //StartText UI displayed
    public void StartText()
    {
        startText.SetActive(true);
        instructions.SetActive(false);
        endReuseScene.SetActive(false);
    }

    //Intructions UI displayed
    public void Intructions()
    {
        startText.SetActive(false);
        instructions.SetActive(true);
        endReuseScene.SetActive(false);
    }

    //End Reuse Scene UI displayed
    public void EndReuseScene()
    {
        startText.SetActive(false);
        instructions.SetActive(false);
        endReuseScene.SetActive(true);
    }

    public void ToRecycle()
    {
        SceneManager.LoadScene("Recycle");
    }

    public void ChangeToRecycleScene()
    {
        SceneManager.LoadScene("Recycle");
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

