using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem>	
{
	[Header("External Assets")]
	[SerializeField] TooltipUI toolTip;
	[SerializeField] TooltipColourPicker colourPicker;

	private void Awake()
	{
		Hide();
	}
	
	public static void Show(string content, string header = "")
	{
		Instance.toolTip.SetText(content, header);
		Instance.toolTip.gameObject.SetActive(true);
	}

	public static void ShowColourPicker(string header = "")
	{
		Instance.colourPicker.SetMoving(true);
		Instance.colourPicker.SetColour(header);
		Instance.colourPicker.EnableDisableObject(true);
		//Instance.colourPicker.gameObject.SetActive(true);
	}


	public static void Hide()
	{
		Instance.toolTip.gameObject.SetActive(false);

	}

	public static void HideColourPicker()
	{
		Instance.colourPicker.SetMoving(false);
		Instance.colourPicker.EnableDisableObject(false);
	}
}
