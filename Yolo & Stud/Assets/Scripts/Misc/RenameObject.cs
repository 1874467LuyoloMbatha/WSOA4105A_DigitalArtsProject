using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenameObject : MonoBehaviour, IPointerClickHandler
{
	[Header("Generic Elements")]
	[SerializeField] string objectName;

	[Header("Booleans")]
	[SerializeField] bool isForButton = true;

	[Header("Integers")]
	public int index;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (isForButton)
			MaterialChangeManager.Instance.SetTempButton(this.gameObject);
	}

	private void Start()
	{
		if (isForButton)
		objectName = gameObject.transform.parent.name + "Btn";

		gameObject.name = objectName;
	}

}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/