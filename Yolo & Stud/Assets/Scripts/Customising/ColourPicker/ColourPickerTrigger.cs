using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColourPickerTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Generic Elements")]
	[Tooltip("We use these string to state what elements will do")]
	[SerializeField] string header;

	[Header("Booleans")]
	[Tooltip("This will allow the player to disable the colour picker window")]
	[SerializeField] bool canEscape = false;
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		TooltipSystem.ShowColourPicker(header);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		TooltipSystem.Hide();
	}

	public void ShowToolTip()
	{
		TooltipSystem.ShowColourPicker(header);
	}

	public void HideToolTip()
	{
		TooltipSystem.HideColourPicker();
	}

	private void OnMouseUp()
	{
		ShowToolTip();
		canEscape = true;
		GetComponent<InteractableObject>().StopHighlight();
	}

	private void OnMouseOver()
	{
		GetComponent<InteractableObject>().StartHighlight();
	}

	private void Update()
	{
		if (canEscape && Input.GetKeyDown(KeyCode.Escape))
		{
			HideToolTip();
			canEscape = false;
			GetComponent<InteractableObject>().StopHighlight();
		}
	}
}
