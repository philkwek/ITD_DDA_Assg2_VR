using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    void Update()
    {
        if (RecycleGame.isGameActive == false)
        {
            Destroy(gameObject);
        }
    }
}
