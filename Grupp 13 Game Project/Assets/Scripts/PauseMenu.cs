using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //The button that is displayed in the game
    public Button pause;

    public int LevelToLoad;
    public int delay;
    public Animator anim;
    public Image blackFade;
   // public AudioClip buttonPress;
    //public AudioSource source;

    //public AudioManager audiomanager;

    public static bool GameIsPaused = false;
    //The pausemenu in the form of a gameobject, that is false and then becomes active when clicking on the pause-button.
    public GameObject pauseMenuUI;


   public void Resume()
    {
        //Disables the pausemenu gameobject again
        pauseMenuUI.SetActive(false);
        // You can use this to create slow motion effects
        // Normal time-rate is 1. Freeze is 0.
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        AudioManager.instance.Play("buttonPress");
        //FindObjectOfType<AudioManager>().Play("buttonPress");
        pauseMenuUI.SetActive(true);
        // You can use this to create slow motion effects or to freeze time completely, like we do here.
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void BackToMainMenu()
    {
        AudioManager.instance.Play("buttonPress");
        //FindObjectOfType<AudioManager>().Play("buttonPress");

        Time.timeScale = 1f;
        GameIsPaused = false;
        StartCoroutine(ChangeLevel());
    }

    private IEnumerator ChangeLevel()
    {
        anim.SetBool("FadeOut", true);
        //väntar tills Imagen blackFade nått fullt alphavärde, sen byter den level.
        yield return new WaitUntil(() => blackFade.color.a == 1);
        yield return new WaitForSeconds(delay); //test för en delay
        //Detta laddar enscen i build index
        SceneManager.LoadScene(LevelToLoad);

    }
}
