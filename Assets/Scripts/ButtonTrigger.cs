/******************************************************************************
Author: Kelly, Donovan, Elicia, Phil, Eileen
Name of Class: Button Trigger
Description of Class: This script is to toggle the text menu in the reuse scene
Date Created: 3/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    //Assign Button Animator
    public Animator buttonAnimator;

    //If button is pressed...
    private void OnTriggerEnter(Collider other)
    {
        //Play ButtonPressed animation
        buttonAnimator.SetBool("isPressed", true);
    }

    //If button is released...
    private void OnTriggerExit(Collider other)
    {
        //Play ButtonReleased animation
        buttonAnimator.SetBool("isPressed", false);
    }
}
