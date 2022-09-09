using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRaycaster2D
{
    private InputActions inputActions;
    private IRayProvider rayProvider;
    private GameObject _hitTarget;

    public GameObject HitTarget { get => _hitTarget; }

    public InputRaycaster2D(InputActions inputActions)
    {
        this.inputActions = inputActions;
        rayProvider = new Ray2DProvider();
    }

    private void PerformCheck(LayerMask layers)
    {
        _hitTarget = null;

        Ray ray = rayProvider.GetRay(inputActions.Mouse.Position.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layers);

        if (hit.transform != null)
        {
            _hitTarget = hit.transform.gameObject;
        }
    }

    // Should this be a utils thing? Probably. Add the Raycaster as an arg.
    public T GetObject<T>(LayerMask hitLayers)
    {
        System.Type objectType = typeof(T);

        PerformCheck(hitLayers);
        if (_hitTarget != null)
        {
            var obj = HitTarget.GetComponent<T>();
            return obj;
        }

        return default(T);
    }
}
