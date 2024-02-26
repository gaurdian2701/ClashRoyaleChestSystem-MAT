using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStateMachine
{
    private Dictionary<ChestState, IStateInterface> states;
    private ChestController controller;
    private IStateInterface currentState;
    public ChestStateMachine(ChestController controller)
    {
        states = new Dictionary<ChestState, IStateInterface> ();
        this.controller = controller;
        CreateStates();
        ChangeState(ChestState.LOCKED);
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(ChestState state)
    {
        currentState?.OnStateExit();
        currentState = states[state];
        currentState?.OnStateEnter();
    }

    private void CreateStates()
    {
        states.Add(ChestState.LOCKED, new ChestLockedState(controller));
        states.Add(ChestState.UNLOCKING, new ChestUnlockingState(controller));
        states.Add(ChestState.UNLOCKED, new ChestUnlockedState(controller));  
        states.Add(ChestState.OPENED, new ChestOpenState(controller));
    }
}
