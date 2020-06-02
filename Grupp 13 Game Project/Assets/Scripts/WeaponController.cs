using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //private AudioSource audioSource;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        InvokeRepeating("fire", delay, fireRate);
    }

    private void fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        AudioManager.instance.Play("ShootingEnemy_Shot");

    }

}
