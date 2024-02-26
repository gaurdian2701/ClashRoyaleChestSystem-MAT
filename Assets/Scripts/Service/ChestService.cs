using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService
{
    private List<ChestScriptableObject> chestsList;
    private Queue<ChestView> chestsOpenQueue;
    private ChestView chestPrefab;
    private int chestsLimit;
    private int numberOfChestsGenerated;
    private System.Random random;

    public ChestService(ChestView chestPrefab, ChestServiceScriptableObject chestServiceSO)
    {
        this.chestPrefab = chestPrefab;
        chestsLimit = chestServiceSO.chestLimit;
        numberOfChestsGenerated = 0;
        chestsOpenQueue = new Queue<ChestView>();
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
        chestsOpenQueue.Enqueue(chestView);
        if (chestsOpenQueue.Count <= 0)
            StartUnlockingChest(chestView);
    }
    public void StartUnlockingChest(ChestView chestView) => chestView.controller.StateMachine.ChangeState(ChestState.UNLOCKING);

    private void CreateChest(ChestScriptableObject chestSO)
    {
        ChestView chest = GameObject.Instantiate(chestPrefab);
        ChestController chestController = new ChestController(chest, chestSO);
        GameService.Instance.EventService.onChestSetupComplete.Invoke(chest);
    }
}
