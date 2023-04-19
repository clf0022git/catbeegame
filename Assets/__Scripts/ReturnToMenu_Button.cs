using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu_Button : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene("_Main_Menu");
    }
}
