using GameCreator.Editor.Installs;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentDetailsCore : GameCreatorFinder2WindowContentDetails
    {
        private readonly GameCreatorFinder2WindowCore window;

        public GameCreatorFinder2WindowContentDetailsCore(GameCreatorFinder2WindowCore window) : base(window)
        {
            this.window = window;
        }

        protected override void ShowComponent(string foldout, int index)
        {
            switch (foldout)
            {
                case "Character":
                    this.ShowComponentCharacter(index);
                    break;
                case "Hotspot":
                    this.ShowComponentHotspot(index);
                    break;
                case "Local List Variable":
                    this.ShowComponentLocalListVariable(index);
                    break;
                case "Local Name Variable":
                    this.ShowComponentLocalNameVariable(index);
                    break;
                case "MainCamera":
                    this.ShowComponentMainCamera(index);
                    break;
                case "Marker":
                    this.ShowComponentMarker(index);
                    break;
                case "Remember":
                    this.ShowComponentRemember(index);
                    break;
                case "ShotCamera":
                    this.ShowComponentShotCamera(index);
                    break;
            }
        }

        protected virtual void ShowComponentCharacter(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement characterObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Character),
                value = windowContentList.listComponentsCharactersFiltered[index]
            };
            this.content.Add(characterObjectField);

            VisualElement characterInspector = new InspectorElement();
            characterInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsCharactersFiltered[index]));
            this.content.Add(characterInspector);
        }

        protected virtual void ShowComponentHotspot(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement hotspotObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Hotspot),
                value = windowContentList.listComponentsHotspotsFiltered[index]
            };
            this.content.Add(hotspotObjectField);

            VisualElement hotspotInspector = new InspectorElement();
            hotspotInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsHotspotsFiltered[index]));
            this.content.Add(hotspotInspector);
        }

        protected virtual void ShowComponentLocalListVariable(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement localListVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(LocalListVariables),
                value = windowContentList.listComponentsLocalListVariablesFiltered[index]
            };
            this.content.Add(localListVariableObjectField);

            VisualElement localListVariableInspector = new InspectorElement();
            localListVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsLocalListVariablesFiltered[index]));
            this.content.Add(localListVariableInspector);
        }

        protected virtual void ShowComponentLocalNameVariable(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement localNameVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(LocalNameVariables),
                value = windowContentList.listComponentsLocalNameVariablesFiltered[index]
            };
            this.content.Add(localNameVariableObjectField);

            VisualElement localNameVariableInspector = new InspectorElement();
            localNameVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsLocalNameVariablesFiltered[index]));
            this.content.Add(localNameVariableInspector);
        }

        protected virtual void ShowComponentMainCamera(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement mainCameraObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(MainCamera),
                value = windowContentList.listComponentsMainCamerasFiltered[index]
            };
            this.content.Add(mainCameraObjectField);

            VisualElement mainCameraInspector = new InspectorElement();
            mainCameraInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsMainCamerasFiltered[index]));
            this.content.Add(mainCameraInspector);
        }

        protected virtual void ShowComponentMarker(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement markerObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Marker),
                value = windowContentList.listComponentsMarkersFiltered[index]
            };
            this.content.Add(markerObjectField);

            VisualElement markerInspector = new InspectorElement();
            markerInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsMarkersFiltered[index]));
            this.content.Add(markerInspector);
        }

        protected virtual void ShowComponentRemember(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement rememberObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Remember),
                value = windowContentList.listComponentsRemembersFiltered[index]
            };
            this.content.Add(rememberObjectField);

            VisualElement rememberInspector = new InspectorElement();
            rememberInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsRemembersFiltered[index]));
            this.content.Add(rememberInspector);
        }

        protected virtual void ShowComponentShotCamera(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement shotCameraObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(ShotCamera),
                value = windowContentList.listComponentsShotCamerasFiltered[index]
            };
            this.content.Add(shotCameraObjectField);

            VisualElement shotCameraInspector = new InspectorElement();
            shotCameraInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listComponentsShotCamerasFiltered[index]));
            this.content.Add(shotCameraInspector);
        }

        protected override void ShowScriptableObject(string foldout, int index)
        {
            switch (foldout)
            {
                case "Installer":
                    this.ShowScriptableObjectExampleInstaller(index);
                    break;
                case "Global List Variable":
                    this.ShowScriptableObjectGlobalListVariable(index);
                    break;
                case "Global Name Variable":
                    this.ShowScriptableObjectGlobalNameVariable(index);
                    break;
                case "Material Sound":
                    this.ShowScriptableObjectMaterialSoundsAsset(index);
                    break;
                case "Animation State":
                    this.ShowScriptableObjectStateAnimation(index);
                    break;
                case "Basic Locomotion State":
                    this.ShowScriptableObjectStateBasicLocomotion(index);
                    break;
                case "Complete Locomotion State":
                    this.ShowScriptableObjectStateCompleteLocomotion(index);
                    break;
            }
        }

        protected virtual void ShowScriptableObjectExampleInstaller(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement installerObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Installer),
                value = windowContentList.listScriptableObjectsInstallersFiltered[index]
            };
            this.content.Add(installerObjectField);

            VisualElement installerInspector = new InspectorElement();
            installerInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listScriptableObjectsInstallersFiltered[index]));
            this.content.Add(installerInspector);
        }

        protected virtual void ShowScriptableObjectGlobalListVariable(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement globalListVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(GlobalListVariables),
                value = windowContentList.listScriptableObjectsGlobalListVariablesFiltered[index]
            };
            this.content.Add(globalListVariableObjectField);

            VisualElement globalListVariableInspector = new InspectorElement();
            globalListVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listScriptableObjectsGlobalListVariablesFiltered[index]));
            this.content.Add(globalListVariableInspector);
        }

        protected virtual void ShowScriptableObjectGlobalNameVariable(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement globalNameVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(GlobalNameVariables),
                value = windowContentList.listScriptableObjectsGlobalNameVariablesFiltered[index]
            };
            this.content.Add(globalNameVariableObjectField);

            VisualElement globalNameVariableInspector = new InspectorElement();
            globalNameVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listScriptableObjectsGlobalNameVariablesFiltered[index]));
            this.content.Add(globalNameVariableInspector);
        }

        protected virtual void ShowScriptableObjectMaterialSoundsAsset(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement materialSoundsAssetObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(MaterialSoundsAsset),
                value = windowContentList.listScriptableObjectsMaterialSoundsAssetsFiltered[index]
            };
            this.content.Add(materialSoundsAssetObjectField);

            VisualElement materialSoundsAssetInspector = new InspectorElement();
            materialSoundsAssetInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listScriptableObjectsMaterialSoundsAssetsFiltered[index]));
            this.content.Add(materialSoundsAssetInspector);
        }

        protected virtual void ShowScriptableObjectStateAnimation(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement stateAnimationObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(StateAnimation),
                value = windowContentList.listScriptableObjectsStateAnimationsFiltered[index]
            };
            this.content.Add(stateAnimationObjectField);

            VisualElement stateAnimationInspector = new InspectorElement();
            stateAnimationInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listScriptableObjectsStateAnimationsFiltered[index]));
            this.content.Add(stateAnimationInspector);
        }

        protected virtual void ShowScriptableObjectStateBasicLocomotion(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement stateBasicLocomotionObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(StateBasicLocomotion),
                value = windowContentList.listScriptableObjectsStateBasicLocomotionsFiltered[index]
            };
            this.content.Add(stateBasicLocomotionObjectField);

            VisualElement stateBasicLocomotionInspector = new InspectorElement();
            stateBasicLocomotionInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listScriptableObjectsStateBasicLocomotionsFiltered[index]));
            this.content.Add(stateBasicLocomotionInspector);
        }

        protected virtual void ShowScriptableObjectStateCompleteLocomotion(int index)
        {
            GameCreatorFinder2WindowContentCore windowContent = (GameCreatorFinder2WindowContentCore)this.window.Content;
            GameCreatorFinder2WindowContentListCore windowContentList = (GameCreatorFinder2WindowContentListCore)windowContent.ContentList;

            VisualElement stateCompleteLocomotionsObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(StateCompleteLocomotion),
                value = windowContentList.listScriptableObjectsStateCompleteLocomotionsFiltered[index]
            };
            this.content.Add(stateCompleteLocomotionsObjectField);

            VisualElement stateCompleteLocomotionsInspector = new InspectorElement();
            stateCompleteLocomotionsInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListCore)this.window.Content.ContentList).listScriptableObjectsStateCompleteLocomotionsFiltered[index]));
            this.content.Add(stateCompleteLocomotionsInspector);
        }
    }
}