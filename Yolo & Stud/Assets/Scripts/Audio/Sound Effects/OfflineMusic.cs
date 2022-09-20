using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] // allows array to be seen in inspector//

public class OfflineMusic 
{
    public AudioClip clip;
    
    public string songName;
    public string artistName;


    //Range attribute adds a slider to each field in inspector
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool Loop;

    public bool PlayOnAwake;

    [HideInInspector]
    public AudioSource AudioSource;
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/