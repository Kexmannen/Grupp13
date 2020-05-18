﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPickUp : PickUp
{
    public int increasedBy = 0;
    [SerializeField] //visar följande variabel i editorn, har den för att variabeln änså inte används i ett annat script.
    private float powerUpDuration = 0f;
    private bool isActivated = false;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isActivated) //om du krockar med playern och powerUpen inte har aktiverats 
        {
            isActivated = true; //Du flaggar att powerUpen har aktiverats. 
            gameObject.GetComponent<SpriteRenderer>().enabled = false; //stänger av spriterenderen 
            gameObject.GetComponent<Mover>().StopMoving();

            if (other.GetComponent<PlayerController>().shotLevel <= other.GetComponent<PlayerController>().shotSpawns.Length - (increasedBy + 1))
            {
                StartCoroutine(givePowerUp(other.gameObject, powerUpDuration)); //Tar in spelarens gameobject och hur länge powerupen ska gälla.
            }
        }
    }
    private IEnumerator givePowerUp(GameObject player, float duration)
    {
        Debug.Log("player picked up ShotUp with duration: " + duration + "sec");
        player.GetComponent<PlayerController>().shotLevel += increasedBy; //Ökar lvl för skotten
        yield return new WaitForSeconds(duration); //vänta i x-antal sec
        player.GetComponent<PlayerController>().shotLevel -= increasedBy; //Sänker lvl för skotten
        Debug.Log("Effect of ShotUp has worn off");
        Destroy(gameObject);
    }
}
