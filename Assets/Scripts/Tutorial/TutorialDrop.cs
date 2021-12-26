/******************************************************************************
Author: Donavan, Eileen, Phil, Kelly, Elicia
Name of Class: TutorialDrop
Description of Class: This scripts makes sure objects in the Tutorial will
spawn back to the original spot if droppped or thrown away
Date Created: 24/12/21
******************************************************************************/
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
        //Check if the object has the name of "Container"
        if (other.transform.name == "Container")
        {
            //If the object has the name of "Container", change it's current position to where it originally is
            other.transform.position = item1.position;
        }
        //Check if the object has the name of "Plastic Bottle Empty"
        else if (other.transform.name == "Plastic Bottle Empty")
        {
            //If the object has the name of "Plastic Bottle Empty", change it's current position to where it originally is
            other.transform.position = item2.position;
        }
        //Check if the object has the name of "Plastic Straw"
        else if (other.transform.name == "Plastic Straw")
        {
            //If the object has the name of "Plastic Straw", change it's current position to where it originally is
            other.transform.position = item3.position;
        }
        //Check if the object has the name of "Metal Spoon"
        else if (other.transform.name == "Metal Spoon")
        {
            //If the object has the name of "Metal Spoon", change it's current position to where it originally is
            other.transform.position = item4.position;
        }
    }
}
