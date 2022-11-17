using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    [Header("Unity Handles")]
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 destination, currentPos, mousePosition;
    [SerializeField] LayerMask layerYouCanMoveTo, layerForBed, layerForDesk, layerForCouch;
    [SerializeField] Transform destTrans;
    [SerializeField] Text debugText;

    [Header("Player Floats")]
    [Tooltip("Handles how faast the player can rotate")]
    [SerializeField] float rotationTime;
    [SerializeField] float distanceBeforeStopping;
    [SerializeField] float yMousePos = 0.12f, distanceForRay;

    [Header("Player Booleans")]
    [SerializeField] bool isMoving;
    [SerializeField] bool canControlPlayer, isInAState;

    [Header("Animation Strings")]
    [SerializeField] string idlingAnimName;
    [SerializeField] string walkingAnimName;
    [SerializeField] string workingAnimName;
    [SerializeField] string restingAnimName;
    [SerializeField] string exerciseAnimName;

    [Header("Animation Integers")]
    int isIdlingHash;
    int isWalkingHash;
    int isWorkingHash;
    int isRestingHash;
    int isExercisingHash;
    #endregion

    private void Awake()
    {
        SettingUp();
    }
    void Start()
    {
        distanceBeforeStopping = agent.stoppingDistance;
    }

   
    void Update()
    {
        HandleRotation();
        HandleMoving();
        CheckStates();
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
                //for the ground
                if (Physics.Raycast(ray, out RaycastHit hit, distanceForRay, layerYouCanMoveTo))
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

    void CheckStates()
	{
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.IdleWalk)
            HandleIdleState();
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Studying)
            HandleWorkingState();
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Resting)
            HandleSleepingState();
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Exercising)
            HandleExercisingState();

    }
    //Handle Idle
    public void HandleIdleState()
    {
        if (!IsInAState())
        {
            HandleIdle();
            SetIsMoving(false);
        }
    }
    void HandleIdle()
    {
        Anim().Play(isIdlingHash);
        Agent().SetDestination(Destination());
        Agent().isStopped = false;

        GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.IdleWalk);
    }
    public void HandleMovingState()
    {
        //distanee
        //Debug.Log(Vector3.Distance(state.GetCurrentPos(), state.Destination()));
        if (Vector3.Distance(GetCurrentPos(), Destination()) <= StoppingDistance)
        {
            //Debug.Log("Reached Destination");
            SetIsMoving(false);

            if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.IdleWalk)
               HandleIdleState();
            if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Studying)
                HandleWorkingState();
        }
        else
        {
            SetIsMoving(true);
            HandleMovement();
        }

        /*	if(GameManager.Instance.GetGameState() == GameManager.GameState.PlayerCustomiserMode)
            {
                state.SetIsMoving(false);
                SwitchState(factory.Idle());
            }*/

        if (!IsMoving())
        {
            SetIsMoving(false);
            if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.IdleWalk)
                HandleIdleState();
            else if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Studying)
                HandleWorkingState();
            else if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Resting)
                HandleSleepingState();
        }
    }
    void HandleMovement()
    {
        Anim().Play(IsWalkingHash);
        Agent().SetDestination(Destination());
        Agent().isStopped = false;

        GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Walking);
    }

    public void HandleWorkingState()
    {
        if (Vector3.Distance(GetCurrentPos(), Destination()) <= StoppingDistance)
        {
            //Debug.Log("Camera Change");
            HandleWorking();
            GameManager.Instance.ChangeToStudyCamera();
        }
    }
    void HandleWorking()
    {
        Debug.Log("Switch Camera + Sit down and work");
        Anim().Play(IsWorkingHash);
        Agent().SetDestination(Destination());
        Agent().isStopped = false;

        GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Studying);
    }
    public void HandleSleepingState()
	{
        HandleSleeping();
	}
    void HandleSleeping()
    {
        Debug.Log("Sleep/Rest now");
        Anim().Play(IsRestingHash);
        Agent().SetDestination(Destination());
        Agent().isStopped = false;

        GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Resting);
    }

    public void HandleExercisingState()
    {
        HandleExercise();
    }
    void HandleExercise()
    {
        Debug.Log("Sleep/Rest now");
        Anim().Play(IsExercisingHash);
        Agent().SetDestination(Destination());
        Agent().isStopped = false;

        GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Exercising);
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
    public bool IsInAState()
    {
        return isInAState;
    }
    public bool SetIsMoving(bool v)
    {
        isMoving = v;
        return isMoving;
    }
    public bool SetIsInState(bool v)
    {
        isInAState = v;
        return isInAState;
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

    public string IsExercisingHash { get { return exerciseAnimName; } }
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
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/