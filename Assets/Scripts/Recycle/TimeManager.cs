/******************************************************************************
Author: Donavan, Eileen, Phil, Kelly, Elicia
Name of Class: TimeManager
Description of Class: This script manage the Timer in the RecycleGame
Date Created: 10/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System.Linq;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timeRemainingDisplay;
    public float time;

    public static float timeRemaining = 20;
    private float msec;
    private float sec;
    private float min;

    private float currentSec;
    private float timeLimit = 0;

    private string resetText = "00:00:00";

    public static float timeElapsed = 0;

    public RecycleGame GameManager;

    //reference for recording time
    public float recordTimeStart;

    IEnumerator StopWatch()
    {
        //Loop the code within the while loop 
        while (true)
        {
            //If remaining time is more than 0
            if (timeRemaining > 0)
            {
                //Minus time passed from the remaining time
                timeRemaining -= Time.deltaTime;
                //Add time passed to time
                time += Time.deltaTime;
                //Calling the DisplayTime function while giving the remaining time as the parameter
                DisplayTime(timeRemaining);
            }
            //If remaining time is not more than 0
            else
            {
                //Time remaining is set to 0
                timeRemaining = 0;

                //Time is set to the same value that is assigned to timeLimit variable
                time = timeLimit;

                //Change the font color of the remaining time text to red 
                timeRemainingDisplay.color = Color.red;

                //Calling the GameOver function from the RecycleGame script
                GameManager.GameOver();
            }
            yield return null;
        }
    }

    //This function is responsible for displaying time as text
    void DisplayTime(float timeToDisplay)
    {
        //If time is lesser than 0
        if(timeToDisplay < 0)
        {
            //Set time to 0
            timeToDisplay = 0;

            //Time is set to the same value that is assigned to timeLimit variable
            time = timeLimit;

            //Debug.Log("time limit " + timeLimit);
        }
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);//removes the mins
    
        float msec = (timeToDisplay % 1) * 1000;//Mathf.FloorToInt((timeRemaining - sec) * 100);
        //Debug.Log("msec... " + msec);
        timeRemainingDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

        msec = Mathf.FloorToInt((time - (int)time) * 100);
        sec = Mathf.FloorToInt(time % 60);
        min = Mathf.FloorToInt(time / 60);
        currentSec = sec;
    }

    private void Update()
    {
        //Records the total time the user spent on the RecycleGame
        timeElapsed = Time.timeSinceLevelLoad;
    }

    public void StartStopWatch()
    {
        //Start "StopWatch"
        StartCoroutine("StopWatch");
    }


    public void StopStopWatch()
    {
        //Stop "StopWatch"
        StopCoroutine("StopWatch");
    }

    public void ResetTimer()
    {
        //Setting the time remaining to 20
        timeRemaining = 20;

        //Change the font color of the remaining time text to white
        timeRemainingDisplay.color = Color.white;

        //Calling the StartStopWatch function
        StartStopWatch();
    }

    //"Starts" timer to record how long player took for game
    public void RecordTimeStart()
    {
        recordTimeStart = Time.realtimeSinceStartup;
    }
    //function for use to get recorded time for how long user spent in minigame round at round end

    public float GetTimeRecorded()
    {
        //gets time spent in minigame in seconds
        float endTime = Time.realtimeSinceStartup;
        float recordedTime = endTime - recordTimeStart;

        //converts time from seconds to minutes and 2dp
        float timeMinute = recordedTime / 60;
        string twoDP = timeMinute.ToString("F2");
        float finalTime = float.Parse(twoDP, CultureInfo.InvariantCulture.NumberFormat);

        return finalTime;
    }
}
