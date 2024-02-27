using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        GameService.Instance.EventService.onChestUnlocked += RemoveChestFromQueue;
    }

    ~ChestService() { GameService.Instance.EventService.onChestUnlocked -= RemoveChestFromQueue; }
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
            HandleChestUnlocking(chestView);
        else
            HandleChestQueueing(chestView);

        chestsInQueueForUnlock.Add(chestView);
    }

    public void ProcessCommand(Command command)
    {
        command.commandData.SetChestIndexInQueue(chestsInQueueForUnlock.IndexOf(command.commandData.ChestView));
        ChestView chestInCommand = chestsInQueueForUnlock.ElementAt(command.commandData.chestIndexInQueue);
        chestInCommand.ProcessCommand(command);
    }

    public void ProcessUndo(ChestView chestView, int index)
    {
        chestsInQueueForUnlock.Insert(index, chestView);
        HandleChestUnlocking(chestView);

        foreach(ChestView c in chestsInQueueForUnlock)
            if(c != chestView)
                HandleChestQueueing(c);
    }

    private void HandleChestUnlocking(ChestView chestView)
    {
        GameService.Instance.EventService.onStartUnlockingChestSuccessful?.Invoke();
        StartUnlockingChest(chestView);
    }

    private void HandleChestQueueing(ChestView chestView)
    {
        if (chestView.controller.StateMachine.ChestState == ChestState.UNLOCKING)
            return;

        SetChestStateForQueueing(chestView);
        GameService.Instance.EventService.onStartUnlockingChestFailed?.Invoke();
    }
    private void RemoveChestFromQueue(ChestView chest)
    {
        if (chestsInQueueForUnlock[0] != chest)
            throw new Exception("Chest unlocked does not match with queue front!");

        chestsInQueueForUnlock.RemoveAt(0);

        if(chestsInQueueForUnlock.Any())
            StartUnlockingChest(chestsInQueueForUnlock[0]);
    }
    private void SetChestStateForQueueing(ChestView chestView) => chestView.OnChestQueued();
    private void StartUnlockingChest(ChestView chestView) => chestView.controller.StateMachine.ChangeState(ChestState.UNLOCKING);

    private void CreateChest(ChestScriptableObject chestSO)
    {
        ChestView chest = GameObject.Instantiate(chestPrefab);
        ChestController chestController = new ChestController(chest, chestSO);
        GameService.Instance.EventService.onChestSetupComplete.Invoke(chest);
    }
}
