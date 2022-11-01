using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Settings page where player can change the volume settings,
/// Game quality and,
/// Screen size
/// </summary>
public class SettingsMenu : Singleton<SettingsMenu>
{
	[Header("External References")]
    [SerializeField] List<ThemeGameObject> themes = new List<ThemeGameObject>();

    [Header("Volume Sliders")]
    public Slider masterVolume;
    public Slider[] currMusicPlayerVolume;
    public Slider[] currFXVolume;
    public Slider currAmbienceVolume;

    [Header("Screen Related GameObjects")]
	[Tooltip("Drag Image from Hierachy under Right Panel that controls fullscreen mode")]
    [SerializeField] Image fullScreenImg;
	[Tooltip("Drag Sprites from assets to switch between minimise and maximise")]
    [SerializeField] Sprite maximiseSpr, minimiseSpr;

    [Header("Accessibility Settings")]
    public Dropdown[] languageDropDown;

    [Header("Text Specific Settings")]
    [Tooltip("Every Text element that will be changed/manipulated")]
    public Text[] fontTexts;
    public Slider[] fontTextsSizes;

    [Header("Screen Settings")]
    [SerializeField] bool fullScreen = true;

	[Header("Audio Settings")]
	[SerializeField] bool offlineMode = true;
    [SerializeField] AudioMixer mainMixer;
    [SerializeField] string musicMixerMaster = "MasterVolume";
    [SerializeField] string musicMixerAmbient = "AmbientVolume";
    [SerializeField] string musicMixerSfx = "SFXVolume";
    [SerializeField] string musicMixerMainMusic = "MusicPlayer";

    [Header("Timer Settings")]
    [SerializeField] bool autoTransitionTimer = true;
    [SerializeField] bool showTimeInWords = false;
    [SerializeField] bool timeIsPaused, firstTimeOpening, timerHasRestarted;

    [Header("General Settings")]
    [Tooltip("This boolean will determine if after clearing a task it disappears or just crossed out")]
    [SerializeField] bool checkMarkDestroys = true;
    [Tooltip("This boolean will set the light or dark theme")]
    [SerializeField] bool darkMode = false;
    [Tooltip("These two colours are for the themes")]
    [SerializeField] Color lightTheme, darkTheme;


	/// <summary>
	/// allows player to change volume of music in settings menu through slider
	/// </summary>
	/// 

	 void Awake()
	{
        SetUpSliders();
	}
	 void Start()
	{
        SetUpPrefs();
        UpdateSliderOutput();

        Screen.fullScreen = fullScreen;

        CheckThemeMode();

        firstTimeOpening = true;
	}

    #region Audio & Music

