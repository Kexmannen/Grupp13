﻿using System.Collections;
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
    private float nextFire;

    public float fireRate;
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;

    public int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        //rigidbody.rotation = Quaternion.Euler(0.0f, rigidbody.velocity.x * -tilt, 0.0f); (Tilt submarine slightly sideways while moving horizontally. Fix later, this doesnt work cuz 2D)

    }

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
            gameController.gameOver();
        }
    }
}
