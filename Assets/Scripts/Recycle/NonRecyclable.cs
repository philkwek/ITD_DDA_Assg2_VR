/******************************************************************************
Author: Donavan, Eileen, Phil, Kelly, Elicia
Name of Class: NonRecyclable
Description of Class: This script checks what comes in the nonrecyclable bin
Date Created: 10/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRecyclable : MonoBehaviour
{
    public Animator plusTime;
    public Animator minusTime;

    public RecycleGame recycleGame;

    public AudioSource correct;
    public AudioSource wrong;

    void Start()
    {
        //Referencing to the RecycleGame script
        GameObject obj = GameObject.Find("GameManager");
        recycleGame = obj.GetComponent<RecycleGame>();
    }

    //When a object with a collier enters
    private void OnTriggerEnter(Collider other)
    {
        //Checking if the object has the tag of 'NonRecyclable"
        if (other.tag == "NonRecyclable")
        {
            //Play the "PlusTime" animation
            plusTime.SetTrigger("PlusTime");

            //Add 5 to the remaining time
            TimeManager.timeRemaining += 5;

            //Score plus 1
            RecycleGame.score += 1;

            //Tracking the score streaks
            recycleGame.TrackScoreStreak(true);

            //Play the audio for getting the correct trash into the correct bin
            correct.Play();
        }
        //Checking if the object has the tag of 'Recyclable"
        else if (other.tag == "Recyclable")
        {
            //Play the "MinusTime" animation
            minusTime.SetTrigger("MinusTime");

            //Minus 3 from the remaining time
            TimeManager.timeRemaining -= 3;

            //Miss plus 1
            RecycleGame.miss += 1;

            //Score streak lost
            recycleGame.TrackScoreStreak(false);

            //Play the audio for getting the wrong trash into the wrong bin
            wrong.Play();
        }

        //Destroy the game object
        Destroy(other.gameObject);

        //Trash can now be spawned again
        RecycleGame.isOne = false;
    }
}
