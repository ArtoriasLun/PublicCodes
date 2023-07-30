using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowPreferencesStats : GameCreatorFinder2WindowPreferences
    {
        protected new const string MENU_TITLE = "GC2 Finder [Stats] - Preferences";

        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_ATTRIBUTE_IDS = "Attribute IDs";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_CLASS_NAMES = "Class Names";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_FORMULA_EXPRESSIONS = "Formula Expressions";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_STAT_IDS = "Stat IDs";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_STATUSEFFECT_IDS = "Status Effect IDs";

        protected const string KEY_SCRIPTABLEOBJECTS_ATTRIBUTE_IDS = "gcfinder2:scriptableobjectsattributeids";
        protected const string KEY_SCRIPTABLEOBJECTS_CLASS_NAMES = "gcfinder2:scriptableobjectsclassnames";
        protected const string KEY_SCRIPTABLEOBJECTS_FORMULA_EXPRESSIONS = "gcfinder2:scriptableobjectsformulaexpressions";
        protected const string KEY_SCRIPTABLEOBJECTS_STAT_IDS = "gcfinder2:scriptableobjectsstatid";
        protected const string KEY_SCRIPTABLEOBJECTS_STATUSEFFECT_IDS = "gcfinder2:scriptableobjectsstatuseffectids";

        protected Toggle toggleScriptableObjectsAttributeIDs;
        protected Toggle toggleScriptableObjectsClassNames;
        protected Toggle toggleScriptableObjectsFormulaExpressions;
        protected Toggle toggleScriptableObjectsStatIDs;
        protected Toggle toggleScriptableObjectsStatusEffectIDs;

        public static bool ScriptableObjectsAttributeIDs
        {
            get => EditorPrefs.GetBool(KEY_SCRIPTABLEOBJECTS_ATTRIBUTE_IDS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPTABLEOBJECTS_ATTRIBUTE_IDS, value);
        }

        public static bool ScriptableObjectsClassNames
        {
            get => EditorPrefs.GetBool(KEY_SCRIPTABLEOBJECTS_CLASS_NAMES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPTABLEOBJECTS_CLASS_NAMES, value);
        }

        public static bool ScriptableObjectsFormulaExpressions
        {
            get => EditorPrefs.GetBool(KEY_SCRIPTABLEOBJECTS_FORMULA_EXPRESSIONS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPTABLEOBJECTS_FORMULA_EXPRESSIONS, value);
        }

        public static bool ScriptableObjectsStatIDs
        {
            get => EditorPrefs.GetBool(KEY_SCRIPTABLEOBJECTS_STAT_IDS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPTABLEOBJECTS_STAT_IDS, value);
        }

        public static bool ScriptableObjectsStatusEffectIDs
        {
            get => EditorPrefs.GetBool(KEY_SCRIPTABLEOBJECTS_STATUSEFFECT_IDS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPTABLEOBJECTS_STATUSEFFECT_IDS, value);
        }

        public static new void OpenWindow()
        {
            if (WINDOW != null) WINDOW.Close();

            WINDOW = GetWindow<GameCreatorFinder2WindowPreferencesStats>(true, MENU_TITLE, true);
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void AddComponentSettings() { }

        protected override void AddScriptableObjectSettings()
        {
            base.AddScriptableObjectSettings();

            this.toggleScriptableObjectsAttributeIDs = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_ATTRIBUTE_IDS) { value = ScriptableObjectsAttributeIDs };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsAttributeIDs);

            this.toggleScriptableObjectsClassNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_CLASS_NAMES) { value = ScriptableObjectsClassNames };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsClassNames);

            this.toggleScriptableObjectsFormulaExpressions = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_FORMULA_EXPRESSIONS) { value = ScriptableObjectsFormulaExpressions };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsFormulaExpressions);

            this.toggleScriptableObjectsStatIDs = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_STAT_IDS) { value = ScriptableObjectsStatIDs };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsStatIDs);

            this.toggleScriptableObjectsStatusEffectIDs = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_STATUSEFFECT_IDS) { value = ScriptableObjectsStatusEffectIDs };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsStatusEffectIDs);
        }

        protected override void ApplyProperties()
        {
            base.ApplyProperties();

            ScriptableObjectsAttributeIDs = this.toggleScriptableObjectsAttributeIDs.value;
            ScriptableObjectsClassNames = this.toggleScriptableObjectsClassNames.value;
            ScriptableObjectsFormulaExpressions = this.toggleScriptableObjectsFormulaExpressions.value;
            ScriptableObjectsStatIDs = this.toggleScriptableObjectsStatIDs.value;
            ScriptableObjectsStatusEffectIDs = this.toggleScriptableObjectsStatusEffectIDs.value;
        }
    }
}