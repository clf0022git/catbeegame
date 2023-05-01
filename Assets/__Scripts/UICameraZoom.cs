using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraZoom : MonoBehaviour
{
    public float startSize;
    private float interval;
    public float duration;
    public Camera camera;
    public bool stopZoom = false;
    public GameObject nextScreen;


    // Start is called before the first frame update
    void Start()
    {
        camera.orthographicSize = startSize;
        interval = (startSize - 3) / duration;
        Invoke("zoom", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void zoom()
    {
        if (!stopZoom)
        {
            camera.orthographicSize -= interval;
        }

        if (camera.orthographicSize <= 3)
        {
            stopZoom = true;
            nextScreen.SetActive(true);
        }

        Invoke("zoom", 0.05f);
    }
}
