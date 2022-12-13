using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float startSize;
    public float gameSize;
    public float maxHeight;
    public float yResetTimer;
    [Space]
    public GameObject target;

    private Camera cam;
    private Vector3 offset;
    private Vector3 pos;

    private bool follow;
    private bool resetY;

    private void Awake()
    {
        follow = true;
        pos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = startSize;

        if (GameManager.gameStarted)
        {
            StartCoroutine(ZoomOut());
        }

        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (follow)
        {
            if (resetY)
            {
                resetY = false;
                StartCoroutine(ResetYPos(yResetTimer));
            }

            transform.position = offset + new Vector3(target.transform.position.x, 0, 0);

            if (GameManager.playerCaptured &&  !GameManager.playerRespawning)
            {
                follow = false;
            }
        }
        else if(GameManager.playerRespawning && FindObjectOfType<MaggotScript>() != null && FindObjectOfType<MaggotScript>().isGrounded)
        {
            Debug.Log("HI");
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            //Solution to bug
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            resetY = true;
        }
        FollowYOffset();
    }

    private void Update()
    {
        ReFollow();
    }

    public IEnumerator ResetYPos(float timer)
    {
        float currentTime = Time.unscaledDeltaTime;
        float localY = transform.position.y;
        while(transform.position.y > 0)
        {
            localY -= (Time.unscaledDeltaTime - currentTime) / timer;
            yield return null;            
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void FollowYOffset()
    {
        if(target.transform.position.y >= maxHeight)
        {
            float dist = target.transform.position.y - maxHeight;
            transform.position += new Vector3(0, dist, 0);
        }
    }

    public void ReFollow()
    {
        if (!follow)
        {
            float dist = target.transform.position.x - transform.position.x;

            if (dist > offset.x)
            {
                follow = true;
            }
        }
    }

    public void ZoomOut(int x = 0)
    {
        StartCoroutine(ZoomOut());
    }

    public void EnableFollow()
    {
        follow = true;
    }

    IEnumerator ZoomOut()
    {
        float currentSize = startSize;

        while (currentSize < gameSize)
        {
            currentSize += Time.deltaTime * 5;
            cam.orthographicSize = currentSize;
            yield return null;
        }

        currentSize = gameSize;
        cam.orthographicSize = currentSize;
    }
}
