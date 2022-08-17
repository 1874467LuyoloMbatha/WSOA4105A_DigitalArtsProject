using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerSleepingState : PlayerBaseState
{
	public PlayerSleepingState(PlayerStateManager ctx, PlayerStateFactory stateFactory)
		: base(ctx, stateFactory) { }
	public override void CheckSwitchStates()
	{

	}

	public override void EnterState()
	{
		
	}

	public override void ExitState()
	{
		
	}

	public override void InitialiseSubState()
	{
		
	}

	public override void UpdateState()
	{
		CheckSwitchStates();
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/