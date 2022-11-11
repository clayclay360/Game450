using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject  restartButton, gameOverText, timerText, endTimeText;
    public bool startGame;

    private float timer;

    public void Start()
    {
        GameManager.gameStarted = startGame;

        if (GameManager.gameStarted)
        {
            StartGame();
        }    
    }

    public void StartGame()
    {
        GameManager.gameStarted = true;
        timerText.SetActive(true);
        timer = 0;
        Debug.Log("Game Started");
    }

    public void GameOver()
    {
        GameManager.gameStarted = false;
        timerText.SetActive(false);
        gameOverText.SetActive(true);
        endTimeText.GetComponent<Text>().text += timerText.GetComponent<Text>().text + "Seconds";
        restartButton.SetActive(true);
    }

    public void RestartGame(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void Update()
    {
        if (GameManager.gameStarted)
        {
            timer += Time.deltaTime;
            timerText.GetComponent<Text>().text = timer.ToString("F2");
        }
    }
}
