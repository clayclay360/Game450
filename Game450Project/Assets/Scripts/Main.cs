using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [Header("Variables")]
    public GameObject restartButton;
    public GameObject gameOverText, timerText, endTimeText, collectibleCounterText;
    public bool startGame;

    [Header("Speed Progression")]
    public float maxSpeedProgressionDistance;
    public float speedProgression;
    private float timer;

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
        timerText.SetActive(true);
        timer = 0;
        collectibleCounterText.SetActive(true);
        GameManager.escapeGoal = 10;
        GameManager.escapeTimer = 3;
        GameManager.collectibleCount = 0;
    }

    public void GameOver()
    {
        GameManager.gameStarted = false;
        gameOverText.SetActive(true);
        timerText.SetActive(false);
        collectibleCounterText.SetActive(false);
        endTimeText.GetComponent<Text>().text += timerText.GetComponent<Text>().text + "Seconds";
        restartButton.SetActive(true);
        FindObjectOfType<HandAIController>().slider.gameObject.SetActive(false);
    }

    public void RestartGame(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void StartSpeedProgression()
    {
        StartCoroutine(SpeedProgression());
    }

    public IEnumerator SpeedProgression()
    {
        float  timeTarget = timer + 15;

        while (GameManager.gameStarted)
        {
            if (timer > timeTarget && timeTarget < maxSpeedProgressionDistance)
            {
                GameManager.playerSpeed += speedProgression;
                timeTarget += 15;
            }
            yield return null;
        }
    }

    public void Update()
    {
        if (GameManager.gameStarted)
        {
            timer += Time.deltaTime;
            timerText.GetComponent<Text>().text = timer.ToString("F2");
            if (collectibleCounterText.GetComponent<Text>().text != "Collectibles: " + GameManager.collectibleCount.ToString())
            {
                collectibleCounterText.GetComponent<Text>().text = "Collectibles: " + GameManager.collectibleCount.ToString();
            }
        }
    }
}
