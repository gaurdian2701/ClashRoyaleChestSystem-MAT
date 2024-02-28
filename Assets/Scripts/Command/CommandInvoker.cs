using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandInvoker
{
    private Stack<Command> commandStack;

    public CommandInvoker() => commandStack = new Stack<Command>();

    public void Init() => GameService.Instance.EventService.onChestOpened += RemoveCommandAssociatedWithChestUnlock;

    public void ProcessCommand(Command command) => ExecuteCommand(command);
    public void ExecuteCommand(Command command)
    {
        command.Execute();
        commandStack.Push(command);
    }
    public void UndoCommand()
    {
        if (commandStack.Count > 0)
        {
            //The while loop is added to handle an edge case where there is a command in the stack but the chestview associated with that command is destroyed
            //because the user has already opened it
            while (commandStack.Peek().commandData.ChestView == null) 
                commandStack.Pop();
            commandStack.Pop().Undo();
        }
    }
    private void RemoveCommandAssociatedWithChestUnlock(ChestView view)
    {
    }
}
