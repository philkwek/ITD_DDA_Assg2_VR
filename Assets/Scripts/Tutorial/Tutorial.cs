using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject door;
    public GameObject throwCongratTxt;

    public GameObject databaseManager;

    public TutorialToggleUI tutorialToggleUI;

    private void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("GameManager");
    }

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
            int completion = databaseManager.GetComponent<RealtimeDbManager>().completion;
            if (completion <= 0)
            {
                databaseManager.GetComponent<RealtimeDbManager>().completion = 1;
            };
            Debug.Log("Collide Door..");
            SceneManager.LoadScene("Reduce");
        }
    }
}
