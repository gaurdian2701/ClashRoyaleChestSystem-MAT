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
    public override void OnStateEnter() 
    {
        controller.ChestView.SetChestStateText(ChestState.UNLOCKING);
    }

    public override void Update()
    {
        if (waitTime <= 0) //This happens for when the chest unlocks on its own without using any gems
        {
            controller.StateMachine.ChangeState(ChestState.UNLOCKED);
            return;
        }

        waitTime -= Time.deltaTime;
        controller.UpdateTimeStep(waitTime);
    }

    public override void OnStateExit()
    {
        GameService.Instance.EventService.onChestUnlocked.Invoke(controller.ChestView);
        waitTime = 0;
        controller.UpdateTimeStep(waitTime);
    }
}
