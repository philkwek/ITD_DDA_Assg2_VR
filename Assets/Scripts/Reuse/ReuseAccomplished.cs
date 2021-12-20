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


    //End Reuse Scene UI displayed
    public void EndReuseScene()
    {
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
            EndReuseScene();
            Debug.Log("DONE!!");
        }
        else
        {
            return;
        }
    }

}
