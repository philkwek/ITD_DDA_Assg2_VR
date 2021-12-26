/******************************************************************************
Author: Donavan, Eileen, Phil, Kelly, Elicia
Name of Class: ReduceDrop
Description of Class: This scripts makes sure objects in the Reduce game will
spawn back to the original spot if droppped or thrown away
Date Created: 25/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceDrop : MonoBehaviour
{
    public Transform item1;
    public Transform item2;

    private void OnTriggerEnter(Collider other)
    {
        //Check if the object has the name of "Metal Spoon"
        if (other.transform.name == "Metal Spoon")
        {
            //If the object has the name of "Metal Spoon", change it's current position to where it originally is
            other.transform.position = item1.position;
        }
        //Check if the object has the name of "Plastic Spoon"
        else if (other.transform.name == "Plastic Spoon")
        {
            //If the object has the name of "Plastic Spoon", change it's current position to where it originally is
            other.transform.position = item2.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        //Check if the object has the name of "Metal Spoon"
        if (collision.transform.name == "Metal Spoon")
        {
            //If the object has the name of "Metal Spoon", change it's current position to where it originally is
            collision.transform.position = item1.position;
        }
        //Check if the object has the name of "Plastic Spoon"
        else if (collision.transform.name == "Plastic Spoon")
        {
            //If the object has the name of "Plastic Spoon", change it's current position to where it originally is
            collision.transform.position = item2.position;
        }
    }
}
