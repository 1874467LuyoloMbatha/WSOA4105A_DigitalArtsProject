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
    public enum PlayerMode { IdleWalk, Walking, Studying, Resting, Exercising, Couch}

	public enum Weather { idle, rain, snow, lightning }
	#endregion

	#region Variables
	[Header("States & Enums")]
	[SerializeField] GameState gameState;
	[SerializeField] PlayerMode playerMode;
	[SerializeField] Weather weather;

	[Header("Mood Variables")]
	[SerializeField] Material defaultSky;
	[SerializeField] Material currentSky, nightSky, daySky;
	[SerializeField] GameObject dayLightVolume, nightLightVolume;
	[SerializeField] GameObject raining, snowing, lightning, monitorScreen;
	[SerializeField] Button personalisationButton;
	[SerializeField] float duration;

	[Header("Player Variables")]
	[SerializeField] PlayerManager player;
	[SerializeField] TimerManager timer;
	[SerializeField] Image controlImage;
	[SerializeField] Sprite canControlSprite, cannotControlSprite;
	[SerializeField] Transform desk, bed, couch, exerciseSpot;

	[Header("Unity Handles")]
	[Tooltip("Drag The Tab Parent here")]
	[SerializeField] GameObject tabParent;
	[Tooltip("Drag clearshot parent here")]
	[SerializeField] GameObject clearShotParent;
	[Tooltip("Drag The Main Virtual Camera Here!")]
	[SerializeField] CinemachineVirtualCamera mainVirtualCam;
	[Tooltip("Drag The Customising Virtual Camera Here!")]
	[SerializeField] CinemachineVirtualCamera customisingVirtualCamera;
	[Tooltip("Drag The Desk Virtual Camera Here!")]
	[SerializeField] CinemachineVirtualCamera deskVirtualCamera;

	[Header("Booleans")]
	[Tooltip("This will determine if the tab parent will be on or off")]
	[SerializeField] bool isTabParentOpen;
	[Tooltip("This will determine if the player is Customising themeselves or not")]
	[SerializeField] bool isCustomising;

	[Header("Intro Handles")]
	[SerializeField] InputField playerNameInputField;
	[SerializeField] string greetingPlayerPrefs = "PlayerName";
	#endregion

	private void Awake()
	{
		Application.runInBackground = true;
		defaultSky = RenderSettings.skybox;

		if (player == null)
			player = FindObjectOfType<PlayerManager>();

		if (timer == null)
			timer = FindObjectOfType<TimerManager>();

		if (playerNameInputField != null)
		{
			if(PlayerPrefs.HasKey(greetingPlayerPrefs))
				playerNameInputField.text = PlayerPrefs.GetString(greetingPlayerPrefs);
		}

		SetMonitorScreen(false);

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
	void Start()
    {
        tabParent.SetActive(isTabParentOpen);
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		//Set The Cameras
		mainVirtualCam.Priority = 1;
		customisingVirtualCamera.Priority = 0;
		deskVirtualCamera.Priority = 0;
	}

   
    void Update()
    {
       if(Input.GetKey(KeyCode.Tab) && timer.PomodoroSteps != TimerManager.PomodoroStages.Pomodoro)
		{
			SwitchToCustomisingMode();
			//isTabParentOpen = !isTabParentOpen;

			////Change the game states
			//if (isTabParentOpen)
			//	gameState = GameState.BuildMode;
			//else
			//	gameState = GameState.PlayMode;

			//tabParent.SetActive(isTabParentOpen);
			//Debug.Log(gameState);
		}

		
    }

	#region Public Functions Referenced
	public GameState GetGameState()
	{
		return gameState;
	}

	public PlayerMode GetPlayerMode()
	{
		return playerMode;
	}

	public PlayerMode SetPlayerMode(PlayerMode m)
	{
		playerMode = m;
		return playerMode;
	}
	public void SwitchToCustomisingMode()
	{
		SetCustomising();

		if(gameState == GameState.PlayerCustomiserMode)
		{
			player.gameObject.transform.position = exerciseSpot.position;
			//mainVirtualCam.Priority = 0;
			customisingVirtualCamera.Priority = 1;
			clearShotParent.SetActive(false);
		}
		else
		{
			//mainVirtualCam.Priority = 1;
			customisingVirtualCamera.Priority = 0;
			clearShotParent.SetActive(true);
		}
	}

	public void SwitchForceCustomisingMode()
	{
		isCustomising = true;
		SetCustomising();

		if (gameState == GameState.PlayerCustomiserMode)
		{
			//mainVirtualCam.Priority = 0;
			customisingVirtualCamera.Priority = 1;
			clearShotParent.SetActive(false);
		}
		else
		{
		//	mainVirtualCam.Priority = 1;
			customisingVirtualCamera.Priority = 0;
			clearShotParent.SetActive(true);
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

	public PlayerManager ReturnPlayerManager()
	{
		return player;
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

	#region Atmosphere
	public void ChangeMood(int v)
	{
		weather = (Weather)v;

		switch (weather)
		{
			case Weather.idle:
				raining.SetActive(false);
				snowing.SetActive(false);
				lightning.SetActive(false);	
				break;
			case Weather.rain:
				raining.SetActive(true);
				snowing.SetActive(false);
				lightning.SetActive(false);
				soundsManager.Instance.PlayRainAmbience();
				break;
			case Weather.snow:
				raining.SetActive(false);
				snowing.SetActive(true);
				lightning.SetActive(false);
				soundsManager.Instance.PlaySnowAmbience();
				break;
			case Weather.lightning:
				raining.SetActive(false);
				snowing.SetActive(false);
				lightning.SetActive(true);
				soundsManager.Instance.PlayRainThunderAmbience();
				break;
			default:
				break;
		}
	}

	public void ChangeSky(int v)
	{
		/*if (v == 0)
			RenderSettings.skybox = defaultSky;
		if (v == 1)
			RenderSettings.skybox = daySky;
		if (v == 2)
			RenderSettings.skybox = nightSky;*/
		if (v == 0)
			dayLightVolume.SetActive(true);
		if (v == 1)
		{
			dayLightVolume.SetActive(true);
			nightLightVolume.SetActive(false);
		}
		if (v == 2)
		{
			nightLightVolume.SetActive(true);
			dayLightVolume.SetActive(false);
		}

		soundsManager.Instance.PlayMainAmbience(v);
	}
	#endregion

	#region Player Controls
	public void EnableDisablePlayerControl()
	{
		player.SetPlayerControl(!player.GetPlayerControl());
		
		if(player.GetPlayerControl())
		{
			if (controlImage != null)
				controlImage.sprite = canControlSprite;
		}
		else
			if (controlImage != null)
				controlImage.sprite = cannotControlSprite;

		clearShotParent.SetActive(true);
	}

	public void GoToWork()
	{
		player.ResetTransformParent();
		player.SetIsMoving(true);
		playerMode = PlayerMode.Studying;

		//player.transform.position = desk.position;
		player.SetDestination(desk.position);
		EnableDisablePlayerControl();
		SetMonitorScreen(false);
		clearShotParent.SetActive(true);
	}

	public void GoToCouch()
	{
		player.ResetTransformParent();
		player.SetIsMoving(true);
		playerMode = PlayerMode.Couch;

		//player.transform.position = couch.position;
		player.SetDestination(couch.position);
		EnableDisablePlayerControl();
		SetMonitorScreen(false);
		clearShotParent.SetActive(true);
	}

	public void GoExercise()
	{
		if (!player.GetExercising())
		{
			player.ResetTransformParent();
			player.SetIsMoving(true);
			player.HandleExercisingState();
			//player.SetExercisingMode(true);
			playerMode = PlayerMode.Exercising;

			//player.transform.position = exerciseSpot.position;
			player.SetDestination(exerciseSpot.position);
		}
		else
		{
			player.ResetTransformParent();
			player.SetExercisingMode(false);
			playerMode = PlayerMode.IdleWalk;
		}

		SetMonitorScreen(false);
		clearShotParent.SetActive(true);
	}

	public void GoRest()
	{
		playerMode = PlayerMode.Resting;
		player.ResetTransformParent();
		player.SetIsMoving(true);

		player.SetDestination(bed.position);

		/*mainVirtualCam.Priority = 1;
		customisingVirtualCamera.Priority = 0;
		deskVirtualCamera.Priority = 0;*/

		EnableDisablePlayerControl();
		SetMonitorScreen(false);
		clearShotParent.SetActive(true);
	}

	public void ChangeToStudyCamera()
	{
		clearShotParent.SetActive(false);
		deskVirtualCamera.Priority = 20;

		SetMonitorScreen(true);
	}

	public void SetMonitorScreen(bool v)
	{
		if(monitorScreen != null)
			monitorScreen.SetActive(v);
	}

	public void SetPersonaliseButton(bool v)
	{
		if (personalisationButton != null)
			personalisationButton.interactable = v;
	}
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/