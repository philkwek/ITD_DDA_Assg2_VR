/******************************************************************************
Author: Kelly, Eileen, Elicia, Phil, Donavan
Name of Class: Reduce UI
Description of Class: This script is to toggle the text menu in the reduce scene
Date Created: 20/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReduceUI : MonoBehaviour
{
    public GameObject intro;
    public GameObject start;
    public GameObject resultsGood;
    public GameObject resultsBad;
    public GameObject nextReuse;

    public GameObject databaseManager;

    public AudioSource doneSound;

    public List<GameObject> directControl;
    public List<GameObject> rayControl;
    public List<GameObject> previousControl;

    // Start is called before the first frame update
    void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        //Show the introduction UI only
        databaseManager = GameObject.Find("DatabaseManager");

        intro.SetActive(true);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);

        doneSound = GetComponent<AudioSource>();
    }

    //Show the start button UI only
    public void SecondNext()
    {
        intro.SetActive(false);
        start.SetActive(true);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);
    }

    //Go back to the gameplay, no UI showing
    public void Back()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);
    }

    //Go to Reuse button shown
    public void Next()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(true);
    }

    //If user choose the Metal Spoon, shows the outcome
    public void ResultGood()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(true);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);
    }

    //If user choose the Plastic Spoon, shows the outcome
    public void ResultBad()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(true);
        nextReuse.SetActive(false);
    }

    //After end of game, go to the video
    public void GoVideo()
    {
        int completion = databaseManager.GetComponent<RealtimeDbManager>().completion;
        if (completion <= 1)
        {
            databaseManager.GetComponent<RealtimeDbManager>().completion = 2;
        };
        databaseManager.GetComponent<RealtimeDbManager>().noOfTaskCompleted += 1;
        SceneManager.LoadScene("3DVideo");
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
