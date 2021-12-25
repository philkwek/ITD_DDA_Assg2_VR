/******************************************************************************
Author: Eileen, Phil, Donavan, Kelly, Elicia
Name of Class: Tutorial
Description of Class: This script is for the tutorial scene
Date Created: 15/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject door;
    public GameObject throwCongratTxt;

    public GameObject databaseManager;

    public TutorialToggleUI tutorialToggleUI;

    private void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("DatabaseManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player thrown an object to the bin, the door for teleporting will be shown
        if (other.tag == "BinCollider") 
        {
            //the door will be shown, which allow users go to next scene
            door.SetActive(true);
            //text to congrat users have thrown successfully will be shown
            throwCongratTxt.SetActive(true); 

            tutorialToggleUI.gameplayInstruction.SetActive(false);
            Debug.Log("Collide with bin..");
        }
    }

    public void OnHover()
    {
        //if users interact with the door, they will be brought to the next scene
        if (gameObject.tag == "Door")
        {
            //Do something
            int completion = databaseManager.GetComponent<RealtimeDbManager>().completion;
            if (completion <= 0)
            {
                databaseManager.GetComponent<RealtimeDbManager>().completion = 1;
            };
            Debug.Log("Interacting with door..");
            SceneManager.LoadScene("Reduce"); //change scene
            SceneManager.LoadScene("Reduce"); //change scene
        }
    }
}
