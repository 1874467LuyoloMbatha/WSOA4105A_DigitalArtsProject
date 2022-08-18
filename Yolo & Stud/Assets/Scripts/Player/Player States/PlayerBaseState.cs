using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class PlayerBaseState 
{
	protected PlayerStateFactory factory;
	protected PlayerStateManager state;
	public PlayerBaseState(PlayerStateManager ctx, PlayerStateFactory stateFactory)
	{
		state = ctx;
		factory = stateFactory;
	}

	public abstract void EnterState();

	public abstract void UpdateState();

	public abstract void CheckSwitchStates();

	public abstract void InitialiseSubState();
	public abstract void ExitState();

	void UpdateStates() { }

	protected void SwitchState(PlayerBaseState newState) 
	{
		//urrent State to be exited
		ExitState();

		//Neww state
		newState.EnterState();

		//Switch
		state.currentPlayerState = newState;
		//Debug.Log("New State: " + newState);
		state.DebugText(newState.ToString());
	}
	protected void SetSuperState() { }
	protected void SetSubState() { }
   
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/