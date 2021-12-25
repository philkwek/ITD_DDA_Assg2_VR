using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject loginPage;
    public GameObject homePage;

    public RealtimeDbManager dbManager;
    public AuthManager authManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dbManager == null)
        {
            GameObject db = GameObject.Find("DatabaseManager");
            dbManager = db.GetComponent<RealtimeDbManager>();
            dbManager.CurrentlyActive();
            dbManager.InsertUsername();

            GameObject auth = GameObject.Find("DatabaseManager");
            authManager = auth.GetComponent<AuthManager>();
            authManager.GetMenu();
        }
    }

    public void QuitGame()
    {
        dbManager.GoOffline();
        Invoke("closeGame", 3);
    }

    private void closeGame()
    {
        Application.Quit();
    }

    public void OpenHomeMenu()
    {
        loginPage.SetActive(false);
        homePage.SetActive(true);
    }

    public void OpenLoginSignupMenu()
    {
        loginPage.SetActive(true);
        homePage.SetActive(false);
    }

    public void StartExperience()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void OpenReduceGame()
    {
        SceneManager.LoadScene("Reduce", LoadSceneMode.Single);
    }

    public void OpenReuseGame()
    {
        SceneManager.LoadScene("Reuse", LoadSceneMode.Single);
    }

    public void OpenRecycleGame()
    {
        SceneManager.LoadScene("Recycle", LoadSceneMode.Single);
    }

    public void OpenTutorial()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    


}
