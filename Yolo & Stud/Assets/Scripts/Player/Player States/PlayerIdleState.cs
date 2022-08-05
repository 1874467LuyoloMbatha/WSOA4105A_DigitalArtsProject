using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
	public override void EnterState(PlayerStateManager stateManager)
	{
		Debug.Log("Idling");
	}

	public override void OnCollisionEnter(PlayerStateManager stateManager)
	{
		
	}

	public override void UpdateState(PlayerStateManager stateManager)
	{
		
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/