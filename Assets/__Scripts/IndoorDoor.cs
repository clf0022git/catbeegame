using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorDoor : MonoBehaviour
{
    public GameObject insideHouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            insideHouse.SetActive(false);
        }
    }
}
