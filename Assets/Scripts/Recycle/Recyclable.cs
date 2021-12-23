using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recyclable : MonoBehaviour
{
    public Animator plusTime;
    public Animator minusTime;

    public RecycleGame recycleGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Recyclable")
        {
            //Do something
            plusTime.SetTrigger("PlusTime");
            TimeManager.timeRemaining += 5;
            RecycleGame.score += 1;
            recycleGame.TrackScoreStreak(true);
        }
        else if (other.tag == "NonRecyclable")
        {
            //Do Something
            minusTime.SetTrigger("MinusTime");
            TimeManager.timeRemaining -= 3;
            RecycleGame.miss += 1;
        }
        Destroy(other.gameObject);

        RecycleGame.isOne = false;
    }
}
