using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    bool goingRight, goingLeft;
    Vector3 originalPosition;
    float platformDelay= 0.02f;
    float platformTimer = 0;
    Vector3 subtractVector = new Vector3(1, 1, 0);
    Vector3 addVector = new Vector3(1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        goingLeft = true;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        platformTimer = platformTimer + Time.deltaTime;
        if (platformTimer >= platformDelay)
        {
            updatePlatform();
            platformTimer = 0; 
        }
    }

    void updatePlatform()
    {
        if (goingLeft == true)
        {
            float x = transform.position.x - 0.005f;
            float y = transform.position.y;

            Vector3 newPostion = new Vector3(x, y, 0);

            transform.position = newPostion;

            if (transform.position == originalPosition - subtractVector)
            {
                goingRight = true;
            }
        }
        else if (goingRight == true)
        {
            float x = transform.position.x + 0.005f;
            float y = transform.position.y + 0.005f;

            Vector3 newPostion = new Vector3(x, y, 0);

            transform.position = newPostion;

            if (transform.position == originalPosition)
            {
                goingRight = true;
            }
        }
    }
}
