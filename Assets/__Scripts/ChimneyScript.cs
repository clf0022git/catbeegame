using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimneyScript : MonoBehaviour
{
    [SerializeField] private GameObject insideHouse;
    [SerializeField] private GameObject key;
    private GameObject player;

    private void Start()
    {
        insideHouse.SetActive(false);
        player = GameObject.Find("Player");
    }

    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            insideHouse.SetActive(true);
            key.transform.parent = null;
            Debug.Log("Player Moved.");
            player.transform.position = new Vector3(64.7f, 1.8f, 0f);
        }
    }
}