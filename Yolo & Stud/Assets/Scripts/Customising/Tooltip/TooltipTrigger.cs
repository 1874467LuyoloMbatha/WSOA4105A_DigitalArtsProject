using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Generic Elements")]
	[Tooltip("We use these string to state what elements will do")]
	[SerializeField] string content;
	[SerializeField] string header;

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		TooltipSystem.Show(content, header);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		TooltipSystem.Hide();
	}

	public void ShowToolTip()
	{
		TooltipSystem.Show(content, header);
	}

	public void HideToolTip()
	{
		TooltipSystem.Hide();
	}
}
