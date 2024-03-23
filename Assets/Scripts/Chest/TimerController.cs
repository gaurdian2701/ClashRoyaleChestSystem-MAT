using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController
{
    private ChestTime currentTime;
    private const int maxMinutesAndSeconds = 60;
    private const int secondsInAnHour = 3600;
    public TimerController(int waitTime)
    {
        currentTime = new ChestTime();
        InitializeTime(waitTime);
    }

    public void InitializeTime(int waitTime)
    {
        int hours = waitTime / maxMinutesAndSeconds;
        int minutes = waitTime % maxMinutesAndSeconds;
        int seconds = 0;
        UpdateTime(hours, minutes, seconds);
    }

    private void UpdateTime(int hours , int minutes, int seconds) => currentTime.SetCurrentTime(hours, minutes, seconds);
    public ChestTime GetCurrentTime() => currentTime;

    //Converts the seconds left to unlock to H/M/S format
    public void CountTime(float timeStep)
    {
        int seconds = Mathf.FloorToInt(timeStep % maxMinutesAndSeconds);
        int minutes = Mathf.FloorToInt((timeStep / maxMinutesAndSeconds) % maxMinutesAndSeconds);
        int hours = Mathf.FloorToInt(timeStep / secondsInAnHour);
        UpdateTime(hours, minutes, seconds);
    }
}

public struct ChestTime
{
    public int hours { get; private set; }
    public int minutes { get; private set; }
    public int seconds { get; private set; }

    public void SetCurrentTime(int hours, int minutes, int seconds)
    {
        this.hours = hours; this.minutes = minutes; this.seconds = seconds;
    }
}

