using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuest : MonoBehaviour
{
    public GameObject CatLadyBug2;
    public GameObject CatLadyBug3;
    public GameObject CatLadyBug4;

    public GameObject CatAnt2;
    public GameObject CatAnt3;
    public GameObject CatAnt4;

    public GameObject CatPillar2;
    public GameObject CatPillar3;
    public GameObject CatPillar4;

    private GameObject grabCollider;
    private GameObject firstWing, secondWing, thirdWing;
    private GameObject player;
    private void Start()
    {
        grabCollider = GameObject.Find("GrabCollider");
        firstWing = GameObject.Find("firstWing");
        secondWing = GameObject.Find("secondWing");
        thirdWing = GameObject.Find("thirdWing");
        player = GameObject.Find("Player");
    }
    public void CatQuest(string bug)
    {
        if (bug == "CatLadyBugStart")
        {
            Instantiate(CatLadyBug2, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (bug == "CatLadyBug2(Clone)Start")
        {
            Instantiate(CatLadyBug3, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (bug == "CatLadyBug3(Clone)Start")
        {
            firstWing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            player.GetComponent<PlayerMovement>().firstWing = true;
            Instantiate(CatLadyBug4, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (bug == "CatAntStart")
        {
            Instantiate(CatAnt2, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (bug == "CatAnt2(Clone)Start")
        {
            Instantiate(CatAnt3, transform.position, transform.rotation);
            Destroy(gameObject);
        } 
        else if (bug == "CatAnt3(Clone)Start")
        {
            secondWing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            player.GetComponent<PlayerMovement>().secondWing = true;
            Instantiate(CatAnt4, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (bug == "CatPillarStart")
        {
            Instantiate(CatPillar2, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (bug == "CatPillar2(Clone)Start")
        {
            Instantiate(CatPillar3, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (bug == "CatPillar3(Clone)Start")
        {
            thirdWing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            player.GetComponent<PlayerMovement>().thirdWing = true;
            Instantiate(CatPillar4, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void CheckQuest(string item)
    {
        if (item == "CatLadyBug2(Clone)Item")
        {
            if (grabCollider.GetComponent<PlayerGrabbing>().item != null && grabCollider.GetComponent<PlayerGrabbing>().item.name == "Key")
            {
                //Debug.Log("Value CHANGED.");
                this.transform.GetComponent<NPC>().checkQuest = true;
                Destroy(grabCollider.GetComponent<PlayerGrabbing>().item);
                grabCollider.GetComponent<PlayerGrabbing>().HoldingObject = false;
                grabCollider.GetComponent<PlayerGrabbing>().Grabbable = false;
            }
        } 
        else if (item == "CatAnt2(Clone)Item")
        {

        }
    }
}
