using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAIController : MonoBehaviour
{

    public GameObject Target;
    public float followYOffset;
    public float smoothDampTime;
    public float verticalMax, verticalMin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();   
    }

    public void Follow()
    {
        float yReference = gameObject.transform.position.y;
        float reference = 0;
        yReference = Mathf.SmoothDamp(transform.position.y, Target.transform.position.y + followYOffset, ref reference, smoothDampTime);
        gameObject.transform.position = new Vector3(transform.position.x, yReference, transform.position.z);
    }
}
