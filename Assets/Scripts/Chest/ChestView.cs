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

    public int coinsReward { get; private set; }
    public int gemsReward {  get; private set; }

    private void Update()
    {
        controller.StateMachine?.Update();
    }

    private void OnDisable()
    {
        controller.StateMachine?.ChangeState(ChestState.LOCKED);
        controller.ResetTimer();
        InitializeChestData();
    }

    public void SetChestStateText(ChestState chestState) => chestStateText.text = chestState.ToString(); //Text that shows whether the chest is locked, unlocked, etc.
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

    private void SetAppropriateText(out string text, int time) => text = time < 10 ? $"0{time}" : time.ToString(); //Adds 0 as prefix to number if needed

    public void SetRewards(int coins, int gems) { coinsReward = coins; gemsReward = gems; }
    public void InitializeChestData() //Sets the image and time to unlock when the chest is locked
    {
        chestImage.sprite = controller.ChestData.ChestImage;
        controller.ResetTimer();
        UpdateChestTimerText(controller.GetTimeLeft());
    }
    public void OnChestQueued()
    {
        controller.StateMachine.ChangeState(ChestState.LOCKED);
        SetChestStateText(ChestState.QUEUED);
    }

    //Based on the state of the chest, the corresponding UI panel would be shown accordingly.
    public void HandleOnClickEvent() => controller.OnChestClicked();

    public void ProcessCommand(Command command) //Processes command to update original time remaining in the Command Data
    {
        command.commandData.SetChestTimeStep(controller.GetTimeStep());
        GameService.Instance.CommandService.CommandInvoker.ProcessCommand(command);
    }
}
