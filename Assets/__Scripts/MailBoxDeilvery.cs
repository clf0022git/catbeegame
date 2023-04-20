using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MailBoxDeilvery : MonoBehaviour
{
    [SerializeField] static public int packages = 0;
    private GameObject grabCollider;
    private GameObject CatPillar;
    static public bool deliverOnce = false;
    static public bool firstDeliver = false;
    static public bool secondDeliver = false;

    private void Start()
    {
        grabCollider = GameObject.Find("GrabCollider");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking for tag for each MailBox
        // MailBox A belongs to CatAnt
        // MailBox B belongs to CatLadyBug - she gets two packages cause amazon shopper

        if(collision.gameObject.CompareTag("MailBoxA") && this.name == "Package1")
        {
            grabCollider.GetComponent<PlayerGrabbing>().HoldingObject = false;
            grabCollider.GetComponent<PlayerGrabbing>().Grabbable = false;
            packages = packages + 1;
            firstDeliver = true;
            Debug.Log(packages);
            GameObject.Find("EndGame").GetComponent<endGameScript>().firstdelivery(packages);
            Destroy(this.gameObject);
        } 
        else if (collision.gameObject.CompareTag("MailBoxB") && (this.name == "Package2" || this.name == "Package3"))
        {
            grabCollider.GetComponent<PlayerGrabbing>().HoldingObject = false;
            grabCollider.GetComponent<PlayerGrabbing>().Grabbable = false;
            packages = packages + 1;
            GameObject.Find("EndGame").GetComponent<endGameScript>().seconddelivery(packages);
            secondDeliver = true;
            Debug.Log(packages);
            Destroy(this.gameObject);
        }
        
    }
}
