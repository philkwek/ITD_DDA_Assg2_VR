using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachReduce : MonoBehaviour
{
    public GameObject metalPanel;
    public GameObject plasticPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            metalPanel.SetActive(true);
        }

        if (other.tag == "PlasticSpoon")
        {
            plasticPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MetalSpoon")
        {
            //Do something
            metalPanel.SetActive(false);
        }

        if (other.tag == "PlasticSpoon")
        {
            //Do something
            plasticPanel.SetActive(false);
        }
    }
}
