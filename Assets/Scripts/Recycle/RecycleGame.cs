using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RecycleGame : MonoBehaviour
{
    public List<GameObject> throwables;
    public static bool isGameActive = false;
    public static bool isOne = false;

    public GameObject gameOver;
    public GameObject intructions;
    public GameObject gameItems;

    public GameObject locked;
    public GameObject unlocked;
    public GameObject lastPanel;

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI wrong;

    public static int score = 0;
    public static int miss = 0;
    public static int throws = 0;

    public static bool isUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        if (isUnlocked == false)
        {
            locked.SetActive(true);
            unlocked.SetActive(false);
        }
        else 
        {
            locked.SetActive(false);
            unlocked.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnThrowables();

        if (TimeManager.gameEnd == true)
        {
            GameOver();
        }

        UpdateScore();

        Debug.Log(throws);
        Debug.Log(score);
        Debug.Log(miss);
    }

    private void SpawnThrowables()
    {
        while (isGameActive && !isOne)
        {
            int index = Random.Range(0, throwables.Count);
            Instantiate(throwables[index].AddComponent<SelfDestruct>());
            isOne = true;
            throws += 1;
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        Time.timeScale = 0;
        gameOver.SetActive(true);
        gameItems.SetActive(false);
        finalScore.text = scoreTxt.text;
        if (miss == 0)
        {
            wrong.text = "* No trash have been place into the wrong bins *";
        }
        else
        {
            wrong.text = string.Format("* {0} trash had been place into the wrong bins *", miss);
        }
    }

    public void StartGame()
    {
        intructions.SetActive(false);
        gameItems.SetActive(true);
        isGameActive = true;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        gameOver.SetActive(false);
        score = 0;
        miss = 0;
        throws = 0;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        //Return to menu
        SceneManager.LoadScene("MainMenu");
    }

    private void Unlocked()
    {
        locked.SetActive(false);
        unlocked.SetActive(true);
    }

    private void UpdateScore()
    {
        scoreTxt.text = "Score: " + score;
    }

    public void LastPanel()
    {
        locked.SetActive(false);
        unlocked.SetActive(false);
        lastPanel.SetActive(true);
    }
}
