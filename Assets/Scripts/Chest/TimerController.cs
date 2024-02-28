using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController
{
    private ChestTime currentTime;
    public TimerController(int waitTime)
    {
        currentTime = new ChestTime();
        InitializeTime(waitTime);
    }

    private void InitializeTime(int waitTime)
    {
        int hours = waitTime / 60;
        int minutes = waitTime % 60;
        int seconds = 0;
        UpdateTime(hours, minutes, seconds);
    }

    private void UpdateTime(int hours , int minutes, int seconds) => currentTime.SetCurrentTime(hours, minutes, seconds);
    public ChestTime GetCurrentTime() => currentTime;

    //Converts the seconds left to unlock to H/M/S format
    public void CountTime(float timeStep)
    {
        int seconds = Mathf.FloorToInt(timeStep % 60);
        int minutes = Mathf.FloorToInt((timeStep / 60) % 60);
        int hours = Mathf.FloorToInt(timeStep / 3600);
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

