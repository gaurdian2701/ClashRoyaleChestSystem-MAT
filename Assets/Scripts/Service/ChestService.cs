using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService : MonoBehaviour
{
    private List<ChestScriptableObject> chestsList;
    private ChestView chestPrefab;

    public ChestService(ChestView chestPrefab)
    {
        this.chestPrefab = chestPrefab;
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
        int chestType = UnityEngine.Random.Range((int)ChestType.Common, (int)ChestType.Legendary);
        ChestScriptableObject chestSO = chestsList[chestType];
        CreateChest(chestSO);
    }

    private void CreateChest(ChestScriptableObject chestSO)
    {
        ChestController chestController = new ChestController();
    }
}
