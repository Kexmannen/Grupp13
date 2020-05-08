using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}

//använd boundary till att lägga tillbaka de "förstörda" objekten i en ny lista och spawna samma objekt hela tiden (se länk om object pooling)