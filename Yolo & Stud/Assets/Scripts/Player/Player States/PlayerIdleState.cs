using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
	public PlayerIdleState(PlayerStateManager ctx, PlayerStateFactory stateFactory)
		: base(ctx, stateFactory) { }
	public override void CheckSwitchStates()
	{
		if (Vector3.Distance(state.GetCurrentPos(), state.Destination()) <= state.StoppingDistance)
		{
			state.SetIsMoving(false);
		}
		else
			state.SetIsMoving(true);

		//when the player moves
		if (state.IsMoving())
		{
			SwitchState(factory.Move());
		}
	}

	public override void EnterState()
	{
		state.Anim().Play(state.IsIdleHash);
		GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.IdleWalk);
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