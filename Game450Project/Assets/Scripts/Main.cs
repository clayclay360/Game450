using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class Main : MonoBehaviour
{
    [Header("Variables")]
    public GameObject restartButton;
    public GameObject gameOverMenu, timerText, endTimeText, collectibleCounterText, endCollectiblesText, respawnButton;
    public bool startGame;
    public float gravityScale;

    [Header("Speed Progression")]
    public float maxSpeedProgressionDistance;
    public float speedProgression;
    private float timer;

    [Header("Respawn")]
    public GameObject maggot;
    public Transform spawnPos;
    private int fruitTarget = 5;
    public void Start()
    {
        GameManager.gameStarted = startGame;
        GameManager.playerCaptured = false;
        GameManager.playerIsGrounded = true;

        if (GameManager.gameStarted)
        {
            StartCoroutine(SpeedProgression());
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
        GameManager.escapeTimer = 5;
        GameManager.collectibleCount = 0;
        GameManager.localnumberOfFruit = 0;
    }

    public void GameOver()
    {
        GameManager.gameStarted = false;
        gameOverMenu.SetActive(true);
        timerText.SetActive(false);
        collectibleCounterText.SetActive(false);
        endTimeText.GetComponent<Text>().text = "Time: " + timer.ToString("F2") + "s";
        endCollectiblesText.GetComponent<Text>().text = "Fruits: " + GameManager.collectibleCount.ToString() + " Fruits";
        restartButton.SetActive(true);
        FindObjectOfType<HandAIController>().slider.gameObject.SetActive(false);

        if(GameManager.localnumberOfFruit >= fruitTarget)
        {
            respawnButton.GetComponent<Button>().interactable = true;
        }
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

    public void Respawn()
    {
        respawnButton.GetComponent<Button>().interactable = false;
        gameOverMenu.SetActive(false);
        GameObject maggotInstance = Instantiate(maggot, spawnPos.position, Quaternion.identity);
        FindObjectOfType<CameraScript>().target = maggotInstance;
        FindObjectOfType<HandAIController>().animator.SetBool("Go Away", true);
        GameManager.localnumberOfFruit -= fruitTarget;
        fruitTarget += 5;
        collectibleCounterText.SetActive(true);
        timerText.SetActive(true);
    }

    public void Update()
    {
        GameManager.gravityScale = gravityScale;

        if (GameManager.gameStarted)
        {
            timer += Time.deltaTime;
            timerText.GetComponent<Text>().text = timer.ToString("F2");
            if (collectibleCounterText.GetComponent<Text>().text != "Fruits: " + GameManager.collectibleCount.ToString())
            {
                collectibleCounterText.GetComponent<Text>().text = "Fruits: " + GameManager.collectibleCount.ToString();
            }
        }
    }
}
