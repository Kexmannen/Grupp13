using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUp : PickUp
{
    public float increasedBy = 0f;
    public GameObject rangeParticles;
    public float destroyParticles;

    [SerializeField] //visar följande variabel i editorn, har den för att variabeln änså inte används i ett annat script.
    private float powerUpDuration = 0f;
    private bool isActivated = false;
    private GameObject instantiatedObject;
    public override void OnTriggerEnter2D(Collider2D other)
        
    {
        if (other.tag == "Player" && !isActivated) //om du krockar med playern och powerUpen inte har aktiverats 
        {
            isActivated = true; //Du flaggar att powerUpen har aktiverats. 


            foreach (Transform child in transform)
                child.gameObject.GetComponent<SpriteRenderer>().enabled = false;//stänger av spriterenderen på objektets children

            gameObject.GetComponent<Mover>().StopMoving(); 
            StartCoroutine(givePowerUp(other.gameObject, powerUpDuration)); //Tar in spelarens gameobject och hur länge poweupen ska gälla.

            instantiatedObject = Instantiate(rangeParticles, transform.position, transform.rotation);

            AudioManager.instance.Play("RangeUp");
        }
    }

    private IEnumerator givePowerUp(GameObject player, float duration)
    {
        Debug.Log("player picked up RangeUp with duration: " + duration + "sec");
        player.GetComponent<PlayerController>().rangeTime += increasedBy; //Ökar hur länge skotten existerar 
        yield return new WaitForSeconds(duration); //vänta i x-antal sec
        player.GetComponent<PlayerController>().rangeTime -= increasedBy; //sänk tiden för hur länge skotten existerar
        Debug.Log("Effect of rangeUp has worn off");
        Destroy(instantiatedObject, destroyParticles);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Debug.Log("förstörd");
    }
}
