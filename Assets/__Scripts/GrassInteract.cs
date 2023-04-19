using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInteract : MonoBehaviour
{
    Animator animator;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() 
    {
       
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" && player.GetComponent<PlayerMovement>().IsMoving())
        {
            animator.speed = 1;
            //Debug.Log("Played");
        } else
        {
            animator.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            animator.speed = 0;
        }
    }
}
