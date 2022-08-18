using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [Header("Singleton")]
    private static TooltipSystem currentIns;

	[Header("External Assets")]
	[SerializeField] TooltipUI toolTip;

	private void Awake()
	{
		currentIns = this;
		Hide();
	}
	
	public static void Show(string content, string header = "")
	{
		currentIns.toolTip.SetText(content, header);
		currentIns.toolTip.gameObject.SetActive(true);
	}

	public static void Hide()
	{
		currentIns.toolTip.gameObject.SetActive(false);
	}
}
