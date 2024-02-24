using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    public void Execute();
    public void Undo();
}

public struct CommandData
{
    public ChestView ChestView;
    public CommandData(ChestView ChestView) => this.ChestView = ChestView;
}

