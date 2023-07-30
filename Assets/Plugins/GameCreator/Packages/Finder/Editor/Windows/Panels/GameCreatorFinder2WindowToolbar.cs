using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public abstract class GameCreatorFinder2WindowToolbar : VisualElement
    {
        protected const string NAME_TOOLBAR_SEARCH_FIELD = "Finder2-Toolbar-SearchField";
        protected const string NAME_TOOLBAR_BUTTON_LABEL = "Finder2-Toolbar-Button-Label";
        protected const string NAME_TOOLBAR_SPACER_LABEL = "Finder2-Toolbar-Spacer-Label";

        private readonly GameCreatorFinder2WindowTemplate window;

        protected Toolbar toolbar;
        protected ToolbarMenu menuTabs;
        protected ToolbarButton buttonSortAZ;
        protected ToolbarButton buttonSortZA;
        protected ToolbarSearchField searchField;

        protected string searchTextTriggers = "";
        protected string searchTextConditions = "";
        protected string searchTextActions = "";
        protected string searchTextComponents = "";
        protected string searchTextScriptableObjects = "";

        // PROPERTIES: ----------------------------------------------------------------------------
         
        public ToolbarSearchField SearchField => this.searchField;

        public GameCreatorFinder2WindowToolbar(GameCreatorFinder2WindowTemplate window)
        {
            this.window = window;
        }

        public virtual void OnEnable()
        {
            this.toolbar = new Toolbar();

            this.window.tabIndex = 0;

            this.SetupTabDropdown();
            this.SetupSpacerWithTabName();
            this.SetupSearchField();
            this.SetupSortButtons();

            this.Add(this.toolbar);
        }

        public virtual void OnDisable() { }

        protected virtual void SetupTabDropdown()
        {
            this.menuTabs = new ToolbarMenu { text = "Switch Tabs" };

            this.menuTabs.menu.AppendAction(
                "Triggers",
                action => this.ChangeTab(0, this.searchTextTriggers)
            );

            this.menuTabs.menu.AppendAction(
                "Conditions",
                action => this.ChangeTab(1, this.searchTextConditions)
            );

            this.menuTabs.menu.AppendAction(
                "Actions",
                action => this.ChangeTab(2, this.searchTextActions)
            );

            this.menuTabs.menu.AppendAction(
                "Components",
                action => this.ChangeTab(3, this.searchTextComponents)
            );

            this.menuTabs.menu.AppendAction(
                "Scriptable Objects",
                action => this.ChangeTab(4, this.searchTextScriptableObjects)
            );

            this.menuTabs.menu.AppendSeparator();

            this.toolbar.Add(this.menuTabs);
        }

        protected virtual void SetupSpacerWithTabName()
        {
            VisualElement spacer = new VisualElement() { name = NAME_TOOLBAR_SPACER_LABEL };

            spacer.Add(new Label { text = ((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex).ToString() });

            this.toolbar.Add(spacer);
        }

        protected virtual void SetupSearchField()
        {
            this.searchField = new ToolbarSearchField() { name = NAME_TOOLBAR_SEARCH_FIELD };
            this.searchField.RegisterValueChangedCallback(this.OnChangeSearch);

            this.toolbar.Add(this.searchField);
        }

        protected virtual void SetupSortButtons()
        {
            this.buttonSortAZ = new ToolbarButton(this.SortAZ)
            {
                tooltip = "Sort A-Z",
                name = NAME_TOOLBAR_BUTTON_LABEL
            };
            this.buttonSortAZ.Add(new Label { text = "A-Z" });

            this.toolbar.Add(this.buttonSortAZ);

            this.buttonSortZA = new ToolbarButton(this.SortZA)
            {
                tooltip = "Sort Z-A",
                name = NAME_TOOLBAR_BUTTON_LABEL
            };
            this.buttonSortZA.Add(new Label { text = "Z-A" });

            this.toolbar.Add(this.buttonSortZA);
        }

        protected virtual void ChangeTab(int tabIndex, string searchText)
        {
            this.window.tabIndex = tabIndex;
            this.RefreshToolbar();
            this.searchField.value = searchText;
        }

        protected virtual void RefreshToolbar()
        {
            this.toolbar.Clear();

            this.SetupTabDropdown();
            this.SetupSpacerWithTabName();
            this.SetupSearchField();
            this.SetupSortButtons();

            this.window.Content.ContentDetails.Clear();
            this.window.Content.ContentList.UpdateShowHideButtons(this.window.tabIndex);
            this.window.Content.ContentList.UpdateList(this.window.tabIndex);
        }

        protected virtual void SortAZ()
        {
            switch (this.window.tabIndex)
            {
                case 0:
                    this.window.Content.ContentList.sortTriggersAZ = true;
                    break;
                case 1:
                    this.window.Content.ContentList.sortConditionsAZ = true;
                    break;
                case 2:
                    this.window.Content.ContentList.sortActionsAZ = true;
                    break;
                case 3:
                    this.window.Content.ContentList.sortComponentsAZ = true;
                    break;
                case 4:
                    this.window.Content.ContentList.sortScriptableObjectsAZ = true;
                    break;
            }

            this.window.Content.ContentList.UpdateList(this.window.tabIndex);
        }

        protected virtual void SortZA()
        {
            switch (this.window.tabIndex)
            {
                case 0:
                    this.window.Content.ContentList.sortTriggersAZ = false;
                    break;
                case 1:
                    this.window.Content.ContentList.sortConditionsAZ = false;
                    break;
                case 2:
                    this.window.Content.ContentList.sortActionsAZ = false;
                    break;
                case 3:
                    this.window.Content.ContentList.sortComponentsAZ = false;
                    break;
                case 4:
                    this.window.Content.ContentList.sortScriptableObjectsAZ = false;
                    break;
            }

            this.window.Content.ContentList.UpdateList(this.window.tabIndex);
        }

        protected virtual void OnChangeSearch(ChangeEvent<string> changeEvent)
        {
            switch (this.window.tabIndex)
            {
                case 0:
                    this.searchTextTriggers = changeEvent.newValue;
                    break;
                case 1:
                    this.searchTextConditions = changeEvent.newValue;
                    break;
                case 2:
                    this.searchTextActions = changeEvent.newValue;
                    break;
                case 3:
                    this.searchTextComponents = changeEvent.newValue;
                    break;
                case 4:
                    this.searchTextScriptableObjects = changeEvent.newValue;
                    break;
            }

            this.window.Content.ContentDetails.Clear();

            this.window.Content.ContentList.UpdateList(this.window.tabIndex);
        }
    }
}