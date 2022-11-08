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
    public float y_deadZone;

    [Space]
    public GameObject body;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerIsGrounded = true;
        rb = GetComponent<Rigidbody2D>();
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
                    rb.AddForce(Vector2.up * jumpforce);
                    jumps++;
                }
            }

            if (body.transform.position.y < y_deadZone && GameManager.gameStarted)
            {
                FindObjectOfType<Main>().GameOver();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("StartingPlatform"))
        {
            GameManager.playerIsGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("StartingPlatform"))
        {
            GameManager.playerIsGrounded = true;
            jumps = 0;
        }
    }
}
