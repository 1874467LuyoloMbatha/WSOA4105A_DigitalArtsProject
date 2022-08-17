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
    public PlayerBaseState currentPlayerState;
    public PlayerStateFactory states;

    [Header("Unity Handles")]
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 destination, currentPos;
    [SerializeField] Transform destTrans;

    [Header("Player Floats")]
    [Tooltip("Handles how faast the player can rotate")]
    [SerializeField] float rotationTime;
    [SerializeField] float distanceBeforeStopping;

    [Header("Player Booleans")]
    [SerializeField] bool isMoving;

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

        states = new PlayerStateFactory(this);

        //set current state to idle by default
        currentPlayerState = states.Idle();
        Debug.Log(currentPlayerState);

        //This is the context
        currentPlayerState.EnterState();

    }
	void Start()
    {
        distanceBeforeStopping = agent.stoppingDistance;
    }

   
    void Update()
    {
        currentPlayerState.UpdateState();
        HandleRotation();
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

    #region Referenced Outside Of Script
    public NavMeshAgent Agent()
    {
        return agent;
    }

    public Animator Anim()
    {
        return anim;
    }
    //Floats
    public float StoppingDistance { get { return distanceBeforeStopping; } }

    //Booleans
    public bool IsMoving()
	{
        return isMoving;
	}
    public bool SetIsMoving(bool v)
    {
        isMoving = v;
        return isMoving;
    }

    //Stringsss
    public string IsIdleHash { get { return idlingAnimName; } }
    public string IsWalkingHash { get { return walkingAnimName; } }
    public string IsWorkingHas { get { return workingAnimName; } }

    //Positions
    public Vector3 GetCurrentPos()
	{
        currentPos = transform.position;
        return currentPos;
	}
    public Vector3 Destination()
	{
        destination = destTrans.position;   
        return destination;
	}

	#endregion
	#endregion

	#region Public Methods
	public void SwitchState(PlayerBaseState state)
    {
        currentPlayerState = state;
        state.EnterState();
    }
    #endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/