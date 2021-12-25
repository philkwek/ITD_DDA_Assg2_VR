/******************************************************************************
Author: Eileen, Elicia, Phil, Donavan, Kelly
Name of Class: Sounds
Description of Class: This script is to display the sound when users throw the trash
Date Created: 24/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource throwSound;

    // Start is called before the first frame update
    void Start()
    {
        throwSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        //if users throw the trash into bin and hit the collider, the throw sound will display
        if (other.tag == "BinCollider")
        {
            throwSound.Play();
        }
    }
}
