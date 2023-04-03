using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject buttonPrompt;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && player != null)
        {
            if(player.GetComponent<PlayerMovement>().canMove)
            {
                player.GetComponent<PlayerMovement>().canMove = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("NPC Interaction enabled");
        player = collision.gameObject;
        buttonPrompt.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("NPC Interaction disabled");
        buttonPrompt.GetComponent<SpriteRenderer>().enabled = false;
        player = collision.gameObject;
        if (Input.GetKey(KeyCode.Space))
        {
            player.GetComponent<PlayerMovement>().canMove = true;
        }
    }
}