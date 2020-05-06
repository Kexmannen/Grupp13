using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //public static GameController gameController;  - temporarily disabled - this should be used in future versions
    public GameObject obstacle; //tror ej vi behöver dessa längre då vi inte sköter spawning i det här scriptet längre
    public GameObject hazard;   //tror ej vi behöver dessa längre då vi inte sköter spawning i det här scriptet längre
    public GameObject player;

    public Text restartText;
    public Text gameOverText;
    public Text scoreText;
    public Image[] uiHealthpoints;
    public int score; //had to make this public because get-method didn't work? (ask about this at handledning later)
    public bool isGame = false;

   
    private bool gameIsOver;
    private bool restart;

    private void Awake()
    {
        //-temporarily disabled - this should be used in future versions
        //if (gameController == null)
        //    gameController = this;
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //DontDestroyOnLoad(gameController);

    }
    void Start()
    {
        isGame = true; //temporary, in future versions this should be set to true when samplescene is loaded 
        gameIsOver = false;
        restart = false;
        restartText.text = "";   //använda setActive istället? - Ja
        gameOverText.text = ""; //använda setActive istället? - Ja
        score = 0;
        updateScore();
    }


    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
            }
        }
        if (gameIsOver)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }
    }


    public void addScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScore();
    }
    private void updateScore()
    {
       scoreText.text = "Score: " + score;
    }

    public void gameOver()
    {
        gameOverText.text = "Game Over";
        gameIsOver = true;
    }
    public void updateUiHealth(int currentHp)
    {
        foreach (var i in uiHealthpoints)  //for each variable i in uiHealthpoints array, if the index is lesser than currentHP, enable image. Otherwise, disable it
        {
            if (System.Array.IndexOf(uiHealthpoints, i) < currentHp)
            {
                i.enabled = true;
            }
            else
            {
                i.enabled = false;
            }
        }
    }

    //public int getScore()
    //{
    //    return score;
    //}
} 
