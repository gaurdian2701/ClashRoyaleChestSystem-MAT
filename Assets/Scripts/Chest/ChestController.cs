using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    public ChestStateMachine StateMachine { get; private set; }
    public ChestScriptableObject ChestData { get; private set; }
    public ChestView ChestView { get; private set; }

    private TimerController timerController;
    public ChestController(ChestView chestView, ChestScriptableObject chestSO)
    {   
        this.ChestView = chestView;
        ChestData = chestSO;
        timerController = new TimerController(ChestData.WaitTime);
        ChestView.SetChestController(this);
        CreateStateMachine();
    }

    public int GetGemsToUnlock()
    {
        ChestTime timeLeft = timerController.GetCurrentTime();
        return timeLeft.hours > 0 ? timeLeft.minutes * timeLeft.hours : timeLeft.minutes;
    }

    public void UpdateTimeStep(float timeStep)
    {
        timerController.CountTime(timeStep);
        ChestView.UpdateChestTimerText(timerController.GetCurrentTime());
    }
    public ChestTime GetTimeLeft() => timerController.GetCurrentTime();
    private void CreateStateMachine()
    {
        StateMachine = new ChestStateMachine(this);
    }
}
