using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class FlyGauge : MonoBehaviour
{
    public Image image;
    private float fillNumber = 1;

    public void SetGauge(float value)
    {
        image.fillAmount = value;
        fillNumber = value;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (fillNumber < 0.25)
        {
            image.color = Color.red;
        } else if (fillNumber < 0.5)
        {
            image.color = Color.yellow;
        }else
        {
            image.color = Color.green;
        }
    }
}
