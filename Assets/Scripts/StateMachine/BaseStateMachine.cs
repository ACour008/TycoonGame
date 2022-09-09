using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine : MonoBehaviour
{
    protected IState _currentState;
    protected Dictionary<Type, Component> _cachedComponents;

    protected abstract void SetState(IState newState);

    protected abstract Dictionary<Type, Component> CreateComponentCache();

    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponents.ContainsKey(typeof(T))) return _cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();

        if (component != null) _cachedComponents.Add(typeof(T), component);

        return component;
    }
}
