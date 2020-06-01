using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    public int SceneToLoad;
public void BackToMenu()
    {
        AudioManager.instance.Play("buttonPress");
        SceneManager.LoadScene(SceneToLoad);
    }
}
