using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPour : MonoBehaviour
{
    public PlayerGrabbing playerGrab;
    Rigidbody2D WateringCan;
    public ParticleSystem WaterLeft;
    public ParticleSystem WaterRight;
    int key = 0;


    // Start is called before the first frame update
    void Start()
    {
        WateringCan = this.gameObject.GetComponent<Rigidbody2D>();
        WaterLeft.Pause();
        WaterLeft.Clear();
        WaterRight.Pause();
        WaterRight.Clear();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            key = 1;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            key = -1;
        }

        if ((this.gameObject == WateringCan.gameObject) && (playerGrab.HoldingObject == true) && Input.GetKey(KeyCode.LeftShift))
        {


            if (key == 1)
            {

                WaterLeft.Pause();
                WaterLeft.Clear();
                WaterRight.Play();

            }
            else if (key == -1)
            {

                WaterRight.Pause();
                WaterRight.Clear();
                WaterLeft.Play();

            }

        }
        else
        {

            WaterLeft.Pause();
            WaterLeft.Clear();
            WaterRight.Pause();
            WaterRight.Clear();

        }


    }



}

