using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachReduce : MonoBehaviour
{
    public GameObject metalBoard;
    public GameObject plasticBoard;
    public GameObject spoonMountain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            metalBoard.SetActive(true);
            spoonMountain.SetActive(true);
        }
        else if (other.tag == "PlasticSpoon")
        {
            plasticBoard.SetActive(true);
            spoonMountain.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            metalBoard.SetActive(false);
            spoonMountain.SetActive(false);
        }

        else if (other.tag == "PlasticSpoon")
        {
            //Do something
            plasticBoard.SetActive(false);
            spoonMountain.SetActive(false);
        }
    }
}
