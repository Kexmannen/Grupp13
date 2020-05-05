using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPickUp : PickUp
{
    public Transform[] shotSpawns;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().shotSpawns = shotSpawns;
            Destroy(gameObject);
        }
          
    }
}