    void SetUpSliders()
	{
        if(masterVolume != null)
		{
            masterVolume.onValueChanged.AddListener(SetMasterLevel);
		}

        if (currMusicPlayerVolume != null)
        {
            foreach (var item in currMusicPlayerVolume)
            {
                item.onValueChanged.AddListener(SetMainBGLevel);
            }
        }

        if (currFXVolume != null)
        {
            foreach (var item in currFXVolume)
            {
                item.onValueChanged.AddListener(SetFXLevel);
            }
        }

        if (currAmbienceVolume != null)
        {
            currAmbienceVolume.onValueChanged.AddListener(SetAmbienceLevel);

        }
    }
    void SetUpPrefs()
    {
        if (PlayerPrefs.HasKey(musicMixerMaster))
        {
            if (masterVolume != null)
            {
                masterVolume.value = PlayerPrefs.GetFloat(musicMixerMaster);
                mainMixer.SetFloat(musicMixerMaster, Mathf.Log10(masterVolume.value) * 20);
            }
        }
        else
        {
            if (masterVolume != null)
            {
                PlayerPrefs.SetFloat(musicMixerMaster, masterVolume.value);
            }
        }

        if (PlayerPrefs.HasKey(musicMixerMainMusic))
        {
            if (currMusicPlayerVolume != null)
            {
                foreach (var item in currMusicPlayerVolume)
                {
                    item.value = PlayerPrefs.GetFloat(musicMixerMainMusic);
                    mainMixer.SetFloat(musicMixerMainMusic, Mathf.Log10(item.value) * 20);
                }
            }
        }
        else
        {

            if (currMusicPlayerVolume != null)
            {
                foreach (var item in currMusicPlayerVolume)
                {
                   // item.value = soundsManager.Instance.GetOfflineVolume();
                    PlayerPrefs.SetFloat(musicMixerMainMusic, item.value);
                }
            }
        }


        if (PlayerPrefs.HasKey(musicMixerSfx))
        {
            if (currFXVolume != null)
            {
                foreach (var item in currFXVolume)
                {
                    item.value = PlayerPrefs.GetFloat(musicMixerSfx);
                    mainMixer.SetFloat(musicMixerSfx, Mathf.Log10(item.value) * 20);
                }
            }
        }
        else
        {

            if (currFXVolume != null)
            {
                foreach (var item in currFXVolume)
                {
                    //item.value = soundsManager.Instance.GetOfflineVolume();
                    PlayerPrefs.SetFloat(musicMixerSfx, item.value);
                }
            }
        }

        if (PlayerPrefs.HasKey(musicMixerAmbient))
        {
            if (currAmbienceVolume != null)
            {
                currAmbienceVolume.value = PlayerPrefs.GetFloat(musicMixerAmbient);
                mainMixer.SetFloat(musicMixerAmbient, Mathf.Log10(currAmbienceVolume.value) * 20);
            }
        }
        else
        {
            if (currAmbienceVolume != null)
            {
              PlayerPrefs.SetFloat(musicMixerAmbient, currAmbienceVolume.value);
               
            }
        }
        //Dark or Light Mode
        if (PlayerPrefs.HasKey("DarkMode"))
		{
            if(PlayerPrefs.GetInt("DarkMode") == 1)
                darkMode = true;
            else if(PlayerPrefs.GetInt("DarkMode") == 0)
                darkMode= false;
		}

        if (PlayerPrefs.HasKey("TimeDisplay"))
        {
            if (PlayerPrefs.GetInt("TimeDisplay") == 1)
                showTimeInWords = true;
            else if (PlayerPrefs.GetInt("TimeDisplay") == 0)
                showTimeInWords = false;
        }
        
    }

    public void UpdateSliderOutput()
    {
        soundsManager.Instance.SetOfflineVolume(masterVolume.value);
    }

    public void SetMasterLevel(float v)
    {
        //masterVolume.value = v;
        mainMixer.SetFloat(musicMixerMaster, Mathf.Log10(v) * 20);

        if (masterVolume != null)
            PlayerPrefs.SetFloat(musicMixerMaster, masterVolume.value);

    }
    public void SetMainBGLevel(float v)
    {
        //masterVolume.value = v;
        mainMixer.SetFloat(musicMixerMainMusic, Mathf.Log10(v) * 20);

        if (currMusicPlayerVolume != null)
        {
            foreach (var item in currMusicPlayerVolume)
            {
                PlayerPrefs.SetFloat(musicMixerMainMusic, item.value);
                item.value = PlayerPrefs.GetFloat(musicMixerMainMusic);
            }
        }
      

        //UpdateSliderOutput();

        
    }
    public void SetAmbienceLevel(float v)
    {
        //masterVolume.value = v;
        mainMixer.SetFloat(musicMixerAmbient, Mathf.Log10(v) * 20);

        if(currAmbienceVolume != null)
          PlayerPrefs.SetFloat(musicMixerAmbient, currAmbienceVolume.value);

       // UpdateSliderOutput();


    }
    public void SetFXLevel(float v)
    {
        mainMixer.SetFloat(musicMixerSfx, Mathf.Log10(v) * 20);

        if (currFXVolume != null)
        {
            foreach (var item in currFXVolume)
            {
                PlayerPrefs.SetFloat(musicMixerSfx, item.value);
                item.value = PlayerPrefs.GetFloat(musicMixerSfx);
            }
        }
        /*foreach (var item in currFXVolume)
        {
            item.value = v;
        }*/
        //UpdateSliderOutput();

    }
	#endregion

