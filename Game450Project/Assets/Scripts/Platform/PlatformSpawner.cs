using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject collectible, player;
    public Transform platformSpawner, collectibleSpawner;
    public float destroyTime;
    public int collectibleSpawnChance;
    
    private Transform platformParent;
    private bool playerHit;
    private bool playerEnter;
    private int maxSpawnCount;
    private int spawnCount;

    private void Start()
    {
        platformParent = GameObject.FindGameObjectWithTag("Environment").transform;
        platformSpawner = transform.Find("Spawn");
        collectibleSpawner = transform.Find("CollectibleSpawn");
        player = GameObject.FindGameObjectWithTag("Player");
        spawnCount = 0;
        maxSpawnCount = 1;
    }

    private void Update()
    {
        if (player.transform.position.x >= platformSpawner.position.x && spawnCount < maxSpawnCount)
        {
            InstantiatePlatform();
            StartCoroutine(DestroyTimer(5f));
            spawnCount++;
        }
    }

    public void InstantiatePlatform()
    {
        int randomPlatformIndex = Random.Range(0, platforms.Length);
        Instantiate(platforms[randomPlatformIndex], platformSpawner.position, Quaternion.identity, platformParent);
        SpawnCollectible();
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
