using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockingState : IStateInterface
{
    private ChestController controller;
    public float waitTime { get; private set; } //Time left to unlock chest
    public ChestUnlockingState(ChestController controller) 
    {
        this.controller = controller;
        SetTimeStep(controller.ChestData.WaitTime * 60);
    }
    public override void OnStateEnter() 
    {
        controller.ChestView.SetChestStateText(ChestState.UNLOCKING);
        SetTimeStep(controller.ChestData.WaitTime * 60);
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

    public override void HandleClickEvent() => GameService.Instance.EventService.onUnlockingChestClicked.Invoke(controller.ChestView);
    public override void OnStateExit()
    {
        waitTime = 0;
        controller.UpdateTimeStep(waitTime);
    }
    public void SetTimeStep(float timeStep) => waitTime = timeStep; //Set the waiting time to value
}
