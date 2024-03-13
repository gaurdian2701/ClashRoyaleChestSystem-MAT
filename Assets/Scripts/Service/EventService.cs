using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public Action<ChestView> OnChestSetupComplete;
    public Action<ChestView> OnLockedChestClicked;
    public Action<ChestView> OnUnlockingChestClicked;
    public Action<ChestView> OnUnlockedChestClicked;
    public Action<ChestView> OnChestUnlocked;
    public Action OnEmptyCanvasClicked;
    public Action OnStartUnlockingChestSuccessful;
    public Action OnStartUnlockingChestFailed;
    public Action<int, int> OnCurrencyUpdated;
}
