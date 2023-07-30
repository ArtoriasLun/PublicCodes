using GameCreator.Runtime.Common;
using GameCreator.Runtime.Stats;
using GameCreator.Runtime.Stats.UnityUI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentListStats : GameCreatorFinder2WindowContentList
    {
        protected static readonly IIcon ICON_ATTRIBUTEUI = new IconAttr(ColorTheme.Type.Blue);
        protected static readonly IIcon ICON_CLASS = new IconClass(ColorTheme.Type.Blue);
        protected static readonly IIcon ICON_FORMULAUI = new IconFormula(ColorTheme.Type.Purple);
        protected static readonly IIcon ICON_STATUI = new IconStat(ColorTheme.Type.Red);
        protected static readonly IIcon ICON_STATUSEFFECTLISTUI = new IconStatusEffect(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_STATUSEFFECTUI = new IconCircleSolid(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_TABLE = new IconTable(ColorTheme.Type.Yellow);
        protected static readonly IIcon ICON_TRAITS = new IconTraits(ColorTheme.Type.Pink);

        private readonly GameCreatorFinder2WindowStats window;

        internal List<AttributeUI> listComponentsAttributeUIs = new List<AttributeUI>();
        internal List<AttributeUI> listComponentsAttributeUIsFiltered = new List<AttributeUI>();
        internal List<FormulaUI> listComponentsFormulaUIs = new List<FormulaUI>();
        internal List<FormulaUI> listComponentsFormulaUIsFiltered = new List<FormulaUI>();
        internal List<StatUI> listComponentsStatUIs = new List<StatUI>();
        internal List<StatUI> listComponentsStatUIsFiltered = new List<StatUI>();
        internal List<StatusEffectListUI> listComponentsStatusEffectListUIs = new List<StatusEffectListUI>();
        internal List<StatusEffectListUI> listComponentsStatusEffectListUIsFiltered = new List<StatusEffectListUI>();
        internal List<StatusEffectUI> listComponentsStatusEffectUIs = new List<StatusEffectUI>();
        internal List<StatusEffectUI> listComponentsStatusEffectUIsFiltered = new List<StatusEffectUI>();
        internal List<Traits> listComponentsTraits = new List<Traits>();
        internal List<Traits> listComponentsTraitsFiltered = new List<Traits>();

        internal List<Attribute> listScriptableObjectsAttributes = new List<Attribute>();
        internal List<Attribute> listScriptableObjectsAttributesFiltered = new List<Attribute>();
        internal List<Class> listScriptableObjectsClasses = new List<Class>();
        internal List<Class> listScriptableObjectsClassesFiltered = new List<Class>();
        internal List<Formula> listScriptableObjectsFormulas = new List<Formula>();
        internal List<Formula> listScriptableObjectsFormulasFiltered = new List<Formula>();
        internal List<Stat> listScriptableObjectsStats = new List<Stat>();
        internal List<Stat> listScriptableObjectsStatsFiltered = new List<Stat>();
        internal List<StatusEffect> listScriptableObjectsStatusEffects = new List<StatusEffect>();
        internal List<StatusEffect> listScriptableObjectsStatusEffectsFiltered = new List<StatusEffect>();
        internal List<Table> listScriptableObjectsTables = new List<Table>();
        internal List<Table> listScriptableObjectsTablesFiltered = new List<Table>();

        protected ListView listViewComponentsAttributeUIs;
        protected ListView listViewComponentsFormulaUIs;
        protected ListView listViewComponentsStatUIs;
        protected ListView listViewComponentsStatusEffectListUIs;
        protected ListView listViewComponentsStatusEffectUIs;
        protected ListView listViewComponentsTraits;

        protected ListView listViewScriptableObjectsAttributes;
        protected ListView listViewScriptableObjectsClasses;
        protected ListView listViewScriptableObjectsFormulas;
        protected ListView listViewScriptableObjectsStats;
        protected ListView listViewScriptableObjectsStatusEffects;
        protected ListView listViewScriptableObjectsTables;

        protected Foldout foldoutComponentsAttributeUIs = new Foldout() { text = "Attribute UIs", value = false };
        protected Foldout foldoutComponentsFormulaUIs = new Foldout() { text = "Formula UIs", value = false };
        protected Foldout foldoutComponentsStatUIs = new Foldout() { text = "Stat UIs", value = false };
        protected Foldout foldoutComponentsStatusEffectListUIs = new Foldout() { text = "Status Effect List UIs", value = false };
        protected Foldout foldoutComponentsStatusEffectUIs = new Foldout() { text = "Status Effect UIs", value = false };
        protected Foldout foldoutComponentsTraits = new Foldout() { text = "Traits", value = false };

        protected Foldout foldoutScriptableObjectsAttributes = new Foldout() { text = "Attributes", value = false };
        protected Foldout foldoutScriptableObjectsClasses = new Foldout() { text = "Classes", value = false };
        protected Foldout foldoutScriptableObjectsFormulas = new Foldout() { text = "Formulas", value = false };
        protected Foldout foldoutScriptableObjectsStats = new Foldout() { text = "Stats", value = false };
        protected Foldout foldoutScriptableObjectsStatusEffects = new Foldout() { text = "Status Effects", value = false };
        protected Foldout foldoutScriptableObjectsTables = new Foldout() { text = "Tables", value = false };

        public GameCreatorFinder2WindowContentListStats(GameCreatorFinder2WindowStats window) : base(window)
        {
            this.window = window;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            this.listViewComponentsAttributeUIs.selectionChanged -= OnContentSelectItemAttributeUI;
            this.listViewComponentsFormulaUIs.selectionChanged -= OnContentSelectItemFormulaUI;
            this.listViewComponentsStatUIs.selectionChanged -= OnContentSelectItemStatUI;
            this.listViewComponentsStatusEffectListUIs.selectionChanged -= OnContentSelectItemStatusEffectListUI;
            this.listViewComponentsStatusEffectUIs.selectionChanged -= OnContentSelectItemStatusEffectUI;
            this.listViewComponentsTraits.selectionChanged -= OnContentSelectItemTrait;

            this.listViewScriptableObjectsAttributes.selectionChanged -= OnContentSelectItemAttribute;
            this.listViewScriptableObjectsClasses.selectionChanged -= OnContentSelectItemClass;
            this.listViewScriptableObjectsFormulas.selectionChanged -= OnContentSelectItemFormula;
            this.listViewScriptableObjectsStats.selectionChanged -= OnContentSelectItemStat;
            this.listViewScriptableObjectsStatusEffects.selectionChanged -= OnContentSelectItemStatusEffect;
            this.listViewScriptableObjectsTables.selectionChanged -= OnContentSelectItemTable;

            this.listViewComponentsAttributeUIs.itemsChosen -= OnContentChooseItemAttributeUI;
            this.listViewComponentsFormulaUIs.itemsChosen -= OnContentChooseItemFormulaUI;
            this.listViewComponentsStatUIs.itemsChosen -= OnContentChooseItemStatUI;
            this.listViewComponentsStatusEffectListUIs.itemsChosen -= OnContentChooseItemStatusEffectListUI;
            this.listViewComponentsStatusEffectUIs.itemsChosen -= OnContentChooseItemStatusEffectUI;
            this.listViewComponentsTraits.itemsChosen -= OnContentChooseItemTrait;

            this.listViewScriptableObjectsAttributes.itemsChosen -= OnContentChooseItemAttribute;
            this.listViewScriptableObjectsClasses.itemsChosen -= OnContentChooseItemClass;
            this.listViewScriptableObjectsFormulas.itemsChosen -= OnContentChooseItemFormula;
            this.listViewScriptableObjectsStats.itemsChosen -= OnContentChooseItemStat;
            this.listViewScriptableObjectsStatusEffects.itemsChosen -= OnContentChooseItemStatusEffect;
            this.listViewScriptableObjectsTables.itemsChosen -= OnContentChooseItemTable;
        }

        protected override void SetupListComponents()
        {
            this.InitializeListComponents();

            this.SetupListComponentsAttributeUIs();
            this.SetupListComponentsFormulaUIs();
            this.SetupListComponentsStatUIs();
            this.SetupListComponentsStatusEffectListUIs();
            this.SetupListComponentsStatusEffectUIs();
            this.SetupListComponentsTraits();
        }

        protected virtual void SetupListComponentsAttributeUIs()
        {
            this.listViewComponentsAttributeUIs = new ListView(this.listComponentsAttributeUIsFiltered, 24, this.MakeItem, this.BindItemAttributeUI)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsAttributeUIs.selectionChanged += OnContentSelectItemAttributeUI;
            this.listViewComponentsAttributeUIs.itemsChosen += OnContentChooseItemAttributeUI;

            this.foldoutComponentsAttributeUIs.Add(this.listViewComponentsAttributeUIs);
        }

        protected virtual void SetupListComponentsFormulaUIs()
        {
            this.listViewComponentsFormulaUIs = new ListView(this.listComponentsFormulaUIsFiltered, 24, this.MakeItem, this.BindItemFormulaUI)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsFormulaUIs.selectionChanged += OnContentSelectItemFormulaUI;
            this.listViewComponentsFormulaUIs.itemsChosen += OnContentChooseItemFormulaUI;

            this.foldoutComponentsFormulaUIs.Add(this.listViewComponentsFormulaUIs);
        }

        protected virtual void SetupListComponentsStatUIs()
        {
            this.listViewComponentsStatUIs = new ListView(this.listComponentsStatUIsFiltered, 24, this.MakeItem, this.BindItemStatUI)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsStatUIs.selectionChanged += OnContentSelectItemStatUI;
            this.listViewComponentsStatUIs.itemsChosen += OnContentChooseItemStatUI;

            this.foldoutComponentsStatUIs.Add(this.listViewComponentsStatUIs);
        }

        protected virtual void SetupListComponentsStatusEffectListUIs()
        {
            this.listViewComponentsStatusEffectListUIs = new ListView(this.listComponentsStatusEffectListUIsFiltered, 24, this.MakeItem, this.BindItemStatusEffectListUI)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsStatusEffectListUIs.selectionChanged += OnContentSelectItemStatusEffectListUI;
            this.listViewComponentsStatusEffectListUIs.itemsChosen += OnContentChooseItemStatusEffectListUI;

            this.foldoutComponentsStatusEffectListUIs.Add(this.listViewComponentsStatusEffectListUIs);
        }

        protected virtual void SetupListComponentsStatusEffectUIs()
        {
            this.listViewComponentsStatusEffectUIs = new ListView(this.listComponentsStatusEffectUIsFiltered, 24, this.MakeItem, this.BindItemStatusEffectUI)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsStatusEffectUIs.selectionChanged += OnContentSelectItemStatusEffectUI;
            this.listViewComponentsStatusEffectUIs.itemsChosen += OnContentChooseItemStatusEffectUI;

            this.foldoutComponentsStatusEffectUIs.Add(this.listViewComponentsStatusEffectUIs);
        }

        protected virtual void SetupListComponentsTraits()
        {
            this.listViewComponentsTraits = new ListView(this.listComponentsTraitsFiltered, 24, this.MakeItem, this.BindItemTrait)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsTraits.selectionChanged += OnContentSelectItemTrait;
            this.listViewComponentsTraits.itemsChosen += OnContentChooseItemTrait;

            this.foldoutComponentsTraits.Add(this.listViewComponentsTraits);
        }

        protected override void SetupListScriptableObjects()
        {
            this.InitializeListScriptableObjects();

            this.SetupListScriptableObjectsAttributes();
            this.SetupListScriptableObjectsClasses();
            this.SetupListScriptableObjectsFormulas();
            this.SetupListScriptableObjectsStats();
            this.SetupListScriptableObjectsStatusEffects();
            this.SetupListScriptableObjectsTables();
        }

        protected virtual void SetupListScriptableObjectsAttributes()
        {
            this.listViewScriptableObjectsAttributes = new ListView(this.listScriptableObjectsAttributesFiltered, 24, this.MakeItem, this.BindItemAttribute)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsAttributes.selectionChanged += OnContentSelectItemAttribute;
            this.listViewScriptableObjectsAttributes.itemsChosen += OnContentChooseItemAttribute;

            this.foldoutScriptableObjectsAttributes.Add(this.listViewScriptableObjectsAttributes);
        }

        protected virtual void SetupListScriptableObjectsClasses()
        {
            this.listViewScriptableObjectsClasses = new ListView(this.listScriptableObjectsClassesFiltered, 24, this.MakeItem, this.BindItemClass)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsClasses.selectionChanged += OnContentSelectItemClass;
            this.listViewScriptableObjectsClasses.itemsChosen += OnContentChooseItemClass;

            this.foldoutScriptableObjectsClasses.Add(this.listViewScriptableObjectsClasses);
        }

        protected virtual void SetupListScriptableObjectsFormulas()
        {
            this.listViewScriptableObjectsFormulas = new ListView(this.listScriptableObjectsFormulasFiltered, 24, this.MakeItem, this.BindItemFormula)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsFormulas.selectionChanged += OnContentSelectItemFormula;
            this.listViewScriptableObjectsFormulas.itemsChosen += OnContentChooseItemFormula;

            this.foldoutScriptableObjectsFormulas.Add(this.listViewScriptableObjectsFormulas);
        }

        protected virtual void SetupListScriptableObjectsStats()
        {
            this.listViewScriptableObjectsStats = new ListView(this.listScriptableObjectsStatsFiltered, 24, this.MakeItem, this.BindItemStat)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsStats.selectionChanged += OnContentSelectItemStat;
            this.listViewScriptableObjectsStats.itemsChosen += OnContentChooseItemStat;

            this.foldoutScriptableObjectsStats.Add(this.listViewScriptableObjectsStats);
        }

        protected virtual void SetupListScriptableObjectsStatusEffects()
        {
            this.listViewScriptableObjectsStatusEffects = new ListView(this.listScriptableObjectsStatusEffectsFiltered, 24, this.MakeItem, this.BindItemStatusEffect)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsStatusEffects.selectionChanged += OnContentSelectItemStatusEffect;
            this.listViewScriptableObjectsStatusEffects.itemsChosen += OnContentChooseItemStatusEffect;

            this.foldoutScriptableObjectsStatusEffects.Add(this.listViewScriptableObjectsStatusEffects);
        }

        protected virtual void SetupListScriptableObjectsTables()
        {
            this.listViewScriptableObjectsTables = new ListView(this.listScriptableObjectsTablesFiltered, 24, this.MakeItem, this.BindItemTable)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsTables.selectionChanged += OnContentSelectItemTable;
            this.listViewScriptableObjectsTables.itemsChosen += OnContentChooseItemTable;

            this.foldoutScriptableObjectsTables.Add(this.listViewScriptableObjectsTables);
        }

        protected virtual void BindItemAttributeUI(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsAttributeUIsFiltered[index].GetType().Name switch
            {
                "AttributeUI" => ICON_ATTRIBUTEUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsAttributeUIsFiltered[index].name} (ID {this.listComponentsAttributeUIsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemFormulaUI(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsFormulaUIsFiltered[index].GetType().Name switch
            {
                "FormulaUI" => ICON_FORMULAUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsFormulaUIsFiltered[index].name} (ID {this.listComponentsFormulaUIsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStatUI(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsStatUIsFiltered[index].GetType().Name switch
            {
                "StatUI" => ICON_STATUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsStatUIsFiltered[index].name} (ID {this.listComponentsStatUIsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStatusEffectListUI(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsStatusEffectListUIsFiltered[index].GetType().Name switch
            {
                "StatusEffectListUI" => ICON_STATUSEFFECTLISTUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsStatusEffectListUIsFiltered[index].name} (ID {this.listComponentsStatusEffectListUIsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStatusEffectUI(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsStatusEffectUIsFiltered[index].GetType().Name switch
            {
                "StatusEffectUI" => ICON_STATUSEFFECTUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsStatusEffectUIsFiltered[index].name} (ID {this.listComponentsStatusEffectUIsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemTrait(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsTraitsFiltered[index].GetType().Name switch
            {
                "Traits" => ICON_TRAITS,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsTraitsFiltered[index].name} (ID {this.listComponentsTraitsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemAttribute(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsAttributesFiltered[index].GetType().Name switch
            {
                "Attribute" => ICON_ATTRIBUTEUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsAttributesFiltered[index].name} (ID {this.listScriptableObjectsAttributesFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemClass(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsClassesFiltered[index].GetType().Name switch
            {
                "Class" => ICON_CLASS,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsClassesFiltered[index].name} (ID {this.listScriptableObjectsClassesFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemFormula(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsFormulasFiltered[index].GetType().Name switch
            {
                "Formula" => ICON_FORMULAUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsFormulasFiltered[index].name} (ID {this.listScriptableObjectsFormulasFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStat(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsStatsFiltered[index].GetType().Name switch
            {
                "Stat" => ICON_STATUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsStatsFiltered[index].name} (ID {this.listScriptableObjectsStatsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemStatusEffect(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsStatusEffectsFiltered[index].GetType().Name switch
            {
                "StatusEffect" => ICON_STATUSEFFECTLISTUI,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsStatusEffectsFiltered[index].name} (ID {this.listScriptableObjectsStatusEffectsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemTable(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsTablesFiltered[index].GetType().Name switch
            {
                "Table" => ICON_TABLE,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsTablesFiltered[index].name} (ID {this.listScriptableObjectsTablesFiltered[index].GetInstanceID()})";
        }

        public override void InitializeListComponents()
        {
            this.listComponentsAttributeUIs.AddRange(GameCreatorFinder2WindowStats.ATTRIBUTEUI_FOUND);
            this.listComponentsFormulaUIs.AddRange(GameCreatorFinder2WindowStats.FORMULAUI_FOUND);
            this.listComponentsStatUIs.AddRange(GameCreatorFinder2WindowStats.STATUI_FOUND);
            this.listComponentsStatusEffectListUIs.AddRange(GameCreatorFinder2WindowStats.STATUSEFFECTLISTUI_FOUND);
            this.listComponentsStatusEffectUIs.AddRange(GameCreatorFinder2WindowStats.STATUSEFFECTUI_FOUND);
            this.listComponentsTraits.AddRange(GameCreatorFinder2WindowStats.TRAITS_FOUND);
        }

        public override void InitializeListScriptableObjects()
        {
            this.listScriptableObjectsAttributes.AddRange(GameCreatorFinder2WindowStats.ATTRIBUTES_FOUND);
            this.listScriptableObjectsClasses.AddRange(GameCreatorFinder2WindowStats.CLASSES_FOUND);
            this.listScriptableObjectsFormulas.AddRange(GameCreatorFinder2WindowStats.FORMULAS_FOUND);
            this.listScriptableObjectsStats.AddRange(GameCreatorFinder2WindowStats.STATS_FOUND);
            this.listScriptableObjectsStatusEffects.AddRange(GameCreatorFinder2WindowStats.STATUSEFFECTS_FOUND);
            this.listScriptableObjectsTables.AddRange(GameCreatorFinder2WindowStats.TABLES_FOUND);
        }

        public override void RemoveListComponents()
        {
            this.listComponentsAttributeUIs = new List<AttributeUI>();
            this.listComponentsFormulaUIs = new List<FormulaUI>();
            this.listComponentsStatUIs = new List<StatUI>();
            this.listComponentsStatusEffectListUIs = new List<StatusEffectListUI>();
            this.listComponentsStatusEffectUIs = new List<StatusEffectUI>();
            this.listComponentsTraits = new List<Traits>();
        }

        protected override void RemoveListScriptableObjects()
        {
            this.listScriptableObjectsAttributes = new List<Attribute>();
            this.listScriptableObjectsClasses = new List<Class>();
            this.listScriptableObjectsFormulas = new List<Formula>();
            this.listScriptableObjectsStats = new List<Stat>();
            this.listScriptableObjectsStatusEffects = new List<StatusEffect>();
            this.listScriptableObjectsTables = new List<Table>();
        }

        protected override void RemoveListComponentsElements()
        {
            try { this.content.Remove(this.foldoutComponentsAttributeUIs); } catch { }
            try { this.foldoutComponentsAttributeUIs.Remove(this.listViewComponentsAttributeUIs); } catch { }

            try { this.content.Remove(this.foldoutComponentsFormulaUIs); } catch { }
            try { this.foldoutComponentsFormulaUIs.Remove(this.listViewComponentsFormulaUIs); } catch { }

            try { this.content.Remove(this.foldoutComponentsStatUIs); } catch { }
            try { this.foldoutComponentsStatUIs.Remove(this.listViewComponentsStatUIs); } catch { }

            try { this.content.Remove(this.foldoutComponentsStatusEffectListUIs); } catch { }
            try { this.foldoutComponentsStatusEffectListUIs.Remove(this.listViewComponentsStatusEffectListUIs); } catch { }

            try { this.content.Remove(this.foldoutComponentsStatusEffectUIs); } catch { }
            try { this.foldoutComponentsStatusEffectUIs.Remove(this.listViewComponentsStatusEffectUIs); } catch { }

            try { this.content.Remove(this.foldoutComponentsTraits); } catch { }
            try { this.foldoutComponentsTraits.Remove(this.listViewComponentsTraits); } catch { }
        }

        protected override void RemoveListScriptableObjectsElements()
        {
            try { this.content.Remove(this.foldoutScriptableObjectsAttributes); } catch { }
            try { this.foldoutScriptableObjectsAttributes.Remove(this.listViewScriptableObjectsAttributes); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsClasses); } catch { }
            try { this.foldoutScriptableObjectsClasses.Remove(this.listViewScriptableObjectsClasses); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsFormulas); } catch { }
            try { this.foldoutScriptableObjectsFormulas.Remove(this.listViewScriptableObjectsFormulas); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsStats); } catch { }
            try { this.foldoutScriptableObjectsStats.Remove(this.listViewScriptableObjectsStats); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsStatusEffects); } catch { }
            try { this.foldoutScriptableObjectsStatusEffects.Remove(this.listViewScriptableObjectsStatusEffects); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsTables); } catch { }
            try { this.foldoutScriptableObjectsTables.Remove(this.listViewScriptableObjectsTables); } catch { }
        }

        public override void UpdateListComponents()
        {
            this.RemoveListElements();

            this.UpdateListComponentsAttributeUIs();
            this.UpdateListComponentsFormulaUIs();
            this.UpdateListComponentsStatUIs();
            this.UpdateListComponentsStatusEffectListUIs();
            this.UpdateListComponentStatusEffectUIs();
            this.UpdateListComponentsTraits();
        }

        protected virtual void UpdateListComponentsAttributeUIs()
        {
            this.listComponentsAttributeUIs = this.sortComponentsAZ ? this.listComponentsAttributeUIs.OrderBy(go => go.name).ToList() : this.listComponentsAttributeUIs.OrderByDescending(go => go.name).ToList();

            this.listComponentsAttributeUIsFiltered = this.listComponentsAttributeUIs.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsAttributeUIsFiltered.Count > 0)
            {
                this.listViewComponentsAttributeUIs.itemsSource = this.listComponentsAttributeUIsFiltered;
                this.listViewComponentsAttributeUIs.Rebuild();

                this.foldoutComponentsAttributeUIs.Add(this.listViewComponentsAttributeUIs);

                this.content.Add(this.foldoutComponentsAttributeUIs);
            }
        }

        protected virtual void UpdateListComponentsFormulaUIs()
        {
            this.listComponentsFormulaUIs = this.sortComponentsAZ ? this.listComponentsFormulaUIs.OrderBy(go => go.name).ToList() : this.listComponentsFormulaUIs.OrderByDescending(go => go.name).ToList();

            this.listComponentsFormulaUIsFiltered = this.listComponentsFormulaUIs.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsFormulaUIsFiltered.Count > 0)
            {
                this.listViewComponentsFormulaUIs.itemsSource = this.listComponentsFormulaUIsFiltered;
                this.listViewComponentsFormulaUIs.Rebuild();

                this.foldoutComponentsFormulaUIs.Add(this.listViewComponentsFormulaUIs);

                this.content.Add(this.foldoutComponentsFormulaUIs);
            }
        }

        protected virtual void UpdateListComponentsStatUIs()
        {
            this.listComponentsStatUIs = this.sortComponentsAZ ? this.listComponentsStatUIs.OrderBy(go => go.name).ToList() : this.listComponentsStatUIs.OrderByDescending(go => go.name).ToList();

            this.listComponentsStatUIsFiltered = this.listComponentsStatUIs.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsStatUIsFiltered.Count > 0)
            {
                this.listViewComponentsStatUIs.itemsSource = this.listComponentsStatUIsFiltered;
                this.listViewComponentsStatUIs.Rebuild();

                this.foldoutComponentsStatUIs.Add(this.listViewComponentsStatUIs);

                this.content.Add(this.foldoutComponentsStatUIs);
            }
        }

        protected virtual void UpdateListComponentsStatusEffectListUIs()
        {
            this.listComponentsStatusEffectListUIs = this.sortComponentsAZ ? this.listComponentsStatusEffectListUIs.OrderBy(go => go.name).ToList() : this.listComponentsStatusEffectListUIs.OrderByDescending(go => go.name).ToList();

            this.listComponentsStatusEffectListUIsFiltered = this.listComponentsStatusEffectListUIs.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsStatusEffectListUIsFiltered.Count > 0)
            {
                this.listViewComponentsStatusEffectListUIs.itemsSource = this.listComponentsStatusEffectListUIsFiltered;
                this.listViewComponentsStatusEffectListUIs.Rebuild();

                this.foldoutComponentsStatusEffectListUIs.Add(this.listViewComponentsStatusEffectListUIs);

                this.content.Add(this.foldoutComponentsStatusEffectListUIs);
            }
        }

        protected virtual void UpdateListComponentStatusEffectUIs()
        {
            this.listComponentsStatusEffectUIs = this.sortComponentsAZ ? this.listComponentsStatusEffectUIs.OrderBy(go => go.name).ToList() : this.listComponentsStatusEffectUIs.OrderByDescending(go => go.name).ToList();

            this.listComponentsStatusEffectUIsFiltered = this.listComponentsStatusEffectUIs.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsStatusEffectUIsFiltered.Count > 0)
            {
                this.listViewComponentsStatusEffectUIs.itemsSource = this.listComponentsStatusEffectUIsFiltered;
                this.listViewComponentsStatusEffectUIs.Rebuild();

                this.foldoutComponentsStatusEffectUIs.Add(this.listViewComponentsStatusEffectUIs);

                this.content.Add(this.foldoutComponentsStatusEffectUIs);
            }
        }

        protected virtual void UpdateListComponentsTraits()
        {
            this.listComponentsTraits = this.sortComponentsAZ ? this.listComponentsTraits.OrderBy(go => go.name).ToList() : this.listComponentsTraits.OrderByDescending(go => go.name).ToList();

            this.listComponentsTraitsFiltered = this.listComponentsTraits.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsTraitsFiltered.Count > 0)
            {
                this.listViewComponentsTraits.itemsSource = this.listComponentsTraitsFiltered;
                this.listViewComponentsTraits.Rebuild();

                this.foldoutComponentsTraits.Add(this.listViewComponentsTraits);

                this.content.Add(this.foldoutComponentsTraits);
            }
        }

        public override void UpdateListScriptableObjects()
        {
            this.RemoveListElements();

            this.UpdateListScriptableObjectsAttributes();
            this.UpdateListScriptableObjectsClasses();
            this.UpdateListScriptableObjectsFormulas();
            this.UpdateListScriptableObjectsStats();
            this.UpdateListScriptableObjectsStatusEffects();
            this.UpdateListScriptableObjectsTables();
        }

        protected virtual void UpdateListScriptableObjectsAttributes()
        {
            this.listScriptableObjectsAttributes = this.sortScriptableObjectsAZ ? this.listScriptableObjectsAttributes.OrderBy(go => go.name).ToList() : this.listScriptableObjectsAttributes.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsAttributesFiltered.Clear();

            List<Attribute> listAttributesFoundByName = this.listScriptableObjectsAttributes.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsAttributesFiltered.AddRange(listAttributesFoundByName);

            if (GameCreatorFinder2WindowPreferencesStats.ScriptableObjectsAttributeIDs)
            {
                List<Attribute> listAttributesFoundByID = new List<Attribute>();

                for (int i = 0; i < this.listScriptableObjectsAttributes.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listScriptableObjectsAttributes[i]);
                    var idProperty = so.FindProperty("m_ID").FindPropertyRelative("m_String");
                    string id = idProperty.stringValue;

                    if (id.Contains(this.window.Toolbar.SearchField.value.ToLower())) listAttributesFoundByID.Add(this.listScriptableObjectsAttributes[i]);
                }

                this.listScriptableObjectsAttributesFiltered.AddRange(listAttributesFoundByID);
            }

            this.listScriptableObjectsAttributesFiltered = this.listScriptableObjectsAttributesFiltered.Distinct().ToList();
            this.listScriptableObjectsAttributesFiltered = this.sortComponentsAZ ? this.listScriptableObjectsAttributesFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsAttributesFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsAttributesFiltered.Count > 0)
            {
                this.listViewScriptableObjectsAttributes.itemsSource = this.listScriptableObjectsAttributesFiltered;
                this.listViewScriptableObjectsAttributes.Rebuild();

                this.foldoutScriptableObjectsAttributes.Add(this.listViewScriptableObjectsAttributes);

                this.content.Add(this.foldoutScriptableObjectsAttributes);
            }
        }

        protected virtual void UpdateListScriptableObjectsClasses()
        {
            this.listScriptableObjectsClasses = this.sortScriptableObjectsAZ ? this.listScriptableObjectsClasses.OrderBy(go => go.name).ToList() : this.listScriptableObjectsClasses.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsClassesFiltered.Clear();

            List<Class> listClassesFoundByName = this.listScriptableObjectsClasses.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsClassesFiltered.AddRange(listClassesFoundByName);

            if (GameCreatorFinder2WindowPreferencesStats.ScriptableObjectsClassNames)
            {
                List<Class> listClassesFoundByNameId = new List<Class>();

                for (int i = 0; i < this.listScriptableObjectsClasses.Count; i++)
                {
                    string name = this.listScriptableObjectsClasses[i].GetName(null);

                    if (name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())) listClassesFoundByNameId.Add(this.listScriptableObjectsClasses[i]);
                }

                this.listScriptableObjectsClassesFiltered.AddRange(listClassesFoundByNameId);
            }

            this.listScriptableObjectsClassesFiltered = this.listScriptableObjectsClassesFiltered.Distinct().ToList();
            this.listScriptableObjectsClassesFiltered = this.sortComponentsAZ ? this.listScriptableObjectsClassesFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsClassesFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsClassesFiltered.Count > 0)
            {
                this.listViewScriptableObjectsClasses.itemsSource = this.listScriptableObjectsClassesFiltered;
                this.listViewScriptableObjectsClasses.Rebuild();

                this.foldoutScriptableObjectsClasses.Add(this.listViewScriptableObjectsClasses);

                this.content.Add(this.foldoutScriptableObjectsClasses);
            }
        }

        protected virtual void UpdateListScriptableObjectsFormulas()
        {
            this.listScriptableObjectsFormulas = this.sortScriptableObjectsAZ ? this.listScriptableObjectsFormulas.OrderBy(go => go.name).ToList() : this.listScriptableObjectsFormulas.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsFormulasFiltered.Clear();

            List<Formula> listFormulasFoundByName = this.listScriptableObjectsFormulas.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsFormulasFiltered.AddRange(listFormulasFoundByName);

            if (GameCreatorFinder2WindowPreferencesStats.ScriptableObjectsFormulaExpressions)
            {
                List<Formula> listFormulasFoundByFormulaExpressions = new List<Formula>();

                for (int i = 0; i < this.listScriptableObjectsFormulas.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listScriptableObjectsFormulas[i]);
                    var formulaProperty = so.FindProperty("m_Formula");
                    string formula = formulaProperty.stringValue;

                    if (formula.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())) listFormulasFoundByFormulaExpressions.Add(this.listScriptableObjectsFormulas[i]);
                }

                this.listScriptableObjectsFormulasFiltered.AddRange(listFormulasFoundByFormulaExpressions);
            }

            this.listScriptableObjectsFormulasFiltered = this.listScriptableObjectsFormulasFiltered.Distinct().ToList();
            this.listScriptableObjectsFormulasFiltered = this.sortComponentsAZ ? this.listScriptableObjectsFormulasFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsFormulasFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsFormulasFiltered.Count > 0)
            {
                this.listViewScriptableObjectsFormulas.itemsSource = this.listScriptableObjectsFormulasFiltered;
                this.listViewScriptableObjectsFormulas.Rebuild();

                this.foldoutScriptableObjectsFormulas.Add(this.listViewScriptableObjectsFormulas);

                this.content.Add(this.foldoutScriptableObjectsFormulas);
            }
        }

        protected virtual void UpdateListScriptableObjectsStats()
        {
            this.listScriptableObjectsStats = this.sortScriptableObjectsAZ ? this.listScriptableObjectsStats.OrderBy(go => go.name).ToList() : this.listScriptableObjectsStats.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsStatsFiltered.Clear();

            List<Stat> listStatsFoundByName = this.listScriptableObjectsStats.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsStatsFiltered.AddRange(listStatsFoundByName);

            if (GameCreatorFinder2WindowPreferencesStats.ScriptableObjectsStatIDs)
            {
                List<Stat> listStatsFoundByID = new List<Stat>();

                for (int i = 0; i < this.listScriptableObjectsStats.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listScriptableObjectsStats[i]);
                    var idProperty = so.FindProperty("m_ID").FindPropertyRelative("m_String");
                    string id = idProperty.stringValue;

                    if (id.Contains(this.window.Toolbar.SearchField.value.ToLower())) listStatsFoundByID.Add(this.listScriptableObjectsStats[i]);
                }

                this.listScriptableObjectsStatsFiltered.AddRange(listStatsFoundByID);
            }

            this.listScriptableObjectsStatsFiltered = this.listScriptableObjectsStatsFiltered.Distinct().ToList();
            this.listScriptableObjectsStatsFiltered = this.sortComponentsAZ ? this.listScriptableObjectsStatsFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsStatsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsStatsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsStats.itemsSource = this.listScriptableObjectsStatsFiltered;
                this.listViewScriptableObjectsStats.Rebuild();

                this.foldoutScriptableObjectsStats.Add(this.listViewScriptableObjectsStats);

                this.content.Add(this.foldoutScriptableObjectsStats);
            }
        }

        protected virtual void UpdateListScriptableObjectsStatusEffects()
        {
            this.listScriptableObjectsStatusEffects = this.sortScriptableObjectsAZ ? this.listScriptableObjectsStatusEffects.OrderBy(go => go.name).ToList() : this.listScriptableObjectsStatusEffects.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsStatusEffectsFiltered.Clear();

            List<StatusEffect> listStatusEffectsFoundByName = this.listScriptableObjectsStatusEffects.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsStatusEffectsFiltered.AddRange(listStatusEffectsFoundByName);

            if (GameCreatorFinder2WindowPreferencesStats.ScriptableObjectsStatusEffectIDs)
            {
                List<StatusEffect> listStatsFoundByID = new List<StatusEffect>();

                for (int i = 0; i < this.listScriptableObjectsStatusEffects.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listScriptableObjectsStatusEffects[i]);
                    var idProperty = so.FindProperty("m_ID").FindPropertyRelative("m_String");
                    string id = idProperty.stringValue;

                    if (id.Contains(this.window.Toolbar.SearchField.value.ToLower())) listStatsFoundByID.Add(this.listScriptableObjectsStatusEffects[i]);
                }

                this.listScriptableObjectsStatusEffectsFiltered.AddRange(listStatsFoundByID);
            }

            this.listScriptableObjectsStatusEffectsFiltered = this.listScriptableObjectsStatusEffectsFiltered.Distinct().ToList();
            this.listScriptableObjectsStatusEffectsFiltered = this.sortComponentsAZ ? this.listScriptableObjectsStatusEffectsFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsStatusEffectsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsStatusEffectsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsStatusEffects.itemsSource = this.listScriptableObjectsStatusEffectsFiltered;
                this.listViewScriptableObjectsStatusEffects.Rebuild();

                this.foldoutScriptableObjectsStatusEffects.Add(this.listViewScriptableObjectsStatusEffects);

                this.content.Add(this.foldoutScriptableObjectsStatusEffects);
            }
        }

        protected virtual void UpdateListScriptableObjectsTables()
        {
            this.listScriptableObjectsTables = this.sortScriptableObjectsAZ ? this.listScriptableObjectsTables.OrderBy(go => go.name).ToList() : this.listScriptableObjectsTables.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsTablesFiltered = this.listScriptableObjectsTables.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsTablesFiltered.Count > 0)
            {
                this.listViewScriptableObjectsTables.itemsSource = this.listScriptableObjectsTablesFiltered;
                this.listViewScriptableObjectsTables.Rebuild();

                this.foldoutScriptableObjectsTables.Add(this.listViewScriptableObjectsTables);

                this.content.Add(this.foldoutScriptableObjectsTables);
            }
        }

        protected override void ShowAllComponents()
        {
            this.foldoutComponentsAttributeUIs.value = true;
            this.foldoutComponentsFormulaUIs.value = true;
            this.foldoutComponentsStatUIs.value = true;
            this.foldoutComponentsStatusEffectListUIs.value = true;
            this.foldoutComponentsStatusEffectUIs.value = true;
            this.foldoutComponentsTraits.value = true;
        }

        protected override void HideAllComponents()
        {
            this.foldoutComponentsAttributeUIs.value = false;
            this.foldoutComponentsFormulaUIs.value = false;
            this.foldoutComponentsStatUIs.value = false;
            this.foldoutComponentsStatusEffectListUIs.value = false;
            this.foldoutComponentsStatusEffectUIs.value = false;
            this.foldoutComponentsTraits.value = false;
        }

        protected override void ShowAllScriptableObjects()
        {
            this.foldoutScriptableObjectsAttributes.value = true;
            this.foldoutScriptableObjectsClasses.value = true;
            this.foldoutScriptableObjectsFormulas.value = true;
            this.foldoutScriptableObjectsStats.value = true;
            this.foldoutScriptableObjectsStatusEffects.value = true;
            this.foldoutScriptableObjectsTables.value = true;
        }

        protected override void HideAllScriptableObjects()
        {
            this.foldoutScriptableObjectsAttributes.value = false;
            this.foldoutScriptableObjectsClasses.value = false;
            this.foldoutScriptableObjectsFormulas.value = false;
            this.foldoutScriptableObjectsStats.value = false;
            this.foldoutScriptableObjectsStatusEffects.value = false;
            this.foldoutScriptableObjectsTables.value = false;
        }

        protected virtual void OnContentSelectItemAttributeUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "AttributeUI", this.listViewComponentsAttributeUIs.selectedIndex);
        }

        protected virtual void OnContentSelectItemFormulaUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "FormulaUI", this.listViewComponentsFormulaUIs.selectedIndex);
        }

        protected virtual void OnContentSelectItemStatUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "StatUI", this.listViewComponentsStatUIs.selectedIndex);
        }

        protected virtual void OnContentSelectItemStatusEffectListUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "StatusEffectListUI", this.listViewComponentsStatusEffectListUIs.selectedIndex);
        }

        protected virtual void OnContentSelectItemStatusEffectUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "StatusEffectUI", this.listViewComponentsStatusEffectUIs.selectedIndex);
        }

        protected virtual void OnContentSelectItemTrait(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Trait", this.listViewComponentsTraits.selectedIndex);
        }

        protected virtual void OnContentSelectItemAttribute(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Attribute", this.listViewScriptableObjectsAttributes.selectedIndex);
        }

        protected virtual void OnContentSelectItemClass(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Class", this.listViewScriptableObjectsClasses.selectedIndex);
        }

        protected virtual void OnContentSelectItemFormula(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Formula", this.listViewScriptableObjectsFormulas.selectedIndex);
        }

        protected virtual void OnContentSelectItemStat(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Stat", this.listViewScriptableObjectsStats.selectedIndex);
        }

        protected virtual void OnContentSelectItemStatusEffect(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Status Effect", this.listViewScriptableObjectsStatusEffects.selectedIndex);
        }

        protected virtual void OnContentSelectItemTable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Table", this.listViewScriptableObjectsTables.selectedIndex);
        }

        protected virtual void OnContentChooseItemAttributeUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "AttributeUI", this.listViewComponentsAttributeUIs.selectedIndex);
        }

        protected virtual void OnContentChooseItemFormulaUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "FormulaUI", this.listViewComponentsFormulaUIs.selectedIndex);
        }

        protected virtual void OnContentChooseItemStatUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "StatUI", this.listViewComponentsStatUIs.selectedIndex);
        }

        protected virtual void OnContentChooseItemStatusEffectListUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "StatusEffectListUI", this.listViewComponentsStatusEffectListUIs.selectedIndex);
        }

        protected virtual void OnContentChooseItemStatusEffectUI(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "StatusEffectUI", this.listViewComponentsStatusEffectUIs.selectedIndex);
        }

        protected virtual void OnContentChooseItemTrait(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Trait", this.listViewComponentsTraits.selectedIndex);
        }

        protected virtual void OnContentChooseItemAttribute(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Attribute", this.listViewScriptableObjectsAttributes.selectedIndex);
        }

        protected virtual void OnContentChooseItemClass(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Class", this.listViewScriptableObjectsClasses.selectedIndex);
        }

        protected virtual void OnContentChooseItemFormula(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Formula", this.listViewScriptableObjectsFormulas.selectedIndex);
        }

        protected virtual void OnContentChooseItemStat(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Stat", this.listViewScriptableObjectsFormulas.selectedIndex);
        }

        protected virtual void OnContentChooseItemStatusEffect(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Status Effect", this.listViewScriptableObjectsStatusEffects.selectedIndex);
        }

        protected virtual void OnContentChooseItemTable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Table", this.listViewScriptableObjectsTables.selectedIndex);
        }
    }
}