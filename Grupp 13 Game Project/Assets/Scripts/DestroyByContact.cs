using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //public GameObject explosion;
    //public GameObject playerExplosion;
    public int scoreValue;
    public int dmgValue; 

    private GameController gameController;
    private PlayerController playerController;
  

    void Start()
    {
        dmgValue = -1; 
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
        if (other.CompareTag("Boundary") || other.CompareTag("Hazard") || other.CompareTag("Obstacle")) //Can't this just be solved with layer collission ignores (?) (if the object collided with is either boundary, hazard or obstacle, ignore it
        {
            return;
        }
        if(other.CompareTag ("Pickup")) //This might not be needed depending on which solution we go for on Pickups, see comments in Pickup script. If we do choose to go with this solution, then add it to the above statement instead.
        {
            return;
        }
        //if (explosion != null)
        //{
        //    Add later: Instantiate explosion "on" hazard
        //}

        if (other.CompareTag("Player")) //if the object collided with is Player, update hp and destroy the gameobject of "this" (not the player)
        {
            //Add later: instantiate explosion "on" player, see Boundaries, Hazards and Enemies > 3. Explosions
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.updateHealthpoints(dmgValue); 
            Destroy(gameObject);
        }
        if (CompareTag("Hazard") && other.CompareTag("Bolts")) //if the player shot hits a hazard, destroy the hazard and add score
        {
            gameController.addScore(scoreValue);
            Destroy(gameObject);
        }
        if (CompareTag("Obstacle") && other.CompareTag("Bolts")) //if the player shot hits an obstacle, destroy the player shot
        {
            Destroy(other.gameObject);
        }

    } 
}

