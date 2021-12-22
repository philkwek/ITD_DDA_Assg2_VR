using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;

public class AuthManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference databaseRef;

    public static string accountKey;
    public MenuManager menuManager;
    public RealtimeDbManager realtimedbManager;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (auth.CurrentUser != user)
        {
            Debug.Log("User is signed in");
            AlreadySignIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlreadySignIn()
    {
        menuManager.OpenHomeMenu(); // opens home menu
        realtimedbManager.InsertUsername();
    }

    public void SignUpNewUser(string email, string password, string username)
    {
        Debug.Log("Email: " + email + " Password: " + password);

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            //If sign up is successful, account is created using CreatePlayer function from script
            LoginUser(email, password);
            CreatePlayerData(username, newUser.UserId, email);

        });

    }

    public void LoginUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            }

            //Code to handle when an error pops up, to tell users what went wrong during login
            if (task.IsFaulted)
            {
                FirebaseException firebaseException = (FirebaseException)task.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "Unknown Error, Please try again.";

                switch (error)
                {
                    case AuthError.MissingEmail:
                        output = "Please Enter your Email";
                        break;
                    case AuthError.MissingPassword:
                        output = "Please Enter your Password";
                        break;
                    case AuthError.InvalidEmail:
                        output = "Invalid Email";
                        break;
                    case AuthError.WrongPassword:
                        output = "Incorrect Password";
                        break;
                    case AuthError.UserNotFound:
                        output = "Account does not exist";
                        break;
                }
                Debug.Log(output);
            }

            if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser newUser = task.Result;

                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                accountKey = newUser.UserId;

                //Login is success, load next scene
                Debug.Log("User logged in Succesfully!");
                menuManager.OpenHomeMenu(); // opens home menu
                realtimedbManager.InsertUsername();
            }

        });
    }

    public void LogoutUser()
    {
        auth.SignOut();
        menuManager.OpenLoginSignupMenu();
    }

    public void CreatePlayerData(string username, string userId, string email)
    {
        //create new objects from obj scripts
        Player player = new Player(false, email, userId, username);
        PlayerProfileData playerProfileData = new PlayerProfileData(0, 0, 0, 0, username);
        MinigameStats minigameStats = new MinigameStats("0", 0, 0, 0, 0, 0, 0);
        PlayerGameData playerGameData = new PlayerGameData(minigameStats, 0, 0, username);

        //convert data into json
        string playerJson = JsonUtility.ToJson(player);
        string playerProfileDataJson = JsonUtility.ToJson(playerProfileData);
        string playerGameDataJson = JsonUtility.ToJson(playerGameData);

        //add json data into realtimedb
        databaseRef.Child("playerGameData").Child(userId).SetRawJsonValueAsync(playerGameDataJson);
        databaseRef.Child("playerProfileData").Child(userId).SetRawJsonValueAsync(playerProfileDataJson);
        databaseRef.Child("players").Child(userId).SetRawJsonValueAsync(playerJson);

        Debug.Log("User Data added into DB");
    }
}
