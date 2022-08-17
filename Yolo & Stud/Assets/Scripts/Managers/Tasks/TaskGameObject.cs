using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

//Place Inside of the ToDO object
public class TaskGameObject : MonoBehaviour
{
	[Header("Object Content Variables")]
	public string gameObjectName;
	public string sortType;
	public int taskIndex;

	[Header("Unity Handles")]
	[Tooltip("Assign The Text Field in the children of this game object")]
	public Text taskText;

	private void Start()
	{
		taskText.text = gameObjectName;
	}

	//Makes it easy to manipulate & update values outside of this script
	public void SetTaskInfo(string name, string type, int indcx)
	{
		this.gameObjectName = name;
		this.sortType = type;
		this.taskIndex = indcx;
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/