using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D other);
    
}

//För idéer om hur vapen-pickups kan fungera, se "extending" tut-video 1h56 och framåt