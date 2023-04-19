using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    public GameObject Rain;
    private bool active = true;
    public float minRange = 1.0f;
    public float maxRange = 5.0f;
    public float offset = 0.0f;
    public float amplitude = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("RainToggle", Random.Range(minRange, maxRange));
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position += transform.up * Mathf.Sin(Time.time * 3.0f + offset) * amplitude;
    }

    private void RainToggle()
    {
        Rain.SetActive(active);
        active = !active;
        Invoke("RainToggle", Random.Range(minRange, maxRange));
    }

}
