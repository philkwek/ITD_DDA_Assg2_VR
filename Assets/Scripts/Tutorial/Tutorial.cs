using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject door;
    public GameObject throwCongratTxt;

    public TutorialToggleUI tutorialToggleUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BinCollider")
        {
            //Do something
            door.SetActive(true);
            throwCongratTxt.SetActive(true);

            tutorialToggleUI.gameplayInstruction.SetActive(false);
            Debug.Log("Collide..");
        }
    }

    public void OnHover()
    {
        if (gameObject.tag == "Door")
        {
            //Do something
            Debug.Log("Collide Door..");
            SceneManager.LoadScene("Reduce");
        }
    }
}
