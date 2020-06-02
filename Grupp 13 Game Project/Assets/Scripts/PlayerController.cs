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
    private GameController gameController;
    private GameObject firedShot;
    private Camera cam;
    private int currentHealth;
    private float nextFire;
    //denna kodrad behövs för att förstöra det instantierade klonobjektet
    private GameObject instantiatedObj;
    private bool isRangedUp = false;
    bool isMoving = false;
    float deltaX;
    float deltaY;
    Vector2 pos;

    public float rangeTime = 0.5f;
    public float fireRate;
    public float speed;
    public float tilt;
    public int shotLevel = 0;
    public int maxHealth = 3;
    public Boundary boundary;
    public GameObject shot;
    public Transform[] shotSpawns;

    //skapar referenser för enexplotion i inspektorn och drar in prefaben för animationsobjektet
    public GameObject playerExplosion;
    //Tiden innan explotionen förstörs, så om animationen har en läng på 1 sek så tilldelar jag det 1 sekund.
    public float destroyExplosionAfterFinished;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        currentHealth = maxHealth;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }

    private void Update()
    {
        
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            for (int i = 0; i <= shotLevel; i++)
            {
                firedShot = Instantiate(shot, shotSpawns[i].position, shotSpawns[i].rotation);
                Destroy(firedShot, rangeTime);

            }
            AudioManager.instance.Play("Shot");
        }

        //Movement by mouse stuff
        pos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            StartMoving();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopMoving();
        }

        if (isMoving)
        {
            UpdateMovement();
        }
    }

    //Movement by mouse stuff

    void StartMoving()
    {
        isMoving = true;
        deltaX = pos.x - transform.position.x;
        deltaY = pos.y - transform.position.y;
    }
    void StopMoving()
    {
        isMoving = false;
    }
    void UpdateMovement()
    {
        
        rigidbody.MovePosition(new Vector2(Mathf.Clamp(pos.x - deltaX, boundary.xMin, boundary.xMax), Mathf.Clamp(pos.y-deltaY, boundary.yMin, boundary.yMax)));

        //probably not needed, remove later
        //rigidbody.position = cam.ScreenToWorldPoint(Input.mousePosition);
        //rigidbody.MovePosition(new Vector2(pos.x - deltaX, rigidbody.position.y));
    }

    //private void FixedUpdate()
    //{
    //    Regular PC keyboard - movement

    //    float moveHorizontal = Input.GetAxis("Horizontal");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
    //    rigidbody.velocity = movement * speed;
    //    rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 0.0f, 0.0f);
    //    rigidbody.rotation = Quaternion.Euler(0.0f, rigidbody.velocity.x * -tilt, 0.0f); (Tilt submarine slightly sideways while moving horizontally.Fix later, this doesnt work cuz 2D)

    //}

    public void updateHealthpoints(int hp)
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        currentHealth += hp;
        gameController.updateUiHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameController.player);
            instantiatedObj = (GameObject)Instantiate(playerExplosion, transform.position, transform.rotation);
            Destroy(instantiatedObj, destroyExplosionAfterFinished);
            gameController.gameOver();
       
        }
    }

}
