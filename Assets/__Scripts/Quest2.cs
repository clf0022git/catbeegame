using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : MonoBehaviour
{

    private static int flowerCount;
    private static bool Quest2Complete;
    private bool IsWatered;
    private GameObject flower;
    private Animator animator;
    
    

    // Start is called before the first frame update
    void Start()
    {
        flowerCount = 0;
        animator = this.GetComponent<Animator>();
        Quest2Complete = false;
        Debug.Log("Quest Start");
        IsWatered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(flowerCount >= 9)
        {
            Quest2Complete = true;
            Debug.Log("Complete");
        }
        Debug.Log(flowerCount);
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Watering")
        {
            animator.SetBool("IsWatered", true);
            Debug.Log("I'm being watered");
        }
    }

    void flowerGrown()
    {
        flowerCount++;
    }
}
