using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int scoreValue;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        //Instansiera explosion "på" hazard
        if(other.tag == "Player")
        {
            //instansiera explosion "på" player, se Boundaries, Hazards and Enemies > 3. Explosions
            gameController.loseHealthpoint();
            Destroy(gameObject);
        }
        if(tag == "Hazard")
        {
            gameController.addScore(scoreValue);
            Destroy(gameObject);
        }
        if(tag == "Obstacle" && other.tag == "Bolts")
        {
            Destroy(other.gameObject);
        }

    } 
}

//fråga angående kollision mellan obstacles och hazards
//fråga om canvas (overlay vs camera)