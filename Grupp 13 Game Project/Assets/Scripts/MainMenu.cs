using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image blackFade;
    public Animator anim;
    public AudioSource source;
    public AudioClip buttonPress;
    public float delay;

    // Lägga till ljud och fade till varje del
    // Public void options
    // Public void highscore
    // public void controls

    public void PlayGame()
    {
        //Detta laddar nästa scen i build index, vi kan sätta det så det blir ett fast värde, men gjorde detta så länge.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        source.PlayOneShot(buttonPress);
        StartCoroutine(ChangeLevel());
    }

    private IEnumerator ChangeLevel()
    {
        anim.SetBool("FadeOut", true);
        // väntar tills Imagen blackFade nått fullt alphavärde, sen byter den level.
        yield return new WaitUntil(() => blackFade.color.a == 1);
        yield return new WaitForSeconds(delay); //test för en delay

        //Detta laddar nästa scen i build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }

}
