using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
	#region Variables
	[Header("External References")]
    [Tooltip("Holds information on the current state the player is in")]
    PlayerBaseState currentPlayerState;
    
    //Create instances of the states the player has
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerSleepingState sleepingState = new PlayerSleepingState();
    public PlayerWorkingState workingState = new PlayerWorkingState();

    [Header("Unity Handles")]
    Animator anim;
    NavMeshAgent agent;
    Vector3 destination;

    [Header("Player Floats")]
    [Tooltip("Handles how faast the player can rotate")]
    [SerializeField] float rotationTime;

    [Header("Animation Strings")]
    [SerializeField] string idlingAnimName;
    [SerializeField] string walkingAnimName;
    [SerializeField] string workingAnimName;

    [Header("Animation Integers")]
    int isIdlingHash;
    int isWalkingHash;
    int isWorkingHash;

#endregion
	private void Awake()
	{
        SettingUp();
	}
	void Start()
    {
        //set current state to idle by default
        currentPlayerState = idleState;

        //This is the context
        currentPlayerState.EnterState(this);
    }

   
    void Update()
    {
        currentPlayerState.UpdateState(this);
    }



	#region Set-Up
    void SettingUp()
	{
        AnimHashSetUp();
        GetAllComponents();
	}

    void AnimHashSetUp()
	{
        //ill reduce performance issues
        isIdlingHash = Animator.StringToHash(idlingAnimName);
        isWalkingHash = Animator.StringToHash(walkingAnimName);
        isWorkingHash = Animator.StringToHash(workingAnimName);
	}

    void GetAllComponents()
	{
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

	}
    #endregion

    #region Player Movement
    void HandleRotation()
	{
        Vector3 posToLookAt;

        //Change the position the player should look at
        posToLookAt.x = destination.x;
        posToLookAt.y = 0;
        posToLookAt.z = destination.y;

        //Get here the player is currently facing
        Quaternion currentRot = transform.rotation;

        //create prefered rotation based on here the player is going
        Quaternion targetRotation = Quaternion.LookRotation(posToLookAt);

        //Rotate the player
        transform.rotation = Quaternion.Slerp(currentRot, targetRotation, rotationTime * Time.deltaTime);

	}
    #endregion

    #region Public Methods
    public void SwitchState(PlayerBaseState state)
    {
        currentPlayerState = state;
        state.EnterState(this);
    }
    #endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/