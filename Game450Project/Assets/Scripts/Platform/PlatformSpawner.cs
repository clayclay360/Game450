using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platforms;
    public Transform platformSpawner;
    public float destroyTime;
    
    private Transform platformParent;
    private bool playerHit;

    private void Start()
    {
        platformParent = GameObject.FindGameObjectWithTag("Environment").transform;
        platformSpawner = transform.Find("Spawn");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.gameStarted)
        {
            if (collision.gameObject.CompareTag("Player") && !playerHit)
            {
                InstantiatePlatform();
                playerHit = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.gameStarted && tag == "StartingPlatform" && !playerHit)
        {
            playerHit = true;
            InstantiatePlatform();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, destroyTime);
        }
    }

    public void InstantiatePlatform()
    {
        int randomPlatformIndex = Random.Range(0, platforms.Length);
        Instantiate(platforms[randomPlatformIndex], platformSpawner.position, Quaternion.identity, platformParent);
    }
}
