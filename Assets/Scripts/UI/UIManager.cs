using UnityEngine.UIElements;

public class UIManager : VisualElement
{
    UIDialog categoriesDialog;
    UIMainNavBar navBar;

    public new class UxmlFactory : UxmlFactory<UIManager, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    public UIManager()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
    }

    public void OnGeometryChanged(GeometryChangedEvent ev)
    {
        navBar = this.Q<UIMainNavBar>();
        categoriesDialog = this.Q<UIDialog>();


        RegisterCallbacks();
    }

    private void RegisterCallbacks()
    {
        navBar.BuildButton.RegisterCallback<ClickEvent>((ev) => navBar.OnBuildButtonClicked(categoriesDialog));
        navBar.MapViewButton.RegisterCallback<ClickEvent>((ev) => navBar.OnMapViewButtonClicked(null));
        navBar.StatsButton.RegisterCallback<ClickEvent>((ev) => navBar.OnSpecialButtonClicked(null));
        navBar.EventsButton.RegisterCallback<ClickEvent>((ev) => navBar.OnEventsButtonClicked(null));
        navBar.SpecialButton.RegisterCallback<ClickEvent>((ev) => navBar.OnSpecialButtonClicked(null));

    }
        
}
