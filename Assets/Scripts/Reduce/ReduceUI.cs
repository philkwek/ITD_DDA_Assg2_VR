/******************************************************************************
Author: Kelly, Eileen, Elicia, Phil, Donavan
Name of Class: Toggle UI
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

    // Start is called before the first frame update
    void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("DatabaseManager");

        intro.SetActive(true);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);

        doneSound = GetComponent<AudioSource>();
    }

    public void SecondNext()
    {
        intro.SetActive(false);
        start.SetActive(true);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);
    }

    public void Back()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);
    }

    public void Next()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
        nextReuse.SetActive(true);
    }

    public void ResultGood()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(true);
        resultsBad.SetActive(false);
        nextReuse.SetActive(false);
    }

    public void ResultBad()
    {
        intro.SetActive(false);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(true);
        nextReuse.SetActive(false);
    }
    public void GoReuse()
    {
        int completion = databaseManager.GetComponent<RealtimeDbManager>().completion;
        if (completion <= 1)
        {
            databaseManager.GetComponent<RealtimeDbManager>().completion = 2;
        };
        databaseManager.GetComponent<RealtimeDbManager>().noOfTaskCompleted += 1;
        SceneManager.LoadScene("Reuse");
    }

    public void ChangeControls()
    {
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(true);
            rayControl[1].SetActive(true);
            directControl[0].SetActive(false);
            directControl[1].SetActive(false);
        }
        else if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(false);
            rayControl[1].SetActive(false);
            directControl[0].SetActive(true);
            directControl[1].SetActive(true);
        }
    }
}
