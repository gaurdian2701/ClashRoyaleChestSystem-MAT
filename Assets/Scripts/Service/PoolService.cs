using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolService
{
    public ChestPool ChestPool { get; private set; }
    public PoolService(ChestView chestPrefab)
    {
        ChestPool = new ChestPool(chestPrefab);
    }
}
