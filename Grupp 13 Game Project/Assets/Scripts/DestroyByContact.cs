using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    //skapar referenser för varje typ av explotion i inspektorn och drar in prefaben för animationsobjektet, tex EnemyDeath osv
    public GameObject enemyExplosion;
    public GameObject playerCollisionExplosion;
    //Tiden innan explotionen förstörs, så om animationen har en läng på 1 sek så tilldelar jag det 1 sekund.
    public float destroyExplosionAfterFinished;
    //public GameObject playerExplosion;
    public int scoreValue;
    public int dmgValue;

    //denna kodrad behövs för att förstöra det instantierade klonobjektet
    private GameObject instantiatedObj;
    private GameObject otherInstantiatedObject;

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
        //if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Obstacle")) //Can't this just be solved with layer collission ignores (?) (if the object collided with is either boundary, hazard or obstacle, ignore it
        // {
        // return;
        //}

        //if (other.CompareTag("Pickup")) //This might not be needed depending on which solution we go for on Pickups, see comments in Pickup script. If we do choose to go with this solution, then add it to the above statement instead.
        //{
        //    return;
        //} //Kanske kommentera in senare om/vid behov.

        if (other.CompareTag("Player")) //if the object collided with is Player, update hp and destroy the gameobject of "this" (not the player)
        {
            //Add later: instantiate explosion "on" player, see Boundaries, Hazards and Enemies > 3. Explosions

            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.updateHealthpoints(dmgValue);
            instantiatedObj = (GameObject)Instantiate(playerCollisionExplosion, transform.position, transform.rotation); //Initierar en klon(i detta fall explotionen) på samma plats som pbjektet
            otherInstantiatedObject = (GameObject)Instantiate(enemyExplosion, transform.position, transform.rotation);
            AudioManager.instance.Play("Explosion_Mini");
            Destroy(gameObject);
            Destroy(instantiatedObj, destroyExplosionAfterFinished); //förstör klonen efter en viss tid
            Destroy(otherInstantiatedObject, destroyExplosionAfterFinished);

        }

        if (CompareTag("Enemy") && other.CompareTag("Bolts")) //if the player shot hits a hazard, destroy the hazard and add score
        {
            
            gameController.addScore(scoreValue);
            instantiatedObj = (GameObject) Instantiate(enemyExplosion, transform.position, transform.rotation); //Initierar en klon(i detta fall explotionen) på samma plats som pbjektet
            AudioManager.instance.Play("Enemy");
            Destroy(gameObject);//förstör enemyn objektet
            Destroy(instantiatedObj, destroyExplosionAfterFinished); //förstör klonen efter en viss tid
        }

        if (CompareTag("ShootingEnemy") && other.CompareTag("Bolts")) //if the player shot hits a hazard, destroy the hazard and add score
        {

            gameController.addScore(scoreValue);
            instantiatedObj = (GameObject)Instantiate(enemyExplosion, transform.position, transform.rotation); //Initierar en klon(i detta fall explotionen) på samma plats som pbjektet
            AudioManager.instance.Play("ShootingEnemy");
            Destroy(gameObject);//förstör enemyn objektet
            Destroy(instantiatedObj, destroyExplosionAfterFinished); //förstör klonen efter en viss tid
        }

        if (CompareTag("Obstacle") && other.CompareTag("Bolts")) //if the player shot hits an obstacle, destroy the player shot
        {
            Destroy(other.gameObject);
        }

    } 
}

