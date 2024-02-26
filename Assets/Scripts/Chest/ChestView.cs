using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    [SerializeField] private Image chestImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI chestStateText;
    public ChestController controller {  get; private set; }

    private void Update()
    {
        controller.StateMachine?.Update();
    }

    public void SetChestStateText(ChestState chestState) => chestStateText.text = chestState.ToString();
    public void SetChestController(ChestController controller) => this.controller = controller;
    public void UpdateChestTimerText(ChestTime timeLeft) //Updates the time shown as text in the UI
    {
        string minsText;
        string secondsText;
        string hoursText;

        SetAppropriateText(out secondsText, timeLeft.seconds);
        SetAppropriateText(out minsText, timeLeft.minutes);
        SetAppropriateText(out hoursText, timeLeft.hours);

        timerText.text = $"{hoursText}: {minsText} : {secondsText}";
    }

    private void SetAppropriateText(out string text, int time) => text = time < 10 ? $"0{time}" : time.ToString();
    public void InitializeChestData() //Sets the image and time to unlock when the chest is locked
    {
        chestImage.sprite = controller.ChestData.ChestImage;
        UpdateChestTimerText(controller.GetTimeLeft());
    }

    public void HandleOnClickEvent()
    {
        switch(controller.StateMachine.ChestState)
        {
            case ChestState.LOCKED:
                GameService.Instance.EventService.onLockedChestClicked.Invoke(this); break;
            case ChestState.UNLOCKING:
                GameService.Instance.EventService.onUnlockingChestClicked.Invoke(this); break;
            default: break;
        }
    }
}
