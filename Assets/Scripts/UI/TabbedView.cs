using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TabbedView : MonoBehaviour
{
    private TabbedViewController controller;

    private void OnEnable()
    {
        UIDocument menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;

        controller = new TabbedViewController(root);

        controller.RegisterTabCallbacks();

    }
}
