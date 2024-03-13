using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPool : GenericObjectPool<ChestView>
{
    private ChestView chestPrefab;
    private ChestScriptableObject chestSO;

    public ChestPool(ChestView chestPrefab)
    {
        this.chestPrefab = chestPrefab;
    }
    public ChestView GetChest(ChestScriptableObject currentChestRequired)
    {
        chestSO = currentChestRequired;
        return GetItem();
    }
    protected override ChestView CreateItem()
    {
        ChestView chest = GameObject.Instantiate(chestPrefab);
        chest.gameObject.name = chestSO.name;
        ChestController chestController = new ChestController(chest, chestSO);

        return chest;
    }
}
