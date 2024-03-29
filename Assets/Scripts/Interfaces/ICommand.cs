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
    public float chestTimeStepDuringUnlock { get; private set; }
    public int gemsLost { get; private set; }

    public void SetChestView(ChestView chestView) => this.ChestView = chestView;
    public void SetChestIndexInQueue(int index) => this.chestIndexInQueue = index;
    public void SetChestTimeStep(float timeStep) => this.chestTimeStepDuringUnlock = timeStep;
    public void SetGemsLost(int gemsLost) => this.gemsLost = gemsLost;
}
