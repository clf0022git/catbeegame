using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Animator animator;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask platformsLayerMask;
    float flyForce = 20.0f;
    float walkForce = 10.0f;
    float maxFlySpeed = 2.0f;
    float maxWalkSpeed = 2.0f;
    float flyTime = 1.0f;
    float flyTemp = 0.0f;
    public ParticleSystem dustLeft;
    public ParticleSystem dustRight;
    float speed;
    public FlyGauge flyGauge;
    int pastKey = 0;
    bool flightTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.boxCollider2d = GetComponent<BoxCollider2D>();
        flyTemp = flyTime;
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.rigid2D.velocity.x == 0 && this.rigid2D.velocity.y == 0)
        {
            animator.Play("CatIdle");
        }
        else
        {
            animator.Play("CatWalk");
        }

        // move left and right
        // Includes my attempt to give the funny bee movement to the bee
        int key = 0;
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            if(pastKey == -1 && this.rigid2D.velocity.y != 0)
            {
                flightTurn = true;
            }

            if (pastKey == -1 && this.rigid2D.velocity.y == 0 && !Input.GetKey(KeyCode.A))
            {
                CreateDust(1);
                pastKey = 0;
            }
            key = 1;
            pastKey = 1;
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (pastKey == 1 && this.rigid2D.velocity.y !=0)
            {
                flightTurn = true;
            }

            if (pastKey == 1 && this.rigid2D.velocity.y == 0 && !Input.GetKey(KeyCode.D))
            {
                CreateDust(-1);
                pastKey = 0;
            }
            key = -1;
            pastKey = -1;
        }

        // decide PC's speed
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        float speedy = Mathf.Abs(this.rigid2D.velocity.y);

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            //Initial fly up
            this.rigid2D.AddForce(transform.up * 100);
            flyGauge.Show();
        }

        //Allows flight for the during of the flightTimer, we can be adjusted as the player progresses
        if (flyTemp > 0 && (this.rigid2D.velocity.y != 0 || Input.GetKey(KeyCode.Space))) //Makes sure the player isn't cheating by flying under a platform
        {
            flyGauge.Show();
            if (Input.GetKey(KeyCode.Space))
            {
                flyTemp -= Time.deltaTime;
                flyGauge.SetGauge(flyTemp / flyTime);
                //Debug.Log(flyTemp);
                //Seems to be printing values when unnecessary, could be improved for performance

                if (Mathf.Sign(this.rigid2D.velocity.y) == -1)
                {
                    flyForce = 50;
                }
                else
                {
                    flyForce = 15;
                }
                //Debug.Log(this.rigid2D.velocity.y);
                if (speedy > maxFlySpeed)
                {
                    flyForce = flyForce / 1.5f;
                }

                this.rigid2D.AddForce(transform.up * flyForce);
            }
            
            // Give enough force to push the other way if necessary
            if (flightTurn == true)
            {
                this.rigid2D.AddForce(transform.right * key * 50);
                flightTurn = false;
                //Debug.Log("boost triggered");
            } 

            if (speedx >= this.maxWalkSpeed)
            {
                speed = speed / 2;
                //Debug.Log(speed);
                this.rigid2D.AddForce(transform.right * key * speed);
            }
            else
            {
                speed = speedx;

                this.rigid2D.AddForce(transform.right * key * this.walkForce);
            }
     
        }

        // Might cause issues later because it resets every frame on the ground
        if (this.rigid2D.velocity.y == 0 && !Input.GetKey(KeyCode.Space) && flyTemp < flyTime)
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
        } else if (flyTemp == flyTime)
        {
            flyGauge.Hide();
        }

        // limit PC's speed and avoid acceleration.
        if (speedx < this.maxWalkSpeed && this.rigid2D.velocity.y == 0)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // reverse spring according to the direction
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        if (this.rigid2D.velocity.x != 0 && this.rigid2D.velocity.y != 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       // Can set colliders here
    }

    void CreateDust(int key)
    {
        // Trying to flip the dust particles but it is not working
        //dust.transform.localScale = new Vector3(key, 1, 1);
        if(key == 1)
        {
            dustLeft.Play();
        }
        else if (key == -1)
        {
            dustRight.Play();
        }
    }

    private bool IsGrounded()
    {
        float extraHeightTest = .1f;
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightTest, platformsLayerMask);
        Color rayColor;
        if (raycastHit2d.collider != null)
        {
            rayColor = Color.green;
        } else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightTest), Vector2.right * (boxCollider2d.bounds.extents.x), rayColor);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }
}

