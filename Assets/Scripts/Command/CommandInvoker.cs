using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private Stack<Command> commandsList;

    public CommandInvoker() => commandsList = new Stack<Command>();

    public void ExecuteCommand(Command command)
    {
        command.Execute();
        commandsList.Push(command);
    }
    public void UndoCommand() => commandsList.Pop().Undo();
}
