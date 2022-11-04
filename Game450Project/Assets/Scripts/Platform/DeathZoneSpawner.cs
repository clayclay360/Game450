using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneSpawner : MonoBehaviour
{
    public GameObject deathZone;
    public Transform deathZoneSpawner;
    public float destroyTime;

    private Transform platformParent;

    private void Start()
    {
        platformParent = GameObject.FindGameObjectWithTag("Environment").transform;
        deathZoneSpawner = transform.Find("Spawn");
    }

    private void Update()
    {
        SpawnDeathZone();
        DespawnDeathZone();
    }

    private void SpawnDeathZone()
    {
        if (GameManager.gameStarted)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player.transform.position.x >= deathZoneSpawner.position.x)
            {
                Instantiate(deathZone, deathZoneSpawner.position, Quaternion.identity, platformParent);
            }
        }
    }

    private void DespawnDeathZone()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x >= deathZone.transform.position.x)
        {
            Debug.Log(gameObject.name);
            Main main = GameObject.Find("Main").GetComponent<Main>();
            if (gameObject != main.startingPlatform)
            {
                Destroy(deathZone, destroyTime);
            }
        }
    }
}
