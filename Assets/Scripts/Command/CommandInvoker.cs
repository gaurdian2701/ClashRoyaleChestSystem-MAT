using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private Stack<ICommand> commandsList;

    public CommandInvoker() => commandsList = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandsList.Push(command);
    }
    public void UndoCommand() => commandsList.Pop().Undo();
}
