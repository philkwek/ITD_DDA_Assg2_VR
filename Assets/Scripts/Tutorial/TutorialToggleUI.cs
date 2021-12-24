/******************************************************************************
Author: Eileen, Donavan, Phil, Kelly, Elicia
Name of Class: TutorialToggleUI
Description of Class: This script is to toggle UIs in tutorial scene
Date Created: 24/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggleUI : MonoBehaviour
{
    //UI of instructions
    public GameObject controllerIntro;
    public GameObject intro;
    public GameObject start;
    public GameObject tutorial01;
    public GameObject tutorial02;

    //UI of gameplay
    public GameObject fixTableTxt;
    public GameObject fixBinLidTxt;
    public GameObject attachCongratTxt;
    public GameObject trash;
    public GameObject gameplayInstruction;

    public List<GameObject> directControl;
    public List<GameObject> rayControl;
    public List<GameObject> previousControl;

    //check number of objects have been attached
    public int checker = 0;

    public AudioSource doneSound;

    // Start is called before the first frame update
    void Start()
    {
        controllerIntro.SetActive(false);
        intro.SetActive(true);
        start.SetActive(false);
        tutorial01.SetActive(false);
        tutorial02.SetActive(false);

        fixTableTxt.SetActive(false);
        fixBinLidTxt.SetActive(false);
        attachCongratTxt.SetActive(false);
        trash.SetActive(false);
        gameplayInstruction.SetActive(false);

        doneSound = GetComponent<AudioSource>();
    }
   
    public void FirstNext()
    {
        controllerIntro.SetActive(true);
        intro.SetActive(false);
        start.SetActive(false);
    }

    public void SecondNext()
    {
        controllerIntro.SetActive(false);
        intro.SetActive(false);
        start.SetActive(true);
    }

    public void ThirdNext()
    {
        controllerIntro.SetActive(false);
        intro.SetActive(false);
        start.SetActive(false);
        tutorial01.SetActive(true);
    }

    public void Tutorial01Next()
    {
        controllerIntro.SetActive(false);
        intro.SetActive(false);
        start.SetActive(false);
        tutorial01.SetActive(false);

        fixTableTxt.SetActive(true);
        fixBinLidTxt.SetActive(true);
    }

    public void Tutorial02Next()
    {
        tutorial02.SetActive(false);

        gameplayInstruction.SetActive(true);
        trash.SetActive(true);
    }

    public void Checker()
    {
        //Increases the checker count by 1
        checker += 1;
        Debug.Log("Checker count:" + checker);

        if (checker >= 2)
        {
            //Change UI if all the elements have been attached
            fixTableTxt.SetActive(false);
            fixBinLidTxt.SetActive(false);

            attachCongratTxt.SetActive(true);
            Raycast();
            //doneSound.Play();
            Debug.Log("DONE!!");
        }
        else
        {
            return;
        }
    }

    public void CheckerNext()
    {
        attachCongratTxt.SetActive(false);

        tutorial02.SetActive(true);
    }

    public void Raycast()
    {
        previousControl.Clear();
        StoreControl();

        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(true);
            rayControl[1].SetActive(true);
            directControl[0].SetActive(false);
            directControl[1].SetActive(false);
        }
    }

    public void Direct()
    {
        previousControl.Clear();
        StoreControl();

        if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(false);
            rayControl[1].SetActive(false);
            directControl[0].SetActive(true);
            directControl[1].SetActive(true);
        }
    }

    public void StoreControl()
    {
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            previousControl.Add(directControl[0]);
            previousControl.Add(directControl[1]);
        }
        else if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            previousControl.Add(rayControl[0]);
            previousControl.Add(rayControl[1]);
        }
    }

    public void RetoreControl()
    {
        if (directControl[0].activeSelf == true && directControl[1].activeSelf == true)
        {
            directControl[0].SetActive(false);
            directControl[1].SetActive(false);
            previousControl[0].SetActive(true);
            previousControl[1].SetActive(true);
        }
        else if (rayControl[0].activeSelf == true && rayControl[1].activeSelf == true)
        {
            rayControl[0].SetActive(false);
            rayControl[1].SetActive(false);
            previousControl[0].SetActive(true);
            previousControl[1].SetActive(true);
        }
    }
}
