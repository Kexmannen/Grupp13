﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image blackFade;
    public Animator anim;
    public AudioSource source;
    public AudioClip buttonPress;
    public int delay;
=======

public class MainMenu : MonoBehaviour
{
>>>>>>> master

    public void PlayGame()
    {
        //Detta laddar nästa scen i build index, vi kan sätta det så det blir ett fast värde, men gjorde detta så länge.
<<<<<<< HEAD
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(ChangeLevel());
    }

    private IEnumerator ChangeLevel()
    {
        anim.SetBool("Fade", true);
        // väntar tills Imagen blackFade nått fullt alphavärde, sen byter den level.
        yield return new WaitUntil(() => blackFade.color.a == 1);
        yield return new WaitForSeconds(delay); //test för en delay

        //Detta laddar nästa scen i build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

=======
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
>>>>>>> master
}
