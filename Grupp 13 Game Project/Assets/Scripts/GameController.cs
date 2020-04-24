using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject hazard;
    public GameObject player;
    public Vector3 spawnValues;
    public int hazardCount;
    public int obstacleCount;
    public float startWait;
    public float hazardSpawnWait;
    public float hazardWaveWait;
    public float obstacleSpawnWait;
    public float obstacleWaveWait;
    

    public Text restartText;
    public Text gameOverText;
    public Text scoreText;
    public Image firstHealthPoint;
    public Image secondHealthPoint;
    public Image thirdHealthPoint;

    private int score;
    private int healthCount;
    private bool gameIsOver;
    private bool restart;

    

    void Start()
    {
        healthCount = 3;
        gameIsOver = false;
        restart = false;
        restartText.text = "";   //använda setActive istället?
        gameOverText.text = ""; //använda setActive istället?
        score = 0;
        updateScore();
        StartCoroutine( spawnHazards());
        StartCoroutine(spawnObstacles());
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
    }

    private IEnumerator spawnHazards()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {

                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(hazardSpawnWait);
            }
            yield return new WaitForSeconds(hazardWaveWait);
            if (gameIsOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    private IEnumerator spawnObstacles() //kodupprepning, fråga på handledning om det går att göra på nåt bättre sätt (egen spawn-klass?)
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(obstacle, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(obstacleSpawnWait);
            }
            yield return new WaitForSeconds(obstacleWaveWait);
            if (gameIsOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
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

    private void gameOver()
    {
        gameOverText.text = "Game Over";
        gameIsOver = true;
    }

    public void loseHealthpoint() //fråga på handledning om det finns en "snyggare"/bättre lösning på detta
    {
        if(healthCount >= 3)
        {
            healthCount--;
            firstHealthPoint.enabled = false;
        }else if(healthCount == 2)
        {
            healthCount--;
            secondHealthPoint.enabled = false;
        }else 
        {
            thirdHealthPoint.enabled = false;
            Destroy(player);
            gameOver();
        } 
    }

    public void gainHealthpoint()
    {
        if (healthCount >= 3)
        {
            return;
        }else if (healthCount == 2)
        {
            healthCount++;
            firstHealthPoint.enabled = true;
        }
        else
        {
            healthCount++;
            secondHealthPoint.enabled = true;
        }
    }
}
