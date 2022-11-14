using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float startSize;
    public float gameSize;
    public float maxHeight;
    [Space]
    public GameObject target;

    private Camera cam;
    private Vector3 offset;
    private Vector3 pos;

    private bool follow;

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
            transform.position = offset + new Vector3(target.transform.position.x, 0, 0);

            if (GameManager.playerCaptured)
            {
                follow = false;
            }
        }
        FollowYOffset();
    }

    private void Update()
    {
        ReFollow();
        
    }

    public void FollowYOffset()
    {
        if(target.transform.position.y >= maxHeight)
        {
            Debug.Log("Calling");
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
