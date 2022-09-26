using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TimerManager : MonoBehaviour
{
    [Header("External Scripts")]
    Timer timer;

    [Header("Time Related Variables")]
    [Tooltip("Assign the text GameObject to display the time")]
    [SerializeField] Text timeDisplay;
    [Tooltip("Assign the colors needed to display the time")]
    [SerializeField] Color defaultColor;
    [SerializeField] Color timesUpColor;

    [Header("Floats")]
    [Tooltip("How long should the time run for")]
    [SerializeField] float duration;
    [Tooltip("This will format the text on screeen")]
    [SerializeField] float timeCap = 3000f;
    [Tooltip("This is the minute cap before timer flashes red")]
    [SerializeField] float flashMinCap = 2f;
    [Tooltip("This is the seconds  cap before timer flashes red")]
    [SerializeField] float flashSecCap = 2f;

    [Header("Unity Events")]
    [Tooltip("Assign the action to be taken after the timer finishes")]
    [SerializeField] UnityEvent onTimerEnd = null;

    void Start()
    {
        TimerSetUp(duration);
    }

    void TimerSetUp(float duration)
    {
        //Make sure the timer is visible
        timeDisplay.gameObject.SetActive(true);
        timeDisplay.gameObject.GetComponent<Animator>().Play("DisplayIdle");
       // timeDisplay.gameObject.GetComponent<Button>().interactable = false;

        //Start Timer
        if (timer == null)
            timer = new Timer(duration);
        else
        {
            timer = null;
            timer = new Timer(duration);
        }

        timer.onTimerEnd += HandleTimeEnd;

        //set Colors
        defaultColor = timeDisplay.color;

        timeCap = duration * .3f;

        //Setting flash values
        flashMinCap = timeCap * .2f;
        flashSecCap = timeCap * .3f;
    }

    void Update()
    {
        timer.TimeTick(Time.deltaTime);
        UpdateUI(timer.secondsLeft);
    }

    #region Private Functions
    void HandleTimeEnd()
    {
        duration = timer.secondsLeft;

        timeDisplay.color = defaultColor;
        timeDisplay.text = "Click Me To Set A Timer";

        onTimerEnd.Invoke();



        //what to do next
        //Destroy(this);
    }

    void UpdateUI(float presentTime)
    {
        if(duration <= 0f)
		{
            return;
		}
        presentTime += 1;

        //THis is to diplay the time in a clock format
        //float hours = Mathf.FloorToInt((presentTime / 3600) % 60);
        // float m = t % 3600;
        float mins = Mathf.FloorToInt(presentTime / 60);
        float secs = Mathf.FloorToInt(presentTime % 60);

        if (presentTime <= 0f)
        {
            presentTime = 0f;
            mins = 0f;
            secs = 0f;
        }

        //UI quality control
        if (presentTime <= timeCap || timer.secondsLeft <= 60f)
            timeDisplay.text = String.Format("{0:00} : {1:00}",mins, secs);
        else
            timeDisplay.text = mins.ToString("00") + " minutes remaining";
        //timeDisplay.text =  hours.ToString("00") + " : " + mins.ToString("00") + " : " +secs.ToString("00");

        //FLash Red & Animation Logic
        if (mins <= flashMinCap && secs <= flashSecCap && timeDisplay != null)
        {
            timeDisplay.gameObject.GetComponent<Animator>().Play("DisplayFlash");
            timeDisplay.color = timesUpColor;
        }
        
	}
	#endregion

	#region Public Functions

    public void SetTimer(string timeToSet)
	{
        
        if (float.TryParse(timeToSet, out duration))
        {
            //Debug.Log("String is the number: " + duration);

            duration *= 60;
            //duration = timeToSet.to;
            TimerSetUp(duration);
        }

	}
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/