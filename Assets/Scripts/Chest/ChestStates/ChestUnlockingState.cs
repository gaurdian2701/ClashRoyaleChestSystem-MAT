using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockingState : IStateInterface
{
    private ChestController controller;
    private float waitTime;
    public ChestUnlockingState(ChestController controller) 
    {
        this.controller = controller;
        waitTime = controller.ChestData.WaitTime * 60;
    }
    public override void OnStateEnter() { }

    public override void Update()
    {
        waitTime -= Time.deltaTime;
        controller.UpdateTimeStep(waitTime);
    }
}
