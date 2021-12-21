using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WelcomePage : MonoBehaviour
{
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;

    public TMP_InputField signupEmailInput;
    public TMP_InputField signupPasswordInput;
    public TMP_InputField signupUsernameInput;

    public AuthManager authManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //checks if email is a valid email
    bool IsValidEmail(string email) //https://www.codegrepper.com/code-examples/csharp/email+validation+c%23
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public void loginBtnClicked()
    {
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;
        authManager.LoginUser(email, password);
    }

    public void signupBtnClicked()
    {
        string email = signupEmailInput.text;
        string password = signupPasswordInput.text;
        string username = signupUsernameInput.text;

        //Handles user signup
        if (email == "" || password == "" || username == "")
        { // if user did not input in all necessary details, do something
            
        }
        else if (IsValidEmail(email) != true)
        { //if user inputs wrong email format, do something


        }
        else if (IsValidEmail(email) == true)
        {// if all fields are correct
            authManager.SignUpNewUser(email, password, username);
            Debug.Log("Creating Account...");
        }
    }
}
