using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickUp
{
    private int healthAdd = 1;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController playercontroller = other.gameObject.GetComponent<PlayerController>();
            playercontroller.updateHealthpoints(healthAdd);
            Destroy(gameObject);
        }
    }
}
