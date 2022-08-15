using System;
using System.Collections;
using System.Collections.Generic;

public class Timer
{
	public float secondsLeft { get; set; }
	public Timer(float duration)
	{
		secondsLeft = duration;
	}

	public event Action onTimerEnd;

	public void TimeTick(float deltaTime)
	{
		// it stops if this is less that 0
		if (secondsLeft == 0f)
			return;

		secondsLeft -= deltaTime;

		CheckTimerEnd();
	}

	void CheckTimerEnd()
	{
		//do nothing if we still have time
		if(secondsLeft > 0f)
			return ;

		secondsLeft = 0f;

		onTimerEnd?.Invoke();
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/