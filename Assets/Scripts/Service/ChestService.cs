using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService
{
    private List<ChestScriptableObject> chestsList;
    private List<ChestView> chestsInQueueForUnlock;
    private ChestView chestPrefab;
    private int chestsLimit;
    private int numberOfChestsGenerated;
    private System.Random random;

    public ChestService(ChestView chestPrefab, ChestServiceScriptableObject chestServiceSO)
    {
        this.chestPrefab = chestPrefab;
        chestsLimit = chestServiceSO.chestLimit;
        numberOfChestsGenerated = 0;
        chestsInQueueForUnlock = new List<ChestView>();
        LoadChests();
    }
    private void LoadChests()
    {
        ChestScriptableObject[] cList;
        cList = Resources.LoadAll<ChestScriptableObject>("Chests");
        chestsList = new List<ChestScriptableObject>(cList);
        chestsList.Sort((x,y) => x.ChestRarity.CompareTo(y.ChestRarity));
    }
    public void GenerateRandomChest()
    {
        if (numberOfChestsGenerated >= chestsLimit)
            return;

        numberOfChestsGenerated++;
        random = new System.Random();

        int chestType = random.Next((int)ChestRarity.Common, (int)ChestRarity.Legendary);

        ChestScriptableObject chestSO = chestsList[chestType];
        CreateChest(chestSO);
    }

    public void AddChestToWaitingQueue(ChestView chestView)
    {
        if (chestsInQueueForUnlock.Count <= 0)
        {
            GameService.Instance.EventService.onStartUnlockingChestSuccessful?.Invoke();
            StartUnlockingChest(chestView);
        }
        else
        {
            SetChestStateForQueueing(chestView);
            GameService.Instance.EventService.onStartUnlockingChestFailed?.Invoke();
        }
        chestsInQueueForUnlock.Add(chestView);
    }

    public void ProcessCommand(Command command)
    {
        command.commandData.SetChestIndexInQueue(chestsInQueueForUnlock.IndexOf(command.commandData.ChestView));
        GameService.Instance.CommandService.CommandInvoker.ProcessCommand(command);
    }

    private void SetChestStateForQueueing(ChestView chestView) => chestView.SetChestStateText(ChestState.QUEUED);
    private void StartUnlockingChest(ChestView chestView) => chestView.controller.StateMachine.ChangeState(ChestState.UNLOCKING);

    private void CreateChest(ChestScriptableObject chestSO)
    {
        ChestView chest = GameObject.Instantiate(chestPrefab);
        ChestController chestController = new ChestController(chest, chestSO);
        GameService.Instance.EventService.onChestSetupComplete.Invoke(chest);
    }
}
