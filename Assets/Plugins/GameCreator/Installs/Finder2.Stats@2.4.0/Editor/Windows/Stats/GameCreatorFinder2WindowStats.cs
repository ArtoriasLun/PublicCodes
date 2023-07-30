using GameCreator.Runtime.Stats;
using GameCreator.Runtime.Stats.UnityUI;
using UnityEditor;
using UnityEngine;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowStats : GameCreatorFinder2WindowTemplate
    {
        protected const string MENU_ITEM = "Game Creator/Finder/Stats";
        protected const string MENU_TITLE = "GC2 Finder [Stats]";

        internal static AttributeUI[] ATTRIBUTEUI_FOUND;
        internal static FormulaUI[] FORMULAUI_FOUND;
        internal static StatUI[] STATUI_FOUND;
        internal static StatusEffectListUI[] STATUSEFFECTLISTUI_FOUND;
        internal static StatusEffectUI[] STATUSEFFECTUI_FOUND;
        internal static Traits[] TRAITS_FOUND;

        internal static Attribute[] ATTRIBUTES_FOUND;
        internal static Class[] CLASSES_FOUND;
        internal static Formula[] FORMULAS_FOUND;
        internal static Stat[] STATS_FOUND;
        internal static StatusEffect[] STATUSEFFECTS_FOUND;
        internal static Table[] TABLES_FOUND;

        [MenuItem(MENU_ITEM)]
        public static void OpenWindow()
        {
            WINDOW = GetWindow<GameCreatorFinder2WindowStats>();
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void Build()
        {
            this.Toolbar = new GameCreatorFinder2WindowToolbarStats(this) { name = NAME_TOOLBAR };
            this.Content = new GameCreatorFinder2WindowContentStats(this) { name = NAME_CONTENT };

            this.rootVisualElement.Add(this.Toolbar);
            this.rootVisualElement.Add(this.Content);

            this.Toolbar.OnEnable();
            ((GameCreatorFinder2WindowContentStats)this.Content).OnEnable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            this.titleContent = new GUIContent(MENU_TITLE, iconWindow.Texture);
        }

        protected override void FindComponents()
        {
            this.FindAttributeUIs();
            this.FindFormulaUIs();
            this.FindStatUIs();
            this.FindStatusEffectListUIs();
            this.FindStatusEffectUIs();
            this.FindTraits();
        }

        protected override void FindScriptableObjects()
        {
            this.FindAttributes();
            this.FindClasses();
            this.FindFormulas();
            this.FindStats();
            this.FindStatusEffects();
            this.FindTables();
        }

        protected virtual void FindAttributeUIs()
        {
            ATTRIBUTEUI_FOUND = FindObjectsOfType<AttributeUI>();
        }

        protected virtual void FindFormulaUIs()
        {
            FORMULAUI_FOUND = FindObjectsOfType<FormulaUI>();
        }

        protected virtual void FindStatUIs()
        {
            STATUI_FOUND = FindObjectsOfType<StatUI>();
        }

        protected virtual void FindStatusEffectListUIs()
        {
            STATUSEFFECTLISTUI_FOUND = FindObjectsOfType<StatusEffectListUI>();
        }

        protected virtual void FindStatusEffectUIs()
        {
            STATUSEFFECTUI_FOUND = FindObjectsOfType<StatusEffectUI>();
        }

        protected virtual void FindTraits()
        {
            TRAITS_FOUND = FindObjectsOfType<Traits>();
        }

        protected virtual void FindAttributes()
        {
            string[] attributesFound = AssetDatabase.FindAssets("t:Attribute");

            ATTRIBUTES_FOUND = new Attribute[attributesFound.Length];

            for (int i = 0; i < attributesFound.Length; i++)
            {
                string guid = attributesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ATTRIBUTES_FOUND[i] = (Attribute)AssetDatabase.LoadAssetAtPath(path, typeof(Attribute));
            }
        }

        protected virtual void FindClasses()
        {
            string[] classesFound = AssetDatabase.FindAssets("t:Class");

            CLASSES_FOUND = new Class[classesFound.Length];

            for (int i = 0; i < classesFound.Length; i++)
            {
                string guid = classesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                CLASSES_FOUND[i] = (Class)AssetDatabase.LoadAssetAtPath(path, typeof(Class));
            }
        }

        protected virtual void FindFormulas()
        {
            string[] formulasFound = AssetDatabase.FindAssets("t:Formula");

            FORMULAS_FOUND = new Formula[formulasFound.Length];

            for (int i = 0; i < formulasFound.Length; i++)
            {
                string guid = formulasFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                FORMULAS_FOUND[i] = (Formula)AssetDatabase.LoadAssetAtPath(path, typeof(Formula));
            }
        }

        protected virtual void FindStats()
        {
            string[] statusEffectsFound = AssetDatabase.FindAssets("t:Stat");

            STATS_FOUND = new Stat[statusEffectsFound.Length];

            for (int i = 0; i < statusEffectsFound.Length; i++)
            {
                string guid = statusEffectsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                STATS_FOUND[i] = (Stat)AssetDatabase.LoadAssetAtPath(path, typeof(Stat));
            }
        }

        protected virtual void FindStatusEffects()
        {
            string[] tablesFound = AssetDatabase.FindAssets("t:StatusEffect");

            STATUSEFFECTS_FOUND = new StatusEffect[tablesFound.Length];

            for (int i = 0; i < tablesFound.Length; i++)
            {
                string guid = tablesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                STATUSEFFECTS_FOUND[i] = (StatusEffect)AssetDatabase.LoadAssetAtPath(path, typeof(StatusEffect));
            }
        }

        protected virtual void FindTables()
        {
            string[] tablesFound = AssetDatabase.FindAssets("t:Table");

            TABLES_FOUND = new Table[tablesFound.Length];

            for (int i = 0; i < tablesFound.Length; i++)
            {
                string guid = tablesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                TABLES_FOUND[i] = (Table)AssetDatabase.LoadAssetAtPath(path, typeof(Table));
            }
        }
    }
}