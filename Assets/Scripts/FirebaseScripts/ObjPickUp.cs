using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPickUp : MonoBehaviour
{
    public GameObject databaseManager;

    // Start is called before the first frame update
    void Start()
    {
        //Finds GameManager under DontDestroyOnLoad()
        databaseManager = GameObject.Find("DatabaseManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerHand")
        {
            databaseManager.GetComponent<RealtimeDbManager>().AddObjPicked();
        }
    }


}
