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

        //Do something
    }

    //If button is released...
    private void OnTriggerExit(Collider other)
    {
        //Play ButtonReleased animation
        buttonAnimator.SetBool("isPressed", false);

        //Do something
    }
}
