using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public abstract class GameCreatorFinder2WindowContentList : VisualElement
    {
        protected const string NAME_SCROLLVIEW = "Finder2-Content-List-ScrollView";

        protected const string NAME_CONTENT_LIST = "Finder2-Content-List";

        protected const string NAME_CONTENT_ELEMENT = "Finder2-Content-Element";

        protected const string NAME_ELEMENT_ICON = "Finder2-Content-Element-Icon";
        protected const string NAME_ELEMENT_TITLE = "Finder2-Content-Element-Title";

        protected const string NAME_HORIZONTAL_LAYOUT_BUTTONS = "Finder2-Content-Horizontal-Layout-Buttons";

        protected const string NAME_SHOW_HIDE_ALL_BUTTONS = "Finder2-Content-Show-Hide-All-Buttons";

        protected const string MESSAGENOCOMPONENTOFTYPEFOUND = "No component of this type was found";

        protected static readonly IIcon ICON_INSTRUCTION = new IconInstructions(ColorTheme.Type.Blue);
        protected static readonly IIcon ICON_CONDITION = new IconConditions(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_TRIGGER = new IconTriggers(ColorTheme.Type.Yellow);
        protected static readonly IIcon ICON_NONE = new IconNone(ColorTheme.Type.White);

        private readonly GameCreatorFinder2WindowTemplate window;

        internal List<Trigger> listTriggers = new List<Trigger>();
        internal List<Trigger> listTriggersFiltered = new List<Trigger>();
        internal List<Conditions> listConditions = new List<Conditions>();
        internal List<Conditions> listConditionsFiltered = new List<Conditions>();
        internal List<Actions> listActions = new List<Actions>();
        internal List<Actions> listActionsFiltered = new List<Actions>();

        protected ListView listViewTriggers;
        protected ListView listViewConditions;
        protected ListView listViewActions;

        protected HelpBox messageBoxNoTriggersFound = new HelpBox(MESSAGENOCOMPONENTOFTYPEFOUND, HelpBoxMessageType.Info) { name = "Triggers", visible = true };
        protected HelpBox messageBoxNoConditionsFound = new HelpBox(MESSAGENOCOMPONENTOFTYPEFOUND, HelpBoxMessageType.Info) { name = "Conditions", visible = true };
        protected HelpBox messageBoxNoActionsFound = new HelpBox(MESSAGENOCOMPONENTOFTYPEFOUND, HelpBoxMessageType.Info) { name = "Actions", visible = true };

        public bool sortTriggersAZ = true;
        public bool sortConditionsAZ = true;
        public bool sortActionsAZ = true;
        public bool sortComponentsAZ = true;
        public bool sortScriptableObjectsAZ = true;

        protected ScrollView content = new ScrollView(ScrollViewMode.Vertical) { name = NAME_SCROLLVIEW };

        protected VisualElement horizontalLayoutButtons = new VisualElement() { name = NAME_HORIZONTAL_LAYOUT_BUTTONS };
        protected Button buttonShowAllComponents;
        protected Button buttonHideAllComponents;
        protected Button buttonShowAllScriptableObjects;
        protected Button buttonHideAllScriptableObjects;

        public GameCreatorFinder2WindowContentList(GameCreatorFinder2WindowTemplate window)
        {
            this.window = window;
        }

        public virtual void OnEnable()
        {
            this.SetupScrollView();
            this.SetupShowHideAllButtons();
            this.SetupLists();

            this.UpdateList(this.window.tabIndex);
        }

        public virtual void OnDisable()
        {
            this.listViewTriggers.selectionChanged -= OnContentSelectItemTrigger;
            this.listViewConditions.selectionChanged -= OnContentSelectItemCondition;
            this.listViewActions.selectionChanged -= OnContentSelectItemAction;

            this.listViewTriggers.itemsChosen -= OnContentChooseItemTrigger;
            this.listViewConditions.itemsChosen -= OnContentChooseItemCondition;
            this.listViewActions.itemsChosen -= OnContentChooseItemAction;
        }

        protected virtual void SetupScrollView()
        {
            this.Add(this.content);
        }

        protected virtual void SetupShowHideAllButtons()
        {
            this.buttonShowAllComponents = new Button(this.ShowAllComponents)
            {
                name = NAME_SHOW_HIDE_ALL_BUTTONS, text = "Show All"
            };

            this.buttonHideAllComponents = new Button(this.HideAllComponents)
            {
                name = NAME_SHOW_HIDE_ALL_BUTTONS, text = "Hide All"
            };

            this.buttonShowAllScriptableObjects = new Button(this.ShowAllScriptableObjects)
            {
                name = NAME_SHOW_HIDE_ALL_BUTTONS, text = "Show All"
            };

            this.buttonHideAllScriptableObjects = new Button(this.HideAllScriptableObjects)
            {
                name = NAME_SHOW_HIDE_ALL_BUTTONS, text = "Hide All"
            };
        }

        internal virtual void SetupLists(int tabIndex = -1)
        {
            if (tabIndex >= 0)
            {
                switch (tabIndex)
                {
                    case 0:
                        this.SetupListTriggers();
                        break;
                    case 1:
                        this.SetupListConditions();
                        break;
                    case 2:
                        this.SetupListActions();
                        break;
                    case 3:
                        this.SetupListComponents();
                        break;
                    case 4:
                        this.SetupListScriptableObjects();
                        break;
                }
            }
            else
            {
                this.SetupListTriggers();
                this.SetupListConditions();
                this.SetupListActions();
                this.SetupListComponents();
                this.SetupListScriptableObjects();
            }
        }

        protected virtual void SetupListTriggers()
        {
            this.InitializeListTriggers();

            this.listViewTriggers = new ListView(this.listTriggersFiltered, 24, this.MakeItem, this.BindItemTrigger)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewTriggers.selectionChanged += OnContentSelectItemTrigger;
            this.listViewTriggers.itemsChosen += OnContentChooseItemTrigger;
        }

        protected virtual void SetupListConditions()
        {
            this.InitializeListConditions();

            this.listViewConditions = new ListView(this.listConditionsFiltered, 24, this.MakeItem, this.BindItemCondition)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewConditions.selectionChanged += OnContentSelectItemCondition;
            this.listViewConditions.itemsChosen += OnContentChooseItemCondition;
        }

        protected virtual void SetupListActions()
        {
            this.InitializeListActions();

            this.listViewActions = new ListView(this.listActionsFiltered, 24, this.MakeItem, this.BindItemAction)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewActions.selectionChanged += OnContentSelectItemAction;
            this.listViewActions.itemsChosen += OnContentChooseItemAction;
        }

        protected virtual void SetupListComponents() { }

        protected virtual void SetupListScriptableObjects() { }

        protected virtual VisualElement MakeItem()
        {
            VisualElement element = new VisualElement() { name = NAME_CONTENT_ELEMENT };

            element.Add(new Image { name = NAME_ELEMENT_ICON });
            element.Add(new Label { name = NAME_ELEMENT_TITLE });
            
            return element;
        }

        protected virtual void BindItemTrigger(VisualElement element, int index)
        {
            IIcon icon = this.listTriggersFiltered[index].GetType().Name switch
            {
                "Trigger" => ICON_TRIGGER,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listTriggersFiltered[index].name} (ID {this.listTriggersFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemCondition(VisualElement element, int index)
        {
            IIcon icon = this.listConditionsFiltered[index].GetType().Name switch
            {
                "Conditions" => ICON_CONDITION,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listConditionsFiltered[index].name} (ID {this.listConditionsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemAction(VisualElement element, int index)
        {
            IIcon icon = this.listActionsFiltered[index].GetType().Name switch
            {
                "Actions" => ICON_INSTRUCTION,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listActionsFiltered[index].name} (ID {this.listActionsFiltered[index].GetInstanceID()})";
        }

        internal virtual void InitializeListTriggers()
        {
            this.listTriggers.AddRange(GameCreatorFinder2WindowTemplate.TRIGGERS_FOUND);
        }

        internal virtual void InitializeListConditions()
        {
            this.listConditions.AddRange(GameCreatorFinder2WindowTemplate.CONDITIONS_FOUND);
        }

        internal virtual void InitializeListActions()
        {
            this.listActions.AddRange(GameCreatorFinder2WindowTemplate.ACTIONS_FOUND);
        }

        public virtual void InitializeListComponents() { }

        public virtual void InitializeListScriptableObjects() { }

        protected virtual void RemoveLists(int tabIndex = -1)
        {
            if (tabIndex >= 0)
            {
                switch (tabIndex)
                {
                    case 0:
                        this.RemoveListTriggers();
                        break;
                    case 1:
                        this.RemoveListConditions();
                        break;
                    case 2:
                        this.RemoveListActions();
                        break;
                    case 3:
                        this.RemoveListComponents();
                        break;
                    case 4:
                        this.RemoveListScriptableObjects();
                        break;
                }
            }
            else
            {
                this.RemoveListTriggers();
                this.RemoveListConditions();
                this.RemoveListActions();
                this.RemoveListComponents();
                this.RemoveListScriptableObjects();
            }
        }

        internal virtual void RemoveListTriggers()
        {
            this.listTriggers = new List<Trigger>();
        }

        internal virtual void RemoveListConditions()
        {
            this.listConditions = new List<Conditions>();
        }

        internal virtual void RemoveListActions()
        {
            this.listActions = new List<Actions>();
        }

        public virtual void RemoveListComponents() { }

        protected virtual void RemoveListScriptableObjects() { }

        public virtual void RemoveListElements()
        {
            this.RemoveListTriggersElements();
            this.RemoveListConditionsElements();
            this.RemoveListActionsElements();
            this.RemoveListComponentsElements();
            this.RemoveListScriptableObjectsElements();
        }

        protected virtual void RemoveListTriggersElements()
        {
            try { this.content.Remove(this.messageBoxNoTriggersFound); } catch { }
            try { this.content.Remove(this.listViewTriggers); } catch { }
        }

        protected virtual void RemoveListConditionsElements()
        {
            try { this.content.Remove(this.messageBoxNoConditionsFound); } catch { }
            try { this.content.Remove(this.listViewConditions); } catch { }
        }

        protected virtual void RemoveListActionsElements()
        {
            try { this.content.Remove(this.messageBoxNoActionsFound); } catch { }
            try { this.content.Remove(this.listViewActions); } catch { }
        }

        protected virtual void RemoveListComponentsElements() { }

        protected virtual void RemoveListScriptableObjectsElements() { }

        protected virtual void RemoveShowHideButtons()
        {
            try { this.Remove(this.horizontalLayoutButtons); } catch { }
            try { this.horizontalLayoutButtons.Remove(this.buttonShowAllComponents); } catch { }
            try { this.horizontalLayoutButtons.Remove(this.buttonHideAllComponents); } catch { }
            try { this.horizontalLayoutButtons.Remove(this.buttonShowAllScriptableObjects); } catch { }
            try { this.horizontalLayoutButtons.Remove(this.buttonHideAllScriptableObjects); } catch { }
        }

        internal virtual void UpdateList(int tabIndex = -1)
        {
            if (tabIndex >= 0)
            {
                switch (tabIndex)
                {
                    case 0:
                        this.UpdateListTriggers();
                        break;
                    case 1:
                        this.UpdateListConditions();
                        break;
                    case 2:
                        this.UpdateListActions();
                        break;
                    case 3:
                        this.UpdateListComponents();
                        break;
                    case 4:
                        this.UpdateListScriptableObjects();
                        break;
                }
            }
            else
            {
                this.UpdateListTriggers();
                this.UpdateListConditions();
                this.UpdateListActions();
                this.UpdateListComponents();
                this.UpdateListScriptableObjects();
            }
        }

        internal virtual void UpdateListTriggers()
        {
            this.listTriggers = this.sortTriggersAZ ? this.listTriggers.OrderBy(go => go.name).ToList() : this.listTriggers.OrderByDescending(go => go.name).ToList();
            this.listTriggersFiltered.Clear();

            List<Trigger> listTriggersFoundByName = this.listTriggers.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listTriggersFiltered.AddRange(listTriggersFoundByName);

            if (GameCreatorFinder2WindowPreferences.TriggersHeaders)
            {
                List<Trigger> listTriggersFoundByTriggersHeaders = new List<Trigger>();

                for (int i = 0; i < this.listTriggers.Count; i++)
                {
                    List<bool> triggerHeadersWithNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listTriggers[i]);
                    SerializedProperty serializedTriggerEvent = so.FindProperty("m_TriggerEvent");

                    if (TypeUtils.GetTitleFromType(TypeUtils.GetTypeFromProperty(serializedTriggerEvent, true)).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()))
                    {
                        listTriggersFoundByTriggersHeaders.Add(this.listTriggers[i]);
                    }
                }

                this.listTriggersFiltered.AddRange(listTriggersFoundByTriggersHeaders);
            }

            if (GameCreatorFinder2WindowPreferences.TriggersInstructionsHeaders)
            {
                List<Trigger> listTriggersFoundByInstructionsTitle = new List<Trigger>();

                for (int i = 0; i < this.listTriggers.Count; i++)
                {
                    List<bool> instructionHeadersWithNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listTriggers[i]);
                    var list = so.FindProperty("m_Instructions").FindPropertyRelative("m_Instructions");
                    Instruction[] instructionList = list.GetValue<Instruction[]>();

                    for (int j = 0; j < instructionList.Length; j++)
                    {
                        instructionHeadersWithNames.Add(instructionList[j].Title.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (instructionHeadersWithNames.Contains(true)) listTriggersFoundByInstructionsTitle.Add(this.listTriggers[i]);
                }

                this.listTriggersFiltered.AddRange(listTriggersFoundByInstructionsTitle);
            }

            this.listTriggersFiltered = this.listTriggersFiltered.Distinct().ToList();
            this.listTriggersFiltered = this.sortTriggersAZ ? this.listTriggersFiltered.OrderBy(go => go.name).ToList() : this.listTriggersFiltered.OrderByDescending(go => go.name).ToList();

            this.RemoveListElements();

            if (this.listTriggersFiltered.Count > 0)
            {
                this.listViewTriggers.itemsSource = this.listTriggersFiltered;
                this.listViewTriggers.Rebuild();

                this.content.Add(this.listViewTriggers);
            }
            else
            {
                this.content.Add(this.messageBoxNoTriggersFound);
            }
        }

        internal virtual void UpdateListConditions()
        {
            this.listConditions = this.sortConditionsAZ ? this.listConditions.OrderBy(go => go.name).ToList() : this.listConditions.OrderByDescending(go => go.name).ToList();
            this.listConditionsFiltered.Clear();

            List<Conditions> listConditionsFoundByName = this.listConditions.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listConditionsFiltered.AddRange(listConditionsFoundByName);

            if (GameCreatorFinder2WindowPreferences.ConditionsDescriptions)
            {
                List<Conditions> listConditionsFoundByConditionsDescriptions = new List<Conditions>();

                for (int i = 0; i < this.listConditions.Count; i++)
                {
                    List<bool> conditionsDescriptionsWithNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listConditions[i]);
                    var list = so.FindProperty("m_Branches").FindPropertyRelative("m_Branches");

                    Branch[] branchList = list.GetValue<Branch[]>();

                    for (int j = 0; j < branchList.Length; j++)
                    {
                        conditionsDescriptionsWithNames.Add(branchList[j].Title.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (conditionsDescriptionsWithNames.Contains(true)) listConditionsFoundByConditionsDescriptions.Add(this.listConditions[i]);
                }

                this.listConditionsFiltered.AddRange(listConditionsFoundByConditionsDescriptions);
            }

            if (GameCreatorFinder2WindowPreferences.ConditionsHeaders)
            {
                List<Conditions> listConditionsFoundByConditionsHeaders = new List<Conditions>();

                for (int i = 0; i < this.listConditions.Count; i++)
                {
                    List<bool> conditionsHeadersWithNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listConditions[i]);
                    var list = so.FindProperty("m_Branches").FindPropertyRelative("m_Branches");
                    Branch[] branchList = list.GetValue<Branch[]>();

                    for (int j = 0; j < branchList.Length; j++)
                    {
                        var list2 = list.GetArrayElementAtIndex(j).FindPropertyRelative("m_ConditionList").FindPropertyRelative("m_Conditions");
                        Condition[] conditionList = list2.GetValue<Condition[]>();

                        for (int k = 0; k < conditionList.Length; k++)
                        {
                            conditionsHeadersWithNames.Add(conditionList[k].Title.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                        }
                    }

                    if (conditionsHeadersWithNames.Contains(true)) listConditionsFoundByConditionsHeaders.Add(this.listConditions[i]);
                }

                this.listConditionsFiltered.AddRange(listConditionsFoundByConditionsHeaders);
            }

            if (GameCreatorFinder2WindowPreferences.ConditionsInstructionsHeaders)
            {
                List<Conditions> listConditionsFoundByInstructionsHeaders = new List<Conditions>();

                for (int i = 0; i < this.listConditions.Count; i++)
                {
                    List<bool> instructionsHeadersWithNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listConditions[i]);
                    var list = so.FindProperty("m_Branches").FindPropertyRelative("m_Branches");
                    Branch[] branchList = list.GetValue<Branch[]>();

                    for (int j = 0; j < branchList.Length; j++)
                    {
                        var list2 = list.GetArrayElementAtIndex(j).FindPropertyRelative("m_InstructionList").FindPropertyRelative("m_Instructions");
                        Instruction[] instructionList = list2.GetValue<Instruction[]>();

                        for (int k = 0; k < instructionList.Length; k++)
                        {
                            instructionsHeadersWithNames.Add(instructionList[k].Title.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                        }
                    }

                    if (instructionsHeadersWithNames.Contains(true)) listConditionsFoundByInstructionsHeaders.Add(this.listConditions[i]);
                }

                this.listConditionsFiltered.AddRange(listConditionsFoundByInstructionsHeaders);
            }

            this.listConditionsFiltered = this.listConditionsFiltered.Distinct().ToList();
            this.listConditionsFiltered = this.sortConditionsAZ ? this.listConditionsFiltered.OrderBy(go => go.name).ToList() : this.listConditionsFiltered.OrderByDescending(go => go.name).ToList();

            this.RemoveListElements();

            if (this.listConditionsFiltered.Count > 0)
            {
                this.listViewConditions.itemsSource = this.listConditionsFiltered;
                this.listViewConditions.Rebuild();

                this.content.Add(this.listViewConditions);
            }
            else
            {
                this.content.Add(this.messageBoxNoConditionsFound);
            }
        }

        internal virtual void UpdateListActions()
        {
            this.listActions = this.sortActionsAZ ? this.listActions.OrderBy(go => go.name).ToList() : this.listActions.OrderByDescending(go => go.name).ToList();
            this.listActionsFiltered.Clear();

            List<Actions> listActionsFoundByName = this.listActions.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listActionsFiltered.AddRange(listActionsFoundByName);

            if (GameCreatorFinder2WindowPreferences.InstructionsHeaders)
            {
                List<Actions> listActionsFoundByInstructionsTitle = new List<Actions>();

                for (int i = 0; i < this.listActions.Count; i++)
                {
                    List<bool> instructionHeadersWithNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listActions[i]);
                    var list = so.FindProperty("m_Instructions").FindPropertyRelative("m_Instructions");
                    Instruction[] instructionList = list.GetValue<Instruction[]>();

                    for (int j = 0; j < instructionList.Length; j++)
                    {
                        instructionHeadersWithNames.Add(instructionList[j].Title.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (instructionHeadersWithNames.Contains(true)) listActionsFoundByInstructionsTitle.Add(this.listActions[i]);
                }

                this.listActionsFiltered.AddRange(listActionsFoundByInstructionsTitle);
            }

            this.listActionsFiltered = this.listActionsFiltered.Distinct().ToList();
            this.listActionsFiltered = this.sortTriggersAZ ? this.listActionsFiltered.OrderBy(go => go.name).ToList() : this.listActionsFiltered.OrderByDescending(go => go.name).ToList();

            this.RemoveListElements();

            if (this.listActionsFiltered.Count > 0)
            {
                this.listViewActions.itemsSource = this.listActionsFiltered;
                this.listViewActions.Rebuild();

                this.content.Add(this.listViewActions);
            }
            else
            {
                this.content.Add(this.messageBoxNoActionsFound);
            }
        }

        public virtual void UpdateListComponents() { }

        public virtual void UpdateListScriptableObjects() { }

        internal virtual void UpdateShowHideButtons(int tabIndex)
        {
            this.RemoveShowHideButtons();

            switch (tabIndex)
            {
                case 3:
                    this.horizontalLayoutButtons.Add(this.buttonShowAllComponents);
                    this.horizontalLayoutButtons.Add(this.buttonHideAllComponents);
                    this.Remove(this.content);
                    this.Add(this.horizontalLayoutButtons);
                    this.Add(this.content);
                    break;
                case 4:
                    this.horizontalLayoutButtons.Add(this.buttonShowAllScriptableObjects);
                    this.horizontalLayoutButtons.Add(this.buttonHideAllScriptableObjects);
                    this.Remove(this.content);
                    this.Add(this.horizontalLayoutButtons);
                    this.Add(this.content);
                    break;
            }
        }

        protected virtual void ShowAllComponents() { }

        protected virtual void HideAllComponents() { }

        protected virtual void ShowAllScriptableObjects() { }

        protected virtual void HideAllScriptableObjects() { }

        protected virtual void OnContentSelectItemTrigger(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "", this.listViewTriggers.selectedIndex);
        }

        protected virtual void OnContentSelectItemCondition(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "", this.listViewConditions.selectedIndex);
        }

        protected virtual void OnContentSelectItemAction(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "", this.listViewActions.selectedIndex);
        }

        protected virtual void OnContentChooseItemTrigger(IEnumerable<object> list)
        {   
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "", this.listViewTriggers.selectedIndex);
        }

        protected virtual void OnContentChooseItemCondition(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "", this.listViewConditions.selectedIndex);
        }

        protected virtual void OnContentChooseItemAction(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "", this.listViewActions.selectedIndex);
        }
    }
}