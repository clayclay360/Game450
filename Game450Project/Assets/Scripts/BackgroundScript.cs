using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public Transform spawnPostion;
    public GameObject backgroundPrefab;

    public GameObject previousBackground;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject background = Instantiate(backgroundPrefab, spawnPostion.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Background").transform);
            background.GetComponent<BackgroundScript>().previousBackground = gameObject;


            if (previousBackground != null)
            {
                Destroy(previousBackground,10);
            }
        }
    }
}
