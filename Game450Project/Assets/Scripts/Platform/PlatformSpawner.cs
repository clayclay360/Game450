using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject collectible;
    public Transform platformSpawner, collectibleSpawner;
    public float destroyTime;
    public int collectibleSpawnChance;
    
    private Transform platformParent;
    private GameObject newPlatform;
    private bool playerHit;
    private bool playerEnter;

    private void Start()
    {
        platformParent = GameObject.FindGameObjectWithTag("Environment").transform;
        platformSpawner = transform.Find("Spawn");
        collectibleSpawner = transform.Find("CollectibleSpawn");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.gameStarted)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerEnter = true;
                if (!playerHit)
                {
                    InstantiatePlatform();
                    playerHit = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.gameStarted)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerEnter = true;
                if (!playerHit)
                {
                    InstantiatePlatform();
                    playerHit = true;
                }
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
            playerEnter = false;
            StartCoroutine(DestroyTimer(5f));
        }
    }

    public void InstantiatePlatform()
    {
        int randomPlatformIndex = Random.Range(0, platforms.Length);
        newPlatform = Instantiate(platforms[randomPlatformIndex], platformSpawner.position, Quaternion.identity, platformParent);
        newPlatform.GetComponent<PlatformSpawner>().SpawnCollectible();
    }

    public void SpawnCollectible()
    {
        int spawnRoll = Random.Range(0, 101);
        if(spawnRoll <= collectibleSpawnChance)
        {
            Instantiate(collectible, collectibleSpawner.position, Quaternion.identity, platformParent);
        }
    }

    IEnumerator DestroyTimer(float timer = 0)
    {
        float currentTime = Time.unscaledTime;
        bool destroyObject = false;

        while (timer > currentTime - Time.unscaledTime)
        {
            yield return null;

            if (playerEnter)
            {
                destroyObject = false;
                break;
            }

            destroyObject = true;
        }
        
        //dont detroy if player is captured or game is over
        if (!GameManager.playerCaptured && GameManager.gameStarted && destroyObject)
        {
            Destroy(gameObject, destroyTime);
        }
    }
}
