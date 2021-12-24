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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NonRecyclable")
        {
            //Do something
            plusTime.SetTrigger("PlusTime");
            TimeManager.timeRemaining += 5;
            RecycleGame.score += 1;
            recycleGame.TrackScoreStreak(true);
            correct.Play();
        }
        else if (other.tag == "Recyclable")
        {
            //Do Something
            minusTime.SetTrigger("MinusTime");
            TimeManager.timeRemaining -= 3;
            RecycleGame.miss += 1;
            wrong.Play();
        }
        Destroy(other.gameObject);

        RecycleGame.isOne = false;
    }
}
