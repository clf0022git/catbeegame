using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endGameScript : MonoBehaviour
{

    public Button yesButton, noButton;
    public Text packageText;
    GameObject CatPillar;
    bool firstDeliver, secondDeliver;
    int fpackage = 1, spackage = 1;

    private void Update()
    {
        if (firstDeliver && secondDeliver)
        {
            if (GameObject.Find("CatPillar2(Clone)") != null)
            {
                CatPillar = GameObject.Find("CatPillar2(Clone)");
                CatPillar.GetComponent<NPC>().checkQuest = true;
                firstDeliver = false;
                secondDeliver = false;
            }
        }
    }
    public void unHide()
    {
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    public void unHideDelivered()
    {
        packageText.gameObject.SetActive(true);
    }

   // public void delivery(float packages)
    //{
    //    packageText.text = "Delivered: " + packages;
   // }

    public void firstdelivery(int packages)
    {
        firstDeliver = true;
        if (packages == 2)
        {
            packageText.text = "Delivered: 1";
        } 
        else if (packages == 4)
        {
            packageText.text = "Delivered: 2";
        }
    }

    public void seconddelivery(int packages)
    {
        secondDeliver = true;
        if (packages == 2)
        {
            packageText.text = "Delivered: 1";
        }
        else if (packages == 4)
        {
            packageText.text = "Delivered: 2";
        }
    }

    public void onClickYesButton()
    {
        SceneManager.LoadScene("_Game_Over");
    }

    public void onClickNoButton()
    {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }
}
