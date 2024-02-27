using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockChestCommand : Command
{
    public UnlockChestCommand(CommandData commandData)
    {
        this.commandData = commandData; 
    }
    public override void Execute()
    {
        ChestView chestInCommand = commandData.ChestView;
        chestInCommand.controller.StateMachine.ChangeState(ChestState.UNLOCKED);
    }

    public override void Undo()
    {
        GameService.Instance.ChestService.ProcessUndo(commandData.ChestView, commandData.chestIndexInQueue);
        UpdateChestInfo();
    }

    private void UpdateChestInfo()
    {
        commandData.ChestView.controller.StateMachine.SetTimeStep(commandData.chestTimeStepDuringUnlock);
    }
}
