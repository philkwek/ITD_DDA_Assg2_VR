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

    public AudioSource doneSound;
    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("Reuse");
    }
}
