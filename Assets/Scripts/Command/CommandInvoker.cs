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
            while(commandStack.Peek().commandData.ChestView == null)
                commandStack.Pop();
            commandStack.Pop().Undo();
        }
    }
    private void RemoveCommandAssociatedWithChestUnlock(ChestView view)
    {
    }
}
