using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachReduce : MonoBehaviour
{
    public GameObject metalBoard;
    public GameObject plasticBoard;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            metalBoard.SetActive(true);
        }

        if (other.tag == "PlasticSpoon")
        {
            plasticBoard.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            metalBoard.SetActive(false);
        }

        if (other.tag == "PlasticSpoon")
        {
            //Do something
            plasticBoard.SetActive(false);
        }
    }
}
