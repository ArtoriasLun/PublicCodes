using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public abstract class GameCreatorFinder2WindowTemplate : EditorWindow
    {
        protected const int MIN_WIDTH = 800;
        protected const int MIN_HEIGHT = 600;

        protected const string USS_PATH = "Assets/Plugins/GameCreator/Packages/Finder/Editor/Windows/Stylesheets/GameCreatorFinder2Window";

        protected const string NAME_TOOLBAR = "Finder2-Toolbar";
        protected const string NAME_CONTENT = "Finder2-Content";

        protected IIcon iconWindow;

        protected static GameCreatorFinder2WindowTemplate WINDOW;

        internal static Trigger[] TRIGGERS_FOUND;
        internal static Conditions[] CONDITIONS_FOUND;
        internal static Actions[] ACTIONS_FOUND;

        public enum GC2_FINDER_MODULE_TABS
        {
            Triggers,
            Conditions,
            Actions,
            Components,
            ScriptableObjects
        };

        public int tabIndex = 0;

        public GameCreatorFinder2WindowToolbar Toolbar { get; protected set; }

        public GameCreatorFinder2WindowContent Content { get; protected set; }

        internal event Action<GC2_FINDER_MODULE_TABS, string, int> EventChangeSelection;

        protected virtual void OnHierarchyChange()
        {
            this.FindTriggers();
            this.FindConditions();
            this.FindActions();
            this.FindComponents();

            this.Content.ContentList.RemoveListElements();

            this.Content.ContentList.RemoveListTriggers();
            this.Content.ContentList.RemoveListConditions();
            this.Content.ContentList.RemoveListActions();
            this.Content.ContentList.RemoveListComponents();

            this.Content.ContentList.InitializeListTriggers();
            this.Content.ContentList.InitializeListConditions();
            this.Content.ContentList.InitializeListActions();
            this.Content.ContentList.InitializeListComponents();

            this.Content.ContentList.UpdateList(this.tabIndex);
        }

        protected virtual void OnEnable()
        {
            this.iconWindow ??= new IconSearch(ColorTheme.Type.TextLight);

            StyleSheet[] styleSheetsCollection = StyleSheetUtils.Load(USS_PATH);
            foreach (StyleSheet styleSheet in styleSheetsCollection)
            {
                this.rootVisualElement.styleSheets.Add(styleSheet);
            }

            this.FindEverything();

            this.Build();
        }

        protected virtual void OnDisable()
        {
            this.Toolbar.OnDisable();
            this.Content.OnDisable();
        }

        protected abstract void Build();

        protected virtual void FindTriggers()
        {
            TRIGGERS_FOUND = FindObjectsOfType<Trigger>();
        }

        protected virtual void FindConditions()
        {
            CONDITIONS_FOUND = FindObjectsOfType<Conditions>();
        }

        protected virtual void FindActions()
        {
            ACTIONS_FOUND = FindObjectsOfType<Actions>();
        }

        protected virtual void FindComponents() { }

        protected virtual void FindScriptableObjects() { }

        internal virtual void FindEverything()
        {
            this.FindTriggers();
            this.FindConditions();
            this.FindActions();
            this.FindComponents();
            this.FindScriptableObjects();
        }

        public virtual void OnChangeSelection(GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS tab, string foldout, int index)
        {
            this.EventChangeSelection?.Invoke(tab, foldout, index);
        }
    }
}