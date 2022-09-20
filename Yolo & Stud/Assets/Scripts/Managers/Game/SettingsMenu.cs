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
    public Slider[] masterVolume;
    public Slider[] currFXVolume;


    [Header("Accessibility Settings")]
    public Dropdown[] languageDropDown;

    [Header("Text Specific Settings")]
    [Tooltip("Every Text element that will be changed/manipulated")]
    public Text[] fontTexts;
    public Slider[] fontTextsSizes;


	[Header("Audio Settings")]
	[SerializeField] bool offlineMode = true;
	/// <summary>
	/// allows player to change volume of music in settings menu through slider
	/// </summary>
	/// 
	private void Start()
	{
        UpdateSliderOutput();
	}

    #region Audio & Music

    void SetUpPrefs()
    {
        if (PlayerPrefs.HasKey("Master"))
        {
            foreach (var item in masterVolume)
            {
                item.value = PlayerPrefs.GetFloat("Master");
            }

        }

        if (PlayerPrefs.HasKey("MainBGMusic"))
        {
           
        }

        if (PlayerPrefs.HasKey("FX"))
        {
            foreach (var item in currFXVolume)
            {
                item.value = PlayerPrefs.GetFloat("FX");
            }
        }
    }

    public void UpdateSliderOutput()
    {


    }
    public void SetMainBGLevel(float v)
    {

        UpdateSliderOutput();

        PlayerPrefs.SetFloat("MainBGMusic", v);
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game... Consider Saving here");
    }

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
}

