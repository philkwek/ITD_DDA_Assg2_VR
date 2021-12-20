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

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI finalScore;

    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
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
    }

    private void SpawnThrowables()
    {
        while (isGameActive && !isOne)
        {
            int index = Random.Range(0, throwables.Count);
            Instantiate(throwables[index]);
            isOne = true;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        gameItems.SetActive(false);
        finalScore.text = scoreTxt.text;
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
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        //Return to menu
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
}
