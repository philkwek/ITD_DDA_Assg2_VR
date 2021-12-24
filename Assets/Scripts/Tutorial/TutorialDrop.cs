using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDrop : MonoBehaviour
{
    public Transform item1;
    public Transform item2;
    public Transform item3;
    public Transform item4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Container")
        {
            other.transform.position = item1.position;
        }
        else if (other.transform.name == "Plastic Bottle Empty")
        {
            other.transform.position = item2.position;
        }
        else if (other.transform.name == "Plastic Straw")
        {
            other.transform.position = item3.position;
        }
        else if (other.transform.name == "Metal Spoon")
        {
            other.transform.position = item4.position;
        }
    }


}
