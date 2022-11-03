using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float startSize;
    public float gameSize;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        
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
