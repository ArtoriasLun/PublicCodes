using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowPreferencesInventory : GameCreatorFinder2WindowPreferences
    {
        protected new const int MIN_HEIGHT = 750;

        protected new const string MENU_TITLE = "GC2 Finder [Inventory] - Preferences";

        protected const string LABEL_TOGGLE_COMPONENTS_BAG_STOCK = "Bag Stock Names";
        protected const string LABEL_TOGGLE_COMPONENTS_BAG_WEALTH = "Bag Wealth Names";

        protected const string LABEL_TOGGLE_COMPONENTS_MERCHANT_NAMES = "Merchant Names";
        protected const string LABEL_TOGGLE_COMPONENTS_MERCHANT_DESCRIPTION = "Merchant Descriptions";

        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_CURRENCY_NAMES = "Currency Names";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_EQUIPMENT_NAMES = "Equipment Names";

        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_NAMES = "Item Names";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_DESCRIPTIONS = "Item Descriptions";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_PROPERTIES = "Item Properties";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_SOCKETS = "Item Sockets";
        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_INGREDIENTS = "Item Ingredients";

        protected const string LABEL_TOGGLE_SCRIPTABLEOBJECTS_LOOTTABLE_NAMES = "Loot Table Names";

        protected const string KEY_COMPONENTS_BAGS_STOCK = "gcfinder2:componentsbagsstock";
        protected const string KEY_COMPONENTS_BAGS_DESCRIPTIONS = "gcfinder2:componentsbagswealth";
        protected const string KEY_COMPONENTS_MERCHANT_NAMES = "gcfinder2:componentsmerchantnames";
        protected const string KEY_COMPONENTS_MERCHANT_DESCRIPTIONS = "gcfinder2:componentsmerchantdescriptions";
        
        protected const string KEY_SCRIPABTLEOBJECTS_CURRENCY_NAMES = "gcfinder2:scriptableobjectscurrencynames";
        protected const string KEY_SCRIPABTLEOBJECTS_EQUIPMENT_NAMES = "gcfinder2:scriptableobjectsequipmentnames";
        protected const string KEY_SCRIPABTLEOBJECTS_ITEM_NAMES = "gcfinder2:scriptableobjectsitemnames";
        protected const string KEY_SCRIPABTLEOBJECTS_ITEM_DESCRIPTIONS = "gcfinder2:scriptableobjectsitemdescriptions";
        protected const string KEY_SCRIPABTLEOBJECTS_ITEM_PROPERTIES = "gcfinder2:scriptableobjectsitemproperties";
        protected const string KEY_SCRIPABTLEOBJECTS_ITEM_SOCKETS = "gcfinder2:scriptableobjectsitemsockets";
        protected const string KEY_SCRIPABTLEOBJECTS_ITEM_INGREDIENTS = "gcfinder2:scriptableobjectsitemingredients";
        protected const string KEY_SCRIPABTLEOBJECTS_LOOTTABLE_NAMES = "gcfinder2:scriptableobjectsloottablenames";

        protected Toggle toggleComponentsBagStock;
        protected Toggle toggleComponentsBagWealth;
        protected Toggle toggleComponentsMerchantNames;
        protected Toggle toggleComponentsMerchantDescriptions;
        protected Toggle toggleScriptableObjectsCurrencyNames;
        protected Toggle toggleScriptableObjectsEquipmentNames;
        protected Toggle toggleScriptableObjectsItemNames;
        protected Toggle toggleScriptableObjectsItemDescriptions;
        protected Toggle toggleScriptableObjectsItemProperties;
        protected Toggle toggleScriptableObjectsItemSockets;
        protected Toggle toggleScriptableObjectsItemIngredients;
        protected Toggle toggleScriptableObjectsLootTableNames;

        public static bool ComponentsBagStock
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_BAGS_STOCK);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_BAGS_STOCK, value);
        }

        public static bool ComponentsBagWealth
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_BAGS_DESCRIPTIONS);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_BAGS_DESCRIPTIONS, value);
        }

        public static bool ComponentsMerchantNames
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_MERCHANT_NAMES);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_MERCHANT_NAMES, value);
        }

        public static bool ComponentsMerchantDescriptions
        {
            get => EditorPrefs.GetBool(KEY_COMPONENTS_MERCHANT_DESCRIPTIONS);
            protected set => EditorPrefs.SetBool(KEY_COMPONENTS_MERCHANT_DESCRIPTIONS, value);
        }

        public static bool ScriptableObjectsCurrencyNames
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_CURRENCY_NAMES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_CURRENCY_NAMES, value);
        }

        public static bool ScriptableObjectsEquipmentNames
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_EQUIPMENT_NAMES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_EQUIPMENT_NAMES, value);
        }

        public static bool ScriptableObjectsItemNames
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_ITEM_NAMES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_ITEM_NAMES, value);
        }

        public static bool ScriptableObjectsItemDescriptions
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_ITEM_DESCRIPTIONS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_ITEM_DESCRIPTIONS, value);
        }

        public static bool ScriptableObjectsItemProperties
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_ITEM_PROPERTIES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_ITEM_PROPERTIES, value);
        }

        public static bool ScriptableObjectsItemSockets
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_ITEM_SOCKETS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_ITEM_SOCKETS, value);
        }

        public static bool ScriptableObjectsItemIngredients
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_ITEM_INGREDIENTS);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_ITEM_INGREDIENTS, value);
        }

        public static bool ScriptableObjectsLootTableNames
        {
            get => EditorPrefs.GetBool(KEY_SCRIPABTLEOBJECTS_LOOTTABLE_NAMES);
            protected set => EditorPrefs.SetBool(KEY_SCRIPABTLEOBJECTS_LOOTTABLE_NAMES, value);
        }

        public static new void OpenWindow()
        {
            if (WINDOW != null) WINDOW.Close();

            WINDOW = GetWindow<GameCreatorFinder2WindowPreferencesInventory>(true, MENU_TITLE, true);
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void AddComponentSettings()
        {
            base.AddComponentSettings();

            this.toggleComponentsBagStock = new Toggle(LABEL_TOGGLE_COMPONENTS_BAG_STOCK) { value = ComponentsBagStock };
            this.bodyComponents.Add(this.toggleComponentsBagStock);

            this.toggleComponentsBagWealth = new Toggle(LABEL_TOGGLE_COMPONENTS_BAG_WEALTH) { value = ComponentsBagWealth };
            this.bodyComponents.Add(this.toggleComponentsBagWealth);

            this.toggleComponentsMerchantNames = new Toggle(LABEL_TOGGLE_COMPONENTS_MERCHANT_NAMES) { value = ComponentsMerchantNames };
            this.bodyComponents.Add(this.toggleComponentsMerchantNames);

            this.toggleComponentsMerchantDescriptions= new Toggle(LABEL_TOGGLE_COMPONENTS_MERCHANT_DESCRIPTION) { value = ComponentsMerchantDescriptions };
            this.bodyComponents.Add(this.toggleComponentsMerchantDescriptions);
        }

        protected override void AddScriptableObjectSettings()
        {
            base.AddScriptableObjectSettings();

            this.toggleScriptableObjectsCurrencyNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_CURRENCY_NAMES) { value = ScriptableObjectsCurrencyNames };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsCurrencyNames);

            this.toggleScriptableObjectsEquipmentNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_EQUIPMENT_NAMES) { value = ScriptableObjectsEquipmentNames };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsEquipmentNames);

            this.toggleScriptableObjectsItemNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_NAMES) { value = ScriptableObjectsItemNames };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsItemNames);

            this.toggleScriptableObjectsItemDescriptions = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_DESCRIPTIONS) { value = ScriptableObjectsItemDescriptions };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsItemDescriptions);

            this.toggleScriptableObjectsItemProperties = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_PROPERTIES) { value = ScriptableObjectsItemProperties };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsItemProperties);

            this.toggleScriptableObjectsItemSockets = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_SOCKETS) { value = ScriptableObjectsItemSockets };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsItemSockets);

            this.toggleScriptableObjectsItemIngredients = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_ITEM_INGREDIENTS) { value = ScriptableObjectsItemIngredients };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsItemIngredients);

            this.toggleScriptableObjectsLootTableNames = new Toggle(LABEL_TOGGLE_SCRIPTABLEOBJECTS_LOOTTABLE_NAMES) { value = ScriptableObjectsLootTableNames };
            this.bodyScriptableObjects.Add(this.toggleScriptableObjectsLootTableNames);
        }

        protected override void ApplyProperties()
        {
            base.ApplyProperties();

            ComponentsBagStock = this.toggleComponentsBagStock.value;
            ComponentsBagWealth = this.toggleComponentsBagWealth.value;
            ComponentsMerchantNames = this.toggleComponentsMerchantNames.value;
            ComponentsMerchantDescriptions = this.toggleComponentsMerchantDescriptions.value;

            ScriptableObjectsCurrencyNames = this.toggleScriptableObjectsCurrencyNames.value;
            ScriptableObjectsEquipmentNames = this.toggleScriptableObjectsEquipmentNames.value;
            ScriptableObjectsItemNames = this.toggleScriptableObjectsItemNames.value;
            ScriptableObjectsItemDescriptions = this.toggleScriptableObjectsItemDescriptions.value;
            ScriptableObjectsItemProperties = this.toggleScriptableObjectsItemProperties.value;
            ScriptableObjectsItemSockets = this.toggleScriptableObjectsItemSockets.value;
            ScriptableObjectsItemIngredients = this.toggleScriptableObjectsItemIngredients.value;
            ScriptableObjectsLootTableNames = this.toggleScriptableObjectsLootTableNames.value;
        }
    }
}