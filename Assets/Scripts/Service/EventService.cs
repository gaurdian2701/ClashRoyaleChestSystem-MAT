using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public Action<ChestView> onChestSetupComplete;
    public Action<ChestView> onLockedChestClicked;
    public Action<ChestView> onUnlockingChestClicked;
    public Action<ChestView> onChestUnlocked;
    public Action onEmptyCanvasClicked;
    public Action onStartUnlockingChestSuccessful;
    public Action onStartUnlockingChestFailed;
}
