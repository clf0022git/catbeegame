using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerReact : MonoBehaviour
{
    public GameObject Player;
    public GameObject Flower;
    private Vector2 flowerOriginalPos;
    bool triggerFlower = true;

    private void Start()
    {
        flowerOriginalPos = Flower.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject == Player)
        {
            if (triggerFlower == true)
            {
                moveFlower();
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject == Player)
        {
            if(triggerFlower == true)
            {
                fixFlower();
            } else
            { triggerFlower = true; }
        }
    }

    private void moveFlower()
    {
        float y = Flower.transform.position.y - 0.1f;
        Flower.transform.position = new Vector2(Flower.transform.position.x, y);
    }

    private void fixFlower()
    {
        Flower.transform.position = flowerOriginalPos;
        triggerFlower = false;
    }
}
