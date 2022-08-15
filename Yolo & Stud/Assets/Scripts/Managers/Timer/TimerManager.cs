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
    [SerializeField] float duration;

    [Header("Unity Events")]
    [Tooltip("Assign the action to be taken after the timer finishes")]
    [SerializeField] UnityEvent onTimerEnd = null;

    void Start()
    {
        //Make sure the timer is visible
        timeDisplay.gameObject.SetActive(true);
        timeDisplay.gameObject.GetComponent<Animator>().Play("DisplayIdle");

        //Start Timer
        timer = new Timer(duration);

        timer.onTimerEnd += HandleTimeEnd;

        //set Colors
        defaultColor = timeDisplay.color;
    }


    void Update()
    {
        timer.TimeTick(Time.deltaTime);
        UpdateUI(timer.secondsLeft);
    }

    void HandleTimeEnd()
	{
        onTimerEnd.Invoke();

        //what to do next
        Destroy(this);
	}

    void UpdateUI(float presentTime)
	{
        presentTime += 1;

        //THis is to diplay the time in a clock format
        float mins = Mathf.FloorToInt(presentTime / 60);
        float secs = Mathf.FloorToInt(presentTime % 60);

        if(presentTime <= 0f)
            presentTime = 0f;

        timeDisplay.text = String.Format("{0:00} : {1:00}", mins, secs);



        //FLash Red & Animation Logic
        if (secs <= 10f && timeDisplay != null)
        {
            timeDisplay.gameObject.GetComponent<Animator>().Play("DisplayFlash");
            timeDisplay.color = timesUpColor;
        }
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/