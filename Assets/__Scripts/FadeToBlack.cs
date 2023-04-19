using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public float delay = 120.0f;
    public float speed = 1.0f;
    private Color color;
    private bool startFade = false;
    public GameObject nextScreen;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Image>().color;
        color.a = 0.0f;
        Invoke("setStart", delay);
    }

    // Update is called once per frame
    void Update()
    {
        if(startFade)
        {
            //color.a += (speed / 255.0f);
            GetComponent<Image>().color = color;
        }

        nextScreen.SetActive(true);

    }

    void setStart()
    {
        startFade = true;
        print("Fade started");
    }
}
