using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState currState;

    public void ChangeState(IState newState)
    {
        if (currState != null)
            currState.Exit();

        currState = newState;
        currState.Enter();
    }

    public void Update()
    {
        if (currState != null) currState.Execute();
    }
}
