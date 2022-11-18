using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;


public class soundsManager : Singleton<soundsManager>
{
    public enum MusicGenre { Afrobeats, Classical, Jazz, Lofi, Electro, Relaxation, Rock, Custom }

    [Header("Genre References")]
    public MusicGenre musicGenre;
    [SerializeField] Dropdown genreDropdown;
    public OfflineMusic[] africanMusic;
    public OfflineMusic[] classicalMusic;
    public OfflineMusic[] jazzMusic;
    public OfflineMusic[] lofiMusic;
    public OfflineMusic[] electroMusic;
    public OfflineMusic[] relaxationMusic;
    public OfflineMusic[] rockMusic;

    [Header("Audio Sources")]
    [SerializeField] AudioSource offlineAudioSource;
    [SerializeField] AudioSource ambienceAudioSource, additionalAmbienceAudioSource;
    [SerializeField] AudioSource soundEffectsSource;

    [Header("External References")]
    public Sound[] sounds;
    public OfflineMusic[] ambienceMusic;
    public AudioClip rainAmbience, rainThunderAmbience, snowAmbience, sunnyAmbience;

    [Header("Music Controller Images")]
    [Tooltip("Drag Play/Pause Button GameObject Here")]
    [SerializeField] Image playAndPauseBtn;
    [Tooltip("Drag Images To Pause And Play here")]
    [SerializeField] Sprite playBtnImg, pauseBtnImg;
    [Tooltip("Drag Loop Button GameObject Here")]
    [SerializeField] Image loopBtn;
    [Tooltip("Drag Images To Loop here")]
    [SerializeField] Sprite loopImg, notLoopingImg;
    [Tooltip("Drag Mute Button GameObject Here")]
    [SerializeField] Image muteBtn;
    [Tooltip("Drag Images To Loop here")]
    [SerializeField] Sprite unMutedImg, mutedImg;

    [Header("Audio Integers")]
    [SerializeField] int trackIndex;
    [SerializeField] int genreIndex;

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

    [Header("Alarm Sound Effects Integers")]
    [Tooltip("This integer should be one higher than the number of alarm clock integers we have")]
    [SerializeField] int maxAlarmClockSoundEffect;

    [Header("Audio Booleans")]
    [SerializeField] bool isPaused;
    [SerializeField] bool isStopped, isLooping;

    // Start is called before the first frame update
    void Awake()
    {
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

        switch (musicGenre)
        {
            case MusicGenre.Afrobeats:
                foreach (OfflineMusic soundCurrentlyLookingAt in africanMusic) //The current sound being checked in "sounds" array 
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
                break;
            case MusicGenre.Classical:
                foreach (OfflineMusic soundCurrentlyLookingAt in classicalMusic) //The current sound being checked in "sounds" array 
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
                break;
            case MusicGenre.Jazz:
                foreach (OfflineMusic soundCurrentlyLookingAt in jazzMusic) //The current sound being checked in "sounds" array 
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
                break;
            case MusicGenre.Lofi:
                foreach (OfflineMusic soundCurrentlyLookingAt in lofiMusic) //The current sound being checked in "sounds" array 
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
                break;
            case MusicGenre.Electro:
                foreach (OfflineMusic soundCurrentlyLookingAt in electroMusic) //The current sound being checked in "sounds" array 
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
                break;
            case MusicGenre.Relaxation:
                foreach (OfflineMusic soundCurrentlyLookingAt in relaxationMusic) //The current sound being checked in "sounds" array 
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
                break;
            case MusicGenre.Rock:
                foreach (OfflineMusic soundCurrentlyLookingAt in rockMusic) //The current sound being checked in "sounds" array 
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
                break;
            case MusicGenre.Custom:
                ImportManager.Instance.LoadFiles();
                break;
            default:
                break;
        }
        //Offline Music

        songToSkip = offlineAudioSource.clip;

    }

    private void Update()
    {
      //  Debug.Log(offlineAudioSource.isPlaying);
    }
    private void Start()
    {
        SetUpDropDownGenre();
        CheckMusicMode();
        UpdateAudioUi();
        PlayPauseAudio();
        AssignTrackIndex();
        SettingsMenu.Instance.SetUpPrefs();

        if(sunnyAmbience != null)
          PlaySunnyAmbience();
    }

