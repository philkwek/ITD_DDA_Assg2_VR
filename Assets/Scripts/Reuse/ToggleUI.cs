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

    //Go to Recycle button shown
    public void ToRecycle()
    {
        SceneManager.LoadScene("Recycle");
    }

    //Go to Reuse button shown
    public void ChangeToRecycleScene()
    {
        SceneManager.LoadScene("Recycle");
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
}

