using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class PlayerBaseState 
{
	public abstract void EnterState(PlayerStateManager stateManager);

	public abstract void UpdateState(PlayerStateManager stateManager);

	public abstract void OnCollisionEnter(PlayerStateManager stateManager);
   
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/