using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchSit : MonoBehaviour
{
    [SerializeField]
    GameObject InteractIcon;
    [SerializeField]
    GameObject PlayerSit;
    Collider2D playerCollider;
    float timer = 0f;
    float timeEnd = 0.05f;
    float waitTimer = 0f;
    float waitTimeEnd = 3f;

    private void Start()
    {
        PlayerSit.SetActive(false);
        InteractIcon.SetActive(false);
    }

    private void Update()
    {
        if (InteractIcon.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            playerCollider.gameObject.SetActive(false);
            PlayerSit.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.E) && PlayerSit.activeSelf == true)
        {
            PlayerSit.SetActive(false);
            playerCollider.gameObject.SetActive(true);
        }

        if (InteractIcon.activeSelf == true && waitTimer >= waitTimeEnd)
        {
            if (timer <= timeEnd)
            {
                timer = timer + Time.deltaTime;
            }
            else if (InteractIcon.GetComponent<SpriteRenderer>().color.a >= 0)
            {
                InteractIcon.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, InteractIcon.GetComponent<SpriteRenderer>().color.a - 0.05f);
                timer = 0f;
            }
        }
        else
        {
            waitTimer = waitTimer + Time.deltaTime;
        }
        Debug.Log(timer);
        Debug.Log(InteractIcon.GetComponent<SpriteRenderer>().color.a);
        Debug.Log(InteractIcon.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractIcon.SetActive(true);
        playerCollider = collision;
        waitTimer = 0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractIcon.SetActive(false);
        timer = 0f;
        InteractIcon.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5373f);
    }
}
