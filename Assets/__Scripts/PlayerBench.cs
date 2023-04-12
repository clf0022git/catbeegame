using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBench : MonoBehaviour
{
    float timeToSleep = 2;
    float sleepTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        sleepTimer = sleepTimer + Time.deltaTime;
        if (sleepTimer >= timeToSleep)
        {
            GetComponent<Animator>().SetBool("IsSleeping", true);
        }
    }

    public void getUp()
    {
        GetComponent<Animator>().SetBool("IsSleeping", false);
        Debug.Log("Get up");
        sleepTimer = 0;
    }
}
