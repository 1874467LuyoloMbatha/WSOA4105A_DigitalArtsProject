using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using StudYolo.Blendshapes;

public class MaterialChangeManager : Singleton<MaterialChangeManager>
{
	[Header("Tootip")]
	[Tooltip("Drag The Main Parent Here!")]
	[SerializeField] GameObject colourPickerParent;
	[SerializeField] GameObject skinHolder, colourHolder, tempButton;

	[Header("External Handles")]
    [SerializeField] matChange wall;
	[SerializeField] matChange bed, couch, carpet;

	[Header("Sibu's Materials")]
	[SerializeField] SibuMatChange skin;
	[SerializeField] SibuMatChange eyes, mainHair, sideHair;
	[SerializeField] SibuMatChange hoodie, hoodiePatch, shorts, socks, shoes;

	[Header("Unity Mat Changing Slider")]
    [Tooltip("Drag the wall's slider here")]
    [SerializeField] Slider wallMatChangeSlider;
	[SerializeField] Slider bedMatChangeSlider, couchMatChangeSlider;
	[SerializeField] Slider carpetMatChangeSlider;

	[Header("Colour Picker")]
	[SerializeField] Text colourPickerHeader;
	[SerializeField] int skinIndex, eyesIndex, mainhairIndex, sideHairIndex;
	[SerializeField] int hoodieIndex, hoodiePatchIndex, shortsIndex, socksIndex, shoesIndex;
	[SerializeField] string header;
	void Start()
    {
		SetPlayerPrefs();
		SetUpSliders();

		SetUpSibuPrefs();
    }

	#region Setting Up
	void SetUpSliders()
    {
		if (wallMatChangeSlider != null)
		{
			wallMatChangeSlider.maxValue = wall.materialsNeeded.Length - 1;
			wallMatChangeSlider.onValueChanged.AddListener(value => UpdateWallSlider((int)value));
		}
		if (bedMatChangeSlider != null)
		{
			bedMatChangeSlider.maxValue = bed.materialsNeeded.Length - 1;
			bedMatChangeSlider.onValueChanged.AddListener(value => UpdateBedSlider((int)value));
		}
		if (couchMatChangeSlider != null)
		{
			couchMatChangeSlider.maxValue = couch.materialsNeeded.Length - 1;
			couchMatChangeSlider.onValueChanged.AddListener(value => UpdateCouchSlider((int)value));
		}
		if (carpetMatChangeSlider != null)
		{
			carpetMatChangeSlider.maxValue = carpet.materialsNeeded.Length - 1;
			carpetMatChangeSlider.onValueChanged.AddListener(value => UpdateCarpetSlider((int)value));
		}
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

	public void SetUpSibuPrefs()
	{

	}

	#endregion

	#region Public Slider functions
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
	#endregion

	#region Public Button Functions
	public void SkinButtonLogic()
	{
		header = "Skin Tone";
		colourPickerHeader.text = header;
		colourPickerParent.SetActive(true);
		skinHolder.SetActive(true);	
		colourHolder.SetActive(false);
	}

	public void SibuMatButtonLogic(string v)
	{
		header = v;
		colourPickerHeader.text = header;
		colourPickerParent.SetActive(true);
		skinHolder.SetActive(false);
		colourHolder.SetActive(true);
	}
	public void SetSkinColour(int v)
	{
		skinIndex = v;

		foreach (var item in skin.materialsToChange)
		{
			item.color = skin.colorsToChange[skinIndex];
		}
		PlayerPrefs.SetInt(skin.bodyType, skinIndex);
	}

	public void SetEyesColour(int v)
	{
		eyesIndex = v;

		foreach (var item in eyes.materialsToChange)
		{
			item.color = eyes.colorsToChange[eyesIndex];
		}
		PlayerPrefs.SetInt(eyes.bodyType, eyesIndex);
	}

	public void SetMainHairColour(int v)
	{
		mainhairIndex = v;

		foreach (var item in mainHair.materialsToChange)
		{
			item.color = mainHair.colorsToChange[mainhairIndex];
		}
		PlayerPrefs.SetInt(mainHair.bodyType, mainhairIndex);
	}

	public void SetSideHairColour(int v)
	{
		sideHairIndex = v;

		foreach (var item in sideHair.materialsToChange)
		{
			item.color = sideHair.colorsToChange[sideHairIndex];
		}
		PlayerPrefs.SetInt(sideHair.bodyType, sideHairIndex);
	}

	public void SetHoodieColour(int v)
	{
		hoodieIndex = v;

		foreach (var item in hoodie.materialsToChange)
		{
			item.color = hoodie.colorsToChange[hoodieIndex];
		}
		PlayerPrefs.SetInt(hoodie.bodyType, hoodieIndex);
	}

	public void SetHoodiePatchColour(int v)
	{
		hoodiePatchIndex = v;

		foreach (var item in hoodiePatch.materialsToChange)
		{
			item.color = hoodiePatch.colorsToChange[hoodiePatchIndex];
		}
		PlayerPrefs.SetInt(hoodiePatch.bodyType, hoodiePatchIndex);
	}

	public void SetShortColour(int v)
	{
		shortsIndex = v;

		foreach (var item in shorts.materialsToChange)
		{
			item.color = shorts.colorsToChange[shortsIndex];
		}
		PlayerPrefs.SetInt(shorts.bodyType, shortsIndex);
	}

	public void SetSocksColour(int v)
	{
		socksIndex = v;

		foreach (var item in socks.materialsToChange)
		{
			item.color = socks.colorsToChange[socksIndex];
		}
		PlayerPrefs.SetInt(socks.bodyType, socksIndex);
	}

	public void SetShoesColour(int v)
	{
		shoesIndex = v;

		foreach (var item in shoes.materialsToChange)
		{
			item.color = shoes.colorsToChange[shoesIndex];
		}
		PlayerPrefs.SetInt(shoes.bodyType, shoesIndex);
	}

	public void SetColour(int v)
	{
		if(tempButton.GetComponent<RenameObject>().index == 1)
			SetEyesColour(v);
		if (tempButton.GetComponent<RenameObject>().index == 2)
			SetMainHairColour(v);
		if (tempButton.GetComponent<RenameObject>().index == 3)
			SetSideHairColour(v);
		if (tempButton.GetComponent<RenameObject>().index == 4)
			SetHoodieColour(v);
		if (tempButton.GetComponent<RenameObject>().index == 5)
			SetHoodiePatchColour(v);
		if (tempButton.GetComponent<RenameObject>().index == 6)
			SetShoesColour(v);
		if (tempButton.GetComponent<RenameObject>().index == 7)
			SetSocksColour(v);
		if (tempButton.GetComponent<RenameObject>().index == 8)
			SetShoesColour(v);
	}
	#endregion

	public void SetTempButton(GameObject v)
	{
		tempButton = v;
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/