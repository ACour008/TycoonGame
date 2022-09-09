using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IState
{
    BaseStateMachine machine;

    public void Enter(BaseStateMachine machine)
    {
        this.machine = machine;
        Debug.Log("Entering Placement State");
    }

    public void Execute(BaseStateMachine machine)
    {
        
    }

    public void Exit(BaseStateMachine machine)
    {
        throw new System.NotImplementedException();
    }
}
