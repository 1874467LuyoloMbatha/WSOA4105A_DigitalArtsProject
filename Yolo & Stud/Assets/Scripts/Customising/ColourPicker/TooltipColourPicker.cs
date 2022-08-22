using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TooltipColourPicker : MonoBehaviour
{
	[Header("Unity Handles")]
	[SerializeField] Text header;
	[SerializeField] LayoutElement layoutElement;
	[SerializeField] RectTransform rect;
	[Tooltip("Drag The Main Parent Here!")]
	[SerializeField] GameObject colourPickerParent;

	[Header("Integers")]
	[SerializeField] int wrapLimit;

	[Header("Offset")]
	[SerializeField] float xOffset, yOffset;

	[Header("Booleans")]
	[Tooltip("Thgis will make it possible for the player to move the object")]
	[SerializeField] bool stopMoving;
	private void Awake()
	{
		rect = GetComponent<RectTransform>();
	}
	public void SetColour(string Header = "")
	{
		if (string.IsNullOrEmpty(Header))
			header.gameObject.SetActive(false);
		else
		{ 
			header.gameObject.SetActive(true);
			header.text = Header;
		}

		int headerLength = header.text.Length;

		if (headerLength > wrapLimit)
			layoutElement.enabled = true;
		else
			layoutElement.enabled = false;
	}

	public bool SetMoving(bool v)
	{
		stopMoving = v;
		return stopMoving;
	}

	public void EnableDisableObject(bool v)
	{
		colourPickerParent.SetActive(v);
	}
	private void Update()
	{
		if (Application.isEditor)
		{ 
			int headerLength = header.text.Length;

			if (headerLength > wrapLimit)
				layoutElement.enabled = true;
			else
				layoutElement.enabled = false;
		}

		if (!stopMoving)
		{
			Vector2 MousePos = Input.mousePosition;

			float pivotX = MousePos.x / Screen.width + xOffset;
			float pivotY = MousePos.y / Screen.height + yOffset;

			rect.pivot = new Vector2(pivotX, pivotY);
			transform.position = MousePos;
		}
	}
}
