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
    InputController inputController;

    public IState CurrentState { get => _currentState; set => SetState(value); }


    private void Start()
    {
        _cachedComponents = CreateComponentCache();
        inputController = GetComponent<InputController>();

        SetState(new SelectState(terrainTag));
    }

    public void OnBuildButtonPressed()
    {
        SetState(new BuildState(clickableTag));
    }

    public override void SetState(IState newState)
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
            inputController.EnableInput();
            _currentState.Execute(this);
        }
        else
        {
            inputController.DisableInput();
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