	#region UI And Stuff
    public void ToggleGameObject(GameObject obj)
	{

        if (obj.activeInHierarchy)
        {
            if(obj.GetComponent<TweeningUI>() != null)
                obj.GetComponent<TweeningUI>().Disable();
            else
                obj.SetActive(false);
        }
        else
            obj.SetActive(true);

	}
	public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game... Consider Saving here");
    }

	#endregion

	#region Public & Private Audio Variables

	public bool GetMusicMode()
	{
        return offlineMode;
	}

    public bool SetMusicMode(bool v)
	{
        offlineMode = v;
        return offlineMode;
	}
	#endregion

	#region Public & Private Timer Related Settings
    public bool GetTransition()
	{
        return autoTransitionTimer;
	}

    public void SetTransition(bool v)
	{
        autoTransitionTimer = v;
	}
    public bool GetRestarted()
    {
        return timerHasRestarted;
    }

    public void SetRestarted(bool v)
    {
        timerHasRestarted = v;
    }
    public bool GetTimeDisplay()
    {
        return showTimeInWords;
    }

    public void SetTimeDisplay(bool v)
    {
        showTimeInWords = v;

        if (showTimeInWords)
            PlayerPrefs.SetInt("TimeDisplay", 1);
        else
            PlayerPrefs.SetInt("TimeDisplay", 0);
    }
    public bool GetFirstTime()
    {
        return firstTimeOpening;
    }

    public void SetFirstTime(bool v)
    {
        firstTimeOpening = v;
    }
    public bool GetTimeState()
	{
        return timeIsPaused;
	}

    public void SetTimeState(bool v)
	{
        timeIsPaused = v;
	}
	#endregion

	#region Public & Private Screen Related Settings

    public void SetScreenMode()
	{
        fullScreen = !fullScreen;

        if (fullScreen)
            fullScreenImg.sprite = maximiseSpr;
        else
            fullScreenImg.sprite = minimiseSpr;

        Screen.fullScreen = fullScreen;
	}
	#endregion

	#region Public & Private General Settings
    public void SetTaskMode(bool v)
	{
        checkMarkDestroys = v;
	}

    public bool GetTaskMode()
	{
        return checkMarkDestroys;
	}

    public void SetTheme(bool v)
	{
        darkMode = v;

        if(darkMode)
            PlayerPrefs.SetInt("DarkMode", 1);
        else
            PlayerPrefs.SetInt("DarkMode", 0);

        CheckThemeMode();
	}

    void CheckThemeMode()
	{
        foreach (var item in themes)
        {
            //set light mode
            if (!darkMode)
            {
                item.themeObject.color = lightTheme;

                if (item.GetComponent<DragWindow>())
                    item.GetComponent<DragWindow>().SetImages();
            }

            //set dark mode
            if (darkMode)
            {
                item.themeObject.color = darkTheme;

                if (item.GetComponent<DragWindow>())
                    item.GetComponent<DragWindow>().SetImages();
            }
        }
    }

    public void EnableTheme(ThemeGameObject item)
	{
        //set light mode
        if (!darkMode)
        {
            item.themeObject.color = lightTheme;
        }

        //set dark mode
        if (darkMode)
        {
            item.themeObject.color = darkTheme;
        }
    }
    public void AddToList(ThemeGameObject obj)
    {
        // obj.SetTaskInfo(ta, index);
        if(!themes.Contains(obj))
            themes.Add(obj);
    }
    #endregion

}

