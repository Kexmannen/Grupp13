using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        //Instansiera explosion "på" hazard
        Destroy(other.gameObject);
        Destroy(gameObject);
    } //if other comparetag == player {instansiera explosion "på" player} se Boundaries, Hazards and Enemies > 3. Explosions
}

