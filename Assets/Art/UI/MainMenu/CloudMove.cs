using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    Vector3 cloudPos;
    public float cloudSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {  

    }

    // Update is called once per frame
    void Update()
    {
        cloudPos = new Vector3(transform.position.x + (cloudSpeed * Time.deltaTime), transform.position.y, transform.position.z);

        transform.position = cloudPos;

        if (cloudPos.x > 12)
        {
            transform.position = new Vector3 (-12f, Random.Range(2.5f, 5f), transform.position.z);
        }
    }
}
