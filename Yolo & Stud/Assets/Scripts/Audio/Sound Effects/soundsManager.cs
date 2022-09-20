using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;


public class soundsManager : Singleton<soundsManager>
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource offlineAudioSource;
    [SerializeField] AudioSource soundEffectsSource;

	[Header("External References")]
    public Sound[] sounds;
    public OfflineMusic[] offlineMusic;

    [Header("Audio Integers")]
    [SerializeField] int trackIndex;

    [Header("Offline Music Strings")]
	[Tooltip("Will be replaced with the artist's name")]
    [SerializeField] string artistName;
    [Tooltip("Will be replaced with the song's name")]
    [SerializeField] string songName;

    [Header("Music Mode Details")]
    [Tooltip("This string will appear if the player is using the music controller offline")]
    [SerializeField] string offlineMusicMode = "Playing Music Offline";
    [Tooltip("This string will appear if the player is using the music controller online")]
    [SerializeField] string onlineMusicMode = "Playing Music On Spotify";
    [Tooltip("Drag the text game object that will show the music mode")]
    [SerializeField] Text musicModeText;

    [Header("Offline Music UI Elements")]
    [Tooltip("Drag The Arist text Here")]
    [SerializeField] Text artistTextName;
    [Tooltip("Drag The Track Text Here")]
    [SerializeField] Text trackName;

    [Header("Audio Booleans")]
    [SerializeField] bool isPaused;
    [SerializeField] bool isStopped;

    // Start is called before the first frame update
    void Awake()
    {
        //Sound Effects
        foreach (Sound soundCurrentlyLookingAt in sounds) //The current sound being checked in "sounds" array 
        {
            if (soundEffectsSource == null)
            {
                soundCurrentlyLookingAt.AudioSource = gameObject.AddComponent<AudioSource>();
                soundEffectsSource = soundCurrentlyLookingAt.AudioSource;
            }
            else
                soundCurrentlyLookingAt.AudioSource = soundEffectsSource;

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

        //Offline Music
        foreach (OfflineMusic soundCurrentlyLookingAt in offlineMusic) //The current sound being checked in "sounds" array 
        {
            if (offlineAudioSource == null)
            {
                soundCurrentlyLookingAt.AudioSource = gameObject.AddComponent<AudioSource>();
                offlineAudioSource = soundCurrentlyLookingAt.AudioSource;
            }
            else
                soundCurrentlyLookingAt.AudioSource = offlineAudioSource;

            
                //clip
                soundCurrentlyLookingAt.AudioSource.clip = soundCurrentlyLookingAt.clip;
          
                //Names
                offlineAudioSource.clip.name = soundCurrentlyLookingAt.songName;

                //volume
                soundCurrentlyLookingAt.AudioSource.volume = soundCurrentlyLookingAt.volume;

                //pitch
                soundCurrentlyLookingAt.AudioSource.pitch = soundCurrentlyLookingAt.pitch;

                //loop
                soundCurrentlyLookingAt.AudioSource.loop = soundCurrentlyLookingAt.Loop;

                soundCurrentlyLookingAt.AudioSource.playOnAwake = soundCurrentlyLookingAt.PlayOnAwake;

            artistName = soundCurrentlyLookingAt.artistName;
            songName = soundCurrentlyLookingAt.songName;

        }
    }

	private void Start()
	{
        CheckMusicMode();
        UpdateAudioUi();
        PlayPauseAudio();
        AssignTrackIndex();
	}

	#region Ui Stuff
	public void CheckMusicMode()
	{
        //Changes rthe text that informs the player if they are connected to Spotify or not
        if (SettingsMenu.Instance.GetMusicMode())
        {
            musicModeText.text = offlineMusicMode;
        }
        else
            musicModeText.text = onlineMusicMode;
	}

    public void UpdateAudioUi()
	{
        //Asign text on screen
        artistTextName.text = artistName;
        trackName.text = songName;
    }
	#endregion
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

    void AssignTrackIndex()
	{
		for (int i = 0; i < offlineMusic.Length; i++)
		{
			if (offlineMusic[i].AudioSource.isPlaying)
			{
                trackIndex = i;
			}
		}
	}

    #region Offline Music Logic
    public void PlayPauseAudio()
    {
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        if (offlineMusic == null) //If sound is not found// 
        {
            Debug.LogWarning("Music reference " + name + " is not found");
            return;
        }

        if (!isStopped)
        {
            if (isPaused)
            {
                isPaused = false;
                offlineAudioSource.Play();
            }
            else if (!isPaused)
            {
                isPaused = true;
                offlineAudioSource.Pause();
            }
        }
        else if (isStopped)
        {
            isStopped = false;
            offlineAudioSource.Play();
        }
    }

    public void StopAudio()
    {
        offlineAudioSource.Stop();
        isStopped = true;
    }

    public void NextSong()
    {
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        //trackIndex++;
        Debug.Log("TODO: NEXT SONG");
        trackIndex++;

        if(trackIndex > offlineMusic.Length - 1)
            trackIndex = 0;

        AudioClip songToPlay = offlineMusic[trackIndex].clip;
        offlineAudioSource.clip = songToPlay;
        offlineAudioSource.Play();
        Details(songToPlay);
    }

    public void PreviousSong()
	{
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;


        trackIndex--;

        if (trackIndex < 0)
            trackIndex = offlineMusic.Length - 1;

        AudioClip songToPlay = offlineMusic[trackIndex].clip;
        offlineAudioSource.clip = songToPlay;
        offlineAudioSource.Play();
        Details(songToPlay);
    }
    void Details(AudioClip clip)
    {
        //ensure artist name is the song that is currently playing
        OfflineMusic song = Array.Find(offlineMusic, sound => sound.clip == clip);
        Debug.Log(song.songName);

        artistName = song.artistName;
        songName = song.songName;

        UpdateAudioUi();

         Debug.Log("Artist: " + artistName);
          Debug.Log("Song: " + songName);
    }

    public void ShuffleMusic()
    {
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        for (int i = 0; i < offlineMusic.Length; i++)
        {
            int random = UnityEngine.Random.Range(0, offlineMusic.Length - 1);

            OfflineMusic temp = offlineMusic[i];

            offlineMusic[i] = offlineMusic[random];
            offlineMusic[random] = temp;

            if(temp.songName == offlineAudioSource.clip.name)
			{
                Debug.Log("Song is already playing");
                NextSong();
			}       
            else
			{
                offlineAudioSource.clip = temp.clip;
                offlineAudioSource.Play();
			}                
        }

        //StartCoroutine(PlayMusic());
    }

    public void LoopSong()
	{

        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        offlineAudioSource.loop = !offlineAudioSource.loop;
	}
        #endregion
        //FindObjectOfType<MusicManager>().Play("Name of audio")//

    }
