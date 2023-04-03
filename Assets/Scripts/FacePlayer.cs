using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public GameObject NPC;

    void OnTriggerEnter2D(Collider2D collision)
    {
        NPC.GetComponent<SpriteRenderer>().flipX = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        NPC.GetComponent<SpriteRenderer>().flipX = true;
    }
}
