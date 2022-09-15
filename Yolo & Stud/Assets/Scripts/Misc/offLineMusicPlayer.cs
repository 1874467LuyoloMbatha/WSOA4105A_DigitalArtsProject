using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offLineMusicPlayer : MonoBehaviour
{
    public AudioClip[] musicClips;
    private AudioSource musicAudioSource;

    void Start()
    {
        musicAudioSource = FindObjectOfType<AudioSource>();
        musicAudioSource.loop = false;  
        
    }

    private AudioClip GetNextClip() 
    {
        return musicClips[Random.Range(0, musicClips.Length)];
        //change from random to chronological order.
    
    } 



    void Update()
    {
        if (!musicAudioSource.isPlaying) 
        {
            musicAudioSource.clip = GetNextClip();
            musicAudioSource.Play(); // will play clip fetched by above function.

          


        }
        
    }
}
