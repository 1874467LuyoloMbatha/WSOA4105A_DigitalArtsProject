using System.Collections;
using System.Collections.Generic;


public class PlayerStateFactory
{
	PlayerStateManager stateManager;

	public PlayerStateFactory(PlayerStateManager ctx)
	{
		stateManager = ctx;
	}

	//Create instances of the states the player has
	public PlayerBaseState Idle()
	{
		return new PlayerIdleState(stateManager, this);
	}

	public PlayerBaseState Move()
	{
		return new PlayerMoveState(stateManager, this);
	}

	public PlayerBaseState Sleeping()
	{
		return new PlayerSleepingState(stateManager, this);
	}

	public PlayerBaseState Working()
	{
		return new PlayerWorkingState(stateManager, this);
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/