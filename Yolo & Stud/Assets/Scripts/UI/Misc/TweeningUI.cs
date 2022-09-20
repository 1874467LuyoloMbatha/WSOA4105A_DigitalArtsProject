using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TweeningUI : MonoBehaviour
{
	public enum tweenType { Move, MoveYPunch, Scale, ScaleX, ScaleY, Fade, FadeIE} //Different types of UI we can use

	[Header("Unity Handles")]
	[Tooltip("Insert Object this is assigned to")]
	[SerializeField] GameObject objectToTween;
	[Tooltip("Only Applies if startAtOffset is True")]
	[SerializeField] Vector3 from, to;

	[Header("Type Of Tween and Options")]
	[SerializeField] tweenType TweenType;
	[Tooltip("What type of ease animation do youw want?")]
	[SerializeField] LeanTweenType easeType;
	[SerializeField] LTDescr tweenObj;

	[Header("Floats")]
	[SerializeField] float duration;
	[SerializeField] float delay;

	[Header("Booleans")]
	[SerializeField] bool loopTween;
	[SerializeField] bool pingpong;
	[Tooltip("The aniamtion starts at an offset if true")]
	[SerializeField] bool startAtOffset;
	[SerializeField] bool showOnEnable;
	[SerializeField] bool workOnDisable;

	public void OnEnable()
	{
		if (showOnEnable)
			Show();
	}

	public void OnDisable()
	{
		if (workOnDisable)
			Disable();
	}

	#region Tweening
	public void Show()
	{
		HandleTween();
	}

	public void HandleTween()
	{
		if (objectToTween == null)
			objectToTween = gameObject;

		LeanTween.cancel(gameObject);

		//This switches between the different options depending on what you type you chose in the inspector
		switch (TweenType)
		{
			case tweenType.Fade:
				Fade();
				break;
			case tweenType.FadeIE:
				StartCoroutine(FadeIE());
				break;
			case tweenType.Move:
				MoveAbs();
				break;
			case tweenType.MoveYPunch:
				MoveYPunch();
				break;
			case tweenType.Scale:
				Scale();
				break;
			case tweenType.ScaleX:
				Scale();
				break;
			case tweenType.ScaleY:
				Scale();
				break;
		}

		tweenObj.setDelay(delay);
		tweenObj.setEase(easeType);

		//Depends on what we choose in Inspector
		//Loop loops
		//Ping pong is like an Instagram Boomerang
		if (loopTween)
			tweenObj.loopCount = int.MaxValue;
		if (pingpong)
			tweenObj.setLoopPingPong();
	}


	//Tween Types
	public void Fade()
	{
		//Clears Errors
		if (gameObject.GetComponent<CanvasGroup>() == null)
			gameObject.AddComponent<CanvasGroup>();

		CanvasGroup cg = objectToTween.gameObject.GetComponent<CanvasGroup>();

		if (startAtOffset)
			cg.alpha = from.x;

		tweenObj = LeanTween.alphaCanvas(cg, to.x, duration);
	}

	public IEnumerator FadeIE()
	{
		Fade();
		yield return new WaitForSeconds(duration);
		SwapDirection();
	}
	public void MoveAbs()
	{
		RectTransform rt = objectToTween.GetComponent<RectTransform>();

		rt.anchoredPosition = from;

		tweenObj = LeanTween.move(rt, to, duration);
	}

	public void MoveYPunch()
	{
		// *********** Pause Button **********
		// Drop pause button in
		RectTransform rt = objectToTween.GetComponent<RectTransform>();
		rt.anchoredPosition3D += from;
		//LeanTween.moveY(objectToTween, rt.anchoredPosition3D.y + -200f, 0.6f).setEase(LeanTweenType.easeOutSine).setDelay(0.6f);
		tweenObj = LeanTween.moveY(objectToTween, rt.anchoredPosition3D.y + -200, duration);

		/*/ Punch Pause Symbol
		RectTransform pauseText = pauseWindow.Find("PauseText").GetComponent<RectTransform>();
		LeanTween.moveZ(pauseText, pauseText.anchoredPosition3D.z - 80f, 1.5f).setEase(LeanTweenType.punch).setDelay(2.0f);

		// Rotate rings around in opposite directions
		LeanTween.rotateAroundLocal(pauseRing1, Vector3.forward, 360f, 12f).setRepeat(-1);
		LeanTween.rotateAroundLocal(pauseRing2, Vector3.forward, -360f, 22f).setRepeat(-1);
		*/
	}
	public void Scale()
	{
		if (startAtOffset)
			objectToTween.GetComponent<RectTransform>().localScale = from;

		tweenObj = LeanTween.scale(objectToTween, to, duration);
	}

	void SwapDirection()
	{
		var temp = from;
		from = to;
		to = temp;
	}

	public void Disable()
	{
		SwapDirection();
		HandleTween();

		tweenObj.setOnComplete(() =>
		{
			//Ensures that next time an object is enabled, its variables return to teh normal set values
			SwapDirection();

			gameObject.SetActive(false);
		});
	}
#endregion

	public float Duration()
	{
		return duration;
	}
}
