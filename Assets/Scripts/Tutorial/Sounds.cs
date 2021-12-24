/******************************************************************************
Author: Eileen, Elicia, Phil, Donavan, Kelly
Name of Class: Sounds
Description of Class: This script is to toggle the text menu in the reuse scene
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
        if (other.tag == "BinCollider")
        {
            //Do something
            throwSound.Play();
        }
    }
}
