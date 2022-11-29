using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variable")]
    public int jumps;
    public int jumpforce;
    public int maxJumps;
    public int maxhovertime;
    public float playerSpeed;
    public float y_deadZone;
    public float breakingForce;
    public bool hovering;

    private float hovertimer;
    private float presstimer;
    private float hoverpresstime;

    [Space]
    public GameObject body;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerIsGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        jumps = 0;
        hovering = false;
        hovertimer = 0;
        presstimer = 0;
        hoverpresstime = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        Escape();

        //if the player is captured, turn off the gravity
        if (GameManager.playerCaptured)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void Movement()
    {
        if (GameManager.gameStarted && !GameManager.playerCaptured)
        {
            gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                if (!GameManager.playerIsGrounded && hovertimer < maxhovertime)
                {
                    presstimer += Time.deltaTime;
                    if (presstimer <= hoverpresstime)
                    {
                        hovering = true;
                        rb.gravityScale = 0;
                        rb.velocity = new Vector2(0, 0);
                        hovertimer += Time.deltaTime;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) || hovertimer >= maxhovertime)
            {
                if (GameManager.playerIsGrounded || (jumps < maxJumps && presstimer < hoverpresstime))
                {
                    rb.AddForce(Vector2.up * jumpforce);
                    jumps++;
                }
                presstimer = 0;
                rb.gravityScale = 1;
                hovering = false;
            }

            if (body.transform.position.y < y_deadZone && GameManager.gameStarted)
            {
                FindObjectOfType<Main>().GameOver();
            }
        }
    }

    public void Escape()
    {
        if (GameManager.playerCaptured)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.escapeRate++;
            }
        }
    }

    public void BreakFree(Vector3 pos)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = pos;
        rb.AddForce(Vector2.up * breakingForce);
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
            hovertimer = 0;
        }
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            GameManager.collectibleCount++;
        }
    }
}
