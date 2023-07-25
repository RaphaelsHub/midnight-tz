using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int secondsInt;

    // Define the timeT struct outside the class and make it public
    public struct timeT
    {
        public float seconds;
        public int minutes;
    }

    public timeT myTime = new timeT
    {
        seconds = 0f,
        minutes = 3,
    };

    private void Update()
    {
        if (GameManager.gameIsActive && !GameManager.gameIsOver && myTime.minutes >= 0)
            TimerDown();
    }

    private void TimerDown()
    {
        myTime.seconds -= Time.deltaTime;

        if (myTime.seconds < 0f && myTime.seconds != 0f)
        {
            myTime.minutes--;
            myTime.seconds = 59.59f;
        }

        secondsInt = (int)myTime.seconds;
    }
}

