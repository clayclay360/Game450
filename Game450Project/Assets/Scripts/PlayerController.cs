using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variable")]
    public int jumps;
    public int jumpForce;
    public int maxJumps;
    public int maxHoverTime;
    public float playerSpeed;
    public float y_deadZone;
    public float breakingForce;
    public float hoverPressTime;
    public float hoverGravityScale;
    public bool hovering;
    public float maxHeight;

    private float hovertimer;
    private float presstimer;

    [Space]
    public GameObject body;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerIsGrounded = true;
        GameManager.playerSpeed = playerSpeed;
        rb = GetComponent<Rigidbody2D>();
        jumps = 0;
        hovering = false;
        hovertimer = 0;
        presstimer = 0;
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
            gameObject.transform.Translate(Vector2.right * GameManager.playerSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                if (!GameManager.playerIsGrounded && hovertimer < maxHoverTime && jumps >= maxJumps)
                {
                    Debug.Log("Hover");
                    presstimer += Time.deltaTime;
                    if (presstimer <= hoverPressTime)
                    {
                        hovering = true;
                        rb.gravityScale = 0.2f;
                        if (hovertimer <= 0)
                        {
                            rb.velocity = new Vector2(0, 0);
                        }
                        hovertimer += Time.deltaTime;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) || hovertimer >= maxHoverTime)
            {
                if (GameManager.playerIsGrounded || (jumps < maxJumps && presstimer < hoverPressTime))
                {
                    rb.AddForce(Vector2.up * jumpForce/ (jumps+1));
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

            float posY = transform.position.y;
            posY = Mathf.Clamp(posY, -20, maxHeight);
            transform.position = new Vector2(transform.position.x, posY);
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
