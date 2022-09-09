using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    protected string clickTag;

    public BaseState(string clickTag)
    {
        this.clickTag = clickTag;
    }

    public abstract void Enter(BaseStateMachine machine);

    public abstract void Execute(BaseStateMachine machine);

    public abstract void Exit(BaseStateMachine machine);

    public virtual void OnDragStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnDragCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnDragPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnMouseZoomPeformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnKeyboardZoomPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnZoomCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnKeyboardPanPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnKeyboardPanCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }

    public virtual void OnClickPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) { }


}
