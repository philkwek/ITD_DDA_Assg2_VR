/******************************************************************************
Author: Elicia, Phil, Donavan, Kelly, Eileen
Name of Class: ReuseAccomplished
Description of Class: This script is to check of player has accomplished task, once accomplished
                      UI will be changed 
Date Created: 20/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReuseAccomplished : MonoBehaviour
{
    public GameObject instructions;
    public GameObject endReuseScene;

    public GameObject databaseManager;

    public List<GameObject> directControl;
    public List<GameObject> rayControl;
    public List<GameObject> previousControl;

    private void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("DatabaseManager");
    }


    //End Reuse Scene UI displayed
    public void EndReuseScene()
    {
        int completion = databaseManager.GetComponent<RealtimeDbManager>().completion;
        if (completion <= 2)
        {
            databaseManager.GetComponent<RealtimeDbManager>().completion = 3;
        }
        databaseManager.GetComponent<RealtimeDbManager>().AddCraftsMade();
        databaseManager.GetComponent<RealtimeDbManager>().AddTaskCompleted();

        instructions.SetActive(false);
        endReuseScene.SetActive(true);
    }

    //Counts if all the objects has been placed on the tree
    public int checker = 0;

    //Check if all the objects has been placed on the tree
    public void Checker()
    {
        //Increases the checker count by 1
        checker += 1;
        Debug.Log("Checker count:" + checker);

        if (checker >= 4)
        {
            //Change UI if all the elements have been hung on the tree
            Raycast();
            EndReuseScene();
            Debug.Log("DONE!!");
        }
        else
        {
            return;
        }
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
