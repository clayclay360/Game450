using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variable")]
    public int jumpforce;
    public float playerSpeed;
    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerIsGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStarted)
        {
            gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.playerIsGrounded = true;
        }
    }
}
