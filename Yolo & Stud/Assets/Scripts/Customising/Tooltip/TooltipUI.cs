using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TooltipUI : MonoBehaviour
{
	[Header("Unity Handles")]
	[SerializeField] Text header;
	[SerializeField] Text contentField;
	[SerializeField] LayoutElement layoutElement;
	[SerializeField] RectTransform rect;

	[Header("Integers")]
	[SerializeField] int wrapLimit;

	[Header("Offset")]
	public float xOffset, yOffset;

	private void Awake()
	{
		rect = GetComponent<RectTransform>();
	}
	public void SetText(string content, string Header = "")
	{
		if (string.IsNullOrEmpty(Header))
			header.gameObject.SetActive(false);
		else
		{ 
			header.gameObject.SetActive(true);
			header.text = Header;
		}

		contentField.text = content;

		int headerLength = header.text.Length;
		int contentLength = contentField.text.Length;

		if (headerLength > wrapLimit || contentLength > wrapLimit)
			layoutElement.enabled = true;
		else
			layoutElement.enabled = false;
	}
	private void Update()
	{
		if (Application.isEditor)
		{ 
			int headerLength = header.text.Length;
			int contentLength = contentField.text.Length;

			if (headerLength > wrapLimit || contentLength > wrapLimit)
				layoutElement.enabled = true;
			else
				layoutElement.enabled = false;
		}

		Vector2 MousePos = Input.mousePosition;

		float pivotX = MousePos.x / Screen.width + xOffset;
		float pivotY = MousePos.y / Screen.height + yOffset;

		rect.pivot = new Vector2(pivotX, pivotY);
		transform.position = MousePos;
	}
}
