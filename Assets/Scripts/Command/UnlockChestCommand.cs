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
        
    }
}
