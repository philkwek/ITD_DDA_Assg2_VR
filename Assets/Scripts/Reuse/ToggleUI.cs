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
    public GameObject nextRecycle;

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
        nextRecycle.SetActive(false);
    }

    //Intructions UI displayed
    public void Intructions()
    {
        startText.SetActive(false);
        instructions.SetActive(true);
        endReuseScene.SetActive(false);
        nextRecycle.SetActive(false);
    }

    //End Reuse Scene UI displayed
    public void EndReuseScene()
    {
        startText.SetActive(false);
        instructions.SetActive(false);
        endReuseScene.SetActive(true);
        nextRecycle.SetActive(false);
    }

    //Proceed to Recycle UI displayed
    public void NextRecycleScene()
    {
        startText.SetActive(false);
        instructions.SetActive(false);
        endReuseScene.SetActive(false);
        nextRecycle.SetActive(true);
    }

    public void ChangeToRecycleScene()
    {
        SceneManager.LoadScene("Recycle");
    }
}

