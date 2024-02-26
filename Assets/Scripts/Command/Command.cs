using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ICommand
{
    public CommandData commandData;
    public abstract void Execute();
    public abstract void Undo();
}

