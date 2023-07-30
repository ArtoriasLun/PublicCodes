using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEngine;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowInventory : GameCreatorFinder2WindowTemplate
    {
        protected const string MENU_ITEM = "Game Creator/Finder/Inventory";
        protected const string MENU_TITLE = "GC2 Finder [Inventory]";

        internal static Bag[] BAGS_FOUND;
        internal static Merchant[] MERCHANTS_FOUND;

        internal static BagSkin[] BAGSKINS_FOUND;
        internal static MerchantSkin[] MERCHANTSKINS_FOUND;
        internal static TinkerSkin[] TINKERSKINS_FOUND;
        internal static Currency[] CURRENCIES_FOUND;
        internal static Equipment[] EQUIPMENTS_FOUND;
        internal static Item[] ITEMS_FOUND;
        internal static LootTable[] LOOTTABLES_FOUND;

        [MenuItem(MENU_ITEM)]
        public static void OpenWindow()
        {
            WINDOW = GetWindow<GameCreatorFinder2WindowInventory>();
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void Build()
        {
            this.Toolbar = new GameCreatorFinder2WindowToolbarInventory(this) { name = NAME_TOOLBAR };
            this.Content = new GameCreatorFinder2WindowContentInventory(this) { name = NAME_CONTENT };

            this.rootVisualElement.Add(this.Toolbar);
            this.rootVisualElement.Add(this.Content);

            this.Toolbar.OnEnable();
            ((GameCreatorFinder2WindowContentInventory)this.Content).OnEnable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            this.titleContent = new GUIContent(MENU_TITLE, iconWindow.Texture);
        }

        protected override void FindComponents()
        {
            this.FindBags();
            this.FindMerchants();
        }

        protected override void FindScriptableObjects()
        {
            this.FindBagSkins();
            this.FindMerchantSkins();
            this.FindTinkerSkins();
            this.FindCurrencies();
            this.FindEquipments();
            this.FindItems();
            this.FindLootTables();
        }

        protected virtual void FindBags()
        {
            BAGS_FOUND = FindObjectsOfType<Bag>();
        }

        protected virtual void FindMerchants()
        {
            MERCHANTS_FOUND = FindObjectsOfType<Merchant>();
        }

        protected virtual void FindBagSkins()
        {
            string[] bagSkinsFound = AssetDatabase.FindAssets("t:BagSkin");

            BAGSKINS_FOUND = new BagSkin[bagSkinsFound.Length];

            for (int i = 0; i < bagSkinsFound.Length; i++)
            {
                string guid = bagSkinsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                BAGSKINS_FOUND[i] = (BagSkin)AssetDatabase.LoadAssetAtPath(path, typeof(BagSkin));
            }
        }

        protected virtual void FindMerchantSkins()
        {
            string[] merchantSkinsFound = AssetDatabase.FindAssets("t:MerchantSkin");

            MERCHANTSKINS_FOUND = new MerchantSkin[merchantSkinsFound.Length];

            for (int i = 0; i < merchantSkinsFound.Length; i++)
            {
                string guid = merchantSkinsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MERCHANTSKINS_FOUND[i] = (MerchantSkin)AssetDatabase.LoadAssetAtPath(path, typeof(MerchantSkin));
            }
        }

        protected virtual void FindTinkerSkins()
        {
            string[] tinkerSkinsFound = AssetDatabase.FindAssets("t:TinkerSkin");

            TINKERSKINS_FOUND = new TinkerSkin[tinkerSkinsFound.Length];

            for (int i = 0; i < tinkerSkinsFound.Length; i++)
            {
                string guid = tinkerSkinsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                TINKERSKINS_FOUND[i] = (TinkerSkin)AssetDatabase.LoadAssetAtPath(path, typeof(TinkerSkin));
            }
        }

        protected virtual void FindCurrencies()
        {
            string[] currenciesFound = AssetDatabase.FindAssets("t:Currency");

            CURRENCIES_FOUND = new Currency[currenciesFound.Length];

            for (int i = 0; i < currenciesFound.Length; i++)
            {
                string guid = currenciesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                CURRENCIES_FOUND[i] = (Currency)AssetDatabase.LoadAssetAtPath(path, typeof(Currency));
            }
        }

        protected virtual void FindEquipments()
        {
            string[] equipmentsFound = AssetDatabase.FindAssets("t:Equipment");

            EQUIPMENTS_FOUND = new Equipment[equipmentsFound.Length];

            for (int i = 0; i < equipmentsFound.Length; i++)
            {
                string guid = equipmentsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                EQUIPMENTS_FOUND[i] = (Equipment)AssetDatabase.LoadAssetAtPath(path, typeof(Equipment));
            }
        }

        protected virtual void FindItems()
        {
            string[] itemsFound = AssetDatabase.FindAssets("t:Item");

            ITEMS_FOUND = new Item[itemsFound.Length];

            for (int i = 0; i < itemsFound.Length; i++)
            {
                string guid = itemsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ITEMS_FOUND[i] = (Item)AssetDatabase.LoadAssetAtPath(path, typeof(Item));
            }
        }

        protected virtual void FindLootTables()
        {
            string[] lootTablesFound = AssetDatabase.FindAssets("t:LootTable");

            LOOTTABLES_FOUND = new LootTable[lootTablesFound.Length];

            for (int i = 0; i < lootTablesFound.Length; i++)
            {
                string guid = lootTablesFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                LOOTTABLES_FOUND[i] = (LootTable)AssetDatabase.LoadAssetAtPath(path, typeof(LootTable));
            }
        }
    }
}