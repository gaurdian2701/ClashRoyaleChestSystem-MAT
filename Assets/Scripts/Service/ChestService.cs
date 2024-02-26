using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService
{
    private List<ChestScriptableObject> chestsList;
    private Queue<ChestScriptableObject> chestsOpenQueue;
    private ChestView chestPrefab;
    private int chestsLimit;
    private int numberOfChestsGenerated;
    private System.Random random;

    public ChestService(ChestView chestPrefab, ChestServiceScriptableObject chestServiceSO)
    {
        this.chestPrefab = chestPrefab;
        chestsLimit = chestServiceSO.chestLimit;
        numberOfChestsGenerated = 0;
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

    private void CreateChest(ChestScriptableObject chestSO)
    {
        ChestView chest = GameObject.Instantiate(chestPrefab);
        ChestController chestController = new ChestController(chest, chestSO);
        GameService.Instance.EventService.onChestSetupComplete.Invoke(chest);
    }
}
