using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
   //public static AudioManager instance;

    //The Idea of the AudioController is to have a list of sounds, that we can add and remove as we go.
    //And Each sound has some different properties
    //An Audioclip, a Volume and pitch setting and the possibility to loop
    //as well as adding volume and pitch randomness or spatial blend

    //Then when we start the game we go through the list, and for each item we add an audiosource, with the appropriate settings.

    //THEN when we want to play a sound, we simply call a PLAY-method in the AudioController where we input the name of the sound we
    //want to play, then the AudioController is going to find the source with that name, and then play the sound.

    //
    public Sound[] sounds;

    public static AudioManager instance;

    // Awake is pretty much the same as Start. but is called right before Start



    void Awake()
    {

        //We dont want the music to cut off between scenes, and we will fix that by making the AudioManager not destroy between scenes.
        //To fix that 2 audiomanagers spawn in the next scene we have the if-else , there can only be 1 instance of the audiomanager.

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        //Before we start the game we need to loop through the sounds in the list and add an audiosource for each sound.
        foreach (Sound s in sounds)
        {

            //s = the sound we are currently looking at

            s.source = gameObject.AddComponent<AudioSource>();
            //we are saving the audiosource in a variable we made in the Sound class that we call source
            //so that later when we want to play the sound, we can call the Play-method on the audiosource.
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;
        }
    }

    // Start is called before the first frame update

    void Start()
    {
        Play("BackgroundMusic");
    }

    //This method takes in a string that is the name of our sound
    //all we need to do is to loop through our sounds and fins the sound with the appropriate name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
           
        
        s.source.Play();
    }

    //att fixa sen 
    /* if (PauseMenu.GameIsPaused)
 {
     Audiosource.pitch += .5f;
 }*/
}
