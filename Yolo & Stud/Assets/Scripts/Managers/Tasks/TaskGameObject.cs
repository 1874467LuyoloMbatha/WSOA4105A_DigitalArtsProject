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
	public int taskIndex;

	[Header("Unity Handles")]
	[Tooltip("Assign The Text Field in the children of this game object")]
	public InputField taskText;
	public Image taskBg;

	[Header("Settings Related Variables")]
	public Color defaultColour;
	public Color finishedColour;

	private void Start()
	{
		taskText.text = gameObjectName;
		TaskManager.Instance.AddToList(this);

		defaultColour = taskBg.color;
	}

	//Makes it easy to manipulate & update values outside of this script
	public void SetTaskInfo(string name, int indcx)
	{
		this.gameObjectName = name;
		this.taskIndex = indcx;
	}

	public void SetInputFieldText(string name)
	{
		this.gameObjectName = name;
		TaskManager.Instance.SaveTaskData();
	}
	public void ConfigureToggle()
	{
		TaskManager.Instance.TaskCheckMark(this);
		TaskManager.Instance.SaveTaskData();
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/