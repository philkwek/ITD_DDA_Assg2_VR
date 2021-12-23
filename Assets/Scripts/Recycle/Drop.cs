using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public RecycleGame recycleGame;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Recyclable" || other.tag == "NonRecyclable")
        {
            Destroy(other.gameObject);
            RecycleGame.isOne = false;
            RecycleGame.miss += 1;
            recycleGame.TrackScoreStreak(false);
        }
    }
}
