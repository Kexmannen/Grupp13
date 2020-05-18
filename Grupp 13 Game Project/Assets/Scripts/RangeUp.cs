using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUp : PickUp
{
    public float increasedBy = 0f;
    [SerializeField] //visar följande variabel i editorn, har den för att variabeln änså inte används i ett annat script.
    private float powerUpDuration = 0f;
    private bool isActivated = false;
    public override void OnTriggerEnter2D(Collider2D other)
        
    {
        if (other.tag == "Player" && !isActivated) //om du krockar med playern och powerUpen inte har aktiverats 
        {
            isActivated = true; //Du flaggar att powerUpen har aktiverats. 
            gameObject.GetComponent<SpriteRenderer>().enabled = false; //stänger av spriterenderen 
            //StartCoroutine(givePowerUp(other.gameObject, powerUpDuration)); //Tar in spelarens gameobject och hur länge poweupen ska gälla.
            other.GetComponent<PlayerController>().GiveRangeUp(powerUpDuration, increasedBy);
           
        }
    }

    //private IEnumerator givePowerUp(GameObject player, float duration)
    //{
    //    Debug.Log("player picked up RangeUp with duration: " + duration + "sec");
    //    player.GetComponent<PlayerController>().rangeTime += increasedBy; //Ökar hur länge skotten existerar 
    //    yield return new WaitForSeconds(duration); //vänta i x-antal sec
    //    Debug.Log("Effect of rangeUp has worn off");
    //    player.GetComponent<PlayerController>().rangeTime -= increasedBy; //sänk tiden för hur länge skotten existerar
    //    Destroy(gameObject);
    //}
    private void OnDestroy()
    {
        Debug.Log("förstörd");
    }
}
