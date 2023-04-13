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
        else
        {
            WaterRight.Pause();
            WaterLeft.Pause();
            WaterRight.Clear();
            WaterLeft.Clear();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject == WateringCan.gameObject) && playerGrab.HoldingObject && Input.GetKey(KeyCode.LeftShift))
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
    }


}
