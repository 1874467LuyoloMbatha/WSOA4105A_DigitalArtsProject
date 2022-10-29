using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TimerManager : MonoBehaviour
{
    public enum PomodoroStages { Pomodoro, shortBreak, longBreak}

	[Header("Timer Stages")]
    public PomodoroStages PomodoroSteps;

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
    [Tooltip("How long should the time run for during breaks")]
    [SerializeField] float shortBreakDuration, longBreakDuration;
    [Tooltip("This will format the text on screeen")]
    [SerializeField] float timeCap = 3000f, shortTimeCap, longTimeCap;
    [Tooltip("This is the minute cap before timer flashes red")]
    [SerializeField] float flashMinCap = 2f;
    [Tooltip("This is the seconds  cap before timer flashes red")]
    [SerializeField] float flashSecCap = 2f;

    [Header("Unity Events")]
    [Tooltip("Assign the action to be taken after the timer finishes")]
    [SerializeField] UnityEvent onTimerEnd = null;

    void Start()
    {
      //  TimerSetUp(duration);
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

       /* timeCap = duration * .3f;
        shortTimeCap = duration * .3f;
        longTimeCap = duration * .3f;*/

        //Setting flash values
        flashMinCap = timeCap * .2f;
        flashSecCap = timeCap * .3f;
    }

    void Update()
    {
        if (timer != null)
        {
            timer.TimeTick(Time.deltaTime);
            UpdateUI(timer.secondsLeft);
        }
    }

    #region Private Functions
    void HandleTimeEnd()
    {
        if (PomodoroSteps == PomodoroStages.Pomodoro)
            duration = timer.secondsLeft;
        if (PomodoroSteps == PomodoroStages.shortBreak)
            shortBreakDuration = timer.secondsLeft;
        if (PomodoroSteps == PomodoroStages.longBreak)
            longBreakDuration = timer.secondsLeft;

        if (SettingsMenu.Instance.GetTransition())
            StagesSetUp();
		else //If player have this setting off, it will just move to the next stage and wait for input
		{
            if (PomodoroSteps == PomodoroStages.Pomodoro)
                PomodoroSteps = PomodoroStages.shortBreak;
            if (PomodoroSteps == PomodoroStages.shortBreak)
                PomodoroSteps = PomodoroStages.longBreak;
            if (PomodoroSteps == PomodoroStages.longBreak)
                PomodoroSteps = PomodoroStages.Pomodoro;
        }


        timeDisplay.color = defaultColor;
        //timeDisplay.text = "Click Me To Set A Timer";

        onTimerEnd.Invoke();



        //what to do next
        //Destroy(this);
    }

    void UpdateUI(float presentTime)
    {
        if (PomodoroSteps == PomodoroStages.Pomodoro)
        {
            if (duration <= 0f)
            {
                return;
            }
        }
        if (PomodoroSteps == PomodoroStages.shortBreak)
        {
            if (shortBreakDuration <= 0f)
            {
                return;
            }
        }
        if (PomodoroSteps == PomodoroStages.longBreak)
        {
            if (longBreakDuration <= 0f)
            {
                return;
            }
        }
        presentTime += 1;

        //THis is to diplay the time in a clock format
        //float hours = Mathf.FloorToInt((presentTime / 3600) % 60);
        // float m = t % 3600;
        float mins = Mathf.FloorToInt(presentTime / 60);
        float secs = Mathf.FloorToInt(presentTime % 60);

        if (presentTime < 0f)
        {
            presentTime = 0f;
            mins = 0f;
            secs = 0f;
        }

        //UI quality control
        if (!SettingsMenu.Instance.GetTimeDisplay())
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

    void StagesSetUp()
	{
        if (PomodoroSteps == PomodoroStages.Pomodoro)
        {
            //duration = timer.secondsLeft;

            if (duration != 0)
            {
                TimerSetUp(duration);
                return;
            }

            if (shortBreakDuration != 0)
            {
                PomodoroSteps = PomodoroStages.shortBreak;
                TimerSetUp(shortBreakDuration);
                return;
            }

            if (shortBreakDuration == 0 && longBreakDuration != 0)
            {
                duration = timer.secondsLeft;
                PomodoroSteps = PomodoroStages.longBreak;
                TimerSetUp(longBreakDuration);
                return;
            }
        }

        if(PomodoroSteps == PomodoroStages.shortBreak)
		{
            if(longBreakDuration != 0)
			{
                PomodoroSteps = PomodoroStages.longBreak;
                TimerSetUp(longBreakDuration);
                return;
            }
		}


        if (PomodoroSteps == PomodoroStages.longBreak)
        {
            longBreakDuration = timer.secondsLeft;

            if (longBreakDuration == 0)
            {
                PomodoroSteps = PomodoroStages.Pomodoro;
            }
        }
    }
	#endregion

	#region Public Functions

    public void PlayTimer()
	{
        if(SettingsMenu.Instance.GetTimeState())
		{
            SettingsMenu.Instance.SetTimeState(!SettingsMenu.Instance.GetTimeState());
            return;
        }

        StagesSetUp();
    }

    public void PauseTimer()
    {
       SettingsMenu.Instance.SetTimeState(!SettingsMenu.Instance.GetTimeState());
    }

    public void SetPomoTimer(string timeToSet)
	{
        
        if (float.TryParse(timeToSet, out duration))
        {
            //Debug.Log("String is the number: " + duration);

            duration *= 60;
            timeCap = duration * .3f;
            //duration = timeToSet.to;
            //  TimerSetUp(duration);
        }

	}

    public void SetShortBreakTimer(string timeToSet)
    {

        if (float.TryParse(timeToSet, out shortBreakDuration))
        {
            //Debug.Log("String is the number: " + duration);

            shortBreakDuration *= 60;
            shortTimeCap = shortBreakDuration * .3f;
            //duration = timeToSet.to;
            //TimerSetUp(shortBreakDuration);
        }

    }
    public void SetLongBreakimer(string timeToSet)
    {

        if (float.TryParse(timeToSet, out longBreakDuration))
        {
            //Debug.Log("String is the number: " + duration);

            longBreakDuration *= 60;
            longTimeCap = longBreakDuration * .3f;
            //duration = timeToSet.to;
            //TimerSetUp(longBreakDuration);
        }

    }
    #endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/