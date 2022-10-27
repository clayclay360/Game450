using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public bool isFollowingPlayer = true;
    private float followDistance;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        followDistance = Target.transform.position.x - transform.position.x;

        animator = GetComponent<Animator>();
        StartCoroutine(Grab());
    }

    // Update is called once per frame
    void Update()
    {
        Follow();   
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

        followXOffset = Mathf.PingPong(Time.time, pingPongValue);
        transform.position = new Vector3(followDistance + followXOffset + Target.transform.position.x, transform.position.y, transform.position.z);
    }

    private IEnumerator Grab()
    {
        while (GameManager.gameStarted)
        {
            float time = 0;
            time = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(time);

            if (GameManager.gameStarted)
            {
                animator.SetTrigger("Grab");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            chocolateInHand.SetActive(true);
            GameManager.gameStarted = false;
        }
    }
}
