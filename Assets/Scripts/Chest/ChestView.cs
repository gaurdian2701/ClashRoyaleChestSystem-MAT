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

    private void Awake()
    {
        SetChestStateText(ChestState.LOCKED);
    }

    public void SetChestStateText(ChestState chestState) => chestStateText.text = chestState.ToString();
    public void SetChestController(ChestController controller) => this.controller = controller;
    public void SetChestTimerText(int mins, int seconds)
    {
        string minsText;
        string secondsText;

        SetAppropriateText(out minsText, mins);
        SetAppropriateText(out secondsText, seconds);

        timerText.text = $"{minsText} : {secondsText}";
    }

    private void SetAppropriateText(out string text, int time) => text = time < 10 ? $"0{time}" : time.ToString();
    public void InitializeChestData()
    {
        chestImage.sprite = controller.chestData.ChestImage;
        SetChestTimerText(controller.chestData.WaitTime, 0);
    }
}
