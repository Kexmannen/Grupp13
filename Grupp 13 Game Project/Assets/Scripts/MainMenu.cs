using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        //Detta laddar nästa scen i build index, vi kan sätta det så det blir ett fast värde, men gjorde detta så länge.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
