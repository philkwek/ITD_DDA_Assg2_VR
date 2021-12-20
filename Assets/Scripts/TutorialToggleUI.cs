using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggleUI : MonoBehaviour
{
    public GameObject controllerIntro;
    public GameObject intro;

    public GameObject fixTableTxt;
    public GameObject attachCongratTxt;
    public GameObject trash;
    public GameObject gameplayInstruction;

    // Start is called before the first frame update
    void Start()
    {
        controllerIntro.SetActive(false);
        intro.SetActive(true);

        fixTableTxt.SetActive(false);
        attachCongratTxt.SetActive(false);
        trash.SetActive(false);
        gameplayInstruction.SetActive(false);
    }
   
    public void FirstNext()
    {
        controllerIntro.SetActive(true);
        intro.SetActive(false);
    }

    public void SecondNext()
    {
        controllerIntro.SetActive(false);
        intro.SetActive(false);

        fixTableTxt.SetActive(true);
    }
}
