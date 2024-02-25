using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService
{
    private List<ChestScriptableObject> chestsList;
    private ChestView chestPrefab;
    private int chestsLimit;
    private int numberOfChestsGenerated;

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
    }
    public void GenerateRandomChest()
    {
        if (numberOfChestsGenerated >= chestsLimit)
            return;

        numberOfChestsGenerated++;
        int chestType = UnityEngine.Random.Range((int)ChestType.Common, (int)ChestType.Legendary);
        ChestScriptableObject chestSO = chestsList[chestType];
        CreateChest(chestSO);
    }

    private void CreateChest(ChestScriptableObject chestSO)
    {
        ChestController chestController = new ChestController(chestPrefab, chestSO);
        ChestView chest = GameObject.Instantiate(chestPrefab);
        chestPrefab.SetChestController(chestController);
        chestPrefab.InitializeChestData();
        GameService.Instance.EventService.onChestSetupComplete.Invoke(chest);
    }
}
