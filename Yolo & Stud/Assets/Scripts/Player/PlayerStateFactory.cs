using System.Collections;
using System.Collections.Generic;

enum PlayerStates
{
	Idle,
	Move,
	Sleep,
	Work
}

public class PlayerStateFactory
{
	PlayerStateManager stateManager;

	//For performance
	Dictionary<PlayerStates, PlayerBaseState> baseStates = new Dictionary<PlayerStates, PlayerBaseState>();	
	public PlayerStateFactory(PlayerStateManager ctx)
	{
		stateManager = ctx;

		//adding the states
		baseStates[PlayerStates.Idle] = new PlayerIdleState(stateManager, this);
		baseStates[PlayerStates.Move] = new PlayerMoveState(stateManager, this);
		baseStates[PlayerStates.Sleep] = new PlayerSleepingState(stateManager, this);
		baseStates[PlayerStates.Work] = new PlayerWorkingState(stateManager, this);
	}

	//Create instances of the states the player has
	public PlayerBaseState Idle()
	{
		return baseStates[PlayerStates.Idle];
	}

	public PlayerBaseState Move()
	{
		return baseStates[PlayerStates.Move];
	}

	public PlayerBaseState Sleeping()
	{
		return baseStates[PlayerStates.Sleep];
	}

	public PlayerBaseState Working()
	{
		return baseStates[PlayerStates.Work];
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/