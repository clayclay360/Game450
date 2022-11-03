using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject player, hand, playerStartPosition, handStartPosition, startButton, gameOverText, startingPlatform;

    public void StartGame()
    {
        GameManager.gameStarted = true;
        gameOverText.SetActive(false);
        player.transform.position = playerStartPosition.transform.position;
        hand.transform.position = handStartPosition.transform.position;
        player.GetComponent<PlayerController>().body.SetActive(true);
        hand.GetComponent<HandAIController>().chocolateInHand.SetActive(false);
        GameObject[] platformsArray = GameObject.FindGameObjectsWithTag("Ground");
        foreach(GameObject platform in platformsArray)
        {
            //Debug.Log(platform.name);
            if (platform != startingPlatform)
            {
                Destroy(platform);
            }
        }
    }

    public void GameOver()
    {
        player.gameObject.GetComponent<PlayerController>().body.SetActive(false);
        GameManager.gameStarted = false;
        gameOverText.SetActive(true);
        startButton.GetComponentInChildren<Text>().text = "Restart";
        startButton.SetActive(true);
    }
}
