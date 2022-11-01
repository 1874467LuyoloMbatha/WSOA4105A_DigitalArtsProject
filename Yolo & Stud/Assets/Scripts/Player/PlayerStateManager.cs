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
    [SerializeField] Vector3 destination, currentPos, mousePosition;
    [SerializeField] LayerMask layerToMoveTo;
    [SerializeField] Transform destTrans;
    [SerializeField] Text debugText;

    [Header("Player Floats")]
    [Tooltip("Handles how faast the player can rotate")]
    [SerializeField] float rotationTime;
    [SerializeField] float distanceBeforeStopping;
    [SerializeField] float yMousePos = 0.12f, distanceForRay;

    [Header("Player Booleans")]
    [SerializeField] bool isMoving;
    [SerializeField] bool canControlPlayer;

    [Header("Animation Strings")]
    [SerializeField] string idlingAnimName;
    [SerializeField] string walkingAnimName;
    [SerializeField] string workingAnimName;
    [SerializeField] string restingAnimName;

    [Header("Animation Integers")]
     int isIdlingHash;
     int isWalkingHash;
     int isWorkingHash;
    int isRestingHash;

#endregion
	private void Awake()
	{
        SettingUp();

        states = new PlayerStateFactory(this);

        //set current state to idle by default
        currentPlayerState = states.Idle();
        
        if(debugText != null)
             DebugText(currentPlayerState.ToString());

        //This is the context
        currentPlayerState.EnterState();

    }
	void Start()
    {
        distanceBeforeStopping = agent.stoppingDistance;
    }

   
    void Update()
    {
        HandleRotation();
        HandleMoving();
        currentPlayerState.UpdateState();
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
        isRestingHash = Animator.StringToHash(restingAnimName);
	}

    void GetAllComponents()
	{
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

	}
    #endregion

    #region Player Movement

    void HandleMoving()
	{
        if (canControlPlayer)
        {
            mousePosition = Input.mousePosition;
            // mousePosition.y = yMousePos;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, distanceForRay, layerToMoveTo))
                {
                  //  destTrans.transform.position = hit.point;
                    destTrans.transform.position = new Vector3(hit.point.x, hit.point.y + yMousePos, hit.point.z);
                }
                GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Walking);
            }
        }
	}
    void HandleRotation()
	{
        if (isMoving)
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

    public Text DebugText(string v)
	{
        if (debugText != null)
        {
            debugText.text = v;
            return debugText;
        }
        return null;
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

    public bool SetPlayerControl(bool v)
    {
        canControlPlayer = v;
        return canControlPlayer;
    }
    public bool GetPlayerControl()
	{
        return canControlPlayer;
	}

    //Stringsss
    public string IsIdleHash { get { return idlingAnimName; } }
    public string IsWalkingHash { get { return walkingAnimName; } }
    public string IsWorkingHash { get { return workingAnimName; } }

    public string IsRestingHash { get { return restingAnimName; } }
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

    public void SetDestination(Vector3 d)
	{
        destTrans.position = d;
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