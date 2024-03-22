using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public delegate void ChestSetupCompleteDelegate(ChestView chestView);
    public delegate void LockedChestClickedDelegate(ChestView chestView);
    public delegate void UnlockingChestClickedDelegate(ChestView chestView);
    public delegate void UnlockedChestClickedDelegate(ChestView chestView);
    public delegate void ChestUnlockedDelegate(ChestView chestView);
    public delegate void EmptyCanvasClickedDelegate();
    public delegate void StartUnlockingChestSuccessfulDelegate();
    public delegate void StartUnlockingChestFailedDelegate();
    public delegate void CurrencyUpdatedDelegate(int coins, int gems);

    public event ChestSetupCompleteDelegate OnChestSetupComplete;
    public event LockedChestClickedDelegate OnLockedChestClicked;
    public event UnlockingChestClickedDelegate OnUnlockingChestClicked;
    public event UnlockedChestClickedDelegate OnUnlockedChestClicked;
    public event ChestUnlockedDelegate OnChestUnlocked;
    public event EmptyCanvasClickedDelegate OnEmptyCanvasClicked;
    public event StartUnlockingChestSuccessfulDelegate OnStartUnlockingChestSuccessful;
    public event StartUnlockingChestFailedDelegate OnStartUnlockingChestFailed;
    public event CurrencyUpdatedDelegate OnCurrencyUpdated;

    public void InvokeChestSetupCompleteEvent(ChestView chestView) => OnChestSetupComplete?.Invoke(chestView);
    public void InvokeLockedChestClickedEvent(ChestView chestView) => OnLockedChestClicked?.Invoke(chestView);
    public void InvokeUnlockingChestClickedEvent(ChestView chestView) => OnUnlockedChestClicked?.Invoke(chestView);
    public void InvokeUnlockedChestClickedEvent(ChestView chestView) => OnUnlockedChestClicked.Invoke(chestView);
    public void InvokeChestUnlockedEvent(ChestView chestView) => OnChestUnlocked?.Invoke(chestView);
    public void InvokeEmptyCanvasClickedEvent() => OnEmptyCanvasClicked?.Invoke();
    public void InvokeStartUnlockingChestSuccessfulEvent() => OnStartUnlockingChestSuccessful?.Invoke();
    public void InvokeStartUnlockingChestFailedEvent() => OnStartUnlockingChestFailed?.Invoke();
    public void InvokeCurrencyUpdatedEvent(int coins, int gems) => OnCurrencyUpdated?.Invoke(coins, gems);

    //This change was implemented so that by using Events, none of the listeners may override by using = and also no one outside EventService can invoke any event.
}
