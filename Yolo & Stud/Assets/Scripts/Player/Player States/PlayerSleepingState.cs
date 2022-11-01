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
		HandleSleeping();
		GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Resting);
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

	void HandleSleeping()
	{
		Debug.Log("Sleep/Rest now");
		state.Anim().Play(state.IsRestingHash);
		state.Agent().SetDestination(state.Destination());
		state.Agent().isStopped = false;
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/