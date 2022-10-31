using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[Header("Generic Elements")]
	[Tooltip("We use these string to state what elements will do")]
	[SerializeField] string content;
	[SerializeField] string header;

	[Header("Offset")]
	[SerializeField] float xOffset = 0.9f, yOffset = 0.5f;

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		if (this.GetComponent<InteractableObject>().chooseType == InteractableObject.TypeOfInteractble.InteractableObject)
			TooltipSystem.Show(content, header);
		else
		{
			this.GetComponent<InteractableObject>().HoverObj().GetComponent<TooltipUI>().xOffset = xOffset;
			this.GetComponent<InteractableObject>().HoverObj().GetComponent<TooltipUI>().yOffset = yOffset;
			this.GetComponent<InteractableObject>().Show(content, header);
			//this.GetComponent<InteractableObject>().Position(this.transform.position, xOffset, yOffset);
		}

		//TooltipSystem.Position(this.GetComponent<InteractableObject>().positionCurrent(), xOffset, yOffset);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		TooltipSystem.Hide();
		this.GetComponent<InteractableObject>().StopHighlight();
	}

	public void ShowToolTip()
	{
		TooltipSystem.Show(content, header);
	}

	public void HideToolTip()
	{
		TooltipSystem.Hide();
	}

	private void OnMouseOver()
	{
		ShowToolTip();
		if (this.GetComponent<InteractableObject>().chooseType == InteractableObject.TypeOfInteractble.InteractableObject)
			GetComponent<InteractableObject>().StartHighlight();
		else
		{
			//	TooltipSystem.Position(this.GetComponent<InteractableObject>().positionCurrent(), xOffset, yOffset);
			//TooltipSystem.Position(this.transform.position, xOffset, yOffset);
			this.GetComponent<InteractableObject>().Show(content, header);
		}
	}

	private void OnMouseExit()
	{
		HideToolTip();
		if (this.GetComponent<InteractableObject>().chooseType == InteractableObject.TypeOfInteractble.InteractableObject)
			GetComponent<InteractableObject>().StopHighlight();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		this.GetComponent<InteractableObject>().StopHighlight();
	}
}
