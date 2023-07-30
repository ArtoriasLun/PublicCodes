using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowPreferencesMelee : GameCreatorFinder2WindowPreferences
    {
        protected new const string MENU_TITLE = "GC2 Finder [Melee] - Preferences";

        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_SKILLS_NAMES = "Skill Names";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_SKILLS_DESCRIPTIONS = "Skill Descriptions";

        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_WEAPON_NAMES = "Weapon Names";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_WEAPON_DESCRIPTIONS = "Weapon Descriptions";

        protected const string KEY_SCRIPABTLEOBJECTS_SKILLS_NAMES = "gcfinder2:scriptableobjectsskillsnames";
        protected const string KEY_SCRIPABTLEOBJECTS_SKILLS_DESCRIPTIONS = "gcfinder2:scriptableobjectsskillssdescriptions";

        protected const string KEY_SCRIPABTLEOBJECTS_WEAPONS_NAMES = "gcfinder2:scriptableobjectsweaponsnames";
        protected const string KEY_SCRIPABTLEOBJECTS_WEAPONS_DESCRIPTIONS = "gcfinder2:scriptableobjectsweaponssdescriptions";

        protected Toggle toggleScriptableObjectsSkillsNames;
        protected Toggle toggleScriptableObjectsSkillsDescriptions;

        protected Toggle toggleScriptableObjectsWeaponsNames;
        protected Toggle toggleScriptableObjectsWeaponsDescriptions;

        public static bool ScriptableObjectsSkillsNames
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_SKILLS_NAMES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_SKILLS_NAMES, value);
        }

        public static bool ScriptableObjectsSkillsDescriptions
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_SKILLS_DESCRIPTIONS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_SKILLS_DESCRIPTIONS, value);
        }

        public static bool ScriptableObjectsWeaponsNames
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_WEAPONS_NAMES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_WEAPONS_NAMES, value);
        }

        public static bool ScriptableObjectsWeaponsDescriptions
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_WEAPONS_DESCRIPTIONS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_WEAPONS_DESCRIPTIONS, value);
        }

        public static new void OpenWindow()
        {
            if (WINDOW != null) WINDOW.Close();

            WINDOW = GetWindow<GameCreatorFinder2WindowPreferencesMelee>(true, MENU_TITLE, true);
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void AddScriptableObjectSettings()
        {
            base.AddScriptableObjectSettings();

            this.toggleScriptableObjectsSkillsNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_SKILLS_NAMES) { value = ScriptableObjectsSkillsNames };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsSkillsNames);

            this.toggleScriptableObjectsSkillsDescriptions = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_SKILLS_DESCRIPTIONS) { value = ScriptableObjectsSkillsDescriptions };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsSkillsDescriptions);

            this.toggleScriptableObjectsWeaponsNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_WEAPON_NAMES) { value = ScriptableObjectsWeaponsNames };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsWeaponsNames);

            this.toggleScriptableObjectsWeaponsDescriptions = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_WEAPON_DESCRIPTIONS) { value = ScriptableObjectsWeaponsDescriptions };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsWeaponsDescriptions);
        }

        protected override void ApplyProperties()
        {
            base.ApplyProperties();

            ScriptableObjectsSkillsNames = this.toggleScriptableObjectsSkillsNames.value;
            ScriptableObjectsSkillsDescriptions = this.toggleScriptableObjectsSkillsDescriptions.value;
            ScriptableObjectsWeaponsNames = this.toggleScriptableObjectsWeaponsNames.value;
            ScriptableObjectsWeaponsDescriptions = this.toggleScriptableObjectsWeaponsDescriptions.value;
        }
    }
}