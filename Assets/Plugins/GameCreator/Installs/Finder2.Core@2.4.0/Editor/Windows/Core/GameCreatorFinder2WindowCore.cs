using GameCreator.Editor.Installs;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowCore : GameCreatorFinder2WindowTemplate
    {
        protected const string MENU_ITEM = "Game Creator/Finder/Core";
        protected const string MENU_TITLE = "GC2 Finder [Core]";

        internal static Character[] CHARACTER_FOUND;
        internal static Hotspot[] HOTSPOT_FOUND;
        internal static LocalListVariables[] LOCALLISTVARIABLES_FOUND;
        internal static LocalNameVariables[] LOCALNAMEVARIABLES_FOUND;
        internal static MainCamera[] MAINCAMERA_FOUND;
        internal static Marker[] MARKER_FOUND;
        internal static Remember[] REMEMBER_FOUND;
        internal static ShotCamera[] SHOTCAMERA_FOUND;

        internal static Installer[] INSTALLERS_FOUND;
        internal static GlobalListVariables[] GLOBALLISTVARIABLES_FOUND;
        internal static GlobalNameVariables[] GLOBALNAMEVARIABLES_FOUND;
        internal static MaterialSoundsAsset[] MATERIALSOUNDSASSETS_FOUND;
        internal static StateAnimation[] STATEANIMATIONS_FOUND;
        internal static StateBasicLocomotion[] STATEBASICLOCOMOTIONS_FOUND;
        internal static StateCompleteLocomotion[] STATECOMPLETELOCOMOTIONS_FOUND;

        [MenuItem(MENU_ITEM)]
        public static void OpenWindow()
        {
            WINDOW = GetWindow<GameCreatorFinder2WindowCore>();
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void Build()
        {
            this.Toolbar = new GameCreatorFinder2WindowToolbarCore(this) { name = NAME_TOOLBAR };
            this.Content = new GameCreatorFinder2WindowContentCore(this) { name = NAME_CONTENT };

            this.rootVisualElement.Add(this.Toolbar);
            this.rootVisualElement.Add(this.Content);

            this.Toolbar.OnEnable();
            ((GameCreatorFinder2WindowContentCore)this.Content).OnEnable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            this.titleContent = new GUIContent(MENU_TITLE, iconWindow.Texture);
        }

        protected override void FindComponents()
        {
            this.FindCharacters();
            this.FindHotspots();
            this.FindLocalListVariables();
            this.FindLocalNameVariables();
            this.FindMainCameras();
            this.FindMarkers();
            this.FindRemembers();
            this.FindShotCameras();
        }

        protected override void FindScriptableObjects()
        {
            this.FindInstallers();
            this.FindGlobalListVariables();
            this.FindGlobalNameVariables();
            this.FindMaterialSoundsAssets();
            this.FindStateAnimations();
            this.FindStateBasicLocomotions();
            this.FindStateCompleteLocomotions();
        }

        protected virtual void FindCharacters()
        {
            CHARACTER_FOUND = FindObjectsOfType<Character>();
        }

        protected virtual void FindHotspots()
        {
            HOTSPOT_FOUND = FindObjectsOfType<Hotspot>();
        }

        protected virtual void FindLocalListVariables()
        {
            LOCALLISTVARIABLES_FOUND = FindObjectsOfType<LocalListVariables>();
        }

        protected virtual void FindLocalNameVariables()
        {
            LOCALNAMEVARIABLES_FOUND = FindObjectsOfType<LocalNameVariables>();
        }

        protected virtual void FindMainCameras()
        {
            MAINCAMERA_FOUND = FindObjectsOfType<MainCamera>();
        }

        protected virtual void FindMarkers()
        {
            MARKER_FOUND = FindObjectsOfType<Marker>();
        }

        protected virtual void FindRemembers()
        {
            REMEMBER_FOUND = FindObjectsOfType<Remember>();
        }

        protected virtual void FindShotCameras()
        {
            SHOTCAMERA_FOUND = FindObjectsOfType<ShotCamera>();
        }

        protected virtual void FindInstallers()
        {
            string[] InstallersFound = AssetDatabase.FindAssets("t:Installer");

            INSTALLERS_FOUND = new Installer[InstallersFound.Length];

            for (int i = 0; i < InstallersFound.Length; i++)
            {
                string guid = InstallersFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                INSTALLERS_FOUND[i] = (Installer)AssetDatabase.LoadAssetAtPath(path, typeof(Installer));
            }
        }

        protected virtual void FindGlobalListVariables()
        {
            string[] globalListVariablesFound = AssetDatabase.FindAssets("t:GlobalListVariables");

            GLOBALLISTVARIABLES_FOUND = new GlobalListVariables[globalListVariablesFound.Length];

            for (int i = 0; i < globalListVariablesFound.Length; i++)
            {
                string guid = globalListVariablesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GLOBALLISTVARIABLES_FOUND[i] = (GlobalListVariables)AssetDatabase.LoadAssetAtPath(path, typeof(GlobalListVariables));
            }
        }

        protected virtual void FindGlobalNameVariables()
        {
            string[] globalNameVariablesFound = AssetDatabase.FindAssets("t:GlobalNameVariables");

            GLOBALNAMEVARIABLES_FOUND = new GlobalNameVariables[globalNameVariablesFound.Length];

            for (int i = 0; i < globalNameVariablesFound.Length; i++)
            {
                string guid = globalNameVariablesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GLOBALNAMEVARIABLES_FOUND[i] = (GlobalNameVariables)AssetDatabase.LoadAssetAtPath(path, typeof(GlobalNameVariables));
            }
        }

        protected virtual void FindMaterialSoundsAssets()
        {
            string[] materialSoundsAssetsFound = AssetDatabase.FindAssets("t:MaterialSoundsAsset");

            MATERIALSOUNDSASSETS_FOUND = new MaterialSoundsAsset[materialSoundsAssetsFound.Length];

            for (int i = 0; i < materialSoundsAssetsFound.Length; i++)
            {
                string guid = materialSoundsAssetsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MATERIALSOUNDSASSETS_FOUND[i] = (MaterialSoundsAsset)AssetDatabase.LoadAssetAtPath(path, typeof(MaterialSoundsAsset));
            }
        }

        protected virtual void FindStateAnimations()
        {
            string[] stateAnimationsFound = AssetDatabase.FindAssets("t:StateAnimation");

            STATEANIMATIONS_FOUND = new StateAnimation[stateAnimationsFound.Length];

            for (int i = 0; i < stateAnimationsFound.Length; i++)
            {
                string guid = stateAnimationsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                STATEANIMATIONS_FOUND[i] = (StateAnimation)AssetDatabase.LoadAssetAtPath(path, typeof(StateAnimation));
            }
        }

        protected virtual void FindStateBasicLocomotions()
        {
            string[] stateBasicLocomotionsFound = AssetDatabase.FindAssets("t:StateBasicLocomotion");

            STATEBASICLOCOMOTIONS_FOUND = new StateBasicLocomotion[stateBasicLocomotionsFound.Length];

            for (int i = 0; i < stateBasicLocomotionsFound.Length; i++)
            {
                string guid = stateBasicLocomotionsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                STATEBASICLOCOMOTIONS_FOUND[i] = (StateBasicLocomotion)AssetDatabase.LoadAssetAtPath(path, typeof(StateBasicLocomotion));
            }
        }

        protected virtual void FindStateCompleteLocomotions()
        {
            string[] stateCompleteLocomotionsFound = AssetDatabase.FindAssets("t:StateCompleteLocomotion");

            STATECOMPLETELOCOMOTIONS_FOUND = new StateCompleteLocomotion[stateCompleteLocomotionsFound.Length];

            for (int i = 0; i < stateCompleteLocomotionsFound.Length; i++)
            {
                string guid = stateCompleteLocomotionsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                STATECOMPLETELOCOMOTIONS_FOUND[i] = (StateCompleteLocomotion)AssetDatabase.LoadAssetAtPath(path, typeof(StateCompleteLocomotion));
            }
        }
    }
}