using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerReact : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerParent;
    private Vector2 flowerOriginalPos;
    private void Start()
    {
        flowerOriginalPos = PlayerParent.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject == Player)
        {
            Player.transform.SetParent(PlayerParent.transform);
            moveFlower();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject == Player)
        {
            fixFlower();
            Player.transform.SetParent(null);
        }
    }

    private void moveFlower()
    {
        float y = PlayerParent.transform.position.y - 0.1f;
        PlayerParent.transform.position = new Vector2(PlayerParent.transform.position.x, y);
    }

    private void fixFlower()
    {
        PlayerParent.transform.position = flowerOriginalPos;
    }
}
