using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recyclable : MonoBehaviour
{
    public Animator plusTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Recyclable")
        {
            //Do something
            plusTime.SetTrigger("PlusTime");
            TimeManager.timeRemaining += 5;
            RecycleGame.score += 1;
        }
        else if (other.tag == "NonRecyclable")
        {
            //Do Something
            Time.timeScale = 0;
            TimeManager.timeRemaining -= 3;
            RecycleGame.miss += 1;
        }
        Destroy(other.gameObject);

        RecycleGame.isOne = false;
    }
}
