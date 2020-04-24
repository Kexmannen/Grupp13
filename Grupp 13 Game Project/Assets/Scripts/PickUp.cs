using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") //Vid utökning och tillägg av flera pickups, kolla bara om taggen på "this" är healthpickup eller annan pickup osv
        {
            gameController.gainHealthpoint();
            Destroy(gameObject);
        }
    }
}
