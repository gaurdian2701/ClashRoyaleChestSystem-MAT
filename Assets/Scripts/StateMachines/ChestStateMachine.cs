using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStateMachine
{
    private Dictionary<ChestState, IStateInterface> states;
    private ChestController controller;
    public IStateInterface CurrentState { get; private set; }
    public ChestState ChestState { get; private set; }
    public ChestStateMachine(ChestController controller)
    {
        states = new Dictionary<ChestState, IStateInterface> ();
        this.controller = controller;
        CreateStates();
        ChangeState(ChestState.LOCKED);
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(ChestState state)
    {
        CurrentState?.OnStateExit();
        CurrentState = states[state];
        ChestState = state;
        CurrentState?.OnStateEnter();
    }

    private void CreateStates()
    {
        states.Add(ChestState.LOCKED, new ChestLockedState(controller));
        states.Add(ChestState.UNLOCKING, new ChestUnlockingState(controller));
        states.Add(ChestState.UNLOCKED, new ChestUnlockedState(controller));  
        states.Add(ChestState.OPENED, new ChestOpenState(controller));
    }
}
