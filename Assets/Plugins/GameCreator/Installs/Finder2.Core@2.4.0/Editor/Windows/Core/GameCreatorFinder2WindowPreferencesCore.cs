using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowPreferencesCore : GameCreatorFinder2WindowPreferences
    {
        protected new const string MENU_TITLE = "GC2 Finder [Core] - Preferences";

        protected const string LABEL_TOGGLE_COMPONENTS_CHARACTERS_IKRIGLAYERS = "Character IK Rig Layers";
        protected const string LABEL_TOGGLE_COMPONENTS_CHARACTERS_FOOTSTEPS = "Character Footsteps";

        protected const string LABEL_TOGGLE_COMPONENTS_HOTSPOTS_SPOTS = "Hotspot Spots";

        protected const string LABEL_TOGGLE_COMPONENTS_LOCALNAMEVARIABLES_NAMES = "Local Name Variable Names";

        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_GLOBALNAMEVARIABLES_NAMES = "Global Name Variable Names";

        protected const string KEY_COMPONENTS_CHARACTERS_IKRIGLAYERS = "gcfinder2:componentscharactersikriglayers";
        protected const string KEY_COMPONENTS_CHARACTERS_FOOTSTEPS = "gcfinder2:componentscharactersfootsteps";
        protected const string KEY_COMPONENTS_HOTSPOTSSPOTS = "gcfinder2:componenthotspotsspots";
        protected const string KEY_COMPONENTS_LOCALNAMEVARIABLES = "gcfinder2:componentslocalnamevariables";
        protected const string KEY_SCRIPABTLEOBJECTS_GLOBALNAMEVARIABLES = "gcfinder2:scriptableobjectsglobalnamevariables";

        protected Toggle toggleComponentsCharactersIKRigLayers;
        protected Toggle toggleComponentCharactersFootsteps;
        protected Toggle toggleComponentsHotspotsSpots;
        protected Toggle toggleComponentsLocalNameVariablesNames;
        protected Toggle toggleScriptableObjectsGlobalNameVariablesNames;

        public static bool ComponentsCharactersIKRigLayers
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_CHARACTERS_IKRIGLAYERS);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_CHARACTERS_IKRIGLAYERS, value);
        }

        public static bool ComponentsCharactersFootsteps
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_CHARACTERS_FOOTSTEPS);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_CHARACTERS_FOOTSTEPS, value);
        }

        public static bool ComponentsHotspotsSpots
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_HOTSPOTSSPOTS);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_HOTSPOTSSPOTS, value);
        }

        public static bool ComponentsLocalNameVariables
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_LOCALNAMEVARIABLES);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_LOCALNAMEVARIABLES, value);
        }

        public static bool ScriptableObjectsGlobalNameVariables
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_GLOBALNAMEVARIABLES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_GLOBALNAMEVARIABLES, value);
        }

        public static new void OpenWindow()
        {
            if (WINDOW != null) WINDOW.Close();

            WINDOW = GetWindow<GameCreatorFinder2WindowPreferencesCore>(true, MENU_TITLE, true);
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void AddComponentSettings()
        {
            base.AddComponentSettings();

            this.toggleComponentsCharactersIKRigLayers = new Toggle(LABEL_TOGGLE_COMPONENTS_CHARACTERS_IKRIGLAYERS) { value = ComponentsCharactersIKRigLayers };
            this.bodyComponents.Add(this.toggleComponentsCharactersIKRigLayers);

            this.toggleComponentCharactersFootsteps = new Toggle(LABEL_TOGGLE_COMPONENTS_CHARACTERS_FOOTSTEPS) { value = ComponentsCharactersFootsteps };
            this.bodyComponents.Add(this.toggleComponentCharactersFootsteps);

            this.toggleComponentsHotspotsSpots = new Toggle(LABEL_TOGGLE_COMPONENTS_HOTSPOTS_SPOTS) { value = ComponentsHotspotsSpots };
            this.bodyComponents.Add(this.toggleComponentsHotspotsSpots);

            this.toggleComponentsLocalNameVariablesNames = new Toggle(LABEL_TOGGLE_COMPONENTS_LOCALNAMEVARIABLES_NAMES) { value = ComponentsLocalNameVariables };
            this.bodyComponents.Add(this.toggleComponentsLocalNameVariablesNames);
        }

        protected override void AddScriptableObjectSettings()
        {
            base.AddScriptableObjectSettings();

            this.toggleScriptableObjectsGlobalNameVariablesNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_GLOBALNAMEVARIABLES_NAMES) { value = ScriptableObjectsGlobalNameVariables };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsGlobalNameVariablesNames);
        }

        protected override void ApplyProperties()
        {
            base.ApplyProperties();

            ComponentsCharactersIKRigLayers = this.toggleComponentsCharactersIKRigLayers.value;
            ComponentsCharactersFootsteps = this.toggleComponentCharactersFootsteps.value;
            ComponentsHotspotsSpots = this.toggleComponentsHotspotsSpots.value;
            ComponentsLocalNameVariables = this.toggleComponentsLocalNameVariablesNames.value;

            ScriptableObjectsGlobalNameVariables = this.toggleScriptableObjectsGlobalNameVariablesNames.value;
        }
    }
}