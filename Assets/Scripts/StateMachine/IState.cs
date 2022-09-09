using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter(BaseStateMachine machine);
    public void Execute(BaseStateMachine machine);
    public void Exit(BaseStateMachine machine);
}