using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDialog : VisualElement
{
    Button closeButton;

    public new class UxmlFactory : UxmlFactory<UIDialog, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    public UIDialog()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        closeButton = this.Q<Button>("close-button");

        closeButton.RegisterCallback<ClickEvent>((ev) => OnCloseButtonClick());
    }

    private void OnCloseButtonClick()
    {
        this.style.display = DisplayStyle.None;
    }
}
