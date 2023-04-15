using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource flySound;
    [SerializeField] private AudioSource grassWalk;
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private ParticleSystem dustLeft;
    [SerializeField] private ParticleSystem dustRight;
    [SerializeField] private FlyGauge flyGauge;
    private Rigidbody2D rigid2D;
    private Animator animator;
    private BoxCollider2D boxCollider2d;
    [SerializeField] float flyTime = 0.9f;
    [SerializeField] float jumpForce = 100f;
    float flyForce = 20.0f;
    float flyHorizontalForce = 10.0f;
    float maxFlyHorizontalForce = 2f;
    float walkForce = 10.0f;
    float maxFlySpeed = 2f;
    float maxWalkSpeed = 2f;
    float flyTemp = 0.0f;
    float speed;
    int pastKey = 0;
    int rkey;
    bool flightTurn = false;
    bool spaceCheck = true;
    [SerializeField] private float waterPushForce = 12f;
    [SerializeField] private float rainForce = 20f;

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
        if (IsGrounded())
        {
            animator.SetBool("IsFlying", false);
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && IsGrounded())
        {
        //    animator.SetBool("IsTilting", false);
            animator.SetBool("IsWalking", true);
        }
       // else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
       // {
        //    animator.SetBool("IsTilting", true);
        //}
        //else
        //{
       //     animator.SetBool("IsTilting", false);
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move left and right
        // Includes my attempt to give the funny bee movement to the bee
        int key = 0;
        if (IsGrounded())
        {
            flightTurn = false;
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            if (pastKey == -1 && !IsGrounded())
            {
                flightTurn = true;
            }

            if (pastKey == -1 && IsGrounded() && !Input.GetKey(KeyCode.A))
            {
                CreateDust(1);
                pastKey = 0;
            }
            key = 1;
            rkey = 1;
            pastKey = 1;
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {

            if (pastKey == 1 && !IsGrounded())
            {
                flightTurn = true;
            }

            if (pastKey == 1 && IsGrounded() && !Input.GetKey(KeyCode.D))
            {
                CreateDust(-1);
                pastKey = 0;
            }
            rkey = -1;
            key = -1;
            pastKey = -1;
        }

        // decide PC's speed
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        float speedy = Mathf.Abs(this.rigid2D.velocity.y);

        if (Input.GetKey(KeyCode.Space) && IsGrounded() && spaceCheck)
        {
            //Initial fly up
            this.rigid2D.AddForce(transform.up * jumpForce); // Initial force applied to the jump
            flyGauge.Show();
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsFlying", true);
            spaceCheck = false;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            spaceCheck = true;
        }

        //Debug.Log(spaceCheck);

        //Allows flight for the during of the flightTimer, we can be adjusted as the player progresses
        if (flyTemp > 0 && (!IsGrounded() || this.rigid2D.velocity.y !=0)) //Makes sure the player isn't cheating by flying under a platform
        {
            flyGauge.Show();
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsFlying", true);
                flyTemp -= Time.deltaTime;
                flyGauge.SetGauge(flyTemp / flyTime);
                //Debug.Log(flyTemp);
                //Seems to be printing values when unnecessary, could be improved for performance

                if (Mathf.Sign(speedy) == -1)
                {
                    flyForce = 50;
                }
                else
                {
                    flyForce = 20;
                }

                //Debug.Log(this.rigid2D.velocity.y);
                if (this.rigid2D.velocity.y > maxFlySpeed)
                {
                    flyForce = flyForce / 1.5f;
                }
                else if (this.rigid2D.velocity.y <= maxFlySpeed)
                {
                    flyForce = 20f;
                }



                this.rigid2D.AddForce(transform.up * flyForce);
            }

            // Give enough force to push the other way if necessary
            if (flightTurn == true)
            {
                this.rigid2D.AddForce(transform.right * key * 50);
                flightTurn = false;
                Debug.Log("boost triggered");
            }

            if (key == 0 && speedx != 0)
            {
                //speed = speed/2;
                //Debug.Log(speed);
                this.rigid2D.AddForce(transform.right * -rkey * speedx);
                //Debug.Log("Decreasing Speed");
            }
            else if (speedx >= this.maxFlyHorizontalForce)
            {
                speed = speed / 2;
                //Debug.Log(speed);
                this.rigid2D.AddForce(transform.right * key * speed);
                //Debug.Log("PP");
            }
            else
            {
                speed = speedx;

                this.rigid2D.AddForce(transform.right * key * this.flyHorizontalForce);
                //Debug.Log("Maintaining Speed");
            }

        }

        // Might cause issues later because it resets every frame on the ground
        if (IsGrounded() && !Input.GetKey(KeyCode.Space) && flyTemp < flyTime)
        {
            flyTemp += (2 * Time.deltaTime);
            flyGauge.SetGauge(flyTemp / flyTime);
            if (flyTemp == flyTime)
            {
                flyGauge.Hide();
            }
        }
        else if (flyTemp > flyTime)
        {
            flyTemp = flyTime;
            flyGauge.Hide();
        }
        else if (flyTemp == flyTime)
        {
            flyGauge.Hide();
        }

        //Debug.Log(speedy);

        // limit PC's speed and avoid acceleration.
        if ((speedx < this.maxWalkSpeed) && IsGrounded())
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // reverse spring according to the direction
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        //if (this.rigid2D.velocity.x != 0 && this.rigid2D.velocity.y != 0)
        //{
        //    this.animator.speed = speedx / 2.0f;
        //}
        //else
        //{
        //    this.animator.speed = 1;
        //}
        //Debug.Log(speedx);

        // Can set colliders here
        if (IsGrounded() && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            //Debug.Log("grasswalk");
            if (!grassWalk.isPlaying)
            {
                grassWalk.Play();
            }
        }
        else
        {
            grassWalk.Stop();
        }

        if (!IsGrounded())
        {
            //Debug.Log("buzzsound");
            if (!flySound.isPlaying)
            {
                flySound.Play();
            }
        }
        else
        {
            flySound.Stop();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            rigid2D.AddForce(new Vector2(waterPushForce, -4.0f), ForceMode2D.Force);
            print("Touching water");
        }

        if (other.tag == "Rain")
        {
            flyTemp -= Time.deltaTime;
            flyGauge.SetGauge(flyTemp / flyTime);
            //rigid2D.AddForce(new Vector2(rainForce, -0.5f), ForceMode2D.Force);
            print("Touching rain");
        }
    }
    void CreateDust(int key)
    {
        // Trying to flip the dust particles but it is not working
        //dust.transform.localScale = new Vector3(key, 1, 1);
        if (key == 1)
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
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightTest), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightTest), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);
        //Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

    public bool IsMoving()
    {
        return animator.GetBool("IsWalking");
    }
}
