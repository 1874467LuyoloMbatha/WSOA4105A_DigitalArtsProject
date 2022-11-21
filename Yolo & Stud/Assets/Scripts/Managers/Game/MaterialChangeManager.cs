using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using StudYolo.Blendshapes;

public class MaterialChangeManager : MonoBehaviour
{
    [Header("External Handles")]
    [SerializeField] matChange wall;
	[SerializeField] matChange bed, couch, sibuClothing, carpet;

	[Header("Unity Handles")]
    [Tooltip("Drag the wall's slider here")]
    [SerializeField] Slider wallMatChangeSlider;
	[SerializeField] Slider bedMatChangeSlider, couchMatChangeSlider, sibuClothingMatChangeSlider;
	[SerializeField] Slider carpetMatChangeSlider;

	void Start()
    {
        SetUpSliders();
		SetPlayerPrefs();
    }

	#region Setting Up
	void SetUpSliders()
    {
		if (wallMatChangeSlider != null)
			wallMatChangeSlider.onValueChanged.AddListener(value => UpdateWallSlider((int)value));

		if (bedMatChangeSlider != null)
			bedMatChangeSlider.onValueChanged.AddListener(value => UpdateBedSlider((int)value));
		
        if (couchMatChangeSlider != null)
			couchMatChangeSlider.onValueChanged.AddListener(value => UpdateCouchSlider((int)value));

		if (sibuClothingMatChangeSlider != null)
			sibuClothingMatChangeSlider.onValueChanged.AddListener(value => UpdateClothingSlider((int)value));

		if (carpetMatChangeSlider != null)
			carpetMatChangeSlider.onValueChanged.AddListener(value => UpdateCarpetSlider((int)value));

	}

	public void SetPlayerPrefs()
	{
		if (PlayerPrefs.HasKey(wall.objectName))
		{
			if (wallMatChangeSlider != null)
			{
				wallMatChangeSlider.value = PlayerPrefs.GetInt(wall.objectName);
				wallMatChangeSlider.maxValue = wall.materialsNeeded.Length - 1;

				foreach (var item in wall.objectType)
				{
					item.material = wall.materialsNeeded[PlayerPrefs.GetInt(wall.objectName)];
				}
			}
		}

		if (PlayerPrefs.HasKey(bed.objectName))
		{
			if (bedMatChangeSlider != null)
			{
				bedMatChangeSlider.value = PlayerPrefs.GetInt(bed.objectName);
				bedMatChangeSlider.maxValue = bed.materialsNeeded.Length - 1;

				foreach (var item in bed.objectType)
				{
					item.material = bed.materialsNeeded[PlayerPrefs.GetInt(bed.objectName)];
				}
			}
		}

		if (PlayerPrefs.HasKey(couch.objectName))
		{
			if (couchMatChangeSlider != null)
			{
				couchMatChangeSlider.value = PlayerPrefs.GetInt(couch.objectName);
				couchMatChangeSlider.maxValue = couch.materialsNeeded.Length - 1;

				foreach (var item in couch.objectType)
				{
					item.material = couch.materialsNeeded[PlayerPrefs.GetInt(couch.objectName)];
				}
			}
		}

		if (PlayerPrefs.HasKey(sibuClothing.objectName))
		{
			if (sibuClothingMatChangeSlider != null)
			{
				sibuClothingMatChangeSlider.value = PlayerPrefs.GetInt(sibuClothing.objectName);
				sibuClothingMatChangeSlider.maxValue = sibuClothing.materialsNeeded.Length - 1;

				foreach (var item in sibuClothing.objectType)
				{
					item.material = sibuClothing.materialsNeeded[PlayerPrefs.GetInt(sibuClothing.objectName)];
				}
			}
		}

		if (PlayerPrefs.HasKey(carpet.objectName))
		{
			if (carpetMatChangeSlider != null)
			{
				carpetMatChangeSlider.value = PlayerPrefs.GetInt(carpet.objectName);
				carpetMatChangeSlider.maxValue = carpet.materialsNeeded.Length - 1;

				foreach (var item in carpet.objectType)
				{
					item.material = carpet.materialsNeeded[PlayerPrefs.GetInt(carpet.objectName)];
				}
			}
		}
	}
	#endregion

	#region Public functions
	public void UpdateWallSlider(int v)
    {
		foreach (var item in wall.objectType)
		{
			item.material = wall.materialsNeeded[v];
		}

		PlayerPrefs.SetInt(wall.objectName, v);
    }

	public void UpdateBedSlider(int v)
	{
		foreach (var item in bed.objectType)
		{
			item.material = bed.materialsNeeded[v];
		}

		PlayerPrefs.SetInt(bed.objectName, v);
	}

	public void UpdateCouchSlider(int v)
	{
		foreach (var item in couch.objectType)
		{
			item.material = couch.materialsNeeded[v];
		}

		PlayerPrefs.SetInt(couch.objectName, v);
	}

	public void UpdateCarpetSlider(int v)
	{
		foreach (var item in carpet.objectType)
		{
			item.material = carpet.materialsNeeded[v];
		}

		PlayerPrefs.SetInt(carpet.objectName, v);
	}

	public void UpdateClothingSlider(int v)
	{
		foreach (var item in sibuClothing.objectType)
		{
			item.material = sibuClothing.materialsNeeded[v];
		}

		PlayerPrefs.SetInt(sibuClothing.objectName, v);
	}
	#endregion

}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/