using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    Text gameOver;
    Button restartButton;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