    #region Ui Stuff
    public void CheckMusicMode()
    {
        //Changes rthe text that informs the player if they are connected to Spotify or not
        if (SettingsMenu.Instance.GetMusicMode())
        {
            if (musicModeText != null)
                musicModeText.text = offlineMusicMode;
        }
        else
        {
            if (musicModeText != null)
                musicModeText.text = onlineMusicMode;
        }
    }

    public void UpdateAudioUi()
    {
        //Asign text on screen
        artistTextName.text = artistName;
        trackName.text = songName;
    }

    public void SetOfflineVolume(float v)
    {
        offlineAudioSource.volume = v;
    }

    public float GetOfflineVolume()
    {
        return offlineAudioSource.volume;
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
        //soundEffectsSource.Play("Name of audio");
    }

    public void PlayRandomAlarm(string name)
    {

        int rendClip = UnityEngine.Random.Range(1, maxAlarmClockSoundEffect);
        Debug.Log(rendClip);


        Sound SoundThatWeFind = Array.Find(sounds, sound => sound.Name == name + rendClip.ToString());

        if (SoundThatWeFind == null) //If sound is not found// 
        {
            Debug.LogWarning("Music reference " + name + " is not found");
            return;
        }

        soundEffectsSource.clip = SoundThatWeFind.clip;
        soundEffectsSource.Play();
        //.Play("Name of audio " + rendClip.ToString());
    }

    void AssignTrackIndex()
    {
		switch (musicGenre)
		{
			case MusicGenre.Afrobeats:
                for (int i = 0; i < africanMusic.Length; i++)
                {
                    if (africanMusic[i].AudioSource.isPlaying)
                    {
                        trackIndex = i;
                    }
                }
                break;
			case MusicGenre.Classical:
                for (int i = 0; i < classicalMusic.Length; i++)
                {
                    if (classicalMusic[i].AudioSource.isPlaying)
                    {
                        trackIndex = i;
                    }
                }
                break;
			case MusicGenre.Jazz:
                for (int i = 0; i < jazzMusic.Length; i++)
                {
                    if (jazzMusic[i].AudioSource.isPlaying)
                    {
                        trackIndex = i;
                    }
                }
                break;
			case MusicGenre.Lofi:
                for (int i = 0; i < lofiMusic.Length; i++)
                {
                    if (lofiMusic[i].AudioSource.isPlaying)
                    {
                        trackIndex = i;
                    }
                }
                break;
			case MusicGenre.Electro:
                for (int i = 0; i < electroMusic.Length; i++)
                {
                    if (electroMusic[i].AudioSource.isPlaying)
                    {
                        trackIndex = i;
                    }
                }
                break;
			case MusicGenre.Relaxation:
                for (int i = 0; i < relaxationMusic.Length; i++)
                {
                    if (relaxationMusic[i].AudioSource.isPlaying)
                    {
                        trackIndex = i;
                    }
                }
                break;
			case MusicGenre.Rock:
                for (int i = 0; i < rockMusic.Length; i++)
                {
                    if (rockMusic[i].AudioSource.isPlaying)
                    {
                        trackIndex = i;
                    }
                }
                break;
			case MusicGenre.Custom:
					trackIndex = 0;
				break;
			default:
				break;
		}
	}

