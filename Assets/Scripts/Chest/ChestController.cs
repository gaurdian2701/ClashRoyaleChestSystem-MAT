using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    private ChestStateMachine stateMachine;
    public ChestScriptableObject chestData { get; private set; }
    public ChestController(ChestView chestView, ChestScriptableObject chestSO)
    {
        chestData = chestSO;
        CreateStateMachine();
    }

    private void CreateStateMachine()
    {
        stateMachine = new ChestStateMachine(this);
    }
}
