using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ColourPicker : Singleton<ColourPicker>
{
	#region Variablws
	[Header("Unity Handles")]
	[SerializeField] RectTransform textureRect;
	[SerializeField] GameObject objectToChange;
	[SerializeField] Texture2D refSprite;

	[Header("Colour")]
	[SerializeField] Color storedColour;

	#endregion

	//Assign this method im the inspector
	public void PickColour()
	{
		SetColour();
	}
	public void SetColour()
	{
		Vector3 colourImagePos = textureRect.position;
		float globalPosX = Input.mousePosition.x - colourImagePos.x;
		float globalPosY = Input.mousePosition.y - colourImagePos.y;

		int localPosX = (int)(globalPosX * (refSprite.width / textureRect.rect.width));
		int localPosY = (int)(globalPosY * (refSprite.height / textureRect.rect.height));

		Color color = refSprite.GetPixel(localPosX, localPosY);
		SetTheColour(color);
	}

	void SetTheColour(Color c)
	{
		objectToChange.GetComponent<Renderer>().material.color = c;
		storedColour = c;
	}

	public void SetObjectToChange(GameObject obj)
	{
		objectToChange = obj;
	}

	public Color GetColour()
	{
		return storedColour;
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/