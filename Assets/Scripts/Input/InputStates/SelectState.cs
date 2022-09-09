using UnityEngine;
using UnityEngine.InputSystem;

public class SelectState : BaseState
{
    InputActions inputActions;
    InputRaycaster2D raycaster;
    LayerMask hitLayers;
    CameraController cameraController;

    bool camShouldDrag = false;
    bool camShouldZoom = false;
    bool camShouldPan = false;

    float zoomDirection;
    Vector2 zoomMousePosition;
    Vector2 panDirection;

    public SelectState(string clickTag): base(clickTag) { }

    #region StateMethods
    public override void Enter(BaseStateMachine machine)
    {
        Debug.Log("Entering Select State");

        InputController inputController = machine.GetComponent<InputController>();

        this.hitLayers = machine.GetComponent<HitLayerSelector>().layers;
        cameraController = machine.GetComponent<CameraController>();
        this.inputActions = inputController.Input;
        this.raycaster = inputController.Raycaster;

        inputActions.Mouse.DragPan.started += OnDragStarted;
        inputActions.Mouse.DragPan.performed += OnDragPerformed;
        inputActions.Mouse.DragPan.canceled += OnDragCanceled;

        inputActions.Mouse.Zoom.performed += OnMouseZoomPeformed;
        inputActions.Mouse.Zoom.canceled += OnZoomCanceled;

        inputActions.Keyboard.Zoom.performed += OnKeyboardZoomPerformed;
        inputActions.Keyboard.Zoom.canceled += OnZoomCanceled;

        inputActions.Keyboard.Pan.performed += OnKeyboardPanPerformed;
        inputActions.Keyboard.Pan.canceled += OnKeyboardPanCanceled;

        inputActions.Mouse.ClickTile.performed += OnClickPerformed;
    }

    public override void Execute(BaseStateMachine machine)
    {
        
        if (camShouldDrag)
        {
            cameraController.Drag(inputActions.Mouse.Position.ReadValue<Vector2>());
        }

        if(camShouldZoom && zoomDirection != 0)
        {
            if (zoomMousePosition.x == 0 && zoomMousePosition.y == 0) cameraController.Zoomer.Zoom(zoomDirection);
            else cameraController.Zoomer.Zoom(zoomDirection, zoomMousePosition);
        }

        if(camShouldPan)
        {
            cameraController.Panner.Pan(panDirection);
        }
    }

    public override void Exit(BaseStateMachine machine)
    {
        inputActions.Mouse.DragPan.started -= OnDragStarted;
        inputActions.Mouse.DragPan.performed -= OnDragPerformed;
        inputActions.Mouse.DragPan.canceled -= OnDragCanceled;

        inputActions.Mouse.Zoom.performed -= OnMouseZoomPeformed;
        inputActions.Mouse.Zoom.canceled -= OnZoomCanceled;

        inputActions.Keyboard.Zoom.performed -= OnKeyboardZoomPerformed;
        inputActions.Keyboard.Zoom.canceled -= OnZoomCanceled;

        inputActions.Keyboard.Pan.performed -= OnKeyboardPanPerformed;
        inputActions.Keyboard.Pan.canceled -= OnKeyboardPanCanceled;

        inputActions.Mouse.ClickTile.performed -= OnClickPerformed;
    }
    #endregion

    #region InputEvents

    public override void OnClickPerformed(InputAction.CallbackContext obj)
    {
        IClickable clickableObject = raycaster.GetObject<IClickable>(hitLayers);
        if (clickableObject != null && clickableObject.GameObject.CompareTag(clickTag))
        {
            clickableObject.OnPointerClicked();
        }
    }

    public override void OnDragStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IClickable clickableObject = raycaster.GetObject<IClickable>(hitLayers);
        if (clickableObject != null) cameraController.SetDragOrigin(inputActions.Mouse.Position.ReadValue<Vector2>());
    }

    public override void OnDragPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IClickable clickableObject = raycaster.GetObject<IClickable>(hitLayers);
        if (clickableObject != null) {
            camShouldDrag = true;
        }
    }

    public override void OnDragCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        camShouldDrag = false;
        cameraController.SetDragOrigin(new Vector2(0, 0));
        cameraController.SetDragVelocity(new Vector2(0, 0));
    }

    public override void OnMouseZoomPeformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IClickable clickableObject = raycaster.GetObject<IClickable>(hitLayers);
        if (clickableObject != null)
        {
            zoomDirection = obj.ReadValue<Vector2>().normalized.y;
            zoomMousePosition = inputActions.Mouse.Position.ReadValue<Vector2>();
            camShouldZoom = true;
        }
    }

    public override void OnKeyboardZoomPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        zoomDirection = obj.ReadValue<float>();
        zoomMousePosition = Vector2.zero;
        camShouldZoom = true;
    }

    public override void OnZoomCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        zoomDirection = 0;
        zoomMousePosition = Vector2.zero;
        camShouldZoom = false;
    }

    public override void OnKeyboardPanPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        panDirection = obj.ReadValue<Vector2>();
        camShouldPan = true;
    }

    public override void OnKeyboardPanCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        panDirection = Vector2.zero;
        camShouldPan = false;
    }
    #endregion
}
