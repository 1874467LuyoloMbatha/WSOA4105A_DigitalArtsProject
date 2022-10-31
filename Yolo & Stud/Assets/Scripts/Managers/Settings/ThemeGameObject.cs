using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ThemeGameObject : MonoBehaviour
{
	[Header("Unity Handles")]
	[Tooltip("Drag Image component from this object to change its colour")]
    public Image themeObject;
   
    void Start()
    {
        SettingsMenu.Instance.AddToList(this);

        if (themeObject == null)
            themeObject = GetComponent<Image>();
    }


}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/