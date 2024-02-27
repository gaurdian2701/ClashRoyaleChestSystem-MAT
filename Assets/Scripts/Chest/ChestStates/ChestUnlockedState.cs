using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockedState : IStateInterface
{
    private ChestController controller;
    public ChestUnlockedState(ChestController controller) { this.controller = controller; }
    public override void OnStateEnter()
    {
        controller.ChestView.SetChestStateText(ChestState.UNLOCKED);
        //Do rewards
    }
}
