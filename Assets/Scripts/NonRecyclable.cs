using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRecyclable : MonoBehaviour
{
    public List<GameObject> note;
    public Animator plusTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NonRecyclable")
        {
            //Do something
            plusTime.SetTrigger("PlusTime");
            TimeManager.timeRemaining += 5;
            RecycleGame.score += 1;
        }
        else if (other.tag == "Recyclable")
        {
            //Check what object
            int index = 0;
            //Do Something
            note[index].SetActive(true);
            Time.timeScale = 0;
        }

        Destroy(other.gameObject);

        RecycleGame.isOne = false;
    }
}
