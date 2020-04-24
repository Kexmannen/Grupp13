using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float nextFire;

    public float fireRate;
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) //i tutorialen använde han Time.time, borde det inte vara Time.Deltatime?
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
           // audio.Play(); //shooting audio
        }
        

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rigidbody.velocity = movement * speed;
        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 0.0f, 0.0f);
        //rigidbody.rotation = Quaternion.Euler(0.0f, rigidbody.velocity.x * -tilt, 0.0f); (Tilta skeppet lite åt sidorna vid rörelse. Fixa senare, detta funkar ej pga vi har 2D)

    }


}
