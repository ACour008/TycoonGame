using UnityEngine.UIElements;
using UnityEngine;

public class TabbedViewController
{
    private const string TAB_CLASS_NAME = "tab-label";
    private const string SELECTED_TAB = "selected-tab";
    private const string UNSELECTED_CONTENT = "unselected-content";
    private const string TAB_NAME_SUFFIX = "-tab";
    private const string CONTENT_NAME_SUFFIX = "-contents";

    private readonly VisualElement root;

    public TabbedViewController(VisualElement root)
    {
        this.root = root;
    }

    public void RegisterTabCallbacks()
    {
        UQueryBuilder<Label> tabs = GetAllTabs();

        tabs.ForEach((Label tab) =>
        {
            tab.RegisterCallback<ClickEvent>(OnTabClick);
        });
    }

    public UQueryBuilder<Label> GetAllTabs()
    {
        return root.Query<Label>(className: TAB_CLASS_NAME);
    }

    private void OnTabClick(ClickEvent evt)
    {
        Label clickedTab = evt.currentTarget as Label;

        if (!TabIsCurrentlySelected(clickedTab))
        {
            GetAllTabs().Where(
                (tab) => tab != clickedTab && TabIsCurrentlySelected(tab)
            ).ForEach(UnselectTab);

            SelectTab(clickedTab);
        }
    }

    private void SelectTab(Label tab)
    {
        tab.AddToClassList(SELECTED_TAB);
        VisualElement content = FindContent(tab);
        content.RemoveFromClassList(UNSELECTED_CONTENT);
    }

    private void UnselectTab(Label tab)
    {
        tab.RemoveFromClassList(SELECTED_TAB);
        VisualElement content = FindContent(tab);
        content.AddToClassList(UNSELECTED_CONTENT);
    }

    private VisualElement FindContent(Label tab)
    {
        return root.Q(GenerateContentName(tab));
    }

    private static string GenerateContentName(Label tab)
    {
        return tab.name.Replace(TAB_NAME_SUFFIX, CONTENT_NAME_SUFFIX);
    }

    private static bool TabIsCurrentlySelected(Label tab)
    {
        return tab.ClassListContains(SELECTED_TAB);
    }
}

