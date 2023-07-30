using GameCreator.Editor.Common;
using GameCreator.Editor.Installs;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Characters.IK;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentListCore : GameCreatorFinder2WindowContentList
    {
        protected static readonly IIcon ICON_CHARACTER = new IconCharacter(ColorTheme.Type.Gray);
        protected static readonly IIcon ICON_CHARACTERRUN = new IconCharacterRun(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_CHARACTERSTATE = new IconCharacterState(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_INSTALLER = new IconInstructions(ColorTheme.Type.Gray);
        protected static readonly IIcon ICON_HOTSPOT = new IconHotspot(ColorTheme.Type.Yellow);
        protected static readonly IIcon ICON_LISTVARIABLES = new IconListVariable(ColorTheme.Type.Teal);
        protected static readonly IIcon ICON_MAINCAMERA = new IconCamera(ColorTheme.Type.Blue);
        protected static readonly IIcon ICON_MARKER = new IconMarker(ColorTheme.Type.Yellow);
        protected static readonly IIcon ICON_NAMEVARIABLES = new IconNameVariable(ColorTheme.Type.Purple);
        protected static readonly IIcon ICON_SAVE = new IconDiskSolid(ColorTheme.Type.Gray);
        protected static readonly IIcon ICON_SHOTCAMERA = new IconCameraShot(ColorTheme.Type.Yellow);
        protected static readonly IIcon ICON_MUSICNOTE = new IconMusicNote(ColorTheme.Type.Yellow);

        private readonly GameCreatorFinder2WindowCore window;

        internal List<Character> listComponentsCharacters = new List<Character>();
        internal List<Character> listComponentsCharactersFiltered = new List<Character>();
        internal List<Hotspot> listComponentsHotspots = new List<Hotspot>();
        internal List<Hotspot> listComponentsHotspotsFiltered = new List<Hotspot>();
        internal List<LocalListVariables> listComponentsLocalListVariables = new List<LocalListVariables>();
        internal List<LocalListVariables> listComponentsLocalListVariablesFiltered = new List<LocalListVariables>();
        internal List<LocalNameVariables> listComponentsLocalNameVariables = new List<LocalNameVariables>();
        internal List<LocalNameVariables> listComponentsLocalNameVariablesFiltered = new List<LocalNameVariables>();
        internal List<MainCamera> listComponentstMainCameras = new List<MainCamera>();
        internal List<MainCamera> listComponentsMainCamerasFiltered = new List<MainCamera>();
        internal List<Marker> listComponentsMarkers = new List<Marker>();
        internal List<Marker> listComponentsMarkersFiltered = new List<Marker>();
        internal List<Remember> listComponentsRemembers = new List<Remember>();
        internal List<Remember> listComponentsRemembersFiltered = new List<Remember>();
        internal List<ShotCamera> listComponentsShotCameras = new List<ShotCamera>();
        internal List<ShotCamera> listComponentsShotCamerasFiltered = new List<ShotCamera>();

        internal List<Installer> listScriptableObjectsInstallers = new List<Installer>();
        internal List<Installer> listScriptableObjectsInstallersFiltered = new List<Installer>();
        internal List<GlobalListVariables> listScriptableObjectsGlobalListVariables = new List<GlobalListVariables>();
        internal List<GlobalListVariables> listScriptableObjectsGlobalListVariablesFiltered = new List<GlobalListVariables>();
        internal List<GlobalNameVariables> listScriptableObjectsGlobalNameVariables = new List<GlobalNameVariables>();
        internal List<GlobalNameVariables> listScriptableObjectsGlobalNameVariablesFiltered = new List<GlobalNameVariables>();
        internal List<MaterialSoundsAsset> listScriptableObjectsMaterialSoundsAssets = new List<MaterialSoundsAsset>();
        internal List<MaterialSoundsAsset> listScriptableObjectsMaterialSoundsAssetsFiltered = new List<MaterialSoundsAsset>();
        internal List<StateAnimation> listScriptableObjectsStateAnimations = new List<StateAnimation>();
        internal List<StateAnimation> listScriptableObjectsStateAnimationsFiltered = new List<StateAnimation>();
        internal List<StateBasicLocomotion> listScriptableObjectsStateBasicLocomotions = new List<StateBasicLocomotion>();
        internal List<StateBasicLocomotion> listScriptableObjectsStateBasicLocomotionsFiltered = new List<StateBasicLocomotion>();
        internal List<StateCompleteLocomotion> listScriptableObjectsStateCompleteLocomotions = new List<StateCompleteLocomotion>();
        internal List<StateCompleteLocomotion> listScriptableObjectsStateCompleteLocomotionsFiltered = new List<StateCompleteLocomotion>();

        protected ListView listViewComponentsCharacters;
        protected ListView listViewComponentsHotspots;
        protected ListView listViewComponentsLocalListVariables;
        protected ListView listViewComponentsLocalNameVariables;
        protected ListView listViewComponentsMainCameras;
        protected ListView listViewComponentsMarkers;
        protected ListView listViewComponentsRemembers;
        protected ListView listViewComponentsShotCameras;

        protected ListView listViewScriptableObjectsExampleInstallers;
        protected ListView listViewScriptableObjectsGlobalListVariables;
        protected ListView listViewScriptableObjectsGlobalNameVariables;
        protected ListView listViewScriptableObjectsMaterialSoundsAssets;
        protected ListView listViewScriptableObjectsStateAnimations;
        protected ListView listViewScriptableObjectsStateBasicLocomotions;
        protected ListView listViewScriptableObjectsStateCompleteLocomotions;

        protected Foldout foldoutComponentsCharacters = new Foldout() { text = "Characters", value = false };
        protected Foldout foldoutComponentsHotspots = new Foldout() { text = "Hotspots", value = false };
        protected Foldout foldoutComponentsLocalListVariables = new Foldout() { text = "Local List Variables", value = false };
        protected Foldout foldoutComponentsLocalNameVariables = new Foldout() { text = "Local Name Variables", value = false };
        protected Foldout foldoutComponentsMainCameras = new Foldout() { text = "Main Cameras", value = false };
        protected Foldout foldoutComponentsMarkers = new Foldout() { text = "Markers", value = false };
        protected Foldout foldoutComponentsRemembers = new Foldout() { text = "Remembers", value = false };
        protected Foldout foldoutComponentsShotCameras = new Foldout() { text = "Shot Cameras", value = false };

        protected Foldout foldoutScriptableObjectsExampleInstallers = new Foldout() { text = "Installers", value = false };
        protected Foldout foldoutScriptableObjectsGlobalListVariables = new Foldout() { text = "Global List Variables", value = false };
        protected Foldout foldoutScriptableObjectsGlobalNameVariables = new Foldout() { text = "Global Name Variables", value = false };
        protected Foldout foldoutScriptableObjectsMaterialSoundsAssets = new Foldout() { text = "Material Sounds", value = false };
        protected Foldout foldoutScriptableObjectsStateAnimations = new Foldout() { text = "Animation States", value = false };
        protected Foldout foldoutScriptableObjectsStateBasicLocomotions = new Foldout() { text = "Basic Locomotion States", value = false };
        protected Foldout foldoutScriptableObjectsStateCompleteLocomotions = new Foldout() { text = "Complete Locomotion States", value = false };

        public GameCreatorFinder2WindowContentListCore(GameCreatorFinder2WindowCore window) : base(window)
        {
            this.window = window;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            this.listViewComponentsCharacters.selectionChanged -= OnContentSelectItemCharacter;
            this.listViewComponentsHotspots.selectionChanged -= OnContentSelectItemHotspot;
            this.listViewComponentsLocalListVariables.selectionChanged -= OnContentSelectItemLocalListVariable;
            this.listViewComponentsLocalNameVariables.selectionChanged -= OnContentSelectItemLocalNameVariable;
            this.listViewComponentsMainCameras.selectionChanged -= OnContentSelectItemMainCamera;
            this.listViewComponentsMarkers.selectionChanged -= OnContentSelectItemMarker;
            this.listViewComponentsRemembers.selectionChanged -= OnContentSelectItemRemember;
            this.listViewComponentsShotCameras.selectionChanged -= OnContentSelectItemShotCamera;

            this.listViewScriptableObjectsExampleInstallers.selectionChanged -= OnContentSelectItemExampleInstaller;
            this.listViewScriptableObjectsGlobalListVariables.selectionChanged -= OnContentSelectItemGlobalListVariable;
            this.listViewScriptableObjectsGlobalNameVariables.selectionChanged -= OnContentSelectItemGlobalNameVariable;
            this.listViewScriptableObjectsMaterialSoundsAssets.selectionChanged -= OnContentSelectItemMaterialSoundsAsset;
            this.listViewScriptableObjectsStateAnimations.selectionChanged -= OnContentSelectItemStateAnimation;
            this.listViewScriptableObjectsStateBasicLocomotions.selectionChanged -= OnContentSelectItemStateBasicLocomotion;
            this.listViewScriptableObjectsStateCompleteLocomotions.selectionChanged -= OnContentSelectItemStateCompleteLocomotion;

            this.listViewComponentsCharacters.itemsChosen -= OnContentChooseItemCharacter;
            this.listViewComponentsHotspots.itemsChosen -= OnContentChooseItemHotspot;
            this.listViewComponentsLocalListVariables.itemsChosen -= OnContentChooseItemLocalListVariable;
            this.listViewComponentsLocalNameVariables.itemsChosen -= OnContentChooseItemLocalNameVariable;
            this.listViewComponentsMainCameras.itemsChosen -= OnContentChooseItemMainCamera;
            this.listViewComponentsMarkers.itemsChosen -= OnContentChooseItemMarker;
            this.listViewComponentsRemembers.itemsChosen -= OnContentChooseItemRemember;
            this.listViewComponentsShotCameras.itemsChosen -= OnContentChooseItemShotCamera;

            this.listViewScriptableObjectsExampleInstallers.itemsChosen -= OnContentChooseItemExampleInstaller;
            this.listViewScriptableObjectsGlobalListVariables.itemsChosen -= OnContentChooseItemGlobalListVariable;
            this.listViewScriptableObjectsGlobalNameVariables.itemsChosen -= OnContentChooseItemGlobalNameVariable;
            this.listViewScriptableObjectsMaterialSoundsAssets.itemsChosen -= OnContentChooseItemMaterialSoundsAsset;
            this.listViewScriptableObjectsStateAnimations.itemsChosen -= OnContentChooseItemStateAnimation;
            this.listViewScriptableObjectsStateBasicLocomotions.itemsChosen -= OnContentChooseItemStateBasicLocomotion;
            this.listViewScriptableObjectsStateCompleteLocomotions.itemsChosen -= OnContentChooseItemStateCompleteLocomotion;
        }

        protected override void SetupListComponents()
        {
            this.InitializeListComponents();

            this.SetupListComponentsCharacters();
            this.SetupListComponentsHotspots();
            this.SetupListComponentsLocalListVariables();
            this.SetupListComponentsLocalNameVariables();
            this.SetupListComponentsMainCameras();
            this.SetupListComponentsMarkers();
            this.SetupListComponentsRemembers();
            this.SetupListComponentsShotCameras();
        }

        protected virtual void SetupListComponentsCharacters()
        {
            this.listViewComponentsCharacters = new ListView(this.listComponentsCharactersFiltered, 24, this.MakeItem, this.BindItemCharacter)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsCharacters.selectionChanged += OnContentSelectItemCharacter;
            this.listViewComponentsCharacters.itemsChosen += OnContentChooseItemCharacter;

            this.foldoutComponentsCharacters.Add(this.listViewComponentsCharacters);
        }

        protected virtual void SetupListComponentsHotspots()
        {
            this.listViewComponentsHotspots = new ListView(this.listComponentsHotspotsFiltered, 24, this.MakeItem, this.BindItemHotspot)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsHotspots.selectionChanged += OnContentSelectItemHotspot;
            this.listViewComponentsHotspots.itemsChosen += OnContentChooseItemHotspot;

            this.foldoutComponentsHotspots.Add(this.listViewComponentsHotspots);
        }

        protected virtual void SetupListComponentsLocalListVariables()
        {
            this.listViewComponentsLocalListVariables = new ListView(this.listComponentsLocalListVariablesFiltered, 24, this.MakeItem, this.BindItemLocalListVariable)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsLocalListVariables.selectionChanged += OnContentSelectItemLocalListVariable;
            this.listViewComponentsLocalListVariables.itemsChosen += OnContentChooseItemLocalListVariable;

            this.foldoutComponentsLocalListVariables.Add(this.listViewComponentsLocalListVariables);
        }

        protected virtual void SetupListComponentsLocalNameVariables()
        {
            this.listViewComponentsLocalNameVariables = new ListView(this.listComponentsLocalNameVariablesFiltered, 24, this.MakeItem, this.BindItemLocalNameVariable)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsLocalNameVariables.selectionChanged += OnContentSelectItemLocalNameVariable;
            this.listViewComponentsLocalNameVariables.itemsChosen += OnContentChooseItemLocalNameVariable;

            this.foldoutComponentsLocalNameVariables.Add(this.listViewComponentsLocalNameVariables);
        }

        protected virtual void SetupListComponentsMainCameras()
        {
            this.listViewComponentsMainCameras = new ListView(this.listComponentsMainCamerasFiltered, 24, this.MakeItem, this.BindItemMainCamera)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsMainCameras.selectionChanged += OnContentSelectItemMainCamera;
            this.listViewComponentsMainCameras.itemsChosen += OnContentChooseItemMainCamera;

            this.foldoutComponentsMainCameras.Add(this.listViewComponentsMainCameras);
        }

        protected virtual void SetupListComponentsMarkers()
        {
            this.listViewComponentsMarkers = new ListView(this.listComponentsMarkersFiltered, 24, this.MakeItem, this.BindItemMarker)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsMarkers.selectionChanged += OnContentSelectItemMarker;
            this.listViewComponentsMarkers.itemsChosen += OnContentChooseItemMarker;

            this.foldoutComponentsMarkers.Add(this.listViewComponentsMarkers);
        }

        protected virtual void SetupListComponentsRemembers()
        {
            this.listViewComponentsRemembers = new ListView(this.listComponentsRemembersFiltered, 24, this.MakeItem, this.BindItemRemember)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsRemembers.selectionChanged += OnContentSelectItemRemember;
            this.listViewComponentsRemembers.itemsChosen += OnContentChooseItemRemember;

            this.foldoutComponentsRemembers.Add(this.listViewComponentsRemembers);
        }

        protected virtual void SetupListComponentsShotCameras()
        {
            this.listViewComponentsShotCameras = new ListView(this.listComponentsShotCamerasFiltered, 24, this.MakeItem, this.BindItemShotCamera)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsShotCameras.selectionChanged += OnContentSelectItemShotCamera;
            this.listViewComponentsShotCameras.itemsChosen += OnContentChooseItemShotCamera;

            this.foldoutComponentsShotCameras.Add(this.listViewComponentsShotCameras);
        }

        protected override void SetupListScriptableObjects()
        {
            this.InitializeListScriptableObjects();

            this.SetupListScriptableObjectsInstallers();
            this.SetupListScriptableObjectsGlobalListVariables();
            this.SetupListScriptableObjectsGlobalNameVariables();
            this.SetupListScriptableObjectsMaterialSoundsAssets();
            this.SetupListScriptableObjectsStateAnimations();
            this.SetupListScriptableObjectsStateBasicLocomotions();
            this.SetupListScriptableObjectsStateCompleteLocomotions();
        }

        protected virtual void SetupListScriptableObjectsInstallers()
        {
            this.listViewScriptableObjectsExampleInstallers = new ListView(this.listScriptableObjectsInstallersFiltered, 24, this.MakeItem, this.BindItemInstaller)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsExampleInstallers.selectionChanged += OnContentSelectItemExampleInstaller;
            this.listViewScriptableObjectsExampleInstallers.itemsChosen += OnContentChooseItemExampleInstaller;

            this.foldoutScriptableObjectsExampleInstallers.Add(this.listViewScriptableObjectsExampleInstallers);
        }

        protected virtual void SetupListScriptableObjectsGlobalListVariables()
        {
            this.listViewScriptableObjectsGlobalListVariables = new ListView(this.listScriptableObjectsGlobalListVariablesFiltered, 24, this.MakeItem, this.BindItemGlobalListVariable)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsGlobalListVariables.selectionChanged += OnContentSelectItemGlobalListVariable;
            this.listViewScriptableObjectsGlobalListVariables.itemsChosen += OnContentChooseItemGlobalListVariable;

            this.foldoutScriptableObjectsGlobalListVariables.Add(this.listViewScriptableObjectsGlobalListVariables);
        }

        protected virtual void SetupListScriptableObjectsGlobalNameVariables()
        {
            this.listViewScriptableObjectsGlobalNameVariables = new ListView(this.listScriptableObjectsGlobalNameVariablesFiltered, 24, this.MakeItem, this.BindItemGlobalNameVariable)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsGlobalNameVariables.selectionChanged += OnContentSelectItemGlobalNameVariable;
            this.listViewScriptableObjectsGlobalNameVariables.itemsChosen += OnContentChooseItemGlobalNameVariable;

            this.foldoutScriptableObjectsGlobalNameVariables.Add(this.listViewScriptableObjectsGlobalNameVariables);
        }

        protected virtual void SetupListScriptableObjectsMaterialSoundsAssets()
        {
            this.listViewScriptableObjectsMaterialSoundsAssets = new ListView(this.listScriptableObjectsMaterialSoundsAssetsFiltered, 24, this.MakeItem, this.BindItemMaterialSoundsAsset)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsMaterialSoundsAssets.selectionChanged += OnContentSelectItemMaterialSoundsAsset;
            this.listViewScriptableObjectsMaterialSoundsAssets.itemsChosen += OnContentChooseItemMaterialSoundsAsset;

            this.foldoutScriptableObjectsMaterialSoundsAssets.Add(this.listViewScriptableObjectsMaterialSoundsAssets);
        }

        protected virtual void SetupListScriptableObjectsStateAnimations()
        {
            this.listViewScriptableObjectsStateAnimations = new ListView(this.listScriptableObjectsStateAnimationsFiltered, 24, this.MakeItem, this.BindItemStateAnimation)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsStateAnimations.selectionChanged += OnContentSelectItemStateAnimation;
            this.listViewScriptableObjectsStateAnimations.itemsChosen += OnContentChooseItemStateAnimation;

            this.foldoutScriptableObjectsStateAnimations.Add(this.listViewScriptableObjectsStateAnimations);
        }

        protected virtual void SetupListScriptableObjectsStateBasicLocomotions()
        {
            this.listViewScriptableObjectsStateBasicLocomotions = new ListView(this.listScriptableObjectsStateBasicLocomotionsFiltered, 24, this.MakeItem, this.BindItemStateBasicLocomotion)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsStateBasicLocomotions.selectionChanged += OnContentSelectItemStateBasicLocomotion;
            this.listViewScriptableObjectsStateBasicLocomotions.itemsChosen += OnContentChooseItemStateBasicLocomotion;

            this.foldoutScriptableObjectsStateBasicLocomotions.Add(this.listViewScriptableObjectsStateBasicLocomotions);
        }

        protected virtual void SetupListScriptableObjectsStateCompleteLocomotions()
        {
            this.listViewScriptableObjectsStateCompleteLocomotions = new ListView(this.listScriptableObjectsStateCompleteLocomotionsFiltered, 24, this.MakeItem, this.BindItemStateCompleteLocomotion)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsStateCompleteLocomotions.selectionChanged += OnContentSelectItemStateCompleteLocomotion;
            this.listViewScriptableObjectsStateCompleteLocomotions.itemsChosen += OnContentChooseItemStateCompleteLocomotion;

            this.foldoutScriptableObjectsStateCompleteLocomotions.Add(this.listViewScriptableObjectsStateCompleteLocomotions);
        }

        protected virtual void BindItemCharacter(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsCharactersFiltered[index].GetType().Name switch
            {
                "Character" => ICON_CHARACTER,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsCharactersFiltered[index].name} (ID {this.listComponentsCharactersFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemHotspot(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsHotspotsFiltered[index].GetType().Name switch
            {
                "Hotspot" => ICON_HOTSPOT,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsHotspotsFiltered[index].name} (ID {this.listComponentsHotspotsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemLocalListVariable(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsLocalListVariablesFiltered[index].GetType().Name switch
            {
                "LocalListVariables" => ICON_LISTVARIABLES,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsLocalListVariablesFiltered[index].name} (ID {this.listComponentsLocalListVariablesFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemLocalNameVariable(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsLocalNameVariablesFiltered[index].GetType().Name switch
            {
                "LocalNameVariables" => ICON_NAMEVARIABLES,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsLocalNameVariablesFiltered[index].name} (ID {this.listComponentsLocalNameVariablesFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemMainCamera(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsMainCamerasFiltered[index].GetType().Name switch
            {
                "MainCamera" => ICON_MAINCAMERA,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsMainCamerasFiltered[index].name} (ID {this.listComponentsMainCamerasFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemMarker(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsMarkersFiltered[index].GetType().Name switch
            {
                "Marker" => ICON_MARKER,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsMarkersFiltered[index].name} (ID {this.listComponentsMarkersFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemRemember(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsRemembersFiltered[index].GetType().Name switch
            {
                "Remember" => ICON_SAVE,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsRemembersFiltered[index].name} (ID {this.listComponentsRemembersFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemShotCamera(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsShotCamerasFiltered[index].GetType().Name switch
            {
                "ShotCamera" => ICON_SHOTCAMERA,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsShotCamerasFiltered[index].name} (ID {this.listComponentsShotCamerasFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemInstaller(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsInstallersFiltered[index].GetType().Name switch
            {
                "Installer" => ICON_INSTALLER,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsInstallersFiltered[index].name} (ID {this.listScriptableObjectsInstallersFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemGlobalListVariable(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsGlobalListVariablesFiltered[index].GetType().Name switch
            {
                "GlobalListVariables" => ICON_LISTVARIABLES,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsGlobalListVariablesFiltered[index].name} (ID {this.listScriptableObjectsGlobalListVariablesFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemGlobalNameVariable(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsGlobalNameVariablesFiltered[index].GetType().Name switch
            {
                "GlobalNameVariables" => ICON_NAMEVARIABLES,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsGlobalNameVariablesFiltered[index].name} (ID {this.listScriptableObjectsGlobalNameVariablesFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemMaterialSoundsAsset(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsMaterialSoundsAssetsFiltered[index].GetType().Name switch
            {
                "MaterialSoundsAsset" => ICON_MUSICNOTE,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsMaterialSoundsAssetsFiltered[index].name} (ID {this.listScriptableObjectsMaterialSoundsAssetsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStateAnimation(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsStateAnimationsFiltered[index].GetType().Name switch
            {
                "StateAnimation" => ICON_CHARACTERSTATE,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsStateAnimationsFiltered[index].name} (ID {this.listScriptableObjectsStateAnimationsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStateBasicLocomotion(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsStateBasicLocomotionsFiltered[index].GetType().Name switch
            {
                "StateBasicLocomotion" => ICON_CHARACTERRUN,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsStateBasicLocomotionsFiltered[index].name} (ID {this.listScriptableObjectsStateBasicLocomotionsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStateCompleteLocomotion(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsStateCompleteLocomotionsFiltered[index].GetType().Name switch
            {
                "StateCompleteLocomotion" => ICON_CHARACTERRUN,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsStateCompleteLocomotionsFiltered[index].name} (ID {this.listScriptableObjectsStateCompleteLocomotionsFiltered[index].GetInstanceID()})";
        }
        
        public override void InitializeListComponents()
        {
            this.listComponentsCharacters.AddRange(GameCreatorFinder2WindowCore.CHARACTER_FOUND);
            this.listComponentsHotspots.AddRange(GameCreatorFinder2WindowCore.HOTSPOT_FOUND);
            this.listComponentsLocalListVariables.AddRange(GameCreatorFinder2WindowCore.LOCALLISTVARIABLES_FOUND);
            this.listComponentsLocalNameVariables.AddRange(GameCreatorFinder2WindowCore.LOCALNAMEVARIABLES_FOUND);
            this.listComponentstMainCameras.AddRange(GameCreatorFinder2WindowCore.MAINCAMERA_FOUND);
            this.listComponentsMarkers.AddRange(GameCreatorFinder2WindowCore.MARKER_FOUND);
            this.listComponentsRemembers.AddRange(GameCreatorFinder2WindowCore.REMEMBER_FOUND);
            this.listComponentsShotCameras.AddRange(GameCreatorFinder2WindowCore.SHOTCAMERA_FOUND);
        }

        public override void InitializeListScriptableObjects()
        {
            this.listScriptableObjectsInstallers.AddRange(GameCreatorFinder2WindowCore.INSTALLERS_FOUND);
            this.listScriptableObjectsGlobalListVariables.AddRange(GameCreatorFinder2WindowCore.GLOBALLISTVARIABLES_FOUND);
            this.listScriptableObjectsGlobalNameVariables.AddRange(GameCreatorFinder2WindowCore.GLOBALNAMEVARIABLES_FOUND);
            this.listScriptableObjectsMaterialSoundsAssets.AddRange(GameCreatorFinder2WindowCore.MATERIALSOUNDSASSETS_FOUND);
            this.listScriptableObjectsStateAnimations.AddRange(GameCreatorFinder2WindowCore.STATEANIMATIONS_FOUND);
            this.listScriptableObjectsStateBasicLocomotions.AddRange(GameCreatorFinder2WindowCore.STATEBASICLOCOMOTIONS_FOUND);
            this.listScriptableObjectsStateCompleteLocomotions.AddRange(GameCreatorFinder2WindowCore.STATECOMPLETELOCOMOTIONS_FOUND);
        }

        public override void RemoveListComponents()
        {
            this.listComponentsCharacters = new List<Character>();
            this.listComponentsHotspots = new List<Hotspot>();
            this.listComponentsLocalListVariables = new List<LocalListVariables>();
            this.listComponentsLocalNameVariables = new List<LocalNameVariables>();
            this.listComponentstMainCameras = new List<MainCamera>();
            this.listComponentsMarkers = new List<Marker>();
            this.listComponentsRemembers = new List<Remember>();
            this.listComponentsShotCameras = new List<ShotCamera>();
        }

        protected override void RemoveListScriptableObjects()
        {
            this.listScriptableObjectsInstallers = new List<Installer>();
            this.listScriptableObjectsGlobalListVariables = new List<GlobalListVariables>();
            this.listScriptableObjectsGlobalNameVariables = new List<GlobalNameVariables>();
            this.listScriptableObjectsMaterialSoundsAssets = new List<MaterialSoundsAsset>();
            this.listScriptableObjectsStateAnimations = new List<StateAnimation>();
            this.listScriptableObjectsStateBasicLocomotions = new List<StateBasicLocomotion>();
            this.listScriptableObjectsStateCompleteLocomotions = new List<StateCompleteLocomotion>();
        }

        protected override void RemoveListComponentsElements()
        {
            try { this.content.Remove(this.foldoutComponentsCharacters); } catch { }
            try { this.foldoutComponentsCharacters.Remove(this.listViewComponentsCharacters); } catch { }

            try { this.content.Remove(this.foldoutComponentsHotspots); } catch { }
            try { this.foldoutComponentsHotspots.Remove(this.listViewComponentsHotspots); } catch { }

            try { this.content.Remove(this.foldoutComponentsLocalListVariables); } catch { }
            try { this.foldoutComponentsLocalListVariables.Remove(this.listViewComponentsLocalListVariables); } catch { }

            try { this.content.Remove(this.foldoutComponentsLocalNameVariables); } catch { }
            try { this.foldoutComponentsLocalNameVariables.Remove(this.listViewComponentsLocalNameVariables); } catch { }

            try { this.content.Remove(this.foldoutComponentsMainCameras); } catch { }
            try { this.foldoutComponentsMainCameras.Remove(this.listViewComponentsMainCameras); } catch { }

            try { this.content.Remove(this.foldoutComponentsMarkers); } catch { }
            try { this.foldoutComponentsMarkers.Remove(this.listViewComponentsMarkers); } catch { }

            try { this.content.Remove(this.foldoutComponentsRemembers); } catch { }
            try { this.foldoutComponentsRemembers.Remove(this.listViewComponentsRemembers); } catch { }

            try { this.content.Remove(this.foldoutComponentsShotCameras); } catch { }
            try { this.foldoutComponentsShotCameras.Remove(this.listViewComponentsShotCameras); } catch { }
        }

        protected override void RemoveListScriptableObjectsElements()
        {
            try { this.content.Remove(this.foldoutScriptableObjectsExampleInstallers); } catch { }
            try { this.foldoutScriptableObjectsExampleInstallers.Remove(this.listViewScriptableObjectsExampleInstallers); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsGlobalListVariables); } catch { }
            try { this.foldoutScriptableObjectsGlobalListVariables.Remove(this.listViewScriptableObjectsGlobalListVariables); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsGlobalNameVariables); } catch { }
            try { this.foldoutScriptableObjectsGlobalNameVariables.Remove(this.listViewScriptableObjectsGlobalNameVariables); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsMaterialSoundsAssets); } catch { }
            try { this.foldoutScriptableObjectsMaterialSoundsAssets.Remove(this.listViewScriptableObjectsMaterialSoundsAssets); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsStateAnimations); } catch { }
            try { this.foldoutScriptableObjectsStateAnimations.Remove(this.listViewScriptableObjectsStateAnimations); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsStateBasicLocomotions); } catch { }
            try { this.foldoutScriptableObjectsStateBasicLocomotions.Remove(this.listViewScriptableObjectsStateBasicLocomotions); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsStateCompleteLocomotions); } catch { }
            try { this.foldoutScriptableObjectsStateCompleteLocomotions.Remove(this.listViewScriptableObjectsStateCompleteLocomotions); } catch { }
        }

        public override void UpdateListComponents()
        {
            this.RemoveListElements();

            this.UpdateListComponentsCharacters();
            this.UpdateListComponentsHotspots();
            this.UpdateListComponentsLocalListVariables();
            this.UpdateListComponentsLocalNameVariables();
            this.UpdateListComponentsMainCameras();
            this.UpdateListComponentsMarkers();
            this.UpdateListComponentsRemembers();
            this.UpdateListComponentsShotCameras();
        }

        protected virtual void UpdateListComponentsCharacters()
        {
            this.listComponentsCharacters = this.sortComponentsAZ ? this.listComponentsCharacters.OrderBy(go => go.name).ToList() : this.listComponentsCharacters.OrderByDescending(go => go.name).ToList();
            this.listComponentsCharactersFiltered.Clear();

            List<Character> listCharactersFoundByName = this.listComponentsCharacters.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listComponentsCharactersFiltered.AddRange(listCharactersFoundByName);

            if (GameCreatorFinder2WindowPreferencesCore.ComponentsCharactersIKRigLayers)
            {
                List<Character> listCharactersFoundByIKRigLayers = new List<Character>();

                for (int i = 0; i < this.listComponentsCharacters.Count; i++)
                {
                    List<bool> characterIKRigLayers = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listComponentsCharacters[i]);
                    var list = so.FindProperty("m_InverseKinematics").FindPropertyRelative("m_RigLayers").FindPropertyRelative("m_Rigs");
                    TRig[] rigList = list.GetValue<TRig[]>();

                    for (int j = 0; j < rigList.Length; j++)
                    {
                        characterIKRigLayers.Add(rigList[j].Title.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (characterIKRigLayers.Contains(true)) listCharactersFoundByIKRigLayers.Add(this.listComponentsCharacters[i]);
                }

                this.listComponentsCharactersFiltered.AddRange(listCharactersFoundByIKRigLayers);
            }

            if (GameCreatorFinder2WindowPreferencesCore.ComponentsCharactersFootsteps)
            {
                List<Character> listCharactersFoundByFootsteps = new List<Character>();

                for (int i = 0; i < this.listComponentsCharacters.Count; i++)
                {
                    List<bool> characterFootsteps = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listComponentsCharacters[i]);
                    var list = so.FindProperty("m_Footsteps").FindPropertyRelative("m_Feet");
                    Bone[] feetList = list.GetValue<Bone[]>();

                    for (int j = 0; j < feetList.Length; j++)
                    {
                        characterFootsteps.Add(feetList[j].ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (characterFootsteps.Contains(true)) listCharactersFoundByFootsteps.Add(this.listComponentsCharacters[i]);
                }

                this.listComponentsCharactersFiltered.AddRange(listCharactersFoundByFootsteps);
            }

            this.listComponentsCharactersFiltered = this.listComponentsCharactersFiltered.Distinct().ToList();
            this.listComponentsCharactersFiltered = this.sortComponentsAZ ? this.listComponentsCharactersFiltered.OrderBy(go => go.name).ToList() : this.listComponentsCharactersFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listComponentsCharactersFiltered.Count > 0)
            {
                this.listViewComponentsCharacters.itemsSource = this.listComponentsCharactersFiltered;
                this.listViewComponentsCharacters.Rebuild();

                this.foldoutComponentsCharacters.Add(this.listViewComponentsCharacters);

                this.content.Add(this.foldoutComponentsCharacters);
            }
        }

        protected virtual void UpdateListComponentsHotspots()
        {
            this.listComponentsHotspots = this.sortComponentsAZ ? this.listComponentsHotspots.OrderBy(go => go.name).ToList() : this.listComponentsHotspots.OrderByDescending(go => go.name).ToList();
            this.listComponentsHotspotsFiltered.Clear();

            List<Hotspot> listHotspotsFoundByName = this.listComponentsHotspots.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listComponentsHotspotsFiltered.AddRange(listHotspotsFoundByName);

            if (GameCreatorFinder2WindowPreferencesCore.ComponentsHotspotsSpots)
            {
                List<Hotspot> listHotspotsFoundBySpots = new List<Hotspot>();

                for (int i = 0; i < this.listComponentsHotspots.Count; i++)
                {
                    List<bool> hotspotsSpots = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listComponentsHotspots[i]);
                    var list = so.FindProperty("m_Spots").FindPropertyRelative("m_Spots");
                    Spot[] spotList = list.GetValue<Spot[]>();

                    for (int j = 0; j < spotList.Length; j++)
                    {
                        hotspotsSpots.Add(spotList[j].Title.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (hotspotsSpots.Contains(true)) listHotspotsFoundBySpots.Add(this.listComponentsHotspots[i]);
                }

                this.listComponentsHotspotsFiltered.AddRange(listHotspotsFoundBySpots);
            }

            this.listComponentsHotspotsFiltered = this.listComponentsHotspotsFiltered.Distinct().ToList();
            this.listComponentsHotspotsFiltered = this.sortComponentsAZ ? this.listComponentsHotspotsFiltered.OrderBy(go => go.name).ToList() : this.listComponentsHotspotsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listComponentsHotspotsFiltered.Count > 0)
            {
                this.listViewComponentsHotspots.itemsSource = this.listComponentsHotspotsFiltered;
                this.listViewComponentsHotspots.Rebuild();

                this.foldoutComponentsHotspots.Add(this.listViewComponentsHotspots);

                this.content.Add(this.foldoutComponentsHotspots);
            }
        }

        protected virtual void UpdateListComponentsLocalListVariables()
        {
            this.listComponentsLocalListVariables = this.sortComponentsAZ ? this.listComponentsLocalListVariables.OrderBy(go => go.name).ToList() : this.listComponentsLocalListVariables.OrderByDescending(go => go.name).ToList();

            this.listComponentsLocalListVariablesFiltered = this.listComponentsLocalListVariables.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsLocalListVariablesFiltered.Count > 0)
            {
                this.listViewComponentsLocalListVariables.itemsSource = this.listComponentsLocalListVariablesFiltered;
                this.listViewComponentsLocalListVariables.Rebuild();

                this.foldoutComponentsLocalListVariables.Add(this.listViewComponentsLocalListVariables);

                this.content.Add(this.foldoutComponentsLocalListVariables);
            }
        }

        protected virtual void UpdateListComponentsLocalNameVariables()
        {
            this.listComponentsLocalNameVariables = this.sortComponentsAZ ? this.listComponentsLocalNameVariables.OrderBy(go => go.name).ToList() : this.listComponentsLocalNameVariables.OrderByDescending(go => go.name).ToList();
            this.listComponentsLocalNameVariablesFiltered.Clear();

            List<LocalNameVariables> listLocalNameVariablesFoundByName = this.listComponentsLocalNameVariables.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listComponentsLocalNameVariablesFiltered.AddRange(listLocalNameVariablesFoundByName);

            if (GameCreatorFinder2WindowPreferencesCore.ComponentsLocalNameVariables)
            {
                List<LocalNameVariables> listLocalNameVariablesFoundByVariableNames = new List<LocalNameVariables>();

                for (int i = 0; i < this.listComponentsLocalNameVariables.Count; i++)
                {
                    List<bool> localNameVariables = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listComponentsLocalNameVariables[i]);
                    var list = so.FindProperty("m_Runtime").FindPropertyRelative("m_List");
                    NameList localNameVariablesList = list.GetValue<NameList>();

                    for (int j = 0; j < localNameVariablesList.Names.Length; j++)
                    {
                        localNameVariables.Add(localNameVariablesList.Names[j].ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (localNameVariables.Contains(true)) listLocalNameVariablesFoundByVariableNames.Add(this.listComponentsLocalNameVariables[i]);
                }

                this.listComponentsLocalNameVariablesFiltered.AddRange(listLocalNameVariablesFoundByVariableNames);
            }

            this.listComponentsLocalNameVariablesFiltered = this.listComponentsLocalNameVariablesFiltered.Distinct().ToList();
            this.listComponentsLocalNameVariablesFiltered = this.sortComponentsAZ ? this.listComponentsLocalNameVariablesFiltered.OrderBy(go => go.name).ToList() : this.listComponentsLocalNameVariablesFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listComponentsLocalNameVariablesFiltered.Count > 0)
            {
                this.listViewComponentsLocalNameVariables.itemsSource = this.listComponentsLocalNameVariablesFiltered;
                this.listViewComponentsLocalNameVariables.Rebuild();

                this.foldoutComponentsLocalNameVariables.Add(this.listViewComponentsLocalNameVariables);

                this.content.Add(this.foldoutComponentsLocalNameVariables);
            }
        }

        protected virtual void UpdateListComponentsMainCameras()
        {
            this.listComponentstMainCameras = this.sortComponentsAZ ? this.listComponentstMainCameras.OrderBy(go => go.name).ToList() : this.listComponentstMainCameras.OrderByDescending(go => go.name).ToList();

            this.listComponentsMainCamerasFiltered = this.listComponentstMainCameras.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsMainCamerasFiltered.Count > 0)
            {
                this.listViewComponentsMainCameras.itemsSource = this.listComponentsMainCamerasFiltered;
                this.listViewComponentsMainCameras.Rebuild();

                this.foldoutComponentsMainCameras.Add(this.listViewComponentsMainCameras);

                this.content.Add(this.foldoutComponentsMainCameras);
            }
        }

        protected virtual void UpdateListComponentsMarkers()
        {
            this.listComponentsMarkers = this.sortComponentsAZ ? this.listComponentsMarkers.OrderBy(go => go.name).ToList() : this.listComponentsMarkers.OrderByDescending(go => go.name).ToList();

            this.listComponentsMarkersFiltered = this.listComponentsMarkers.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsMarkersFiltered.Count > 0)
            {
                this.listViewComponentsMarkers.itemsSource = this.listComponentsMarkersFiltered;
                this.listViewComponentsMarkers.Rebuild();

                this.foldoutComponentsMarkers.Add(this.listViewComponentsMarkers);

                this.content.Add(this.foldoutComponentsMarkers);
            }
        }

        protected virtual void UpdateListComponentsRemembers()
        {
            this.listComponentsRemembers = this.sortComponentsAZ ? this.listComponentsRemembers.OrderBy(go => go.name).ToList() : this.listComponentsRemembers.OrderByDescending(go => go.name).ToList();

            this.listComponentsRemembersFiltered = this.listComponentsRemembers.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsRemembersFiltered.Count > 0)
            {
                this.listViewComponentsRemembers.itemsSource = this.listComponentsRemembersFiltered;
                this.listViewComponentsRemembers.Rebuild();

                this.foldoutComponentsRemembers.Add(this.listViewComponentsRemembers);

                this.content.Add(this.foldoutComponentsRemembers);
            }
        }

        protected virtual void UpdateListComponentsShotCameras()
        {
            this.listComponentsShotCameras = this.sortComponentsAZ ? this.listComponentsShotCameras.OrderBy(go => go.name).ToList() : this.listComponentsShotCameras.OrderByDescending(go => go.name).ToList();

            this.listComponentsShotCamerasFiltered = this.listComponentsShotCameras.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsShotCamerasFiltered.Count > 0)
            {
                this.listViewComponentsShotCameras.itemsSource = this.listComponentsShotCamerasFiltered;
                this.listViewComponentsShotCameras.Rebuild();

                this.foldoutComponentsShotCameras.Add(this.listViewComponentsShotCameras);

                this.content.Add(this.foldoutComponentsShotCameras);
            }
        }

        public override void UpdateListScriptableObjects()
        {
            this.RemoveListElements();

            this.UpdateListScriptableObjectsInstallers();
            this.UpdateListScriptableObjectsGlobalListVariables();
            this.UpdateListScriptableObjectsGlobalNameVariables();
            this.UpdateListScriptableObjectsMaterialSoundsAssets();
            this.UpdateListScriptableObjectsStateAnimations();
            this.UpdateListScriptableObjectsStateBasicLocomotions();
            this.UpdateListScriptableObjectsStateComplexLocomotions();
        }

        protected virtual void UpdateListScriptableObjectsInstallers()
        {
            this.listScriptableObjectsInstallers = this.sortScriptableObjectsAZ ? this.listScriptableObjectsInstallers.OrderBy(go => go.name).ToList() : this.listScriptableObjectsInstallers.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsInstallersFiltered = this.listScriptableObjectsInstallers.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsInstallersFiltered.Count > 0)
            {
                this.listViewScriptableObjectsExampleInstallers.itemsSource = this.listScriptableObjectsInstallersFiltered;
                this.listViewScriptableObjectsExampleInstallers.Rebuild();

                this.foldoutScriptableObjectsExampleInstallers.Add(this.listViewScriptableObjectsExampleInstallers);

                this.content.Add(this.foldoutScriptableObjectsExampleInstallers);
            }
        }

        protected virtual void UpdateListScriptableObjectsGlobalListVariables()
        {
            this.listScriptableObjectsGlobalListVariables = this.sortScriptableObjectsAZ ? this.listScriptableObjectsGlobalListVariables.OrderBy(go => go.name).ToList() : this.listScriptableObjectsGlobalListVariables.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsGlobalListVariablesFiltered = this.listScriptableObjectsGlobalListVariables.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsGlobalListVariablesFiltered.Count > 0)
            {
                this.listViewScriptableObjectsGlobalListVariables.itemsSource = this.listScriptableObjectsGlobalListVariablesFiltered;
                this.listViewScriptableObjectsGlobalListVariables.Rebuild();

                this.foldoutScriptableObjectsGlobalListVariables.Add(this.listViewScriptableObjectsGlobalListVariables);

                this.content.Add(this.foldoutScriptableObjectsGlobalListVariables);
            }
        }

        protected virtual void UpdateListScriptableObjectsGlobalNameVariables()
        {
            this.listScriptableObjectsGlobalNameVariables = this.sortScriptableObjectsAZ ? this.listScriptableObjectsGlobalNameVariables.OrderBy(go => go.name).ToList() : this.listScriptableObjectsGlobalNameVariables.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsGlobalNameVariablesFiltered.Clear();

            List<GlobalNameVariables> listGlobalNameVariablesFoundByName = this.listScriptableObjectsGlobalNameVariables.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsGlobalNameVariablesFiltered.AddRange(listGlobalNameVariablesFoundByName);

            if (GameCreatorFinder2WindowPreferencesCore.ScriptableObjectsGlobalNameVariables)
            {
                List<GlobalNameVariables> listGlobalNameVariablesFoundByVariableNames = new List<GlobalNameVariables>();

                for (int i = 0; i < this.listScriptableObjectsGlobalNameVariables.Count; i++)
                {
                    List<bool> globalNameVariables = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listScriptableObjectsGlobalNameVariables[i]);
                    var list = so.FindProperty("m_NameList");
                    NameList globalNameVariablesList = list.GetValue<NameList>();

                    for (int j = 0; j < globalNameVariablesList.Names.Length; j++)
                    {
                        globalNameVariables.Add(globalNameVariablesList.Names[j].ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (globalNameVariables.Contains(true)) listGlobalNameVariablesFoundByVariableNames.Add(this.listScriptableObjectsGlobalNameVariables[i]);
                }

                this.listScriptableObjectsGlobalNameVariablesFiltered.AddRange(listGlobalNameVariablesFoundByVariableNames);
            }

            this.listScriptableObjectsGlobalNameVariablesFiltered = this.listScriptableObjectsGlobalNameVariablesFiltered.Distinct().ToList();
            this.listScriptableObjectsGlobalNameVariablesFiltered = this.sortComponentsAZ ? this.listScriptableObjectsGlobalNameVariablesFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsGlobalNameVariablesFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsGlobalNameVariablesFiltered.Count > 0)
            {
                this.listViewScriptableObjectsGlobalNameVariables.itemsSource = this.listScriptableObjectsGlobalNameVariablesFiltered;
                this.listViewScriptableObjectsGlobalNameVariables.Rebuild();

                this.foldoutScriptableObjectsGlobalNameVariables.Add(this.listViewScriptableObjectsGlobalNameVariables);

                this.content.Add(this.foldoutScriptableObjectsGlobalNameVariables);
            }
        }

        protected virtual void UpdateListScriptableObjectsMaterialSoundsAssets()
        {
            this.listScriptableObjectsMaterialSoundsAssets = this.sortScriptableObjectsAZ ? this.listScriptableObjectsMaterialSoundsAssets.OrderBy(go => go.name).ToList() : this.listScriptableObjectsMaterialSoundsAssets.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsMaterialSoundsAssetsFiltered = this.listScriptableObjectsMaterialSoundsAssets.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsMaterialSoundsAssetsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsMaterialSoundsAssets.itemsSource = this.listScriptableObjectsMaterialSoundsAssetsFiltered;
                this.listViewScriptableObjectsMaterialSoundsAssets.Rebuild();

                this.foldoutScriptableObjectsMaterialSoundsAssets.Add(this.listViewScriptableObjectsMaterialSoundsAssets);

                this.content.Add(this.foldoutScriptableObjectsMaterialSoundsAssets);
            }
        }

        protected virtual void UpdateListScriptableObjectsStateAnimations()
        {
            this.listScriptableObjectsStateAnimations = this.sortScriptableObjectsAZ ? this.listScriptableObjectsStateAnimations.OrderBy(go => go.name).ToList() : this.listScriptableObjectsStateAnimations.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsStateAnimationsFiltered = this.listScriptableObjectsStateAnimations.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsStateAnimationsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsStateAnimations.itemsSource = this.listScriptableObjectsStateAnimationsFiltered;
                this.listViewScriptableObjectsStateAnimations.Rebuild();

                this.foldoutScriptableObjectsStateAnimations.Add(this.listViewScriptableObjectsStateAnimations);

                this.content.Add(this.foldoutScriptableObjectsStateAnimations);
            }
        }

        protected virtual void UpdateListScriptableObjectsStateBasicLocomotions()
        {
            this.listScriptableObjectsStateBasicLocomotions = this.sortScriptableObjectsAZ ? this.listScriptableObjectsStateBasicLocomotions.OrderBy(go => go.name).ToList() : this.listScriptableObjectsStateBasicLocomotions.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsStateBasicLocomotionsFiltered = this.listScriptableObjectsStateBasicLocomotions.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsStateBasicLocomotionsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsStateBasicLocomotions.itemsSource = this.listScriptableObjectsStateBasicLocomotionsFiltered;
                this.listViewScriptableObjectsStateBasicLocomotions.Rebuild();

                this.foldoutScriptableObjectsStateBasicLocomotions.Add(this.listViewScriptableObjectsStateBasicLocomotions);

                this.content.Add(this.foldoutScriptableObjectsStateBasicLocomotions);
            }
        }

        protected virtual void UpdateListScriptableObjectsStateComplexLocomotions()
        {
            this.listScriptableObjectsStateCompleteLocomotions = this.sortScriptableObjectsAZ ? this.listScriptableObjectsStateCompleteLocomotions.OrderBy(go => go.name).ToList() : this.listScriptableObjectsStateCompleteLocomotions.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsStateCompleteLocomotionsFiltered = this.listScriptableObjectsStateCompleteLocomotions.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsStateCompleteLocomotionsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsStateCompleteLocomotions.itemsSource = this.listScriptableObjectsStateCompleteLocomotionsFiltered;
                this.listViewScriptableObjectsStateCompleteLocomotions.Rebuild();

                this.foldoutScriptableObjectsStateCompleteLocomotions.Add(this.listViewScriptableObjectsStateCompleteLocomotions);

                this.content.Add(this.foldoutScriptableObjectsStateCompleteLocomotions);
            }
        }

        protected override void ShowAllComponents()
        {
            this.foldoutComponentsCharacters.value = true;
            this.foldoutComponentsHotspots.value = true;
            this.foldoutComponentsLocalListVariables.value = true;
            this.foldoutComponentsLocalNameVariables.value = true;
            this.foldoutComponentsMainCameras.value = true;
            this.foldoutComponentsMarkers.value = true;
            this.foldoutComponentsRemembers.value = true;
            this.foldoutComponentsShotCameras.value = true;
        }

        protected override void HideAllComponents()
        {
            this.foldoutComponentsCharacters.value = false;
            this.foldoutComponentsHotspots.value = false;
            this.foldoutComponentsLocalListVariables.value = false;
            this.foldoutComponentsLocalNameVariables.value = false;
            this.foldoutComponentsMainCameras.value = false;
            this.foldoutComponentsMarkers.value = false;
            this.foldoutComponentsRemembers.value = false;
            this.foldoutComponentsShotCameras.value = false;
        }

        protected override void ShowAllScriptableObjects()
        {
            this.foldoutScriptableObjectsExampleInstallers.value = true;
            this.foldoutScriptableObjectsGlobalListVariables.value = true;
            this.foldoutScriptableObjectsGlobalNameVariables.value = true;
            this.foldoutScriptableObjectsMaterialSoundsAssets.value = true;
            this.foldoutScriptableObjectsStateAnimations.value = true;
            this.foldoutScriptableObjectsStateBasicLocomotions.value = true;
            this.foldoutScriptableObjectsStateCompleteLocomotions.value = true;
        }

        protected override void HideAllScriptableObjects()
        {
            this.foldoutScriptableObjectsExampleInstallers.value = false;
            this.foldoutScriptableObjectsGlobalListVariables.value = false;
            this.foldoutScriptableObjectsGlobalNameVariables.value = false;
            this.foldoutScriptableObjectsMaterialSoundsAssets.value = false;
            this.foldoutScriptableObjectsStateAnimations.value = false;
            this.foldoutScriptableObjectsStateBasicLocomotions.value = false;
            this.foldoutScriptableObjectsStateCompleteLocomotions.value = false;
        }

        protected virtual void OnContentSelectItemCharacter(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Character", this.listViewComponentsCharacters.selectedIndex);
        }

        protected virtual void OnContentSelectItemHotspot(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Hotspot", this.listViewComponentsHotspots.selectedIndex);
        }

        protected virtual void OnContentSelectItemLocalListVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Local List Variable", this.listViewComponentsLocalListVariables.selectedIndex);
        }

        protected virtual void OnContentSelectItemLocalNameVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Local Name Variable", this.listViewComponentsLocalNameVariables.selectedIndex);
        }

        protected virtual void OnContentSelectItemMainCamera(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "MainCamera", this.listViewComponentsMainCameras.selectedIndex);
        }

        protected virtual void OnContentSelectItemMarker(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Marker", this.listViewComponentsMarkers.selectedIndex);
        }

        protected virtual void OnContentSelectItemRemember(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Remember", this.listViewComponentsRemembers.selectedIndex);
        }

        protected virtual void OnContentSelectItemShotCamera(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "ShotCamera", this.listViewComponentsShotCameras.selectedIndex);
        }

        protected virtual void OnContentSelectItemExampleInstaller(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Installer", this.listViewScriptableObjectsExampleInstallers.selectedIndex);
        }

        protected virtual void OnContentSelectItemGlobalListVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Global List Variable", this.listViewScriptableObjectsGlobalListVariables.selectedIndex);
        }

        protected virtual void OnContentSelectItemGlobalNameVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Global Name Variable", this.listViewScriptableObjectsGlobalNameVariables.selectedIndex);
        }

        protected virtual void OnContentSelectItemMaterialSoundsAsset(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Material Sound", this.listViewScriptableObjectsMaterialSoundsAssets.selectedIndex);
        }

        protected virtual void OnContentSelectItemStateAnimation(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Animation State", this.listViewScriptableObjectsStateAnimations.selectedIndex);
        }

        protected virtual void OnContentSelectItemStateBasicLocomotion(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Basic Locomotion State", this.listViewScriptableObjectsStateBasicLocomotions.selectedIndex);
        }

        protected virtual void OnContentSelectItemStateCompleteLocomotion(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Complete Locomotion State", this.listViewScriptableObjectsStateCompleteLocomotions.selectedIndex);
        }

        protected virtual void OnContentChooseItemCharacter(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Character", this.listViewComponentsCharacters.selectedIndex);
        }

        protected virtual void OnContentChooseItemHotspot(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Hotspot", this.listViewComponentsHotspots.selectedIndex);
        }

        protected virtual void OnContentChooseItemLocalListVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Local List Variable", this.listViewComponentsLocalListVariables.selectedIndex);
        }

        protected virtual void OnContentChooseItemLocalNameVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Local Name Variable", this.listViewComponentsLocalNameVariables.selectedIndex);
        }

        protected virtual void OnContentChooseItemMainCamera(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "MainCamera", this.listViewComponentsMainCameras.selectedIndex);
        }

        protected virtual void OnContentChooseItemMarker(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Marker", this.listViewComponentsMarkers.selectedIndex);
        }

        protected virtual void OnContentChooseItemRemember(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Remember", this.listViewComponentsRemembers.selectedIndex);
        }

        protected virtual void OnContentChooseItemShotCamera(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "ShotCamera", this.listViewComponentsShotCameras.selectedIndex);
        }

        protected virtual void OnContentChooseItemExampleInstaller(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Example Installer", this.listViewScriptableObjectsExampleInstallers.selectedIndex);
        }

        protected virtual void OnContentChooseItemGlobalListVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Global List Variable", this.listViewScriptableObjectsGlobalListVariables.selectedIndex);
        }

        protected virtual void OnContentChooseItemGlobalNameVariable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Global Name Variable", this.listViewScriptableObjectsGlobalNameVariables.selectedIndex);
        }

        protected virtual void OnContentChooseItemMaterialSoundsAsset (IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Material Sound", this.listViewScriptableObjectsGlobalNameVariables.selectedIndex);
        }

        protected virtual void OnContentChooseItemStateAnimation(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Animation State", this.listViewScriptableObjectsStateAnimations.selectedIndex);
        }

        protected virtual void OnContentChooseItemStateBasicLocomotion(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Basic Locomotion State", this.listViewScriptableObjectsStateBasicLocomotions.selectedIndex);
        }

        protected virtual void OnContentChooseItemStateCompleteLocomotion(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Complete Locomotion State", this.listViewScriptableObjectsStateCompleteLocomotions.selectedIndex);
        }
    }
}