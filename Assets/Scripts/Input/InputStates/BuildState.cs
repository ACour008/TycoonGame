using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : BaseState
{
    BaseStateMachine machine;

    public BuildState(string clickTag): base(clickTag) { }

    public override void Enter(BaseStateMachine machine)
    {
        this.machine = machine;
        Debug.Log("Entering Build State");
    }

    public override void Execute(BaseStateMachine machine)
    {

    }

    public override void Exit(BaseStateMachine machine)
    {

    }
}
