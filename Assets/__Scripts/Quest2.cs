using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : MonoBehaviour
{

    private static int flowerCount;
    private static bool Quest2Complete;
    private bool IsWatered;
    private GameObject flower;
    private GameObject CatAnt;
    private Animator animator;

    bool finishedFlowers = false;
    

    // Start is called before the first frame update
    void Start()
    {
        flowerCount = 0;
        animator = this.GetComponent<Animator>();
        Quest2Complete = false;
        //Debug.Log("Quest Start");
        IsWatered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flowerCount == 12 && finishedFlowers == false)
        {
            if (GameObject.Find("CatAnt2(Clone)") != null)
            {
                CatAnt= GameObject.Find("CatAnt2(Clone)");
            }
            CatAnt.GetComponent<NPC>().checkQuest = true;
            //Debug.Log("Complete");
            flowerCount = 0;
            finishedFlowers = true;
        }
       // Debug.Log(flowerCount);
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Watering")
        {
            animator.SetBool("IsWatered", true);
            //Debug.Log("I'm being watered");
        }
    }

    public void flowerGrown()
    {
        flowerCount++;
    }
}
