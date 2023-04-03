using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    float cameraGroundPosition = -1.5f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, cameraGroundPosition , transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        // Keeps the camera down until the player flies too high
        if(this.player.transform.position.y > 2 || this.player.transform.position.y < -3.5)
        {
            Vector3 playerPos = this.player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
        else
        {
            Vector3 playerPos = this.player.transform.position;
            transform.position = new Vector3(playerPos.x, cameraGroundPosition, transform.position.z);
        }
    }
}
