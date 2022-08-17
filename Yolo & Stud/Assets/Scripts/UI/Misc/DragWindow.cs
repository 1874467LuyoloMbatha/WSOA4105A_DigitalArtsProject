using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Credits due to Code Monkey on Youtube: https://youtu.be/Mb2oua3FjZg
/// </summary>


//Place in the game object you wwant to be clicked in order to drag
public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
	[Header("Unity Handles")]
	[Tooltip("Drag in the game object parent/window you want to move with this object")]
	[SerializeField] RectTransform rectTransform;
	[Tooltip("Drag the canvas!")]
	[SerializeField] RectTransform canvasRectTransform;
	[Tooltip("Drag the canvas AGAIN!")]
	[SerializeField] Canvas canvas;
	[Tooltip("This gameobject will be the one that will change colour")]
	[SerializeField] Image bgImage;

	[Header("Variables")]
	[SerializeField] Color bgColor;
	[SerializeField] float bgAlpha;
	Vector3 defaultPos;

	[Header("Screen Clamp Variables")]
	[SerializeField] Vector2 screenBounds;
	[SerializeField] float objWidth, objHeight;
	private void Awake()
	{
		bgColor = bgImage.color;
		bgAlpha = bgColor.a;

		defaultPos =  rectTransform.anchoredPosition;
	}

	private void Start()
	{
		//Get screen bounds
		//screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		//objWidth = rectTransform.rect.width / 2;
		//objHeight = rectTransform.rect.height / 2;
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		bgColor.a = bgColor.a / 2f;
		bgImage.color = bgColor;
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Takes mouse possition and make sthe indo follow it
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

		//limit window to screen
		Vector2 anchorPos = rectTransform.anchoredPosition;

		/*Vector2 localPoint = rectTransform.transform.position;
		localPoint.x = Mathf.Clamp(localPoint.x, screenBounds.x + objWidth, screenBounds.x * -1 - objWidth);
		localPoint.y = Mathf.Clamp(localPoint.y, screenBounds.y + objHeight, screenBounds.y * -1 - objHeight);
		rectTransform.transform.position = localPoint;
		*/

		//Stop Fro going to the right
		if (anchorPos.x + rectTransform.rect.width > canvasRectTransform.rect.width)
		{
			Debug.Log("right");
			anchorPos.x = canvasRectTransform.rect.width - rectTransform.rect.width;
		}
		//Stop going up
		if (anchorPos.y + rectTransform.rect.height > canvasRectTransform.rect.height)
		{
			Debug.Log("up");
			anchorPos.y = canvasRectTransform.rect.height - rectTransform.rect.height;
		}

		rectTransform.anchoredPosition = anchorPos;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		bgColor.a = bgAlpha;
		bgImage.color = bgColor;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		rectTransform.SetAsLastSibling();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			//Reset Pos
			rectTransform.anchoredPosition = defaultPos;
		}
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/