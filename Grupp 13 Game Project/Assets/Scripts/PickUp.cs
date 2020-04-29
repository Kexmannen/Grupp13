using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    private int healthAdd = 1;
    public abstract void OnTriggerEnter2D(Collider2D other);
    
}
