using UnityEngine;

public class BuildState : BaseState
{
    BaseStateMachine machine;

    public BuildState(string clickTag): base(clickTag) { }

    public override void Enter(BaseStateMachine machine)
    {
        this.machine = machine;
        Debug.Log("Entering Build State");

        InputController inputController = machine.GetComponent<InputController>();

        inputController.Input.Keyboard.Esc.performed += Esc_performed;
    }

    private void Esc_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        machine.SetState(new SelectState("Terrain"));
    }

    public override void Execute(BaseStateMachine machine)
    {

    }

    public override void Exit(BaseStateMachine machine)
    {

    }
}
