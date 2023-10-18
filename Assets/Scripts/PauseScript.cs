using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Camera.main.GetComponent<AudioSource>().Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Camera.main.GetComponent<AudioSource>().UnPause(); 
    }
}
