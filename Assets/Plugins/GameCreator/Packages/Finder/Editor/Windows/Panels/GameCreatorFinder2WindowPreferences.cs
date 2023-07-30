using GameCreator.Editor.Common;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public abstract class GameCreatorFinder2WindowPreferences : EditorWindow
    {
        protected const string MENU_TITLE = "GC2 Finder [module] - Preferences";

        protected const int MIN_WIDTH = 400;
        protected const int MIN_HEIGHT = 575;

        protected const string LABEL_TITLE = "GC2 Finder Search Preferences";

        protected const string LABEL_TAB_TRIGGERS = "Triggers:";
        protected const string LABEL_TAB_CONDITIONS = "Conditions:";
        protected const string LABEL_TAB_ACTIONS = "Actions:";
        protected const string LABEL_TAB_COMPONENTS = "Components:";
        protected const string LABEL_TAB_SCRIPTABLEOBJECTS = "Scriptable Objects:";

        protected const string LABEL_TOGGLE_TRIGGERS_HEADERS = "Headers";
        protected const string LABEL_TOGGLE_TRIGGERS_INSTRUCTIONS_HEADERS = "Instructions Headers";

        protected const string LABEL_TOGGLE_CONDITIONS_DESCRIPTIONS = "Descriptions";
        protected const string LABEL_TOGGLE_CONDITIONS_HEADERS = "Headers";
        protected const string LABEL_TOGGLE_CONDITIONS_INSTRUCTIONS_HEADERS = "Instructions Headers";

        protected const string LABEL_TOGGLE_INSTRUCTIONS_HEADERS = "Headers";

        protected const string LABEL_BUTTON_APPLY_PREFERENCES = "Apply Preferences";

        protected const string LABEL_SAVED_PREFERENCES = "Saved Preferences";

        protected const string KEY_TRIGGERS_HEADERS = "gcfinder2:triggersheaders";
        protected const string KEY_TRIGGERS_INSTRUCTIONS_HEADERS = "gcfinder2:triggersinstructionsheaders";
        protected const string KEY_CONDITIONS_DESCRIPTIONS = "gcfinder2:conditionsdescriptions";
        protected const string KEY_CONDITIONS_HEADERS = "gcfinder2:conditionsheaders";
        protected const string KEY_CONDITIONS_INSTRUCTIONS_HEADERS = "gcfinder2:conditionsinstructionsheaders";
        protected const string KEY_INSTRUCTIONS_HEADERS = "gcfinder2:instructionsheaders";

        protected const string USS_PATH = "Assets/Plugins/GameCreator/Packages/Finder/Editor/Windows/Stylesheets/GameCreatorFinder2Window";

        protected const string NAME_PREFERENCES_HEAD = "Finder2-Preferences-Head";
        protected const string NAME_PREFERENCES_BODY = "Finder2-Preferences-Body";
        protected const string NAME_PREFERENCES_FOOT = "Finder2-Preferences-Foot";

        protected const string NAME_PREFERENCES_LABEL_BOLD = "Finder2-Preferences-Label-Bold";

        protected static GameCreatorFinder2WindowPreferences WINDOW;

        protected Label labelTitle;
        protected Label labelTriggers;
        protected Label labelConditions;
        protected Label labelActions;
        protected Label labelComponents;
        protected Label labelScriptableObjects;
        protected Label labelSavedPreferences;

        protected Toggle toggleTriggersHeaders;
        protected Toggle toggleTriggersInstructionsHeaders;
        protected Toggle toggleConditionsDescriptions;
        protected Toggle toggleConditionsHeaders;
        protected Toggle toggleConditionsInstructionsHeaders;
        protected Toggle toggleInstructionsHeaders;

        protected Button buttonApplyPreferences;

        protected VisualElement head;
        protected VisualElement bodyTriggers;
        protected VisualElement bodyConditions;
        protected VisualElement bodyActions;
        protected VisualElement bodyComponents;
        protected VisualElement bodyScriptableObjects;
        protected VisualElement foot;

        public static bool TriggersHeaders
        {
            get => EditorPrefs.GetBool(KEY_TRIGGERS_HEADERS);
            protected set => EditorPrefs.SetBool(KEY_TRIGGERS_HEADERS, value);
        }

        public static bool TriggersInstructionsHeaders
        {
            get => EditorPrefs.GetBool(KEY_TRIGGERS_INSTRUCTIONS_HEADERS);
            protected set => EditorPrefs.SetBool(KEY_TRIGGERS_INSTRUCTIONS_HEADERS, value);
        }

        public static bool ConditionsDescriptions
        {
            get => EditorPrefs.GetBool(KEY_CONDITIONS_DESCRIPTIONS);
            protected set => EditorPrefs.SetBool(KEY_CONDITIONS_DESCRIPTIONS, value);
        }

        public static bool ConditionsHeaders
        {
            get => EditorPrefs.GetBool(KEY_CONDITIONS_HEADERS);
            protected set => EditorPrefs.SetBool(KEY_CONDITIONS_HEADERS, value);
        }

        public static bool ConditionsInstructionsHeaders
        {
            get => EditorPrefs.GetBool(KEY_CONDITIONS_INSTRUCTIONS_HEADERS);
            protected set => EditorPrefs.SetBool(KEY_CONDITIONS_INSTRUCTIONS_HEADERS, value);
        }

        public static bool InstructionsHeaders
        {
            get => EditorPrefs.GetBool(KEY_INSTRUCTIONS_HEADERS);
            protected set => EditorPrefs.SetBool(KEY_INSTRUCTIONS_HEADERS, value);
        }

        public static void OpenWindow()
        {
            if (WINDOW != null) WINDOW.Close();

            WINDOW = GetWindow<GameCreatorFinder2WindowPreferences>(true, MENU_TITLE, true);
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected virtual void OnEnable()
        {
            StyleSheet[] styleSheetsCollection = StyleSheetUtils.Load(USS_PATH);
            foreach (StyleSheet styleSheet in styleSheetsCollection)
            {
                this.rootVisualElement.styleSheets.Add(styleSheet);
            }

            this.head = new VisualElement { name = NAME_PREFERENCES_HEAD };
            this.bodyTriggers = new VisualElement { name = NAME_PREFERENCES_BODY };
            this.bodyConditions = new VisualElement { name = NAME_PREFERENCES_BODY };
            this.bodyActions = new VisualElement { name = NAME_PREFERENCES_BODY };
            this.bodyComponents = new VisualElement { name = NAME_PREFERENCES_BODY };
            this.bodyScriptableObjects = new VisualElement { name = NAME_PREFERENCES_BODY };
            this.foot = new VisualElement { name = NAME_PREFERENCES_FOOT };

            this.labelTitle = new Label(LABEL_TITLE);
            this.head.Add(this.labelTitle);

            this.labelTriggers = new Label(LABEL_TAB_TRIGGERS) { name = NAME_PREFERENCES_LABEL_BOLD};
            this.labelConditions = new Label(LABEL_TAB_CONDITIONS) { name = NAME_PREFERENCES_LABEL_BOLD };
            this.labelActions = new Label(LABEL_TAB_ACTIONS) { name = NAME_PREFERENCES_LABEL_BOLD };
            this.labelComponents = new Label(LABEL_TAB_COMPONENTS) { name = NAME_PREFERENCES_LABEL_BOLD };
            this.labelScriptableObjects = new Label(LABEL_TAB_SCRIPTABLEOBJECTS) { name = NAME_PREFERENCES_LABEL_BOLD };

            this.bodyTriggers.Add(this.labelTriggers);

            this.toggleTriggersHeaders = new Toggle(LABEL_TOGGLE_TRIGGERS_HEADERS) { value = TriggersHeaders };
            this.bodyTriggers.Add(this.toggleTriggersHeaders);

            this.toggleTriggersInstructionsHeaders = new Toggle(LABEL_TOGGLE_TRIGGERS_INSTRUCTIONS_HEADERS) { value = TriggersInstructionsHeaders };
            this.bodyTriggers.Add(this.toggleTriggersInstructionsHeaders);

            this.bodyConditions.Add(this.labelConditions);

            this.toggleConditionsDescriptions = new Toggle(LABEL_TOGGLE_CONDITIONS_DESCRIPTIONS) { value = ConditionsDescriptions };
            this.bodyConditions.Add(this.toggleConditionsDescriptions);

            this.toggleConditionsHeaders = new Toggle(LABEL_TOGGLE_CONDITIONS_HEADERS) { value = ConditionsHeaders };
            this.bodyConditions.Add(this.toggleConditionsHeaders);

            this.toggleConditionsInstructionsHeaders = new Toggle(LABEL_TOGGLE_CONDITIONS_INSTRUCTIONS_HEADERS) { value = ConditionsInstructionsHeaders };
            this.bodyConditions.Add(this.toggleConditionsInstructionsHeaders);

            this.bodyActions.Add(this.labelActions);

            this.toggleInstructionsHeaders = new Toggle(LABEL_TOGGLE_INSTRUCTIONS_HEADERS) { value = InstructionsHeaders };
            this.bodyActions.Add(this.toggleInstructionsHeaders);

            this.AddComponentSettings();

            this.AddScriptableObjectSettings();

            this.labelSavedPreferences = new Label(LABEL_SAVED_PREFERENCES) { name = NAME_PREFERENCES_LABEL_BOLD, visible = false };
            this.foot.Add(this.labelSavedPreferences);

            this.buttonApplyPreferences = new Button(this.ApplyPreferences) { text = LABEL_BUTTON_APPLY_PREFERENCES};
            this.foot.Add(this.buttonApplyPreferences);

            this.rootVisualElement.Add(this.head);
            this.rootVisualElement.Add(this.bodyTriggers);
            this.rootVisualElement.Add(this.bodyConditions);
            this.rootVisualElement.Add(this.bodyActions);
            this.rootVisualElement.Add(this.bodyComponents);
            this.rootVisualElement.Add(this.bodyScriptableObjects);
            this.rootVisualElement.Add(this.foot);
        }

        protected virtual void AddComponentSettings()
        {
            this.bodyComponents.Add(this.labelComponents);
        }

        protected virtual void AddScriptableObjectSettings()
        {
            this.bodyScriptableObjects.Add(this.labelScriptableObjects);
        }

        protected async virtual void ApplyPreferences()
        {
            this.ApplyProperties();

            this.labelSavedPreferences.visible = true;

            await Task.Delay(2000);

            this.labelSavedPreferences.visible = false;
        }

        protected virtual void ApplyProperties()
        {
            TriggersHeaders = this.toggleTriggersHeaders.value;
            TriggersInstructionsHeaders = this.toggleTriggersInstructionsHeaders.value;

            ConditionsDescriptions = this.toggleConditionsDescriptions.value;
            ConditionsHeaders = this.toggleConditionsHeaders.value;
            ConditionsInstructionsHeaders = this.toggleConditionsInstructionsHeaders.value;

            InstructionsHeaders = this.toggleInstructionsHeaders.value;
        }
    }
}