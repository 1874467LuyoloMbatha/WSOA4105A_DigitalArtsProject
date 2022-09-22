using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
	public PlayerMoveState(PlayerStateManager ctx, PlayerStateFactory stateFactory)
		: base(ctx, stateFactory) { }
	public override void CheckSwitchStates()
	{
		//distanee
		//Debug.Log(Vector3.Distance(state.GetCurrentPos(), state.Destination()));
		if(Vector3.Distance(state.GetCurrentPos(), state.Destination()) <= state.StoppingDistance)
		{
			Debug.Log("Reached Destination");
			state.SetIsMoving(false);
			SwitchState(factory.Idle());
		}
		
		if(GameManager.Instance.GetGameState() == GameManager.GameState.PlayerCustomiserMode)
		{
			state.SetIsMoving(false);
			SwitchState(factory.Idle());
		}

		if(!state.IsMoving())
		{
			state.SetIsMoving(false);
			SwitchState(factory.Idle());
		}
	}

	public override void EnterState()
	{
		//logic for the player moving
		HandleMovement();
	}

	public override void ExitState()
	{
		state.Agent().isStopped = true;
	}

	public override void InitialiseSubState()
	{
		
	}

	public override void UpdateState()
	{
		CheckSwitchStates();
	}

	void HandleMovement()
	{
		state.Anim().Play(state.IsWalkingHash);
		state.Agent().SetDestination(state.Destination());
		state.Agent().isStopped = false;
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/