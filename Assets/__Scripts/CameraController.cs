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
        if (this.player.transform.position.y > 0.5 && (this.player.transform.position.x < 0 && this.player.transform.position.x > -20))
        {
            Vector3 playerPos = this.player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
        else if (this.player.transform.position.x < -18.5f && this.player.transform.position.x > -55.5f)
        {
            Vector3 playerPos = this.player.transform.position;
            transform.position = new Vector3(playerPos.x, -3.0f, transform.position.z);
        }
        else if (this.player.transform.position.x < -75 || this.player.transform.position.y < -5)
        {
            Vector3 playerPos = this.player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
        else if (this.player.transform.position.y < -3.5 && this.player.transform.position.x > 25)
        {
            Vector3 playerPos = this.player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
        else if (this.player.transform.position.x > 52.5f)
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
