using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region Enums
	public enum GameState { PlayMode, BuildMode, DesignMode}
    public enum PlayerMode { IdleWalk, Studying, Resting, Customising}
	#endregion

	#region Variables
	[Header("States & Enums")]
	[SerializeField] GameState gameState;
	[SerializeField] PlayerMode playerMode;

	[Header("Unity Handles")]
	[Tooltip("Drag The Tab Parent here")]
	[SerializeField] GameObject tabParent;

	[Header("Booleans")]
	[Tooltip("This will determine if the tab parent will be on or off")]
	[SerializeField] bool isTabParentOpen;
	#endregion
	void Start()
    {
        tabParent.SetActive(isTabParentOpen);
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
		{
			isTabParentOpen = !isTabParentOpen;

			//Change the game states
			if (isTabParentOpen)
				gameState = GameState.BuildMode;
			else
				gameState = GameState.PlayMode;

			tabParent.SetActive(isTabParentOpen);
			//Debug.Log(gameState);
		}
    }

	#region Public Functions Referenced
	
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/