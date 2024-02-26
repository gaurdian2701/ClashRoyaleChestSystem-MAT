using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockedCommand : Command
{
    public ChestUnlockedCommand(CommandData commandData)
    {
        this.commandData = commandData; 
    }
    public override void Execute()
    {
        
    }

    public override void Undo()
    {
        
    }
}
