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
    [Header("Volume Sliders")]
    public Slider masterVolume;
    public Slider[] currFXVolume;

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

    [Header("Timer Settings")]
    [SerializeField] bool autoTransitionTimer = true;
    [SerializeField] bool showTimeInWords = false;
    [SerializeField] bool timeIsPaused;
	/// <summary>
	/// allows player to change volume of music in settings menu through slider
	/// </summary>
	/// 
	private void Start()
	{
        SetUpPrefs();
        UpdateSliderOutput();

        Screen.fullScreen = fullScreen;
	}

    #region Audio & Music

    void SetUpPrefs()
    {
        if (PlayerPrefs.HasKey("MainBGMusic"))
        {
            masterVolume.value = PlayerPrefs.GetFloat("MainBGMusic");
            soundsManager.Instance.SetOfflineVolume(masterVolume.value);
        }
        else
        {
            masterVolume.value = soundsManager.Instance.GetOfflineVolume();
            PlayerPrefs.SetFloat("MainBGMusic", masterVolume.value);
        }


        if (PlayerPrefs.HasKey("FX"))
        {
            foreach (var item in currFXVolume)
            {
                item.value = PlayerPrefs.GetFloat("FX");
            }
        }

        masterVolume.value = soundsManager.Instance.GetOfflineVolume();
    }

    public void UpdateSliderOutput()
    {
        soundsManager.Instance.SetOfflineVolume(masterVolume.value);
    }
    public void SetMainBGLevel(float v)
    {
        masterVolume.value = v;
        PlayerPrefs.SetFloat("MainBGMusic", masterVolume.value);

        UpdateSliderOutput();

        
    }

    public void SetFXLevel(float v)
    {
        foreach (var item in currFXVolume)
        {
            item.value = v;
        }
        UpdateSliderOutput();

        PlayerPrefs.SetFloat("FX", v);
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

    public bool GetTimeDisplay()
    {
        return showTimeInWords;
    }

    public void SetTimeDisplay(bool v)
    {
        showTimeInWords = v;
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
}

