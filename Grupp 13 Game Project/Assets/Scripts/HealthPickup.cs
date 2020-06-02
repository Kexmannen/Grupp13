using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickUp
{

    public GameObject healthParticles;
    public float destroyParticles;

    private int healthAdd = 1;
    private GameObject instantiatedObject;

    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            PlayerController playercontroller = other.gameObject.GetComponent<PlayerController>();

            instantiatedObject = Instantiate(healthParticles, transform.position, transform.rotation);

            playercontroller.updateHealthpoints(healthAdd);

            AudioManager.instance.Play("HealthUp");

            Destroy(instantiatedObject, destroyParticles);
            Destroy(gameObject);
        }
    }
}
