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
    public float delay;

    public static bool OptionsMenuOn = false;
    public static bool ControlsMenuOn = false;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject controlsMenuUI;

    //private GameController gameController;  - temporarily disabled - might be needed for highscorelist-stuff

    private void Awake()
    {
        // - temporarily disabled -  might be needed for highscorelist-stuff

        //GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        //if (gameControllerObject != null)
        //{
        //    gameController = gameControllerObject.GetComponent<GameController>();
        //}
        //if (gameController == null)
        //{
        //    Debug.Log("Cannot find 'GameController' script");
        //}  



    }
    // Lägga till ljud och fade till varje del


    // Public void highscore



    public void PlayGame()
    {
        //FindObjectOfType<AudioManager>().Play("buttonPress");
       AudioManager.instance.Play("buttonPress");
       // audiomanager.Play("buttonPress");
        //Detta laddar nästa scen i build index, vi kan sätta det så det blir ett fast värde, men gjorde detta så länge.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //source.PlayOneShot(buttonPress);
        StartCoroutine(ChangeLevel());
    }

    private IEnumerator ChangeLevel()
    {
        anim.SetBool("FadeOut", true);
        // väntar tills Imagen blackFade nått fullt alphavärde, sen byter den level.
        yield return new WaitUntil(() => blackFade.color.a == 1);
        yield return new WaitForSeconds(delay); //test för en delay

        //Detta laddar nästa scen i build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //gameController.isGame = true; - temporarily disabled - this should be used in future versions
    }

    public void OpenOptions()
    {
        AudioManager.instance.Play("buttonPress");
        //FindObjectOfType<AudioManager>().Play("buttonPress");
        optionsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
        OptionsMenuOn = true;
    }

    public void BackToMenu()
    {
        optionsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        OptionsMenuOn = false;
        ControlsMenuOn = false;
        mainMenuUI.SetActive(true);
        //FindObjectOfType<AudioManager>().Play("buttonPress");
        AudioManager.instance.Play("buttonPress");
    }

    public void OpenControls()
    {
        AudioManager.instance.Play("buttonPress");
        //FindObjectOfType<AudioManager>().Play("buttonPress");
        controlsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
        ControlsMenuOn = true;

    }

    public void OpenHighScore()
    {
        AudioManager.instance.Play("buttonPress");
        SceneManager.LoadScene(2);

    }



}
