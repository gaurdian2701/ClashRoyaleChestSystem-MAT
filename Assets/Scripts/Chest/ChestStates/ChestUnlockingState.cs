using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockingState : IStateInterface
{
    private ChestController controller;
    public ChestUnlockingState(ChestController controller) {  this.controller = controller; }
    public override void OnStateEnter()
    {
    }
}
