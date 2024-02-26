using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenedCommand : Command
{
    public ChestOpenedCommand(CommandData commandData)
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
