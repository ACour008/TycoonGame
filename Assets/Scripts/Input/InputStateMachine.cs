using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public sealed class InputStateMachine : BaseStateMachine
{
    [SerializeField] HitLayerSelector hitLayers;
    [SerializeField] string terrainTag;
    [SerializeField] string clickableTag;

    [SerializeField] Component[] components;

    private InputActions inputActions;

    public IState CurrentState { get => _currentState; set => SetState(value); }

    private void Awake()
    {
        _cachedComponents = CreateComponentCache();

        InputController inputController = GetComponent<InputController>();
        inputActions = inputController.Input;
    }

    private void Start()
    {
        SetState(new SelectState(terrainTag));
    }

    // Put this in Input Controller.
    private void OnEnable()
    {
        inputActions.Enable();
    }

    // Put this in Input Controller.
    private void OnDisable() => inputActions.Disable();

    public void OnBuildButtonPressed()
    {
        SetState(new BuildState(clickableTag));
    }

    protected override void SetState(IState newState)
    {
        // How to handle if newState is already currentState??

        if (_currentState != null) _currentState.Exit(this);

        _currentState = newState;
        _currentState.Enter(this);
    }

    private void Update()
    {
        DoNotClickUIAndWorldSimultaneously();
    }

    private void DoNotClickUIAndWorldSimultaneously()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            inputActions.Enable(); // Make this available through Input Controller.
            _currentState.Execute(this);
        }
        else
        {
            inputActions.Disable(); // Make this available through Input Controller.
        }
    }

    protected override Dictionary<Type, Component> CreateComponentCache()
    {
        Dictionary<Type, Component> cachedComponents = new Dictionary<Type, Component>();

        for (int i = 0; i < components.Length; i++)
        {
            cachedComponents.Add(components[i].GetType(), components[i]);
        }

        return cachedComponents;
    }
}
