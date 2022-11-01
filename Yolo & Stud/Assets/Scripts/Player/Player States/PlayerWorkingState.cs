using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerWorkingState : PlayerBaseState
{
	public PlayerWorkingState(PlayerStateManager ctx, PlayerStateFactory stateFactory)
		: base(ctx, stateFactory) { }
	public override void CheckSwitchStates()
	{
		
	}


	public override void EnterState()
	{
		HandleWorking();
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

	void HandleWorking()
	{
		Debug.Log("Switch Camera + Sit down and work");
		state.Anim().Play(state.IsWorkingHash);
		state.Agent().SetDestination(state.Destination());
		state.Agent().isStopped = false;
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/