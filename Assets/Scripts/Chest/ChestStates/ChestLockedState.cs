using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestLockedState : IStateInterface
{
    private ChestController controller;
    public ChestLockedState(ChestController controller) { this.controller = controller; }
    public override void OnStateEnter()
    {
        controller.ChestView.SetChestStateText(ChestState.LOCKED);
        controller.ChestView.InitializeChestData();
    }

    public override void HandleClickEvent() => GameService.Instance.EventService.onLockedChestClicked.Invoke(controller.ChestView);
}
