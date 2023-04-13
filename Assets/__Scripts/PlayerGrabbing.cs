using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerGrabbing : MonoBehaviour
{

    public GameObject grabPosition;
    private GameObject item;
    private GameObject player;
    private Rigidbody2D itemRigidBody;
    private SpriteRenderer itemSprite;
    private BoxCollider2D itemCollider;

    
    public bool HoldingObject = false;
    int key = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPos = player.transform.position;
        Vector3 itemPos;
      
        

       if (HoldingObject == true)
       {

        itemPos = playerPos;
        itemPos.y = itemPos.y - 0.25f;
        itemPos.x = itemPos.x + 0.5f;
        item.transform.position = itemPos;

        if (Input.GetKey(KeyCode.D))
        {
            key = 1;
           

        }
        else if (Input.GetKey(KeyCode.A))
        {
            key = -1;
           
        }
        else if (itemRigidBody.rotation < 0)
        { 
            itemRigidBody.rotation += 3.0f;
        }
        else if (itemRigidBody.rotation > 0)
        {
            itemRigidBody.rotation -= 3.0f;
        }
        
        // if 1 watering can is on right side, if 2 watering can is on left side
        if (key == 1)
        {
            item.transform.position = itemPos;
        }
        if (key == -1)
        {
            itemPos.x = itemPos.x - 1f;
            item.transform.position = itemPos;
        }


        // for flipping the watering can
        if (key != 0)
        {
            item.transform.localScale = new Vector3(key, 1, 1);
        }
        
           
        
        
        itemCollider.enabled = false;
        itemSprite.sortingOrder = 2; 
        
         Debug.Log("HOLDING");
         if(Input.GetKeyDown(KeyCode.E))
        {
         Debug.Log("LetGO!");
                    itemCollider.enabled = true;
                    itemSprite.sortingOrder = -1;
                    HoldingObject = false;
        }
         
       }

       
    }

    

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            Debug.Log("ITEM!");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            
            if(Input.GetKeyUp(KeyCode.E))
            {
                if(!HoldingObject)
                {
                Debug.Log("YOU PICKED IT UP");
                HoldingObject = true;
                itemRigidBody = other.gameObject.GetComponent<Rigidbody2D>();
                item = other.gameObject;
                itemSprite = other.gameObject.GetComponent<SpriteRenderer>();
                itemCollider = other.gameObject.GetComponent<BoxCollider2D>();

                }
            }
        }
    }
    
    
}
