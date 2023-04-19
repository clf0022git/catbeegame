using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Range : MonoBehaviour
{
    public GameObject buttonPrompt;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("NPC Interaction enabled");
            transform.parent.gameObject.GetComponent<NPC>().inRange = true;
            buttonPrompt.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("NPC Interaction disabled");
            transform.parent.gameObject.GetComponent<NPC>().inRange = false;
            buttonPrompt.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
