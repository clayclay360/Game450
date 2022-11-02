using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platforms;
    public Transform spawner;
    public float destroyTime;
    
    private Transform platformParent;
    private bool playerHit;

    private void Start()
    {
        platformParent = GameObject.FindGameObjectWithTag("Environment").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerHit)
        {
            int randomPlatformIndex = Random.Range(0, platforms.Length);
            Instantiate(platforms[randomPlatformIndex], spawner.position, Quaternion.identity, platformParent);
            playerHit = true;
        }
    }

    private void nCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerHit)
        {
            Destroy(gameObject, destroyTime);
        }
    }
}
