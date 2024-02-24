using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestLockedState : IStateInterface
{
    private ChestController controller;
    public ChestOpenState(ChestController controller) { this.controller = controller; }
    public void OnStateEnter()
    {
    }
}
