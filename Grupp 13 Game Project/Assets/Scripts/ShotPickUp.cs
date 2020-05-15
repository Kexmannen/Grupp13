using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPickUp : PickUp
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().shotLevel <= other.GetComponent<PlayerController>().shotSpawns.Length - 3)
            {
                other.GetComponent<PlayerController>().shotLevel += 2;
            }
            Destroy(gameObject);
        }

    }
}
