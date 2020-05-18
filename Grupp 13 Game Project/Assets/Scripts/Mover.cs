using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    private float maxSpeed = 0;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * speed;
        maxSpeed = rigidbody.velocity.magnitude; // Saves the inital speed as the max value
    }

    private void Update()
    {
        if(rigidbody.velocity.y > speed) //if y-velocity is positive, its moving upwards
        {
            rigidbody.AddForce( transform.up * speed * 0.2f); //add downwards force

            if(rigidbody.velocity.y > maxSpeed) //safety-check, if y-speed is greater than maxSpeed, limit speed to maxSpeed
            {
                rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed); // Clamps/limits the length of rigidbody.velocyty to the value of maxSpeed
            }
        }
    }

    public void StopMoving()
    {
        speed = 0f;
        rigidbody.velocity = new Vector2(0, 0); //Nollställer velocity
        rigidbody.isKinematic = true; //gör så att den inte kan krocka med något
        rigidbody.Sleep(); //Sålänge inget ger den en ny fart, kommer den stanna i minst 1 frame. 
   }
}

//Idéer för när fiendeobjekten kolliderar - vid kollission, addera kraft! så att du studsar
//multiplicera objektets nuvarande kraft med tex 2
//kolla även ProjectOnPlane och Reflect (unity documentation - vectors)
//Man kan även kolla på physics materials
//prova mass och physics material