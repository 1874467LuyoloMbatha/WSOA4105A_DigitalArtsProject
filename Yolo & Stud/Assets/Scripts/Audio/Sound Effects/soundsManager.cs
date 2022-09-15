using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class soundsManager : Singleton<soundsManager>
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {

        foreach (Sound soundCurrentlyLookingAt in sounds) //The current sound being checked in "sounds" array 
        {
            soundCurrentlyLookingAt.AudioSource = gameObject.AddComponent<AudioSource>();

            //clip
            soundCurrentlyLookingAt.AudioSource.clip = soundCurrentlyLookingAt.clip;

            //volume
            soundCurrentlyLookingAt.AudioSource.volume = soundCurrentlyLookingAt.volume;

            //pitch
            soundCurrentlyLookingAt.AudioSource.pitch = soundCurrentlyLookingAt.pitch;

            //loop
            soundCurrentlyLookingAt.AudioSource.loop = soundCurrentlyLookingAt.Loop;

            soundCurrentlyLookingAt.AudioSource.playOnAwake = soundCurrentlyLookingAt.PlayOnAwake;

        }
    }
    /// <summary>
    /// looks through the array sounds to find the name of the called sound
    /// </summary>

    // Update is called once per frame
    public void Play(string name) 
    {
        Sound SoundThatWeFind = Array.Find(sounds, sound => sound.Name == name);
        SoundThatWeFind.AudioSource.Play();

        if (SoundThatWeFind == null) //If sound is not found// 
        {
            Debug.LogWarning("Music reference " + name + " is not found");
            return;
        }
        FindObjectOfType<soundsManager>().Play("Name of audio");
    }

    //FindObjectOfType<MusicManager>().Play("Name of audio")//
   
}
