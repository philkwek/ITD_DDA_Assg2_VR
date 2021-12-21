using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggleUI : MonoBehaviour
{
    public GameObject controllerIntro;
    public GameObject intro;
    public GameObject start;

    public GameObject fixTableTxt;
    public GameObject attachCongratTxt;
    public GameObject trash;
    public GameObject gameplayInstruction;

    public int checker = 0;

    // Start is called before the first frame update
    void Start()
    {
        controllerIntro.SetActive(false);
        intro.SetActive(true);
        start.SetActive(false);

        fixTableTxt.SetActive(false);
        attachCongratTxt.SetActive(false);
        trash.SetActive(false);
        gameplayInstruction.SetActive(false);
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

        fixTableTxt.SetActive(true);
    }

    public void Checker()
    {
        //Increases the checker count by 1
        checker += 1;
        Debug.Log("Checker count:" + checker);

        if (checker >= 1)
        {
            //Change UI if all the elements have been hung on the tree
            attachCongratTxt.SetActive(true);
            Debug.Log("DONE!!");
        }
        else
        {
            return;
        }
    }

    public void CheckerNext()
    {
        fixTableTxt.SetActive(false);
        attachCongratTxt.SetActive(false);

        gameplayInstruction.SetActive(true);
        trash.SetActive(true);
    }
}
