/******************************************************************************
Author: Donavan
Name of Class: Drop
Description of Class: This script destroys trash that are dropped and spawn a
new one
Date Created: 10/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    //Referencing to the RecycleGame script
    public RecycleGame recycleGame;

    //When a object with a collier enters
    private void OnTriggerEnter(Collider other)
    {
        //Checking if the object has the tag of 'Recyclable" or "NonRecyclable"
        if (other.tag == "Recyclable" || other.tag == "NonRecyclable")
        {
            //Destroy the game object
            Destroy(other.gameObject);

            //New trash can now be spawned again
            RecycleGame.isOne = false;

            //Score streak lost
            recycleGame.TrackScoreStreak(false);
        }
    }
}
