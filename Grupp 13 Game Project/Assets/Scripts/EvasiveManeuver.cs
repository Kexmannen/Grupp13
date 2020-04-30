using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public Boundary boundary;
    public Vector2 startWait;
    public Vector2 maneuverWait;
    public Vector2 maneuverTime;
    public float dodge;
    public float smoothing;
    //public float tilt;

    private float currentSpeed;
    private float maneuverTarget;
    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentSpeed = rigidbody.velocity.y;
        StartCoroutine (evade());
    }

    IEnumerator evade()
    {

        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            maneuverTarget = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x); //ship will always dodge inward, prevents from dodging out of screen
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            maneuverTarget = 0f;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }


    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rigidbody.velocity.x, maneuverTarget, Time.deltaTime * smoothing);
        rigidbody.velocity = new Vector2(newManeuver, currentSpeed);
        rigidbody.position = new Vector2(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax));
        //rigidbody.rotation = Quaternion.Euler(0.0f, rigidbody.velocity.x * -tilt, 0.0f);
    }
}
//To get the enemy ship to move towards the player: see "expanding.." tut-video at 1h45min
