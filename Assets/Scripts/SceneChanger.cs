using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
   

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        if (Time.timeScale != 1) { Time.timeScale = 1; }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
