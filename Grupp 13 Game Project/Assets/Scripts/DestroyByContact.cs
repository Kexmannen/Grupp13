using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
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
        if (other.tag == "Boundary")
        {
            return;
        }
        if(other.tag == "Pickup") //This might not be needed depending on which solution we go for on Pickups, see comments in Pickup script
        {
            return;
        }
        //Add later: Instantiate explosion "on" hazard
        if(other.tag == "Player")
        {
            //Add later: instantiate explosion "on" player, see Boundaries, Hazards and Enemies > 3. Explosions
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.updateHealthpoints(dmgValue); 
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

//angående kollision mellan obstacles och hazards - prova mass och physics material
//rullande bakgrund - använd spawner och sätt bakgrunden och lagrenas hastighet till (minus)spelarens hastighet
//använd boundary till att lägga tillbaka de "förstörda" objekten i en ny lista och spawna samma objekt hela tiden (se länk om object pooling)
//spawna pickups- spawna random från en array (random.Range)