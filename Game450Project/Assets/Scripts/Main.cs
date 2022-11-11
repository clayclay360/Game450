using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject  restartButton, gameOverText, timerText, endTimeText;
    public bool startGame;

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
        Debug.Log("Game Started");
    }

    public void GameOver()
    {
        GameManager.gameStarted = false;
        timerText.SetActive(false);
        gameOverText.SetActive(true);
        endTimeText.GetComponent<Text>().text += "\n" + timerText.GetComponent<Text>().text;
        restartButton.SetActive(true);
    }

    public void RestartGame(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void Update()
    {
        while (GameManager.gameStarted)
        {
            timerText.GetComponent<Text>().text = Time.deltaTime.ToString("F2");
        }
    }
}
