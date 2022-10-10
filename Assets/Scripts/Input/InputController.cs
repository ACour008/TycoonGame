using UnityEngine;

public class InputController : MonoBehaviour
{
    private InputActions _inputActions;
    private InputRaycaster2D _raycaster;

    public InputActions Input { get => _inputActions; }
    public InputRaycaster2D Raycaster { get => _raycaster; }

    private void Awake()
    {
        _inputActions = new InputActions();
        _raycaster = new InputRaycaster2D(_inputActions);
    }

    private void OnEnable() => _inputActions.Enable();
    public void OnDisable() =>_inputActions.Disable();

    public void EnableInput() => _inputActions.Enable();
    public void DisableInput() => _inputActions.Disable();
}