    #region Offline Music Logic
    public void PlayPauseAudio()
    {
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        /* if (lofiMusic == null) //If sound is not found// 
         {
             Debug.LogWarning("Music reference " + name + " is not found");
             return;
         }*/

        if (!isStopped)
        {
            if (isPaused)
            {
                isPaused = false;

                //Change Image
                if (playAndPauseBtn != null)
                    playAndPauseBtn.sprite = pauseBtnImg;

                offlineAudioSource.Play();
            }
            else if (!isPaused)
            {
                isPaused = true;

                //Change Image
                if (playAndPauseBtn != null)
                    playAndPauseBtn.sprite = playBtnImg;

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

    [Header("Temporary Variables to find details and such")]
    [SerializeField] OfflineMusic song;
    [SerializeField] AudioClip songToPlay, songToSkip;
    public void NextSong()
    {
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;


        if (musicGenre == MusicGenre.Afrobeats)
        {
            songToSkip = africanMusic[trackIndex].clip;

            //trackIndex++;
            trackIndex++;

            if (trackIndex > africanMusic.Length - 1)
                trackIndex = 0;
        }
        if (musicGenre == MusicGenre.Classical)
        {
            songToSkip = classicalMusic[trackIndex].clip;

            //trackIndex++;
            trackIndex++;

            if (trackIndex > classicalMusic.Length - 1)
                trackIndex = 0;
        }
        if (musicGenre == MusicGenre.Jazz)
        {
            songToSkip = jazzMusic[trackIndex].clip;

            //trackIndex++;
            trackIndex++;

            if (trackIndex > jazzMusic.Length - 1)
                trackIndex = 0;
        }
        if (musicGenre == MusicGenre.Lofi)
        {
            songToSkip = lofiMusic[trackIndex].clip;

            //trackIndex++;
            trackIndex++;

            if (trackIndex > lofiMusic.Length - 1)
                trackIndex = 0;
        }
        if (musicGenre == MusicGenre.Electro)
        {
            songToSkip = electroMusic[trackIndex].clip;

            //trackIndex++;
            trackIndex++;

            if (trackIndex > electroMusic.Length - 1)
                trackIndex = 0;
        }
        if (musicGenre == MusicGenre.Relaxation)
        {
            songToSkip = relaxationMusic[trackIndex].clip;

            //trackIndex++;
            trackIndex++;

            if (trackIndex > relaxationMusic.Length - 1)
                trackIndex = 0;
        }
        if (musicGenre == MusicGenre.Rock)
        {
            songToSkip = rockMusic[trackIndex].clip;

            //trackIndex++;
            trackIndex++;

            if (trackIndex > rockMusic.Length - 1)
                trackIndex = 0;
        }
		if (musicGenre == MusicGenre.Custom)
		{
			songToSkip = ImportManager.Instance.toPopulate[trackIndex];

			//trackIndex++;
			trackIndex++;

			if (trackIndex > ImportManager.Instance.toPopulate.Count - 1)
				trackIndex = 0;
		}
		offlineAudioSource.clip = songToSkip;
        offlineAudioSource.Play();
        Details(songToSkip);
    }

    public void PreviousSong()
	{
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        // = lofiMusic[trackIndex].clip;
        if (musicGenre == MusicGenre.Afrobeats)
        {
            songToPlay = africanMusic[trackIndex].clip;

            trackIndex--;

            if (trackIndex < 0)
                trackIndex = africanMusic.Length - 1;
        }


        if (musicGenre == MusicGenre.Classical)
        { 
            songToPlay = classicalMusic[trackIndex].clip;

            trackIndex--;

            if (trackIndex < 0)
                trackIndex = classicalMusic.Length - 1;
        }
        if (musicGenre == MusicGenre.Jazz)
        {
            songToPlay = jazzMusic[trackIndex].clip;

            trackIndex--;

            if (trackIndex < 0)
                trackIndex = jazzMusic.Length - 1;
        }
        if (musicGenre == MusicGenre.Lofi)
        {
            songToPlay = lofiMusic[trackIndex].clip;

            trackIndex--;

            if (trackIndex < 0)
                trackIndex = lofiMusic.Length - 1;
        }
        if (musicGenre == MusicGenre.Electro)
        {
            songToPlay = electroMusic[trackIndex].clip;

            trackIndex--;

            if (trackIndex < 0)
                trackIndex = electroMusic.Length - 1;
        }
        if (musicGenre == MusicGenre.Relaxation)
        {
            songToPlay = relaxationMusic[trackIndex].clip;

            trackIndex--;

            if (trackIndex < 0)
                trackIndex = relaxationMusic.Length - 1;
        }
        if (musicGenre == MusicGenre.Rock)
        {
            songToPlay = rockMusic[trackIndex].clip;

            trackIndex--;

            if (trackIndex < 0)
                trackIndex = rockMusic.Length - 1;
        }
      
        if(musicGenre == MusicGenre.Custom)
		{
			songToPlay = ImportManager.Instance.toPopulate[trackIndex];

			trackIndex--;

			if (trackIndex < 0)
				trackIndex = ImportManager.Instance.toPopulate.Count - 1;
		}

        offlineAudioSource.clip = songToPlay;
        offlineAudioSource.Play();
        Details(songToPlay);
    }


    void Details(AudioClip clip)
    {
        
        //Check genres
        switch (musicGenre)
		{
			case MusicGenre.Afrobeats:
               song = Array.Find(africanMusic, sound => sound.clip == clip);
                break;
			case MusicGenre.Classical:
                song = Array.Find(classicalMusic, sound => sound.clip == clip);
                break;
			case MusicGenre.Jazz:
                song = Array.Find(jazzMusic, sound => sound.clip == clip);
                break;
			case MusicGenre.Lofi:
                song = Array.Find(lofiMusic, sound => sound.clip == clip);
                break;
			case MusicGenre.Electro:
                song = Array.Find(electroMusic, sound => sound.clip == clip);
                break;
			case MusicGenre.Relaxation:
                song = Array.Find(relaxationMusic, sound => sound.clip == clip);
                break;
			case MusicGenre.Rock:
                song = Array.Find(rockMusic, sound => sound.clip == clip);
                break;
			case MusicGenre.Custom:
				break;
			default:
				break;
		}
		//ensure artist name is the song that is currently playing
		//OfflineMusic song = Array.Find(lofiMusic, sound => sound.clip == clip);
        Debug.Log(song.songName);

        if (musicGenre != MusicGenre.Custom)
        {
            artistName = song.artistName;
            songName = song.songName;
        }
        else
        {
            artistName = "Unknown";
            songName = "Unknown";
        }

        UpdateAudioUi();

         Debug.Log("Artist: " + artistName);
          Debug.Log("Song: " + songName);
    }

    public void ShuffleMusic()
    {
        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        switch (musicGenre)
        {
            case MusicGenre.Afrobeats:
                for (int i = 0; i < africanMusic.Length; i++)
                {
                    int random = UnityEngine.Random.Range(0, africanMusic.Length - 1);

                    OfflineMusic temp = africanMusic[i];

                    africanMusic[i] = africanMusic[random];
                    africanMusic[random] = temp;

                    if (temp.songName == offlineAudioSource.clip.name)
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
                break;
            case MusicGenre.Classical:
                for (int i = 0; i < classicalMusic.Length; i++)
                {
                    int random = UnityEngine.Random.Range(0, classicalMusic.Length - 1);

                    OfflineMusic temp = classicalMusic[i];

                    classicalMusic[i] = classicalMusic[random];
                    classicalMusic[random] = temp;

                    if (temp.songName == offlineAudioSource.clip.name)
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
                break;
            case MusicGenre.Jazz:
                for (int i = 0; i < jazzMusic.Length; i++)
                {
                    int random = UnityEngine.Random.Range(0, jazzMusic.Length - 1);

                    OfflineMusic temp = jazzMusic[i];

                    jazzMusic[i] = jazzMusic[random];
                    jazzMusic[random] = temp;

                    if (temp.songName == offlineAudioSource.clip.name)
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
                break;
            case MusicGenre.Lofi:
                for (int i = 0; i < lofiMusic.Length; i++)
                {
                    int random = UnityEngine.Random.Range(0, lofiMusic.Length - 1);

                    OfflineMusic temp = lofiMusic[i];

                    lofiMusic[i] = lofiMusic[random];
                    lofiMusic[random] = temp;

                    if (temp.songName == offlineAudioSource.clip.name)
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
                break;
            case MusicGenre.Electro:
                for (int i = 0; i < electroMusic.Length; i++)
                {
                    int random = UnityEngine.Random.Range(0, electroMusic.Length - 1);

                    OfflineMusic temp = electroMusic[i];

                    electroMusic[i] = electroMusic[random];
                    electroMusic[random] = temp;

                    if (temp.songName == offlineAudioSource.clip.name)
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
                break;
            case MusicGenre.Relaxation:
                for (int i = 0; i < relaxationMusic.Length; i++)
                {
                    int random = UnityEngine.Random.Range(0, relaxationMusic.Length - 1);

                    OfflineMusic temp = relaxationMusic[i];

                    relaxationMusic[i] = relaxationMusic[random];
                    relaxationMusic[random] = temp;

                    if (temp.songName == offlineAudioSource.clip.name)
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
                break;
            case MusicGenre.Rock:
                for (int i = 0; i < lofiMusic.Length; i++)
                {
                    int random = UnityEngine.Random.Range(0, rockMusic.Length - 1);

                    OfflineMusic temp = rockMusic[i];

                    rockMusic[i] = rockMusic[random];
                    rockMusic[random] = temp;

                    if (temp.songName == offlineAudioSource.clip.name)
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
                break;
            case MusicGenre.Custom:
				for (int i = 0; i < ImportManager.Instance.toPopulate.Count; i++)
				{
					int random = UnityEngine.Random.Range(0, ImportManager.Instance.toPopulate.Count - 1);

					AudioClip temp = ImportManager.Instance.toPopulate[i];

					ImportManager.Instance.toPopulate[i] = ImportManager.Instance.toPopulate[random];
					ImportManager.Instance.toPopulate[random] = temp;

					if (temp == offlineAudioSource.clip)
					{
						Debug.Log("Song is already playing");
						NextSong();
					}
					else
					{
						offlineAudioSource.clip = temp;
						offlineAudioSource.Play();
					}
				}
				break;
            default:
                break;
        }

		//StartCoroutine(PlayMusic());
    }

    public void RepeatSong()
    {

        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;
        isLooping = !isLooping;

        if (isLooping)
        {
            if (loopBtn != null)
                loopBtn.sprite = loopImg;
        }
        else
		{

            if (loopBtn != null)
                loopBtn.sprite = notLoopingImg;
        }

        offlineAudioSource.loop = !offlineAudioSource.loop;
    }

    public void MuteAudio()
	{
        offlineAudioSource.mute = !offlineAudioSource.mute;

        if (offlineAudioSource.mute)
        {
            if (muteBtn != null)
                muteBtn.sprite = mutedImg;
        }
        else
		{
            if (muteBtn != null)
                muteBtn.sprite = unMutedImg;
        }
    }
    public void LoopSong(GameObject loopTxt)
	{

        //Only Allow these buttons to work if the player is in Offline Mode
        if (!SettingsMenu.Instance.GetMusicMode())
            return;

        offlineAudioSource.loop = !offlineAudioSource.loop;
        loopTxt.SetActive(offlineAudioSource.loop);
	}
	#endregion

	#region Offline Music Genre Logic

    void SetUpDropDownGenre()
	{
        genreIndex = ((int)musicGenre);

        int indexAgain = System.Enum.GetValues(typeof(MusicGenre)).Length;
        genreDropdown.options.Clear();

        Debug.Log(indexAgain);
        for (int i = 0; i < indexAgain; i++)
        {
            musicGenre = (MusicGenre)i;
            genreDropdown.options.Add(new Dropdown.OptionData() { text = musicGenre.ToString() });
        }

        genreDropdown.value = genreIndex;

      //  SortThroughGenre();
    }
    //Assigned in Inspector for the genre dropdown
    public void GenreSelected(int v)
	{
        genreIndex = v;

        musicGenre = (MusicGenre)genreIndex;

        SortThroughGenre();
        
	}

    void SortThroughGenre()
	{
        StopAudio();

        if (SettingsMenu.Instance.GetAutoPlayMode())
        {
            isPaused = false;

            //Change Image
            if (playAndPauseBtn != null)
                playAndPauseBtn.sprite = pauseBtnImg;

            offlineAudioSource.Play();
            NextSong();
        }
        else if (!SettingsMenu.Instance.GetAutoPlayMode())
        {
            isPaused = true;

            //Change Image
            if (playAndPauseBtn != null)
                playAndPauseBtn.sprite = playBtnImg;

            NextSong();
            offlineAudioSource.Pause();
        }
    }
	#endregion

	//FindObjectOfType<MusicManager>().Play("Name of audio")//
    public void PlayMainAmbience(int v)
	{
        if(v == 1)
		{
            ambienceAudioSource.clip = ambienceMusic[0].clip;
            ambienceAudioSource.loop = true;
            ambienceAudioSource.Play();
		}
        if (v == 2)
        {
            ambienceAudioSource.clip = ambienceMusic[1].clip;
            ambienceAudioSource.loop = true;
            ambienceAudioSource.Play();
        }
    }

    public void PlaySunnyAmbience()
    {
        additionalAmbienceAudioSource.clip = sunnyAmbience;
        additionalAmbienceAudioSource.loop = true;
        additionalAmbienceAudioSource.Play();
    }

    public void PlayRainAmbience()
	{
        additionalAmbienceAudioSource.clip = rainAmbience;
        additionalAmbienceAudioSource.loop = true;
        additionalAmbienceAudioSource.Play();
	}

    public void PlayRainThunderAmbience()
    {
        additionalAmbienceAudioSource.clip = rainThunderAmbience;
        additionalAmbienceAudioSource.loop = true;
        additionalAmbienceAudioSource.Play();
    }

    public void PlaySnowAmbience()
    {
        additionalAmbienceAudioSource.clip = snowAmbience;
        additionalAmbienceAudioSource.loop = true;
        additionalAmbienceAudioSource.Play();
    }
}
