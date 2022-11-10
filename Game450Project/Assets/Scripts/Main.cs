using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject  restartButton, gameOverText;
    public bool startGame;

    public void Start()
    {
        GameManager.gameStarted = startGame;

        if (GameManager.gameStarted)
        {
            GameManager.playerCaptured = false;
            GameManager.playerIsGrounded = true;
            StartGame();
        }    
    }

    public void StartGame()
    {
        GameManager.gameStarted = true;
        GameManager.escapeGoal = 10;
        GameManager.escapeTimer = 3;
    }

    public void GameOver()
    {
        GameManager.gameStarted = false;
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
    }

    public void RestartGame(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
}
