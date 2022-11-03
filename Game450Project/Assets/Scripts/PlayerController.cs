using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variable")]
    public int jumps;
    public int jumpforce;
    public int maxJumps;
    public float playerSpeed;
    public GameObject body, main;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerIsGrounded = true;
        jumps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStarted)
        {
            gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameManager.playerIsGrounded || jumps < maxJumps)
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
                    jumps++;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.playerIsGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            main.GetComponent<Main>().GameOver();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.playerIsGrounded = true;
            jumps = 0;
        }
    }
}
