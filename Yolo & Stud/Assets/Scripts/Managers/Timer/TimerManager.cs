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
    [Tooltip("Assign the timer pause button")]
    [SerializeField] Button pauseBtn;
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
    [Tooltip("Inputted values")]
    [SerializeField] float playerDuration, playerShortBreak, playerLongBreak;
    [Tooltip("Default values for POMODORO")]
    [SerializeField] float defaultDuration = 1200f, defaultShortBreak = 300f, defaultLongBreak = 900f;
    [Tooltip("This will format the text on screeen")]
    [SerializeField] float timeCap = 3000f, shortTimeCap, longTimeCap;
    [Tooltip("This is the minute cap before timer flashes red")]
    [SerializeField] float flashMinCap = 2f;
    [Tooltip("This is the seconds  cap before timer flashes red")]
    [SerializeField] float flashSecCap = 2f;

    [Header("Integers")]
    [Tooltip("Track if the timer has repeated")]
    [SerializeField] int checkIfTimerRan;

    [Header("Unity Events")]
    [Tooltip("Assign the action to be taken after the timer finishes")]
    [SerializeField] UnityEvent onTimerEnd = null;

    void Start()
    {
        //  TimerSetUp(duration);
        //set Colors
        defaultColor = timeDisplay.color;
        timeDisplay.gameObject.GetComponent<Animator>().Play("DisplayIdle");
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
        timeDisplay.color = defaultColor;

        /* timeCap = duration * .3f;
         shortTimeCap = duration * .3f;
         longTimeCap = duration * .3f;*/

        //Setting flash values
        if (PomodoroSteps == PomodoroStages.Pomodoro)
        {
            flashMinCap = timeCap * .2f;
            flashSecCap = timeCap * .3f;
        }
        if (PomodoroSteps == PomodoroStages.shortBreak)
        {
            flashMinCap = shortTimeCap * .2f;
            flashSecCap = shortTimeCap * .3f;
        }
        if (PomodoroSteps == PomodoroStages.longBreak)
        {
            flashMinCap = longTimeCap * .2f;
            flashSecCap = longTimeCap * .3f;
        }
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
        if(checkIfTimerRan >= 2)
            SettingsMenu.Instance.SetRestarted(true);

        if (!SettingsMenu.Instance.GetRestarted())
        {
            if (PomodoroSteps == PomodoroStages.Pomodoro)
                duration = timer.secondsLeft;
            if (PomodoroSteps == PomodoroStages.shortBreak)
                shortBreakDuration = timer.secondsLeft;
            if (PomodoroSteps == PomodoroStages.longBreak)
                longBreakDuration = timer.secondsLeft;
        }
        else
		{
            timeDisplay.color = defaultColor;

            duration = playerDuration;
            shortBreakDuration = playerShortBreak;
            longBreakDuration = playerLongBreak;

            SettingsMenu.Instance.SetTimeState(true);
            TimerSetUp(duration);
            UpdateUI(duration);
		}

        /*else
		{
            if (PomodoroSteps == PomodoroStages.Pomodoro)
                duration = playerDuration;
            if (PomodoroSteps == PomodoroStages.shortBreak)
                shortBreakDuration = playerShortBreak;
            if (PomodoroSteps == PomodoroStages.longBreak)
                longBreakDuration = playerLongBreak;
        }*/


        if (SettingsMenu.Instance.GetTransition())
            StagesSetUp();
		else //If player have this setting off, it will just move to the next stage and wait for input
		{
            if (PomodoroSteps == PomodoroStages.Pomodoro)
            {
                PomodoroSteps = PomodoroStages.shortBreak;
               // TimerSetUp(shortBreakDuration);
                UpdateUI(shortBreakDuration);
                pauseBtn.onClick.Invoke();
                SettingsMenu.Instance.SetTimeState(false);
            }
            if (PomodoroSteps == PomodoroStages.shortBreak)
            {
                PomodoroSteps = PomodoroStages.longBreak;
                // TimerSetUp(shortBreakDuration);
                UpdateUI(longBreakDuration);
                pauseBtn.onClick.Invoke();
                SettingsMenu.Instance.SetTimeState(false);
            }
            if (PomodoroSteps == PomodoroStages.longBreak)
            {
                PomodoroSteps = PomodoroStages.Pomodoro;
                // TimerSetUp(shortBreakDuration);
                UpdateUI(duration);
                pauseBtn.onClick.Invoke();
                SettingsMenu.Instance.SetTimeState(false);
            }
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
       /* if(!SettingsMenu.Instance.GetTransition())
            SettingsMenu.Instance.SetTimeState(false);  */

        if(SettingsMenu.Instance.GetFirstTime())
		{
            duration = defaultDuration;
            shortBreakDuration = defaultShortBreak;
            longBreakDuration = defaultLongBreak;
            SettingsMenu.Instance.SetFirstTime(false);
		}

        if (SettingsMenu.Instance.GetRestarted())
            return;

        if (PomodoroSteps == PomodoroStages.Pomodoro)
        {
            //duration = timer.secondsLeft;

            if (duration != 0)
            {
                TimerSetUp(duration);
               // return;
            }

            else if (shortBreakDuration != 0)
            {
                TimerSetUp(shortBreakDuration);
                PomodoroSteps = PomodoroStages.shortBreak;
               
               // return;
            }

           else if (shortBreakDuration == 0 && longBreakDuration != 0)
            {
                TimerSetUp(longBreakDuration);
                duration = timer.secondsLeft;
                PomodoroSteps = PomodoroStages.longBreak;
               // return;
            }
        }

       else if(PomodoroSteps == PomodoroStages.shortBreak)
		{
            if(!SettingsMenu.Instance.GetRestarted() && checkIfTimerRan <= 0)
			{
                PomodoroSteps = PomodoroStages.Pomodoro;
                duration = playerDuration;
                TimerSetUp(duration);
                checkIfTimerRan++;
			}
            else if(longBreakDuration != 0 && checkIfTimerRan >= 1)
			{
                PomodoroSteps = PomodoroStages.longBreak;
                TimerSetUp(longBreakDuration);
                //return;
            }
		}


        else if (PomodoroSteps == PomodoroStages.longBreak)
        {
            longBreakDuration = timer.secondsLeft;

            if (longBreakDuration == 0)
            {
                PomodoroSteps = PomodoroStages.Pomodoro;
                checkIfTimerRan = 3;
                pauseBtn.onClick.Invoke();
                HandleTimeEnd();
            }
        }
    }
	#endregion

	#region Public Functions

    public void PlayTimer()
	{

        if (!SettingsMenu.Instance.GetFirstTime() && SettingsMenu.Instance.GetRestarted() && SettingsMenu.Instance.GetTimeState())
        {
            checkIfTimerRan = 0;
            SettingsMenu.Instance.SetRestarted(false);
            SettingsMenu.Instance.SetTimeState(!SettingsMenu.Instance.GetTimeState());
        }
        else if (SettingsMenu.Instance.GetTimeState())
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
            playerDuration = duration;
            timeCap = duration * .3f;
            SettingsMenu.Instance.SetFirstTime(false);
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
            playerShortBreak = shortBreakDuration;
            shortTimeCap = shortBreakDuration * .3f;
            SettingsMenu.Instance.SetFirstTime(false);
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
            playerLongBreak = longBreakDuration;
            longTimeCap = longBreakDuration * .3f;
            SettingsMenu.Instance.SetFirstTime(false);
            //duration = timeToSet.to;
            //TimerSetUp(longBreakDuration);
        }

    }
    #endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/