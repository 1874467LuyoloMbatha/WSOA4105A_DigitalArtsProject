using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] // allows array to be seen in inspector//

public class Sound
{
    public string Name;

    public AudioClip clip;

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
