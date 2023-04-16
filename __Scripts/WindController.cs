using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public float windStrength;
    public Vector3 windDirection;
    // set the windZone of the player to false, so when entering it can be triggered
    public bool inWindZone = false;
    public GameObject windZone;
    public Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Can set colliders here

        // The following function detects for windzone -jv
        // If the gameObject is tagged as a wind area
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You are entering the wind zone.");

            windZone.gameObject.SetActive(true);
            //if (rb != null)
            //{
            //    rb.AddForce(transform.right * windStrength, (ForceMode2D)ForceMode.Force);
           
            //}
            //// you have entered a windzone, set windzone to true
            //windZone = other.gameObject;
            //inWindZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            windZone.gameObject.SetActive(false);
            Debug.Log("You are leaving the wind zone.");
        }
        
    }


    public void FixedUpdate()
    {
        if (inWindZone)
        {
            // Add force to the rigid body by calculating wind direction * wind strength
            rb.AddForce(windZone.GetComponent<WindController>().windDirection
                * windZone.GetComponent<WindController>().windStrength);
        }
    }
}
