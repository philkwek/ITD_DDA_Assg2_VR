using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recyclable : MonoBehaviour
{
    public List<GameObject> note;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Recyclable")
        {
            //Do something
            TimeManager.timeRemaining += 5;
        }
        else if (other.tag == "NonRecyclable")
        {
            //Check what object
            int index = 0;
            //Do Something
            note[index].SetActive(true);
            Time.timeScale = 0;
        }

        Destroy(other.gameObject);

        RecycleGame.isOne = false;
    }
}
