using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject musicPlayer;

    public void OnClickStart()
    {
        //if (musicPlayer != null)
        //{
        //    musicPlayer.GetComponent<AudioSource>().Stop();
        //}
        //Destroy(musicPlayer);
        SceneManager.LoadScene("_Intro");
    }

    public void OnClickOptions()
    {
        SceneManager.LoadScene("_Options");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
