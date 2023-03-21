using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    public ParticleSystemRenderer pRender;
    float jumpForce = 300.0f;
    float walkForce = 20.0f;
    float maxWalkSpeed = 2.0f;
    float flyTime = 3.0f;
    float flyTemp = 0.0f;
    public ParticleSystem dust;
    float speed;
    public FlyGauge flyGauge;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        flyTemp = flyTime;
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move left and right
        // Includes my attempt to give the funny bee movement to the bee
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (key == -1 && this.rigid2D.velocity.y == 0)
            {
                CreateDust(1);
            }
            key = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (key == 1 && this.rigid2D.velocity.y == 0)
            {
                CreateDust(-1);
            }
        key = -1;
        }

        // decide PC's speed
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        float speedy = Mathf.Abs(this.rigid2D.velocity.y);

        if (Input.GetKey(KeyCode.UpArrow) && this.rigid2D.velocity.y == 0)
        {
            //Initial fly up
            this.rigid2D.AddForce(transform.up * 20);
            flyGauge.Show();
        }

        //Allows flight for the during of the flightTimer, we can be adjusted as the player progresses
        if (flyTemp > 0 && this.rigid2D.velocity.y != 0)
        {
            flyTemp -= Time.deltaTime;
            flyGauge.SetGauge(flyTemp / flyTime);
            //Debug.Log(flyTemp);
            //Seems to be printing values when unnecessary, could be improved for performance
            if (speedy != 0 && speedx >= this.maxWalkSpeed)
            {
                speed = speedx / 1.3f;
                // Good solution but doesn't give the bee drift
                //this.rigid2D.velocity = new Vector2(speed, this.rigid2D.velocity.y);
                this.rigid2D.AddForce(transform.right * key * speed);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                //Amount of force for flying up
                this.rigid2D.AddForce(transform.up * 20);
            }
        }

        // Might cause issues later because it resets every frame on the ground
        if (this.rigid2D.velocity.y == 0 && flyTemp < flyTime)
        {
            flyTemp += (2 * Time.deltaTime);
            flyGauge.SetGauge(flyTemp / flyTime);
            if (flyTemp == flyTime)
            {
                flyGauge.Hide();
            }
        } else if (flyTemp > flyTime)
        {
            flyTemp = flyTime;
            flyGauge.Hide();
        }

        // limit PC's speed and avoid acceleration.
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // reverse spring according to the direction
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        this.animator.speed = speedx / 2.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       // Can set colliders here
    }

    void CreateDust(int key)
    {
        // Trying to flip the dust particles but it is not working
        //dust.transform.localScale = new Vector3(key, 1, 1);
        dust.Play();
    }
}

