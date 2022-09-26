using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region Enums
	public enum GameState { PlayMode, BuildMode, PlayerCustomiserMode}
    public enum PlayerMode { IdleWalk, Studying, Resting}
	#endregion

	#region Variables
	[Header("States & Enums")]
	[SerializeField] GameState gameState;
	[SerializeField] PlayerMode playerMode;

	[Header("Unity Handles")]
	[Tooltip("Drag The Tab Parent here")]
	[SerializeField] GameObject tabParent;
	[Tooltip("Drag The Main Virtual Camera Here!")]
	[SerializeField] CinemachineVirtualCamera mainVirtualCam;
	[Tooltip("Drag The Customising Virtual Camera Here!")]
	[SerializeField] CinemachineVirtualCamera customisingVirtualCamera;

	[Header("Booleans")]
	[Tooltip("This will determine if the tab parent will be on or off")]
	[SerializeField] bool isTabParentOpen;
	[Tooltip("This will determine if the player is Customising themeselves or not")]
	[SerializeField] bool isCustomising;
	#endregion

	private void Awake()
	{
		Application.runInBackground = true;
	}
	void Start()
    {
        tabParent.SetActive(isTabParentOpen);

		//Set The Cameras
		mainVirtualCam.Priority = 1;
		customisingVirtualCamera.Priority = 0;
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
	public GameState GetGameState()
	{
		return gameState;
	}

	public void SwitchToCustomisingMode()
	{
		SetCustomising();

		if(gameState == GameState.PlayerCustomiserMode)
		{
			mainVirtualCam.Priority = 0;
			customisingVirtualCamera.Priority = 1;
		}
		else
		{
			mainVirtualCam.Priority = 1;
			customisingVirtualCamera.Priority = 0;
		}
	}

	public void SetCustomising()
	{
		isCustomising = !isCustomising;

		if (isCustomising)
			gameState = GameState.PlayerCustomiserMode;
		else
			gameState = GameState.PlayMode;

	}

	//Assign To Buttons you want to open links
	public void OpenUrl(string link)
	{
		Application.OpenURL(link);
	}

	public void ExitApplication()
	{
		Application.Quit();
	}
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/