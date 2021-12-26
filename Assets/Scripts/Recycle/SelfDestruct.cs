/******************************************************************************
Author: Donavan, Eileen, Phil, Kelly, Elicia
Name of Class: SelfDestruct
Description of Class: This script makes sure that all object that are spawned 
are destroy when the game ends
Date Created: 10/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    void Update()
    {
        //If Game not activated or deactivated
        if (RecycleGame.isGameActive == false)
        {
            //Destroy the gameobject
            Destroy(gameObject);
        }
    }
}
