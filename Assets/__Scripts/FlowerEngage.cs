using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEngage : MonoBehaviour
{

    private int flowerCount;
    private GameObject flower;
    private Animator animator;
    
    

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Watering")
        {
            animator.SetBool("IsWatered", true);
        }
    }

    void flowerGrown()
    {
        
    }
}
