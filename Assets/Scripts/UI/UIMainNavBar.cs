using UnityEngine;
using UnityEngine.UIElements;

public class UIMainNavBar : VisualElement
{
    Button mapViewButton;
    Button statsButton;
    Button buildButton;
    Button eventsButton;
    Button specialButton;

    public Button BuildButton { get => buildButton; }
    public Button StatsButton { get => statsButton; }
    public Button EventsButton { get => eventsButton; }
    public Button SpecialButton { get => specialButton; }
    public Button MapViewButton { get => mapViewButton; }

    public new class UxmlFactory : UxmlFactory<UIMainNavBar, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    public UIMainNavBar()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        mapViewButton = this.Q<Button>("map-view-btn");
        statsButton = this.Q<Button>("stats-btn");
        buildButton = this.Q<Button>("build-btn");
        eventsButton = this.Q<Button>("events-btn");
        specialButton = this.Q<Button>("special-btn");
    }

    public void OnBuildButtonClicked(VisualElement element)
    {
        element.style.display = (element.style.display == DisplayStyle.None) ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void OnMapViewButtonClicked(VisualElement element)
    {
        Debug.Log("Clicked ViewMap Button");
    }

    public void OnStatsButtonClicked(VisualElement element)
    {
        Debug.Log("Clicked Stats Button");
    }

    public void OnEventsButtonClicked(VisualElement element)
    {
        Debug.Log("CLicked Events Button");
    }

    public void OnSpecialButtonClicked(VisualElement element)
    {
        Debug.Log("Clicked Special Button");
    }
}
