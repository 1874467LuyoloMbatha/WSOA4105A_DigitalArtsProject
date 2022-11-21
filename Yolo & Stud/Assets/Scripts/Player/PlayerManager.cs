using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    [Header("Unity Handles")]
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 destination, currentPos, mousePosition;
    [SerializeField] LayerMask layerYouCanMoveTo, layerForBed, layerForDesk, layerForCouch;
    [SerializeField] Transform playerParent, destTrans, toRotate, couchParent, bedParent, deskParent, normalParent;
    [SerializeField] Text debugText;

    [Header("Player Floats")]
    [Tooltip("Handles how faast the player can rotate")]
    [SerializeField] float rotationTime;
    [SerializeField] float distanceBeforeStopping;
    [SerializeField] float yMousePos = 0.12f, distanceForRay;
    [SerializeField] float baseOffsetDefault, baseOffsetDeskValue = 0.0018f;

	[Header("Player Booleans")]
    [SerializeField] bool isMoving;
    [SerializeField] bool canControlPlayer, isInAState, comesFromAnimation;

    [Header("Aniamtion Unity Variables")]
    [SerializeField] AnimationClip[] exercisingAnimClips;

	[Header("Animation Vectors/Floats")]
	[SerializeField] Vector3 standToSitOnBedRotation;
	[SerializeField] Vector3 standToSitOnBedPosition;
    [SerializeField] Vector3 layOnBedPosition;
	[SerializeField] Vector3 standToSitOnCouchRotation;
	[SerializeField] Vector3 standToSitOnCouchPosition;
	[SerializeField] Vector3 sitOnCouchPosition;
	[SerializeField] Vector3 SitAtDeskhRotation;
	[SerializeField] Vector3 SitAtDeskhPosition;

	[Header("Animation Strings")]
    [SerializeField] string idlingAnimName;
	[SerializeField] string sitToStandCouchAnimName;
	[SerializeField] string walkingAnimName;
    [SerializeField] string workingAnimName;
    [SerializeField] string goingToWorkAnimName;
	[SerializeField] string goingToCouchAnimName;
	[SerializeField] string restingAnimName;
    [SerializeField] string exerciseAnimName;

    [Header("Animation Integers")]
    [SerializeField] int numberOfExercisingAnimations;
    int isIdlingHash;
    int isSittingToStandCouchHash;
    int isWalkingHash;
    int isWorkingHash;
    int isGoingToWorkHash;
	int isGoingToCouchHash;
	int isRestingHash;
    int isExercisingHash;

    [Header("Animation Booleans")]
    [SerializeField] bool sitOnBedAnimHasPlayed;
	[SerializeField] bool sitOnCouchAnimHasPlayed, sitAtDeskAnimHasPlayed, isExercising;
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
		HandleMoving();

		HandleRotation();
        HandleMovingState();

      //  CheckStates();
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
        isSittingToStandCouchHash = Animator.StringToHash(sitToStandCouchAnimName);
        isWalkingHash = Animator.StringToHash(walkingAnimName);
        isWorkingHash = Animator.StringToHash(workingAnimName);
        isGoingToWorkHash = Animator.StringToHash(goingToWorkAnimName);
        isGoingToCouchHash = Animator.StringToHash(goingToCouchAnimName);
        isRestingHash = Animator.StringToHash(restingAnimName);
        isExercisingHash = Animator.StringToHash(exerciseAnimName);
    }

    void GetAllComponents()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (playerParent == null)
            playerParent = GetComponentInParent<Transform>();

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
                    Debug.Log("Ground");
					//  destTrans.transform.position = hit.point;
					ResetTransformParent();
					SetIsMoving(true);
                    isExercising = false;
                    destTrans.transform.position = new Vector3(hit.point.x, hit.point.y + yMousePos, hit.point.z);
                    GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Walking);
                }

                //for the desk
                if (Physics.Raycast(ray, out RaycastHit deskHit, distanceForRay, layerForDesk))
                {
                    Debug.Log("Desk");
                    SetIsMoving(true);
					isExercising = false;
					//  destTrans.transform.position = hit.point;
					//  destTrans.transform.position = new Vector3(deskHit.point.x, hit.point.y + yMousePos, hit.point.z);
					ResetTransformParent();
					GameManager.Instance.GoToWork();
                }

                //for the bed
                if (Physics.Raycast(ray, out RaycastHit bedHit, distanceForRay, layerForBed))
                {
                    Debug.Log("Bed");
                    SetIsMoving(true);
					isExercising = false;
					//  destTrans.transform.position = hit.point;
					//  destTrans.transform.position = new Vector3(deskHit.point.x, hit.point.y + yMousePos, hit.point.z);
					ResetTransformParent();
                    GameManager.Instance.GoRest();
                }

                //for the couch
                if (Physics.Raycast(ray, out RaycastHit couchHit, distanceForRay, layerForCouch))
                {
                    Debug.Log("Couch");
                    SetIsMoving(true);
					isExercising = false;
					//  destTrans.transform.position = hit.point;
					//  destTrans.transform.position = new Vector3(deskHit.point.x, hit.point.y + yMousePos, hit.point.z);
					ResetTransformParent();
					GameManager.Instance.GoToCouch();
                }
            }
        }

		//HandleRotation();
	}

    public void ResetTransformParent()
    {
        if(playerParent.parent == couchParent)
            playerParent.SetParent(normalParent);

		if (playerParent.parent == bedParent)
			playerParent.SetParent(normalParent);

		if (playerParent.parent == deskParent)
			playerParent.SetParent(normalParent);
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
        if(GameManager.Instance.GetPlayerMode() != GameManager.PlayerMode.Studying)
			GameManager.Instance.SetMonitorScreen(false);


		if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Studying)
            HandleWorkingState();
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Resting)
            HandleSleepingState();
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Exercising)
            HandleExercisingState();
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Couch)
            HandleCouchState();
        if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.Walking)
        {
			SetIsInState(false);
			HandleIdleState();
        }
		if (GameManager.Instance.GetPlayerMode() == GameManager.PlayerMode.IdleWalk)
			HandleIdleState();

		DebugText(GameManager.Instance.GetPlayerMode().ToString());

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
        SetIsInState(false);
        Anim().Play(isIdlingHash);
        Agent().SetDestination(Destination());
        Agent().isStopped = true;

        GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.IdleWalk);
    }
    public void HandleMovingState()
    {
		if (!isMoving)
		{
			SetIsMoving(false);
			CheckStates();
            return;
		}

		if (isMoving)
        {
			//GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Walking);
			//distanee
			//Debug.Log(Vector3.Distance(state.GetCurrentPos(), state.Destination()));
			if (Vector3.Distance(GetCurrentPos(), Destination()) <= StoppingDistance)
            {
                //Debug.Log("Reached Destination");
                SetIsMoving(false);

                if(isExercising)
                    HandleExercisingState();
                else
                    CheckStates();

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
        }
        //      if(!isMoving)
        //{
        //	SetIsMoving(false);
        //	CheckStates();
        //}

    }
    void HandleMovement()
    {
        SetIsInState(true);
        if (comesFromAnimation)
        {
          //  Anim().SetTrigger("StandUp");
            Anim().Play(IsSittingToStandCouchHash);
			comesFromAnimation = false;
		}
        else
            Anim().Play(IsWalkingHash);

      

        Agent().SetDestination(Destination());
        Agent().baseOffset = baseOffsetDefault;
        Agent().isStopped = false;
		Agent().updateRotation = true;

		//  GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Walking);
	}

    public void HandleWorkingState()
    {

            //Debug.Log("Camera Change");
            HandleWorking();
    }
    void HandleWorking()
    {
        if (!sitAtDeskAnimHasPlayed)
        {
            Debug.Log("Switch Camera + Sit down and work");
            SetIsInState(true);
            comesFromAnimation = true;

            ForceSleepPosition(SitAtDeskhPosition);
            ForceSleepRotation(SitAtDeskhRotation);

            Agent().isStopped = true;
            Agent().baseOffset = baseOffsetDeskValue;

            Anim().Play(IsGoingToWorkHash);

            GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Studying);
			StartCoroutine(TypingAnimationEventHandler());
		}
    }
	public IEnumerator TypingAnimationEventHandler()
	{
		sitAtDeskAnimHasPlayed = !sitAtDeskAnimHasPlayed;

		yield return new WaitForSeconds(1.2f);
		ForceSleepPosition(SitAtDeskhPosition);
        ChangeWorkingCamera();
	}
	//Call through animation events
	public void ChangeWorkingCamera()
	{
      //  Anim().Play(IsGoingToWorkHash);
        GameManager.Instance.ChangeToStudyCamera();
    }
    public void HandleSleepingState()
	{
        HandleSleeping();
	}
    void HandleSleeping()
    {
        if (!sitOnBedAnimHasPlayed)
		{
			SetIsInState(true);
			comesFromAnimation = true;
			Debug.Log("Sleep/Rest now");

			//Agent().SetDestination(Destination());
			Agent().isStopped = true;

            Anim().SetTrigger(restingAnimName);
			//Anim().Play(IsRestingHash);
			
          
			ForceSleepPosition(standToSitOnBedPosition);
            ForceSleepRotation(standToSitOnBedRotation);

			toRotate.transform.rotation = new Quaternion(standToSitOnBedRotation.x, standToSitOnBedRotation.y, standToSitOnBedRotation.z, 1);
			//playerParent.transform.SetParent(bedParent);

			GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Resting);
            StartCoroutine(SleepingAnimationEventHandler());
        }
    }

    public void HandleExercisingState()
    {
        HandleExercise();
    }
    void HandleExercise()
    {
            /*if (GetExercising())
                return;*/

            SetIsInState(true);
            Debug.Log("Exercising");
       
        if (!isExercising)
        {
            numberOfExercisingAnimations = Random.Range(0, exercisingAnimClips.Length - 1);
            Debug.Log(exercisingAnimClips[numberOfExercisingAnimations].name);
            Agent().isStopped = true;
            isExercising = true;
        }
		Anim().Play(exercisingAnimClips[numberOfExercisingAnimations].name);
		//SetIsMoving(false);
		GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Exercising);
        
    }

    public void HandleCouchState()
    {
        HandleCouch();
    }
    void HandleCouch()
    {
		if (!sitOnCouchAnimHasPlayed)
		{
			SetIsInState(true);
            comesFromAnimation= true;
			Debug.Log("Couch/Rest now");

			//Agent().SetDestination(Destination());
			Agent().isStopped = true;
           
			//Anim().SetTrigger(restingAnimName);
			Anim().Play(IsGoingToCouchHash);

            playerParent.transform.SetParent(couchParent);
			ForceSleepPosition(standToSitOnCouchPosition);
			toRotate.transform.rotation = new Quaternion(standToSitOnCouchRotation.x, standToSitOnCouchRotation.y, standToSitOnCouchRotation.z, 1);
			//ForceSleepRotation(standToSitOnCouchRotation);

			GameManager.Instance.SetPlayerMode(GameManager.PlayerMode.Couch);
			StartCoroutine(CouchLayingAnimationEventHandler());
		}
		//ForceSleepRotation(standToSitOnCouchRotation);
	}
	public IEnumerator CouchLayingAnimationEventHandler()
	{
		sitOnCouchAnimHasPlayed = !sitOnCouchAnimHasPlayed;

		yield return new WaitForSeconds(5f);
		ForceSleepPosition(sitOnCouchPosition);
		Agent().baseOffset = -0.25f;
        Agent().updateRotation= false;
		//ForceSleepRotation(standToSitOnCouchRotation);
	}
	public IEnumerator SleepingAnimationEventHandler()
    {
        sitOnBedAnimHasPlayed = !sitOnBedAnimHasPlayed;

        yield return new WaitForSeconds(4f);
        Agent().baseOffset = -2f;
        ForceSleepPosition(layOnBedPosition);
		
	}
    void ForceSleepPosition(Vector3 pos)
    {
        Debug.Log("Position Set");
       playerParent.transform.position = pos;
   //  playerParent.transform.position = Vector3.Lerp(playerParent.transform.position, pos, rotationTime * Time.deltaTime);
    }
    void ForceSleepRotation(Vector3 pos)
    {
		Debug.Log("Rotation Set");
        Vector3 posToLookAt;

        ////Change the position the player should look at
        posToLookAt.x = pos.x;
        posToLookAt.y = pos.z;
        posToLookAt.z = pos.y;

        ////Get here the player is currently facing
        // Quaternion currentRot =new Vector3(posToLookAt;

        ////create prefered rotation based on here the player is going
        //	Quaternion targetRotation = Quaternion.LookRotation(pos);
        //transform.Rotate(pos);
		transform.rotation = new Quaternion(pos.x, pos.y, pos.z, Time.deltaTime);
		//   transform.rotation = posToLookAt;
		//Rotate the player
		//transform.rotation = Quaternion.Slerp(currentRot, targetRotation, rotationTime * Time.deltaTime);

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

    public bool SetExercisingMode(bool v)
    {
        isExercising = v;
        return isExercising;
    }

    public bool GetExercising()
    {
        return isExercising;
    }
    //Stringsss
    public string IsIdleHash { get { return idlingAnimName; } }
	public string IsSittingToStandCouchHash { get { return sitToStandCouchAnimName; } }
	public string IsWalkingHash { get { return walkingAnimName; } }
    public string IsWorkingHash { get { return workingAnimName; } }
    public string IsGoingToWorkHash { get { return goingToWorkAnimName; } }
	public string IsGoingToCouchHash { get { return goingToCouchAnimName; } }

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