using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColourPickerTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Generic Elements")]
	[Tooltip("We use these string to state what elements will do")]
	[SerializeField] string header = "Material Colour";

	[Header("Booleans")]
	[Tooltip("This will allow the player to disable the colour picker window")]
	public bool canEscape = false;
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
	//Store Temp gameObject
	GameObject temp;

	private void OnMouseUp()
	{
		ShowToolTip();

		temp = TooltipSystem.Instance.TempGameObject(this.gameObject);
		temp.GetComponent<InteractableObject>().IsSelected(true);
		ColourPicker.Instance.SetObjectToChange(temp);

		canEscape = true;
		temp.GetComponent<InteractableObject>().StopHighlight();
	}

	private void OnMouseOver()
	{
		//Disabled highlighting when player has already clicked on the object
		if(!canEscape)
			GetComponent<InteractableObject>().StartHighlight();
	}

	private void OnMouseExit()
	{
		GetComponent<InteractableObject>().StopHighlight();
	}

	private void Update()
	{
		if (canEscape && Input.GetKeyDown(KeyCode.Escape))
		{
			HideToolTip();
			canEscape = false;
			GetComponent<InteractableObject>().ChangeColour(ColourPicker.Instance.GetColour());
			GetComponent<InteractableObject>().StopHighlight();

			GetComponent<InteractableObject>().IsSelected(false);
			temp = null;
		}
	}
}
