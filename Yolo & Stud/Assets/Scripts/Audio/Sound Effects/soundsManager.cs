using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class soundsManager : MonoBehaviour
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
    void Update()
    {
        
    }

    //7.24
}
