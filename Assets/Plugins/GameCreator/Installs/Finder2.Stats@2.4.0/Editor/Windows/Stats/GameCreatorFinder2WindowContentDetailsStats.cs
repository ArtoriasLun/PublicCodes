using GameCreator.Runtime.Stats;
using GameCreator.Runtime.Stats.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentDetailsStats : GameCreatorFinder2WindowContentDetails
    {

        private readonly GameCreatorFinder2WindowStats window;

        public GameCreatorFinder2WindowContentDetailsStats(GameCreatorFinder2WindowStats window) : base(window)
        {
            this.window = window;
        }

        protected override void ShowComponent(string foldout, int index)
        {
            switch (foldout)
            {
                case "AttributeUI":
                    this.ShowComponentAttributeUI(index);
                    break;
                case "FormulaUI":
                    this.ShowComponentFormulaUI(index);
                    break;
                case "StatUI":
                    this.ShowComponentStatUI(index);
                    break;
                case "StatusEffectListUI":
                    this.ShowComponentStatusEffectListUI(index);
                    break;
                case "StatusEffectUI":
                    this.ShowComponentStatusEffectUI(index);
                    break;
                case "Trait":
                    this.ShowComponentTraits(index);
                    break;
            }
        }

        protected virtual void ShowComponentAttributeUI(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement characterObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(AttributeUI),
                value = windowContentList.listComponentsAttributeUIsFiltered[index]
            };
            this.content.Add(characterObjectField);

            VisualElement characterInspector = new InspectorElement();
            characterInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listComponentsAttributeUIsFiltered[index]));
            this.content.Add(characterInspector);
        }

        protected virtual void ShowComponentFormulaUI(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement hotspotObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(FormulaUI),
                value = windowContentList.listComponentsFormulaUIsFiltered[index]
            };
            this.content.Add(hotspotObjectField);

            VisualElement hotspotInspector = new InspectorElement();
            hotspotInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listComponentsFormulaUIsFiltered[index]));
            this.content.Add(hotspotInspector);
        }

        protected virtual void ShowComponentStatUI(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement localListVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(StatUI),
                value = windowContentList.listComponentsStatUIsFiltered[index]
            };
            this.content.Add(localListVariableObjectField);

            VisualElement localListVariableInspector = new InspectorElement();
            localListVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listComponentsStatUIsFiltered[index]));
            this.content.Add(localListVariableInspector);
        }

        protected virtual void ShowComponentStatusEffectListUI(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement localNameVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(StatusEffectListUI),
                value = windowContentList.listComponentsStatusEffectListUIsFiltered[index]
            };
            this.content.Add(localNameVariableObjectField);

            VisualElement localNameVariableInspector = new InspectorElement();
            localNameVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listComponentsStatusEffectListUIsFiltered[index]));
            this.content.Add(localNameVariableInspector);
        }

        protected virtual void ShowComponentStatusEffectUI(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement mainCameraObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(StatusEffectUI),
                value = windowContentList.listComponentsStatusEffectUIsFiltered[index]
            };
            this.content.Add(mainCameraObjectField);

            VisualElement mainCameraInspector = new InspectorElement();
            mainCameraInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listComponentsStatusEffectUIsFiltered[index]));
            this.content.Add(mainCameraInspector);
        }

        protected virtual void ShowComponentTraits(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement markerObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Traits),
                value = windowContentList.listComponentsTraitsFiltered[index]
            };
            this.content.Add(markerObjectField);

            VisualElement markerInspector = new InspectorElement();
            markerInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listComponentsTraitsFiltered[index]));
            this.content.Add(markerInspector);
        }

        protected override void ShowScriptableObject(string foldout, int index)
        {
            switch (foldout)
            {
                case "Attribute":
                    this.ShowScriptableObjectAttribute(index);
                    break;
                case "Class":
                    this.ShowScriptableObjectClass(index);
                    break;
                case "Formula":
                    this.ShowScriptableObjectFormula(index);
                    break;
                case "Stat":
                    this.ShowScriptableObjectStat(index);
                    break;
                case "Status Effect":
                    this.ShowScriptableObjectStatusEffect(index);
                    break;
                case "Table":
                    this.ShowScriptableObjectTable(index);
                    break;
            }
        }

        protected virtual void ShowScriptableObjectAttribute(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement installerObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Attribute),
                value = windowContentList.listScriptableObjectsAttributesFiltered[index]
            };
            this.content.Add(installerObjectField);

            VisualElement installerInspector = new InspectorElement();
            installerInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listScriptableObjectsAttributesFiltered[index]));
            this.content.Add(installerInspector);
        }

        protected virtual void ShowScriptableObjectClass(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement globalListVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Class),
                value = windowContentList.listScriptableObjectsClassesFiltered[index]
            };
            this.content.Add(globalListVariableObjectField);

            VisualElement globalListVariableInspector = new InspectorElement();
            globalListVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listScriptableObjectsClassesFiltered[index]));
            this.content.Add(globalListVariableInspector);
        }

        protected virtual void ShowScriptableObjectFormula(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement globalNameVariableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Formula),
                value = windowContentList.listScriptableObjectsFormulasFiltered[index]
            };
            this.content.Add(globalNameVariableObjectField);

            VisualElement globalNameVariableInspector = new InspectorElement();
            globalNameVariableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listScriptableObjectsFormulasFiltered[index]));
            this.content.Add(globalNameVariableInspector);
        }

        protected virtual void ShowScriptableObjectStat(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement materialSoundsAssetObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Stat),
                value = windowContentList.listScriptableObjectsStatsFiltered[index]
            };
            this.content.Add(materialSoundsAssetObjectField);

            VisualElement materialSoundsAssetInspector = new InspectorElement();
            materialSoundsAssetInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listScriptableObjectsStatsFiltered[index]));
            this.content.Add(materialSoundsAssetInspector);
        }

        protected virtual void ShowScriptableObjectStatusEffect(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement stateAnimationObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(StatusEffect),
                value = windowContentList.listScriptableObjectsStatusEffectsFiltered[index]
            };
            this.content.Add(stateAnimationObjectField);

            VisualElement stateAnimationInspector = new InspectorElement();
            stateAnimationInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listScriptableObjectsStatusEffectsFiltered[index]));
            this.content.Add(stateAnimationInspector);
        }

        protected virtual void ShowScriptableObjectTable(int index)
        {
            GameCreatorFinder2WindowContentStats windowContent = (GameCreatorFinder2WindowContentStats)this.window.Content;
            GameCreatorFinder2WindowContentListStats windowContentList = (GameCreatorFinder2WindowContentListStats)windowContent.ContentList;

            VisualElement stateBasicLocomotionObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Table),
                value = windowContentList.listScriptableObjectsTablesFiltered[index]
            };
            this.content.Add(stateBasicLocomotionObjectField);

            VisualElement stateBasicLocomotionInspector = new InspectorElement();
            stateBasicLocomotionInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListStats)this.window.Content.ContentList).listScriptableObjectsTablesFiltered[index]));
            this.content.Add(stateBasicLocomotionInspector);
        }
    }
}