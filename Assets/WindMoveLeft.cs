using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMoveLeft : MonoBehaviour
{
    Vector3 windPos;
    Vector3 startPos;
    public float windSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        windPos = new Vector3(transform.position.x - (windSpeed * Time.deltaTime), transform.position.y, transform.position.z);

        transform.position = windPos;

        if (windPos.x <= startPos.x - 5 )
        {
            transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
        }
    }
}
