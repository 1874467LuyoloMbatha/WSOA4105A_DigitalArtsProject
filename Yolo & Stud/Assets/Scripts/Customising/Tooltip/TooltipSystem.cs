using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem>	
{
	[Header("External Assets")]
	[SerializeField] TooltipUI toolTip;
	[SerializeField] TooltipColourPicker colourPicker;

	[Header("Temp GameObject")]
	[Tooltip("This will just temporarily store the currently active game object")]
	[SerializeField] GameObject tempGameObject;

	private void Awake()
	{
		Hide();
	}
	
	public static void Show(string content, string header = "")
	{
		Instance.toolTip.SetText(content, header);
		Instance.toolTip.gameObject.SetActive(true);
	}
	public static void Position(Vector3 pos, float xPOffset, float yOffset)
	{
		//RectTransform rectTrans = Instance.toolTip.gameObject.GetComponent<RectTransform>();
		Instance.toolTip.gameObject.transform.position = new Vector3(pos.x + xPOffset, pos.y + yOffset);

	}
	public static void ShowColourPicker(string header = "")
	{
		//Disable Previous Temporary Object
		if(Instance.tempGameObject != null)
		{
			Instance.tempGameObject.GetComponent<InteractableObject>().IsSelected(false);
			Instance.tempGameObject.GetComponent<ColourPickerTrigger>().canEscape = false;
			Instance.tempGameObject = null;
		}

		Instance.colourPicker.SetMoving(true);
		Instance.colourPicker.SetColour(header);
		Instance.colourPicker.EnableDisableObject(true);
		//Instance.colourPicker.gameObject.SetActive(true);
	}

	public GameObject TempGameObject(GameObject temp)
	{
		tempGameObject = temp;
		return tempGameObject;
	}

	public static void Hide()
	{
		Instance.toolTip.gameObject.SetActive(false);
	}

	public static void HideColourPicker()
	{
		if (Instance.tempGameObject != null)
			Instance.tempGameObject = null;


		Instance.colourPicker.SetMoving(false);
		Instance.colourPicker.EnableDisableObject(false);
	}
}
