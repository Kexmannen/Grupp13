using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
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
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
        

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rigidbody.velocity = movement * speed;
        rigidbody.position = new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
                0.0f
            );
        //rigidbody.rotation = Quaternion.Euler(0.0f, rigidbody.velocity.x * -tilt, 0.0f); (Tilta skeppet lite åt sidorna vid rörelse. Fixa senare, detta funkar ej pga vi har 2D)

    }


}
