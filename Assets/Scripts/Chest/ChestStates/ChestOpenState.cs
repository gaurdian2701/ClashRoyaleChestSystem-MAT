using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenState : IStateInterface
{
    private ChestController controller;
    public ChestOpenState(ChestController controller) { this.controller = controller; }
    public override void OnStateEnter()
    {

    }
}
