using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;
    public int best;
    public int score;
    public float time = 60;
    public int displayTime = 60;
    public int currentStage = 0;
    public GameObject leaderboardMenu;

    Scene currentScene;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        // Load the saved highscore
        best = PlayerPrefs.GetInt("Highscore");

         currentScene = SceneManager.GetActiveScene();
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        FindObjectOfType<BallController>().PlayPowerup();
    }

    private void Update()
    {
        if (currentScene.name == "3" ) 
            { if (time > 0)
            { time -= Time.deltaTime; }
            displayTime = Mathf.FloorToInt(time);
            if(time< 1)
            {
                Time.timeScale = 0;
                FindObjectOfType<PlayfabManager>().GetLeaderboard();
                leaderboardMenu.SetActive(true);
                Camera.main.GetComponent<AudioSource>().Stop();
            }
        }
        
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > best)
        {
            PlayerPrefs.SetInt("Highscore", score);
            best = score;
        }
    }


    public void RestartLevel()
    {
        FindObjectOfType<PlayfabManager>().SendLeaderboard(score);
        Debug.Log("Restarting Level");
        // Show Adds Advertisement.Show();
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);

        
    }

    
}
