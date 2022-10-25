using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAIController : MonoBehaviour
{
    [Header("Variables")]
    public GameObject Target;
    public float followYOffset;
    public float smoothDampTime;

    [Header("Grab Mechanic")]
    public float minTime;
    public float maxTime;

    [HideInInspector]
    public bool isFollowingPlayer = true;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    private IEnumerator Grab()
    {
        while (GameManager.gameStarted)
        {
            float time = 0;
            time = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(time);
            animator.SetTrigger("Grab");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gotcha");
        }
    }
}
