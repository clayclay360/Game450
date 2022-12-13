using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotScript : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float crawlTime;
    public Transform playerSpawn;

    [HideInInspector]
    public bool isGrounded;
    private bool isTransforming;

    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Crawl();

        if (transform.position.y < -10)
        {
            FindObjectOfType<Main>().GameOver();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Walk", true);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            StartCoroutine(Cacoon(crawlTime));
        }
    }

    public void Crawl()
    {
        if (isGrounded && !isTransforming)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    IEnumerator Cacoon(float time)
    {
        yield return new WaitForSeconds(time);
        isTransforming = true;
        animator.SetBool("Transform", true);
    }

    public void RespawnPlayer()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        HandAIController hand =FindObjectOfType<HandAIController>();
        player.GetComponent<PlayerController>().body.SetActive(true);
        player.GetComponent<PlayerController>().ResetVariables();
        player.transform.position = playerSpawn.position;
        hand.chocolateInHand.gameObject.SetActive(false);
        hand.miniGameStarted = false;
        FindObjectOfType<CameraScript>().target = player;
        FindObjectOfType<CameraScript>().EnableFollow();
        FindObjectOfType<HandAIController>().animator.SetBool("Go Away", false);
        GameManager.playerCaptured = false;
        GameManager.gameStarted = true;
        Destroy(gameObject, 1);
    }
}
