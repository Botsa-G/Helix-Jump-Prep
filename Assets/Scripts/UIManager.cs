using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtBest;
    [SerializeField] private Text txtTimer;
    [SerializeField] private Text scoreOne;
    [SerializeField] private Text scoreTwo;
    [SerializeField] private Text scoreThree;

    Scene current;
    private void Start()
    {
         current = SceneManager.GetActiveScene();
    }
    void Update()
    {
        txtBest.text = "Best: " + GameManager.singleton.best;
        txtScore.text = "Score: " + GameManager.singleton.score;

        if (current.name == "3")
        {
            txtTimer.text = "Time: " + GameManager.singleton.displayTime;
            scoreOne.text = (FindObjectOfType<PlayfabManager>().firstScore).ToString();
            scoreTwo.text = (FindObjectOfType<PlayfabManager>().secondScore).ToString();
            scoreThree.text = (FindObjectOfType<PlayfabManager>().thirdScore).ToString();
        }
        
    }
}
