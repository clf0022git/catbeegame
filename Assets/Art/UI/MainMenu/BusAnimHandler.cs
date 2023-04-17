using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusAnimHandler : MonoBehaviour
{
    private float busWaitTimer = 2;
    private float time;
    private bool timeStart = false;
    private Animator animator;
    public GameObject catBee;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart == true)
        {
            if (time <= busWaitTimer)
            {
                time += Time.deltaTime;
            } 
            else
            {
                animator.SetBool("IsWaitOver", true);
                timeStart = false;
            }
        }       
    }

    public void StartTimer()
    {
        catBee.transform.SetParent(null);
        catBee.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        timeStart = true;
    }

    public void changeScene()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
