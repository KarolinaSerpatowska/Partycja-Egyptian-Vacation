using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDodge : IState
{
    public void Enter()
    {
        Debug.Log("entering dodge state");
    }

    public void Execute()
    {
        Debug.Log("updating dodge state");
    }

    public void Exit()
    {

        Debug.Log("exiting dodge state");
    }
}
