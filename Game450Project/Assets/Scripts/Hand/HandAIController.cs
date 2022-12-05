using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandAIController : MonoBehaviour
{
    [Header("Variables")]
    public GameObject Target;
    public GameObject chocolateInHand;
    [HideInInspector]
    public float followYOffset;
    [HideInInspector]
    public float followXOffset;
    public float smoothDampTime;
    public float pingPongValue;

    [Header("Grab Mechanic")]
    public float minTime;
    public float maxTime;

    [Header("UI")]
    public Slider slider;

    [HideInInspector]
    public bool isFollowingPlayer = true;
    [HideInInspector]
    public bool canGrab = false;
    [HideInInspector]
    public bool isGrabbing = true;
    private bool shocked = false;
    private bool miniGameStarted;
    private float followDistance;
    private float dist;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        followDistance = Target.transform.position.x - transform.position.x;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStarted)
        {
            Follow();
            Captured();
            EnemyShocked();

            if (!canGrab && !GameManager.playerCaptured)
            {
                StartCoroutine(Grab());
                canGrab = true;
            }
        }
    }

    public void Follow()
    {
        if (isFollowingPlayer)
        {
            float yReference = gameObject.transform.position.y;
            float reference = 0;

            yReference = Mathf.SmoothDamp(transform.position.y, Target.transform.position.y + followYOffset, ref reference, smoothDampTime);
            gameObject.transform.position = new Vector3(transform.position.x, yReference, transform.position.z);
        }

        if (!shocked)
        {
            followXOffset = Mathf.PingPong(Time.time, pingPongValue);
            transform.position = new Vector3(followDistance + followXOffset + Target.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private IEnumerator Grab()
    {
        while (!GameManager.playerCaptured && GameManager.gameStarted)
        {
            float time = 0;
            time = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(time);

            if (!GameManager.playerCaptured)
            {
                animator.SetTrigger("Grab");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isGrabbing)
        {
            other.gameObject.GetComponentInParent<PlayerController>().body.SetActive(false);
            chocolateInHand.SetActive(true);
            slider.gameObject.SetActive(true);
            GameManager.playerCaptured = true;
        }
    }

    public void EnemyShocked()
    {
        if (shocked)
        {
            dist = Target.transform.position.x - transform.position.x;

            if(dist > followDistance)
            {
                shocked = false;
            }
        }
    }

    public void Captured()
    {
        if (GameManager.playerCaptured)
        {
            slider.value = GameManager.escapeRate;
            slider.maxValue = GameManager.escapeGoal;

            if (!miniGameStarted)
            {
                //if (GameManager.collectibleCount >= GameManager.numberofCaptures * 2)
                //{
                    canGrab = false;
                    miniGameStarted = true;
                    GameManager.escapeRate = 0;
                    GameManager.escapeGoal = GameManager.escapeGoal + (GameManager.numberofCaptures * 3);
                    //GameManager.collectibleCount -= GameManager.numberofCaptures * 2;
                    GameManager.numberofCaptures++;
                    StartCoroutine(EscapeMiniGame(GameManager.escapeTimer + (GameManager.numberofCaptures * 2)));
                //}
                //else
                //{
                //    FindObjectOfType<Main>().GameOver();
                //}
            }
        }
    }

    IEnumerator EscapeMiniGame(float timer)
    {
        float currentTime = Time.unscaledTime;
        while (timer > Time.unscaledTime - currentTime)
        {
            if (GameManager.escapeRate >= GameManager.escapeGoal && GameManager.gameStarted)
            {
                //free player
                shocked = true;
                chocolateInHand.gameObject.SetActive(false);
                FindObjectOfType<PlayerController>().body.gameObject.SetActive(true);
                FindObjectOfType<PlayerController>().BreakFree(chocolateInHand.transform.position);
                miniGameStarted = false;
                GameManager.playerCaptured = false;
                slider.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }

        if(GameManager.escapeRate < GameManager.escapeGoal)
        {
            FindObjectOfType<Main>().GameOver();
        }

        slider.gameObject.SetActive(false);
    }
}
