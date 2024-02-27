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
    public ChestView ChestView { get; private set; }
    public int chestIndexInQueue { get; private set; }
    public ChestTime timeDuringUnlock { get; private set; }

    public void SetChestView(ChestView chestView) => this.ChestView = chestView;
    public void SetChestIndexInQueue(int index) => this.chestIndexInQueue = index;

    public void SetChestTime(ChestTime chestTime) => this.timeDuringUnlock = chestTime;
}
