/******************************************************************************
Author: Kelly, Eileen, Elicia, Phil, Donavan
Name of Class: Attach Reduce
Description of Class: This script is to allow users to attach spoons to the box in the reduce scene
Date Created: 9/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachReduce : MonoBehaviour
{
    public GameObject metalBoard;
    public GameObject plasticBoard;
    public GameObject spoonMountain;
    public GameObject resultsGood;
    public GameObject resultsBad;

    public ReduceUI reduceUI;

    private void OnTriggerEnter(Collider other)
    {
        //If users chosen the Metal Spoon, the board and the mountain spoon will be spawned
        if (other.tag == "MetalSpoon")
        {
            //Do something
            reduceUI.Raycast();
            metalBoard.SetActive(true);
            spoonMountain.SetActive(true);
            //resultsGood.SetActive(true);
            //resultsBad.SetActive(false);
            
        }

        //If users chosen the Plastic Spoon, the board and the mountain spoon will be spawned
        else if (other.tag == "PlasticSpoon")
        {
            reduceUI.Raycast();
            plasticBoard.SetActive(true);
            spoonMountain.SetActive(true);
            //resultsGood.SetActive(false);
            //resultsBad.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If users picks up the Metal Spoon from the box, the board and the mountain spoon will disappear
        if (other.tag == "MetalSpoon")
        {
            //Do something
            reduceUI.Direct();
            metalBoard.SetActive(false);
            spoonMountain.SetActive(false);
            resultsGood.SetActive(false);
            resultsBad.SetActive(false);
            Debug.Log("Error123");
        }

        //If users picks up the Plastic Spoon from the box, the board and the mountain spoon will disappear
        else if (other.tag == "PlasticSpoon")
        {
            //Do something
            reduceUI.Direct();
            plasticBoard.SetActive(false);
            spoonMountain.SetActive(false);
            resultsGood.SetActive(false);
            resultsBad.SetActive(false);
        }
    }

}
