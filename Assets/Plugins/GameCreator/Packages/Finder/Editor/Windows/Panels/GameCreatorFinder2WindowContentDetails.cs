using GameCreator.Runtime.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public abstract class GameCreatorFinder2WindowContentDetails : VisualElement
    {
        protected const string NAME_SCROLLVIEW = "Finder2-Content-Details-ScrollView";

        protected const string NAME_BUTTON_REMOVE_COMPONENT = "Finder2-Content-Details-Button-Remove-Component";
        protected const string NAME_BUTTON_REMOVE_GAMEOBJECT = "Finder2-Content-Details-Button-Remove-GameObject";
        protected const string NAME_OBJECTFIELD = "Finder2-Content-Details-ObjectField";
        protected const string NAME_CONTAINER_BUTTONS = "Finder2-Content-Details-Container-Buttons";

        protected string selectedList = "";
        protected int selectedIndex = -1;

        // MEMBERS: -------------------------------------------------------------------------------

        private GameCreatorFinder2WindowTemplate window;

        protected ScrollView content;

        public GameCreatorFinder2WindowContentDetails(GameCreatorFinder2WindowTemplate window)
        {
            this.window = window;
        }

        public virtual void OnEnable()
        {
            this.Clear();

            this.window.EventChangeSelection += this.OnChangeSelection;
        }

        public virtual void OnDisable()
        {
            this.window.EventChangeSelection -= this.OnChangeSelection;
        }

        internal virtual void OnChangeSelection(GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS tab, string foldout, int index)
        {
            this.Clear();

            this.content = new ScrollView(ScrollViewMode.Vertical) { name = NAME_SCROLLVIEW };
            this.Add(this.content);

            switch (tab)
            {
                case GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS.Triggers:
                    this.ShowTrigger(index);
                    break;

                case GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS.Conditions:
                    this.ShowCondition(index);
                    break;

                case GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS.Actions:
                    this.ShowAction(index);
                    break;

                case GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS.Components:
                    this.ShowComponent(foldout, index);
                    break;

                case GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS.ScriptableObjects:
                    this.ShowScriptableObject(foldout, index);
                    break;
            }
        }

        protected virtual void ShowTrigger(int index)
        {
            this.selectedList = "Triggers";
            this.selectedIndex = index;

            VisualElement triggerObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Trigger),
                value = this.window.Content.ContentList.listTriggersFiltered[index],
            };
            this.content.Add(triggerObjectField);

            VisualElement triggerInspector = new InspectorElement();
            triggerInspector.Bind(new SerializedObject(this.window.Content.ContentList.listTriggersFiltered[index]));
            this.content.Add(triggerInspector);

            if (!PrefabUtility.IsPartOfAnyPrefab(this.window.Content.ContentList.listTriggersFiltered[index])) this.AddRemovalButtons();
        }

        protected virtual void ShowCondition(int index)
        {
            this.selectedList = "Conditions";
            this.selectedIndex = index;

            VisualElement conditionObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Condition),
                value = this.window.Content.ContentList.listConditionsFiltered[index]
            };
            this.content.Add(conditionObjectField);

            VisualElement conditionInspector = new InspectorElement();
            conditionInspector.Bind(new SerializedObject(this.window.Content.ContentList.listConditionsFiltered[index]));
            this.content.Add(conditionInspector);

            if (!PrefabUtility.IsPartOfAnyPrefab(this.window.Content.ContentList.listConditionsFiltered[index])) this.AddRemovalButtons();
        }

        protected virtual void ShowAction(int index)
        {
            this.selectedList = "Actions";
            this.selectedIndex = index;

            VisualElement actionObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Condition),
                value = this.window.Content.ContentList.listActionsFiltered[index]
            };
            this.content.Add(actionObjectField);

            VisualElement actionInspector = new InspectorElement();
            actionInspector.Bind(new SerializedObject(this.window.Content.ContentList.listActionsFiltered[index]));
            this.content.Add(actionInspector);

            if (!PrefabUtility.IsPartOfAnyPrefab(this.window.Content.ContentList.listActionsFiltered[index])) this.AddRemovalButtons();
        }

        protected virtual void ShowComponent(string foldout, int index) { }

        protected virtual void ShowScriptableObject(string foldout, int index) { }

        protected virtual void AddRemovalButtons()
        {
            VisualElement twoButton = new VisualElement() { name = NAME_CONTAINER_BUTTONS };
            this.content.Add(twoButton);

            VisualElement buttonRemoveGameObject = new Button(this.RemoveGameObject) { name = NAME_BUTTON_REMOVE_GAMEOBJECT, text = "Remove GameObject" };
            twoButton.Add(buttonRemoveGameObject);

            VisualElement buttonRemoveComponent = new Button(this.RemoveComponent) { name = NAME_BUTTON_REMOVE_COMPONENT, text = "Remove Component" };
            twoButton.Add(buttonRemoveComponent);
        }

        protected virtual void RemoveGameObject()
        {
            GameObject gameObjectToDestroy = new GameObject();

            switch (this.selectedList)
            {
                case "Triggers":
                    gameObjectToDestroy = this.window.Content.ContentList.listTriggersFiltered[this.selectedIndex].gameObject;
                    this.TryDestroyGameObject(gameObjectToDestroy);
                    break;

                case "Conditions":
                    gameObjectToDestroy = this.window.Content.ContentList.listConditionsFiltered[this.selectedIndex].gameObject;
                    this.TryDestroyGameObject(gameObjectToDestroy);
                    break;

                case "Actions":
                    gameObjectToDestroy = this.window.Content.ContentList.listActionsFiltered[this.selectedIndex].gameObject;
                    this.TryDestroyGameObject(gameObjectToDestroy);
                    break;
            }

            this.Clear();
        }

        protected virtual void RemoveComponent()
        {
            Component componentToDestroy = new Component();

            switch (this.selectedList)
            {
                case "Triggers":
                    componentToDestroy = this.window.Content.ContentList.listTriggersFiltered[this.selectedIndex];
                    this.TryDestroyComponent(componentToDestroy);
                    break;

                case "Conditions":
                    componentToDestroy = this.window.Content.ContentList.listConditionsFiltered[this.selectedIndex];
                    this.TryDestroyComponent(componentToDestroy);
                    break;

                case "Actions":
                    componentToDestroy = this.window.Content.ContentList.listActionsFiltered[this.selectedIndex];
                    this.TryDestroyComponent(componentToDestroy);
                    break;
            }

            this.Clear();
        }

        protected virtual void TryDestroyGameObject(GameObject gameObjectToDestroy)
        {
            if (!PrefabUtility.IsPartOfAnyPrefab(gameObjectToDestroy)) Object.DestroyImmediate(gameObjectToDestroy);
            else Debug.LogError($"GameObject {gameObjectToDestroy.name} is part of a prefab - This action is not allowed. Unpack the prefab first before deleting it.");
        }

        protected virtual void TryDestroyComponent(Component componentToDestroy)
        {
            if (!PrefabUtility.IsPartOfAnyPrefab(componentToDestroy)) Object.DestroyImmediate(componentToDestroy);
            else Debug.LogError($"Component {componentToDestroy.name} is part of a prefab - This action is not allowed. Unpack the prefab first before deleting it.");
        }
    }
}