/******************************************************************************
Author: Elicia, Phil, Donavan, Kelly, Eileen
Name of Class: ChangeUIText
Description of Class: This script is to check of player has accomplished task, once accomplished
                      UI will be changed 
Date Created: 20/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIText : MonoBehaviour
{
    public GameObject instructions;
    public GameObject congratsMessage;


    //End Reuse Scene UI displayed
    public void ChangeText()
    {
        instructions.SetActive(false);
        congratsMessage.SetActive(true);
    }

    //Counts if all the objects has been placed on the tree
    public int checker = 0;

    //Check if all the objects has been placed on the tree
    public void Checker()
    {
        //Increases the checker count by 1
        checker += 1;
        Debug.Log("Checker count:" + checker);

        if (checker >= 1)
        {
            //Change UI if all the elements have been hung on the tree
            ChangeText();
            Debug.Log("DONE!!");
        }
        else
        {
            return;
        }
    }

}
