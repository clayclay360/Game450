using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject deathZone;
    public float destroyTime;
    
    private Transform platformParent, platformSpawner, deathZoneSpawner;
    private bool playerHit;

    private void Start()
    {
        platformParent = GameObject.FindGameObjectWithTag("Environment").transform;
        platformSpawner = transform.Find("Spawn");
        deathZoneSpawner = platformParent.Find("DeathZone").Find("Spawn");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.gameStarted)
        {
            if (collision.gameObject.CompareTag("Player") && !playerHit)
            {
                int randomPlatformIndex = Random.Range(0, platforms.Length);
                Instantiate(platforms[randomPlatformIndex], platformSpawner.position, Quaternion.identity, platformParent);
                Instantiate(deathZone, deathZoneSpawner.position, Quaternion.identity, platformParent);
                playerHit = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerHit)
        {
            Debug.Log(gameObject.name);
            Main main = GameObject.Find("Main").GetComponent<Main>();
            if (gameObject != main.startingPlatform)
            {
                Destroy(gameObject, destroyTime);
                Destroy(deathZone);
            }
        }
    }
}
