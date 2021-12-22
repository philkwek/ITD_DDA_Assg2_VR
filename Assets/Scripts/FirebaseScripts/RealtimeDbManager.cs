using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;

public class RealtimeDbManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference databaseRef;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Start is called before the first frame update
    void Start()
    {
        InsertUsername();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InsertUsername()
    {
        //Changes username in player child
        Query searchPlayer = databaseRef.Child("players").OrderByChild("userID").EqualTo(auth.CurrentUser.UserId).LimitToFirst(1);

        searchPlayer.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                throw task.Exception;
            }

            if (!task.IsCompleted)
            {
                return;
            }

            //Take snapshot of database
            DataSnapshot ds = task.Result;
            if (ds.Exists)
            {
                Debug.Log("Player found...");
                Debug.Log(ds);

                foreach (DataSnapshot snap in ds.Children)
                {
                    Player playerDetails = JsonUtility.FromJson<Player>(snap.GetRawJsonValue());
                    Debug.Log(playerDetails);
                }
            }
            else
            {
                Debug.Log("Player does not exist");
            }
           });
    }
}