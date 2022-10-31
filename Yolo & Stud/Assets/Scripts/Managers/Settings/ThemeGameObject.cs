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

    [Header("Booleans")]
    [Tooltip("Enable of objects that should be off when app starts")]
    [SerializeField] bool shouldBeOff;
    void Start()
    {
        SettingsMenu.Instance.AddToList(this);

        

       // gameObject.SetActive(shouldBeOff);
    }

	private void OnEnable()
	{
        SettingsMenu.Instance.AddToList(this);

        if (themeObject == null && GetComponent<Image>())
            themeObject = GetComponent<Image>();

        SettingsMenu.Instance.EnableTheme(this);

        if (GetComponent<DragWindow>())
            GetComponent<DragWindow>().SetImages();
    }

}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/