using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMove : MonoBehaviour
{
    Vector3 busPos;
    public float busSpeed;

    // Start is called before the first frame update
    void Start()
    {
        busSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        busPos = new Vector3(transform.position.x + (busSpeed * Time.deltaTime), transform.position.y, transform.position.z);

        transform.position = busPos;

        if (busPos.x > 30)
        {
            transform.position = new Vector3 (-35f, transform.position.y, transform.position.z);
        }
    }
}
