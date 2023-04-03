using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandFlower : MonoBehaviour
{
    public GameObject Player;
    public GameObject Flower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

}
