using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachReduce : MonoBehaviour
{
    public GameObject metalBoard;
    public GameObject plasticBoard;
    public GameObject spoonMountain;
    public GameObject resultsGood;
    public GameObject resultsBad;

    public ReduceUI reduceUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            reduceUI.Raycast();
            metalBoard.SetActive(true);
            spoonMountain.SetActive(true);
            //resultsGood.SetActive(true);
            //resultsBad.SetActive(false);
            
        }
        else if (other.tag == "PlasticSpoon")
        {
            reduceUI.Raycast();
            plasticBoard.SetActive(true);
            spoonMountain.SetActive(true);
            //resultsGood.SetActive(false);
            //resultsBad.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            reduceUI.Direct();
            metalBoard.SetActive(false);
            spoonMountain.SetActive(false);
            resultsGood.SetActive(false);
            resultsBad.SetActive(false);
            Debug.Log("Error123");
        }

        else if (other.tag == "PlasticSpoon")
        {
            //Do something
            reduceUI.Direct();
            plasticBoard.SetActive(false);
            spoonMountain.SetActive(false);
            resultsGood.SetActive(false);
            resultsBad.SetActive(false);
        }
    }

}
