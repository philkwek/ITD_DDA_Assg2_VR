using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggleUI : MonoBehaviour
{
    public GameObject controllerIntro;
    public GameObject intro;
    public GameObject start;
    public GameObject tutorial01;
    public GameObject tutorial02;

    public GameObject fixTableTxt;
    public GameObject fixBinLidTxt;
    public GameObject attachCongratTxt;
    public GameObject trash;
    public GameObject gameplayInstruction;

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

    public void Tutorial02Next()
    {
        tutorial02.SetActive(false);

        gameplayInstruction.SetActive(true);
        trash.SetActive(true);
    }
}
