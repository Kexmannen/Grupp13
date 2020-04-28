using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D other);
    
}
