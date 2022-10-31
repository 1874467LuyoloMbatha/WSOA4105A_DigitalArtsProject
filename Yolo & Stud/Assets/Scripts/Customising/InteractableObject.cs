using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public enum TypeOfInteractble { InteractableObject, UI };

	[Header("Choose What This Is For")]
	[Tooltip("Is it for UI elements or in game objects?")]
	public TypeOfInteractble chooseType;

    [Header("Unity Handles")]
    [SerializeField] Vector3 startingPos;
	[SerializeField] Vector3 currentPos;
    [SerializeField] Quaternion startRot;

    [Header("Interacting Values")]
    [SerializeField] Material defaultMat;
    [SerializeField] Material highlightMat;
	[SerializeField] GameObject hoverObj;

	[Header("Floats")]
	[SerializeField] float moveSpeed = 5f;

	[Header("Booleans")]
	[Tooltip("Only enable for objects that will change colour")]
	[SerializeField] bool isColourPicker = false;
	[Tooltip("Will make it possible to check which object is selected")]
	[SerializeField] bool isSelected = false;
	[Tooltip("For Objects that can be moved")]
	[SerializeField] bool canBeMoved = false;

	private void Awake()
	{
		if(defaultMat == null && chooseType == TypeOfInteractble.InteractableObject)
			defaultMat = gameObject.GetComponent<Renderer>().sharedMaterial;

		if (chooseType == TypeOfInteractble.InteractableObject)
		{
			startingPos = transform.position;
			currentPos = startingPos;
			startRot = transform.rotation;
		}
		else
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			startingPos = rectTransform.anchoredPosition;
			currentPos = startingPos;
			startRot = rectTransform.rotation;
		}
	}
	private void Update()
	{
		if(canBeMoved)
			transform.position = Vector3.Lerp(transform.position, currentPos, Time.deltaTime * moveSpeed);
	}

	public Vector3 positionCurrent()
	{
		return currentPos;
	}
	public void Select(Vector3 focusPos, Vector3 tar)
	{
        currentPos = focusPos;
        transform.LookAt(tar);
	}

    public void DeSelect()
	{
        currentPos = startingPos;
		transform.rotation = startRot;
	}

    public void StartHighlight()
	{
		if(chooseType == TypeOfInteractble.InteractableObject)
			gameObject.GetComponent<Renderer>().sharedMaterial = highlightMat;
		if(chooseType == TypeOfInteractble.UI && hoverObj != null)
		{
			
			Debug.Log("Runs");
		}
	}

	public void StopHighlight()
	{
		if (chooseType == TypeOfInteractble.InteractableObject)
		{
			if (isColourPicker && isSelected)
				defaultMat.color = ColourPicker.Instance.GetColour();

		gameObject.GetComponent<Renderer>().sharedMaterial = defaultMat;
		}
		if(chooseType == TypeOfInteractble.UI && hoverObj != null)
		{
		
			hoverObj.SetActive(false);
		}
	}
	public void Show(string content, string header = "")
	{
		hoverObj.GetComponent<TooltipUI>().SetText(content, header);
		hoverObj.SetActive(true);
	}

	public  void Position(Vector3 pos, float xPOffset, float yOffset)
	{
		//RectTransform rectTrans = Instance.toolTip.gameObject.GetComponent<RectTransform>();
		hoverObj.transform.position = new Vector3(pos.x + xPOffset, pos.y + yOffset);

	}

	public GameObject HoverObj()
	{
		return hoverObj;
	}
	public void ChangeColour(Color c)
	{
		if(isColourPicker && isSelected)
		{
			defaultMat.color = c;
		}
	}

	public bool IsSelected(bool v)
	{
		isSelected = v;
		return isSelected;
	}
}
