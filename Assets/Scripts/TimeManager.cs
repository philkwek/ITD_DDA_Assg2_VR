using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    IEnumerator StopWatch()
    {
        while (true)
        {
            int fps = (int)(1 / Time.unscaledDeltaTime);

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
            Debug.Log("time limit" + timeLimit);
            
        }
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);//removes the mins
    
        float msec = (timeToDisplay % 1) * 1000;//Mathf.FloorToInt((timeRemaining - sec) * 100);
        Debug.Log("msec... " + msec);
        timeRemainingDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

        msec = Mathf.FloorToInt((time - (int)time) * 100);
        sec = Mathf.FloorToInt(time % 60);
        min = Mathf.FloorToInt(time / 60);
        currentSec = sec;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    public void StartTimer()
    {
        //startButton.enabled = false;
        StartCoroutine("StopWatch");
        gameEnd = false;
    }


    public void StopTimer()
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
        StartTimer();
    }
}
