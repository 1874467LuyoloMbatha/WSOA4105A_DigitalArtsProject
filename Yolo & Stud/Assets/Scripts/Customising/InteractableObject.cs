using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] Vector3 startingPos;
	[SerializeField] Vector3 currentPos;
    [SerializeField] Quaternion startRot;

    [Header("Interacting Values")]
    [SerializeField] Material defaultMat;
    [SerializeField] Material highlightMat;

	[Header("Floats")]
	[SerializeField] float moveSpeed = 5f;

	[Header("Booleans")]
	[Tooltip("Only enable for objects that will change colour")]
	[SerializeField] bool isColourPicker = false;

	private void Awake()
	{
		defaultMat = gameObject.GetComponent<Renderer>().sharedMaterial;

		startingPos = transform.position;
		currentPos = startingPos;
		startRot = transform.rotation;
	}
	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, currentPos, Time.deltaTime * moveSpeed);
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
		gameObject.GetComponent<Renderer>().sharedMaterial = highlightMat;
	}

    public void StopHighlight()
	{
		gameObject.GetComponent<Renderer>().sharedMaterial = defaultMat;
	}
}
