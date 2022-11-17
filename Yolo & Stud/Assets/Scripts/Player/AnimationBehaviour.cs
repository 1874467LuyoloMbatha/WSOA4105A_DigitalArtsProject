using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : StateMachineBehaviour
{
	[Header("Variables")]
	[Tooltip("Handles the time before the animations will change")]
	[SerializeField] float timeUntilIdleChange;
	[Tooltip("Handles the time when idling")]
	[SerializeField] float idleTime;
	[Tooltip("Handles the transition throughout animations")]
	[SerializeField] float transitionTime = 0.2f;
	[Tooltip("Enter The number of animations we have")]
	[SerializeField] int numberOfAnimations;
	[Tooltip("Stores the current animation")]
	[SerializeField] int boredAnim;

	[Header("Booleans")]
	[Tooltip("Stores when the player can change the animation")]
	[SerializeField] bool canChange;

	[Header("Generic Variables")]
	[Tooltip("Stores the string to trigger idle")]
	[SerializeField] string idleParameterName;

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		ResetIdle();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(!canChange)
		{
			idleTime += Time.deltaTime;

			if(idleTime > timeUntilIdleChange && stateInfo.normalizedTime % 1 < 0.02f)
			{
				canChange = true;
				boredAnim = Random.Range(1, numberOfAnimations + 1);
				boredAnim = boredAnim * 2 - 1;

				animator.SetFloat(idleParameterName, boredAnim - 1);
			}
		}
		else if(stateInfo.normalizedTime % 1 > 0.98f)
		{
			ResetIdle();
		}

		animator.SetFloat(idleParameterName, boredAnim, transitionTime, Time.deltaTime);
	}

	void ResetIdle()
	{
		if (canChange)
			boredAnim--;

		canChange = false;
		idleTime = 0;
	}
}
