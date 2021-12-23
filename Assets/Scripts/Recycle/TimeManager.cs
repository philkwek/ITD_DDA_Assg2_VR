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
    //public Button startButton;
    public float time;

    public static float timeRemaining = 20;
    private float msec;
    private float sec;
    private float min;

    private float currentSec;
    private float timeLimit = 0;

    public static bool gameEnd = false;

    private string resetText = "00:00:00";

    public static float timeElapsed = 0;

    //reference for recording time
    public float recordTimeStart;

    IEnumerator StopWatch()
    {
        while (true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                time += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                time = timeLimit;
                timeRemainingDisplay.color = Color.red;
                gameEnd = true;
            }
            yield return null;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        timeElapsed = Time.timeSinceLevelLoad;
    }

    public void StartStopWatch()
    {
        //startButton.enabled = false;
        StartCoroutine("StopWatch");
        gameEnd = false;
    }


    public void StopStopWatch()
    {
        //startButton.enabled = true;
        StopCoroutine("StopWatch");
    }

    public void SetTimeRemaining(float newTimeRemaing)
    {
        timeRemaining = newTimeRemaing;
        timeLimit = newTimeRemaing;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public float GetCurrentSec()
    {
        return currentSec;
    }

    public void ResetTimer()
    {
        timeRemaining = 20;
        timeRemainingDisplay.color = Color.white;
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
