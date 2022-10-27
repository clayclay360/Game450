using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int jumpforce;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerIsGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector2.right * GameManager.scrollSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
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
