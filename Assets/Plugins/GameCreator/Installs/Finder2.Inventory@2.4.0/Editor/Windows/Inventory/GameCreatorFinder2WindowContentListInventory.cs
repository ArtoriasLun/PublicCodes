using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentListInventory : GameCreatorFinder2WindowContentList
    {
        protected static readonly IIcon ICON_BAG = new IconBagSolid(ColorTheme.Type.Yellow);
        protected static readonly IIcon ICON_MERCHANT = new IconMerchant(ColorTheme.Type.Purple);
        protected static readonly IIcon ICON_TINKER = new IconCraft(ColorTheme.Type.Blue);
        protected static readonly IIcon ICON_CURRENCY = new IconCurrency(ColorTheme.Type.Yellow);
        protected static readonly IIcon ICON_EQUIPMENT = new IconEquipment(ColorTheme.Type.Blue);
        protected static readonly IIcon ICON_ITEM = new IconItem(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_LOOTTABLE = new IconLoot(ColorTheme.Type.Red);

        private readonly GameCreatorFinder2WindowInventory window;

        internal List<Bag> listComponentsBags = new List<Bag>();
        internal List<Bag> listComponentsBagsFiltered = new List<Bag>();
        internal List<Merchant> listComponentsMerchants = new List<Merchant>();
        internal List<Merchant> listComponentsMerchantsFiltered = new List<Merchant>();

        internal List<BagSkin> listScriptableObjectsBagSkins = new List<BagSkin>();
        internal List<BagSkin> listScriptableObjectsBagSkinsFiltered = new List<BagSkin>();
        internal List<MerchantSkin> listScriptableObjectsMerchantSkins = new List<MerchantSkin>();
        internal List<MerchantSkin> listScriptableObjectsMerchantSkinsFiltered = new List<MerchantSkin>();
        internal List<TinkerSkin> listScriptableObjectsTinkerSkins = new List<TinkerSkin>();
        internal List<TinkerSkin> listScriptableObjectsTinkerSkinsFiltered = new List<TinkerSkin>();
        internal List<Currency> listScriptableObjectsCurrencies = new List<Currency>();
        internal List<Currency> listScriptableObjectsCurrenciesFiltered = new List<Currency>();
        internal List<Equipment> listScriptableObjectsEquipments = new List<Equipment>();
        internal List<Equipment> listScriptableObjectsEquipmentsFiltered = new List<Equipment>();
        internal List<Item> listScriptableObjectsItems = new List<Item>();
        internal List<Item> listScriptableObjectsItemsFiltered = new List<Item>();
        internal List<LootTable> listScriptableObjectsLootTables = new List<LootTable>();
        internal List<LootTable> listScriptableObjectsLootTablesFiltered = new List<LootTable>();

        protected ListView listViewComponentsBags;
        protected ListView listViewComponentsMerchants;

        protected ListView listViewScriptableObjectsBagSkins;
        protected ListView listViewScriptableObjectsMerchantSkins;
        protected ListView listViewScriptableObjectsTinkerSkins;
        protected ListView listViewScriptableObjectsCurrencies;
        protected ListView listViewScriptableObjectsEquipments;
        protected ListView listViewScriptableObjectsItems;
        protected ListView listViewScriptableObjectsLootTables;

        protected Foldout foldoutComponentsBags = new Foldout() { text = "Bags", value = false };
        protected Foldout foldoutComponentsMerchants = new Foldout() { text = "Merchants", value = false };

        protected Foldout foldoutScriptableObjectsBagSkins = new Foldout() { text = "Bag Skins", value = false };
        protected Foldout foldoutScriptableObjectsMerchantSkins = new Foldout() { text = "Merchant Skins", value = false };
        protected Foldout foldoutScriptableObjectsTinkerSkins = new Foldout() { text = "Tinker Skins", value = false };
        protected Foldout foldoutScriptableObjectsCurrencies = new Foldout() { text = "Currencies", value = false };
        protected Foldout foldoutScriptableObjectsEquipments = new Foldout() { text = "Equipment", value = false };
        protected Foldout foldoutScriptableObjectsItems = new Foldout() { text = "Items", value = false };
        protected Foldout foldoutScriptableObjectsLootTables = new Foldout() { text = "Loot tables", value = false };

        public GameCreatorFinder2WindowContentListInventory(GameCreatorFinder2WindowInventory window) : base(window)
        {
            this.window = window;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            this.listViewComponentsBags.selectionChanged -= OnContentSelectItemBag;
            this.listViewComponentsMerchants.selectionChanged -= OnContentSelectItemMerchant;

            this.listViewScriptableObjectsBagSkins.selectionChanged -= OnContentSelectItemBagSkin;
            this.listViewScriptableObjectsMerchantSkins.selectionChanged -= OnContentSelectItemMerchantSkin;
            this.listViewScriptableObjectsTinkerSkins.selectionChanged -= OnContentSelectItemTinkerSkin;
            this.listViewScriptableObjectsCurrencies.selectionChanged -= OnContentSelectItemCurrency;
            this.listViewScriptableObjectsEquipments.selectionChanged -= OnContentSelectItemEquipment;
            this.listViewScriptableObjectsItems.selectionChanged -= OnContentSelectItemItem;
            this.listViewScriptableObjectsLootTables.selectionChanged -= OnContentSelectItemLootTable;

            this.listViewComponentsBags.itemsChosen -= OnContentChooseItemBag;
            this.listViewComponentsMerchants.itemsChosen -= OnContentChooseItemMerchant;

            this.listViewScriptableObjectsBagSkins.itemsChosen -= OnContentChooseItemBagSkin;
            this.listViewScriptableObjectsMerchantSkins.itemsChosen -= OnContentChooseItemMerchantSkin;
            this.listViewScriptableObjectsTinkerSkins.itemsChosen -= OnContentChooseItemTinkerSkin;
            this.listViewScriptableObjectsCurrencies.itemsChosen -= OnContentChooseItemCurrency;
            this.listViewScriptableObjectsEquipments.itemsChosen -= OnContentChooseItemEquipment;
            this.listViewScriptableObjectsItems.itemsChosen -= OnContentChooseItemItem;
            this.listViewScriptableObjectsLootTables.itemsChosen -= OnContentChooseItemLootTable;
        }

        protected override void SetupListComponents()
        {
            this.InitializeListComponents();

            this.SetupListComponentsBags();
            this.SetupListComponentsMerchants();
        }

        protected virtual void SetupListComponentsBags()
        {
            this.listViewComponentsBags = new ListView(this.listComponentsBagsFiltered, 24, this.MakeItem, this.BindItemBag)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsBags.selectionChanged += OnContentSelectItemBag;
            this.listViewComponentsBags.itemsChosen += OnContentChooseItemBag;

            this.foldoutComponentsBags.Add(this.listViewComponentsBags);
        }

        protected virtual void SetupListComponentsMerchants()
        {
            this.listViewComponentsMerchants = new ListView(this.listComponentsMerchantsFiltered, 24, this.MakeItem, this.BindItemMerchant)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsMerchants.selectionChanged += OnContentSelectItemMerchant;
            this.listViewComponentsMerchants.itemsChosen += OnContentChooseItemMerchant;

            this.foldoutComponentsMerchants.Add(this.listViewComponentsMerchants);
        }

        protected override void SetupListScriptableObjects()
        {
            this.InitializeListScriptableObjects();

            this.SetupListScriptableObjectsBagSkins();
            this.SetupListScriptableObjectsMerchantSkins();
            this.SetupListScriptableObjectsTinkerSkins();
            this.SetupListScriptableObjectsCurrencies();
            this.SetupListScriptableObjectsEquipments();
            this.SetupListScriptableObjectsItems();
            this.SetupListScriptableObjectsLootTables();
        }

        protected virtual void SetupListScriptableObjectsBagSkins()
        {
            this.listViewScriptableObjectsBagSkins = new ListView(this.listScriptableObjectsBagSkinsFiltered, 24, this.MakeItem, this.BindItemBagSkin)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsBagSkins.selectionChanged += OnContentSelectItemBagSkin;
            this.listViewScriptableObjectsBagSkins.itemsChosen += OnContentChooseItemBagSkin;

            this.foldoutScriptableObjectsBagSkins.Add(this.listViewScriptableObjectsBagSkins);
        }

        protected virtual void SetupListScriptableObjectsMerchantSkins()
        {
            this.listViewScriptableObjectsMerchantSkins = new ListView(this.listScriptableObjectsMerchantSkinsFiltered, 24, this.MakeItem, this.BindItemMerchantSkin)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsMerchantSkins.selectionChanged += OnContentSelectItemMerchantSkin;
            this.listViewScriptableObjectsMerchantSkins.itemsChosen += OnContentChooseItemMerchantSkin;

            this.foldoutScriptableObjectsMerchantSkins.Add(this.listViewScriptableObjectsMerchantSkins);
        }

        protected virtual void SetupListScriptableObjectsTinkerSkins()
        {
            this.listViewScriptableObjectsTinkerSkins = new ListView(this.listScriptableObjectsTinkerSkinsFiltered, 24, this.MakeItem, this.BindItemTinkerSkin)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsTinkerSkins.selectionChanged += OnContentSelectItemTinkerSkin;
            this.listViewScriptableObjectsTinkerSkins.itemsChosen += OnContentChooseItemTinkerSkin;

            this.foldoutScriptableObjectsTinkerSkins.Add(this.listViewScriptableObjectsTinkerSkins);
        }

        protected virtual void SetupListScriptableObjectsCurrencies()
        {
            this.listViewScriptableObjectsCurrencies = new ListView(this.listScriptableObjectsCurrenciesFiltered, 24, this.MakeItem, this.BindItemCurrency)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsCurrencies.selectionChanged += OnContentSelectItemCurrency;
            this.listViewScriptableObjectsCurrencies.itemsChosen += OnContentChooseItemCurrency;

            this.foldoutScriptableObjectsCurrencies.Add(this.listViewScriptableObjectsCurrencies);
        }

        protected virtual void SetupListScriptableObjectsEquipments()
        {
            this.listViewScriptableObjectsEquipments = new ListView(this.listScriptableObjectsEquipmentsFiltered, 24, this.MakeItem, this.BindItemEquipment)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsEquipments.selectionChanged += OnContentSelectItemEquipment;
            this.listViewScriptableObjectsEquipments.itemsChosen += OnContentChooseItemEquipment;

            this.foldoutScriptableObjectsEquipments.Add(this.listViewScriptableObjectsEquipments);
        }

        protected virtual void SetupListScriptableObjectsItems()
        {
            this.listViewScriptableObjectsItems = new ListView(this.listScriptableObjectsItemsFiltered, 24, this.MakeItem, this.BindItemItem)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsItems.selectionChanged += OnContentSelectItemItem;
            this.listViewScriptableObjectsItems.itemsChosen += OnContentChooseItemItem;

            this.foldoutScriptableObjectsItems.Add(this.listViewScriptableObjectsItems);
        }

        protected virtual void SetupListScriptableObjectsLootTables()
        {
            this.listViewScriptableObjectsLootTables = new ListView(this.listScriptableObjectsLootTablesFiltered, 24, this.MakeItem, this.BindItemLootTable)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsLootTables.selectionChanged += OnContentSelectItemLootTable;
            this.listViewScriptableObjectsLootTables.itemsChosen += OnContentChooseItemLootTable;

            this.foldoutScriptableObjectsLootTables.Add(this.listViewScriptableObjectsLootTables);
        }

        protected virtual void BindItemBag(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsBagsFiltered[index].GetType().Name switch
            {
                "Bag" => ICON_BAG,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsBagsFiltered[index].name} (ID {this.listComponentsBagsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemMerchant(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsMerchantsFiltered[index].GetType().Name switch
            {
                "Merchant" => ICON_MERCHANT,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsMerchantsFiltered[index].name} (ID {this.listComponentsMerchantsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemBagSkin(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsBagSkinsFiltered[index].GetType().Name switch
            {
                "BagSkin" => ICON_BAG,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsBagSkinsFiltered[index].name} (ID {this.listScriptableObjectsBagSkinsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemMerchantSkin(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsMerchantSkinsFiltered[index].GetType().Name switch
            {
                "MerchantSkin" => ICON_MERCHANT,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsMerchantSkinsFiltered[index].name} (ID {this.listScriptableObjectsMerchantSkinsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemTinkerSkin(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsTinkerSkinsFiltered[index].GetType().Name switch
            {
                "TinkerSkin" => ICON_TINKER,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsTinkerSkinsFiltered[index].name} (ID {this.listScriptableObjectsTinkerSkinsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemCurrency(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsCurrenciesFiltered[index].GetType().Name switch
            {
                "Currency" => ICON_CURRENCY,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsCurrenciesFiltered[index].name} (ID {this.listScriptableObjectsCurrenciesFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemEquipment(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsEquipmentsFiltered[index].GetType().Name switch
            {
                "Equipment" => ICON_EQUIPMENT,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsEquipmentsFiltered[index].name} (ID {this.listScriptableObjectsEquipmentsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemItem(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsItemsFiltered[index].GetType().Name switch
            {
                "Item" => ICON_ITEM,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsItemsFiltered[index].name} (ID {this.listScriptableObjectsItemsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemLootTable(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsLootTablesFiltered[index].GetType().Name switch
            {
                "LootTable" => ICON_LOOTTABLE,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsLootTablesFiltered[index].name} (ID {this.listScriptableObjectsLootTablesFiltered[index].GetInstanceID()})";
        }

        public override void InitializeListComponents()
        {
            this.listComponentsBags.AddRange(GameCreatorFinder2WindowInventory.BAGS_FOUND);
            this.listComponentsMerchants.AddRange(GameCreatorFinder2WindowInventory.MERCHANTS_FOUND);
        }

        public override void InitializeListScriptableObjects()
        {
            this.listScriptableObjectsBagSkins.AddRange(GameCreatorFinder2WindowInventory.BAGSKINS_FOUND);
            this.listScriptableObjectsMerchantSkins.AddRange(GameCreatorFinder2WindowInventory.MERCHANTSKINS_FOUND);
            this.listScriptableObjectsTinkerSkins.AddRange(GameCreatorFinder2WindowInventory.TINKERSKINS_FOUND);
            this.listScriptableObjectsCurrencies.AddRange(GameCreatorFinder2WindowInventory.CURRENCIES_FOUND);
            this.listScriptableObjectsEquipments.AddRange(GameCreatorFinder2WindowInventory.EQUIPMENTS_FOUND);
            this.listScriptableObjectsItems.AddRange(GameCreatorFinder2WindowInventory.ITEMS_FOUND);
            this.listScriptableObjectsLootTables.AddRange(GameCreatorFinder2WindowInventory.LOOTTABLES_FOUND);
        }

        public override void RemoveListComponents()
        {
            this.listComponentsBags = new List<Bag>();
            this.listComponentsMerchants = new List<Merchant>();
        }

        protected override void RemoveListScriptableObjects()
        {
            this.listScriptableObjectsBagSkins = new List<BagSkin>();
            this.listScriptableObjectsMerchantSkins = new List<MerchantSkin>();
            this.listScriptableObjectsTinkerSkins = new List<TinkerSkin>();
            this.listScriptableObjectsCurrencies = new List<Currency>();
            this.listScriptableObjectsEquipments = new List<Equipment>();
            this.listScriptableObjectsItems = new List<Item>();
            this.listScriptableObjectsLootTables = new List<LootTable>();
        }

        protected override void RemoveListComponentsElements()
        {
            try { this.content.Remove(this.foldoutComponentsBags); } catch { }
            try { this.foldoutComponentsBags.Remove(this.listViewComponentsBags); } catch { }

            try { this.content.Remove(this.foldoutComponentsMerchants); } catch { }
            try { this.foldoutComponentsMerchants.Remove(this.listViewComponentsMerchants); } catch { }
        }

        protected override void RemoveListScriptableObjectsElements()
        {
            try { this.content.Remove(this.foldoutScriptableObjectsBagSkins); } catch { }
            try { this.foldoutScriptableObjectsBagSkins.Remove(this.listViewScriptableObjectsBagSkins); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsMerchantSkins); } catch { }
            try { this.foldoutScriptableObjectsMerchantSkins.Remove(this.listViewScriptableObjectsMerchantSkins); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsTinkerSkins); } catch { }
            try { this.foldoutScriptableObjectsTinkerSkins.Remove(this.listViewScriptableObjectsTinkerSkins); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsCurrencies); } catch { }
            try { this.foldoutScriptableObjectsCurrencies.Remove(this.listViewScriptableObjectsCurrencies); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsEquipments); } catch { }
            try { this.foldoutScriptableObjectsEquipments.Remove(this.listViewScriptableObjectsEquipments); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsItems); } catch { }
            try { this.foldoutScriptableObjectsItems.Remove(this.listViewScriptableObjectsItems); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsLootTables); } catch { }
            try { this.foldoutScriptableObjectsLootTables.Remove(this.listViewScriptableObjectsLootTables); } catch { }
        }

        public override void UpdateListComponents()
        {
            this.RemoveListElements();

            this.UpdateListComponentsBags();
            this.UpdateListComponentsMerchants();
        }

        protected virtual void UpdateListComponentsBags()
        {
            this.listComponentsBags = this.sortComponentsAZ ? this.listComponentsBags.OrderBy(go => go.name).ToList() : this.listComponentsBags.OrderByDescending(go => go.name).ToList();
            this.listComponentsBagsFiltered.Clear();
            
            List<Bag> listBagsFoundByName = this.listComponentsBags.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listComponentsBagsFiltered.AddRange(listBagsFoundByName);

            if (GameCreatorFinder2WindowPreferencesInventory.ComponentsBagStock)
            {
                List<Bag> listBagsFoundByStock = new List<Bag>();

                for (int i = 0; i < this.listComponentsBags.Count; i++)
                {
                    List<bool> bagsStock = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listComponentsBags[i]);
                    var list = so.FindProperty("m_Stock").FindPropertyRelative("m_Stock");
                    StockData[] stockList = list.GetValue<StockData[]>();

                    for (int j = 0; j < stockList.Length; j++)
                    {
                        bagsStock.Add(stockList[j].Item.ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (bagsStock.Contains(true)) listBagsFoundByStock.Add(this.listComponentsBags[i]);
                }

                this.listComponentsBagsFiltered.AddRange(listBagsFoundByStock);
            }

            if (GameCreatorFinder2WindowPreferencesInventory.ComponentsBagWealth)
            {
                List<Bag> listBagsFoundByWealth = new List<Bag>();

                for (int i = 0; i < this.listComponentsBags.Count; i++)
                {
                    List<bool> bagsWealth = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listComponentsBags[i]);
                    var list = so.FindProperty("m_Stock").FindPropertyRelative("m_Wealth");
                    WealthData[] wealthList = list.GetValue<WealthData[]>();

                    for (int j = 0; j < wealthList.Length; j++)
                    {
                        bagsWealth.Add(wealthList[j].Currency.ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (bagsWealth.Contains(true)) listBagsFoundByWealth.Add(this.listComponentsBags[i]);
                }

                this.listComponentsBagsFiltered.AddRange(listBagsFoundByWealth);
            }

            this.listComponentsBagsFiltered = this.listComponentsBagsFiltered.Distinct().ToList();
            this.listComponentsBagsFiltered = this.sortComponentsAZ ? this.listComponentsBagsFiltered.OrderBy(go => go.name).ToList() : this.listComponentsBagsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listComponentsBagsFiltered.Count > 0)
            {
                this.listViewComponentsBags.itemsSource = this.listComponentsBagsFiltered;
                this.listViewComponentsBags.Rebuild();

                this.foldoutComponentsBags.Add(this.listViewComponentsBags);

                this.content.Add(this.foldoutComponentsBags);
            }
        }

        protected virtual void UpdateListComponentsMerchants()
        {
            this.listComponentsMerchants = this.sortComponentsAZ ? this.listComponentsMerchants.OrderBy(go => go.name).ToList() : this.listComponentsMerchants.OrderByDescending(go => go.name).ToList();
            this.listComponentsMerchantsFiltered.Clear();

            List<Merchant> listMerchantsFoundByName = this.listComponentsMerchants.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listComponentsMerchantsFiltered.AddRange(listMerchantsFoundByName);

            if (GameCreatorFinder2WindowPreferencesInventory.ComponentsMerchantNames)
            {
                List<Merchant> listMerchantsFoundByNames = new List<Merchant>();

                for (int i = 0; i < this.listComponentsMerchants.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listComponentsMerchants[i]);
                    var info = so.FindProperty("m_Info");
                    MerchantInfo merchantInfo = info.GetValue<MerchantInfo>();

                    if (merchantInfo.m_Name.Get(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()))
                    {
                        listMerchantsFoundByNames.Add(this.listComponentsMerchants[i]);
                    }
                }

                this.listComponentsMerchantsFiltered.AddRange(listMerchantsFoundByNames);
            }

            if (GameCreatorFinder2WindowPreferencesInventory.ComponentsMerchantDescriptions)
            {
                List<Merchant> listMerchantsFoundByNames = new List<Merchant>();

                for (int i = 0; i < this.listComponentsMerchants.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listComponentsMerchants[i]);
                    var info = so.FindProperty("m_Info");
                    MerchantInfo merchantInfo = info.GetValue<MerchantInfo>();

                    if (merchantInfo.m_Description.Get(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()))
                    {
                        listMerchantsFoundByNames.Add(this.listComponentsMerchants[i]);
                    }
                }

                this.listComponentsMerchantsFiltered.AddRange(listMerchantsFoundByNames);
            }

            this.listComponentsMerchantsFiltered = this.listComponentsMerchantsFiltered.Distinct().ToList();
            this.listComponentsMerchantsFiltered = this.sortComponentsAZ ? this.listComponentsMerchantsFiltered.OrderBy(go => go.name).ToList() : this.listComponentsMerchantsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listComponentsMerchantsFiltered.Count > 0)
            {
                this.listViewComponentsMerchants.itemsSource = this.listComponentsMerchantsFiltered;
                this.listViewComponentsMerchants.Rebuild();

                this.foldoutComponentsMerchants.Add(this.listViewComponentsMerchants);

                this.content.Add(this.foldoutComponentsMerchants);
            }
        }

        public override void UpdateListScriptableObjects()
        {
            this.RemoveListElements();

            this.UpdateListScriptableObjectsBagSkins();
            this.UpdateListScriptableObjectsMerchantSkins();
            this.UpdateListScriptableObjectsTinkerSkins();
            this.UpdateListScriptableObjectsCurrencies();
            this.UpdateListScriptableObjectsEquipments();
            this.UpdateListScriptableObjectsItems();
            this.UpdateListScriptableObjectsLootTables();
        }

        protected virtual void UpdateListScriptableObjectsBagSkins()
        {
            this.listScriptableObjectsBagSkins = this.sortScriptableObjectsAZ ? this.listScriptableObjectsBagSkins.OrderBy(go => go.name).ToList() : this.listScriptableObjectsBagSkins.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsBagSkinsFiltered = this.listScriptableObjectsBagSkins.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsBagSkinsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsBagSkins.itemsSource = this.listScriptableObjectsBagSkinsFiltered;
                this.listViewScriptableObjectsBagSkins.Rebuild();

                this.foldoutScriptableObjectsBagSkins.Add(this.listViewScriptableObjectsBagSkins);

                this.content.Add(this.foldoutScriptableObjectsBagSkins);
            }
        }

        protected virtual void UpdateListScriptableObjectsMerchantSkins()
        {
            this.listScriptableObjectsMerchantSkins = this.sortScriptableObjectsAZ ? this.listScriptableObjectsMerchantSkins.OrderBy(go => go.name).ToList() : this.listScriptableObjectsMerchantSkins.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsMerchantSkinsFiltered = this.listScriptableObjectsMerchantSkins.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsMerchantSkinsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsMerchantSkins.itemsSource = this.listScriptableObjectsMerchantSkinsFiltered;
                this.listViewScriptableObjectsMerchantSkins.Rebuild();

                this.foldoutScriptableObjectsMerchantSkins.Add(this.listViewScriptableObjectsMerchantSkins);

                this.content.Add(this.foldoutScriptableObjectsMerchantSkins);
            }
        }

        protected virtual void UpdateListScriptableObjectsTinkerSkins()
        {
            this.listScriptableObjectsTinkerSkins = this.sortScriptableObjectsAZ ? this.listScriptableObjectsTinkerSkins.OrderBy(go => go.name).ToList() : this.listScriptableObjectsTinkerSkins.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsTinkerSkinsFiltered = this.listScriptableObjectsTinkerSkins.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsTinkerSkinsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsTinkerSkins.itemsSource = this.listScriptableObjectsTinkerSkinsFiltered;
                this.listViewScriptableObjectsTinkerSkins.Rebuild();

                this.foldoutScriptableObjectsTinkerSkins.Add(this.listViewScriptableObjectsTinkerSkins);

                this.content.Add(this.foldoutScriptableObjectsTinkerSkins);
            }
        }

        protected virtual void UpdateListScriptableObjectsCurrencies()
        {
            this.listScriptableObjectsCurrencies = this.sortScriptableObjectsAZ ? this.listScriptableObjectsCurrencies.OrderBy(go => go.name).ToList() : this.listScriptableObjectsCurrencies.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsCurrenciesFiltered.Clear();

            List<Currency> listCurrenciesFoundByName = this.listScriptableObjectsCurrencies.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsCurrenciesFiltered.AddRange(listCurrenciesFoundByName);

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsCurrencyNames)
            {
                List<Currency> listCurrenciesFoundByNames = new List<Currency>();

                for (int i = 0; i < this.listScriptableObjectsCurrencies.Count; i++)
                {
                    List<bool> currenciesNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listScriptableObjectsCurrencies[i]);
                    var list = so.FindProperty("m_Coins").FindPropertyRelative("m_List");
                    Coin[] coinList = list.GetValue<Coin[]>();

                    for (int j = 0; j < coinList.Length; j++)
                    {
                        currenciesNames.Add(coinList[j].Name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (currenciesNames.Contains(true)) listCurrenciesFoundByNames.Add(this.listScriptableObjectsCurrencies[i]);
                }

                this.listScriptableObjectsCurrenciesFiltered.AddRange(listCurrenciesFoundByNames);
            }

            this.listScriptableObjectsCurrenciesFiltered = this.listScriptableObjectsCurrenciesFiltered.Distinct().ToList();
            this.listScriptableObjectsCurrenciesFiltered = this.sortScriptableObjectsAZ ? this.listScriptableObjectsCurrenciesFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsCurrenciesFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsCurrenciesFiltered.Count > 0)
            {
                this.listViewScriptableObjectsCurrencies.itemsSource = this.listScriptableObjectsCurrenciesFiltered;
                this.listViewScriptableObjectsCurrencies.Rebuild();

                this.foldoutScriptableObjectsCurrencies.Add(this.listViewScriptableObjectsCurrencies);

                this.content.Add(this.foldoutScriptableObjectsCurrencies);
            }
        }

        protected virtual void UpdateListScriptableObjectsEquipments()
        {
            this.listScriptableObjectsEquipments = this.sortScriptableObjectsAZ ? this.listScriptableObjectsEquipments.OrderBy(go => go.name).ToList() : this.listScriptableObjectsEquipments.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsEquipmentsFiltered.Clear();

            List<Equipment> listEquipmentsFoundByName = this.listScriptableObjectsEquipments.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsEquipmentsFiltered.AddRange(listEquipmentsFoundByName);

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsEquipmentNames)
            {
                List<Equipment> listEquipmentsFoundByNames = new List<Equipment>();

                for (int i = 0; i < this.listScriptableObjectsEquipments.Count; i++)
                {
                    List<bool> equipmentsNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listScriptableObjectsEquipments[i]);
                    var list = so.FindProperty("m_Slots").FindPropertyRelative("m_Slots");
                    EquipmentSlot[] equipmentSlotList = list.GetValue<EquipmentSlot[]>();

                    for (int j = 0; j < equipmentSlotList.Length; j++)
                    {
                        equipmentsNames.Add(equipmentSlotList[j].Base.ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (equipmentsNames.Contains(true)) listEquipmentsFoundByNames.Add(this.listScriptableObjectsEquipments[i]);
                }

                this.listScriptableObjectsEquipmentsFiltered.AddRange(listEquipmentsFoundByNames);
            }

            this.listScriptableObjectsEquipmentsFiltered = this.listScriptableObjectsEquipmentsFiltered.Distinct().ToList();
            this.listScriptableObjectsEquipmentsFiltered = this.sortScriptableObjectsAZ ? this.listScriptableObjectsEquipmentsFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsEquipmentsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsEquipmentsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsEquipments.itemsSource = this.listScriptableObjectsEquipmentsFiltered;
                this.listViewScriptableObjectsEquipments.Rebuild();

                this.foldoutScriptableObjectsEquipments.Add(this.listViewScriptableObjectsEquipments);

                this.content.Add(this.foldoutScriptableObjectsEquipments);
            }
        }

        protected virtual void UpdateListScriptableObjectsItems()
        {
            this.listScriptableObjectsItems = this.sortScriptableObjectsAZ ? this.listScriptableObjectsItems.OrderBy(go => go.name).ToList() : this.listScriptableObjectsItems.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsItemsFiltered.Clear();

            List<Item> listItemsFoundByName = this.listScriptableObjectsItems.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsItemsFiltered.AddRange(listItemsFoundByName);

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsItemNames)
            {
                List<Item> listItemsFoundByNames = new List<Item>();

                for (int i = 0; i < this.listScriptableObjectsItems.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listScriptableObjectsItems[i]);
                    Info info = so.FindProperty("m_Info").GetValue<Info>();

                    if (info.Name(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())) listItemsFoundByNames.Add(this.listScriptableObjectsItems[i]);
                }

                this.listScriptableObjectsItemsFiltered.AddRange(listItemsFoundByNames);
            }

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsItemDescriptions)
            {
                List<Item> listItemsFoundByDescriptions = new List<Item>();

                for (int i = 0; i < this.listScriptableObjectsItems.Count; i++)
                {
                    SerializedObject so = new SerializedObject(this.listScriptableObjectsItems[i]);
                    Info info = so.FindProperty("m_Info").GetValue<Info>();

                    if (info.Description(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())) listItemsFoundByDescriptions.Add(this.listScriptableObjectsItems[i]);
                }

                this.listScriptableObjectsItemsFiltered.AddRange(listItemsFoundByDescriptions);
            }

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsItemProperties)
            {
                List<Item> listItemsFoundByProperties = new List<Item>();

                for (int i = 0; i < this.listScriptableObjectsItems.Count; i++)
                {
                    List<bool> propertiesNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listScriptableObjectsItems[i]);
                    var list = so.FindProperty("m_Properties").FindPropertyRelative("m_List");
                    Property[] propertyList = list.GetValue<Property[]>();

                    for (int j = 0; j < propertyList.Length; j++)
                    {
                        propertiesNames.Add(propertyList[j].Text(Args.EMPTY).ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (propertiesNames.Contains(true)) listItemsFoundByProperties.Add(this.listScriptableObjectsItems[i]);
                }

                this.listScriptableObjectsItemsFiltered.AddRange(listItemsFoundByProperties);
            }

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsItemSockets)
            {
                List<Item> listItemsFoundBySockets = new List<Item>();

                for (int i = 0; i < this.listScriptableObjectsItems.Count; i++)
                {
                    List<bool> socketsNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listScriptableObjectsItems[i]);
                    var list = so.FindProperty("m_Sockets").FindPropertyRelative("m_List");
                    Socket[] socketList = list.GetValue<Socket[]>();

                    for (int j = 0; j < socketList.Length; j++)
                    {
                        socketsNames.Add(socketList[j].Base.ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (socketsNames.Contains(true)) listItemsFoundBySockets.Add(this.listScriptableObjectsItems[i]);
                }

                this.listScriptableObjectsItemsFiltered.AddRange(listItemsFoundBySockets);
            }

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsItemIngredients)
            {
                List<Item> listItemsFoundByIngredients = new List<Item>();

                for (int i = 0; i < this.listScriptableObjectsItems.Count; i++)
                {
                    List<bool> ingredientsNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listScriptableObjectsItems[i]);
                    var list = so.FindProperty("m_Crafting").FindPropertyRelative("m_Ingredients");
                    Ingredient[] ingredientList = list.GetValue<Ingredient[]>();

                    for (int j = 0; j < ingredientList.Length; j++)
                    {
                        ingredientsNames.Add(ingredientList[j].Item.ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (ingredientsNames.Contains(true)) listItemsFoundByIngredients.Add(this.listScriptableObjectsItems[i]);
                }

                this.listScriptableObjectsItemsFiltered.AddRange(listItemsFoundByIngredients);
            }

            this.listScriptableObjectsItemsFiltered = this.listScriptableObjectsItemsFiltered.Distinct().ToList();
            this.listScriptableObjectsItemsFiltered = this.sortScriptableObjectsAZ ? this.listScriptableObjectsItemsFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsItemsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsItemsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsItems.itemsSource = this.listScriptableObjectsItemsFiltered;
                this.listViewScriptableObjectsItems.Rebuild();

                this.foldoutScriptableObjectsItems.Add(this.listViewScriptableObjectsItems);

                this.content.Add(this.foldoutScriptableObjectsItems);
            }
        }

        protected virtual void UpdateListScriptableObjectsLootTables()
        {
            this.listScriptableObjectsLootTables = this.sortScriptableObjectsAZ ? this.listScriptableObjectsLootTables.OrderBy(go => go.name).ToList() : this.listScriptableObjectsLootTables.OrderByDescending(go => go.name).ToList();
            this.listScriptableObjectsLootTablesFiltered.Clear();

            List<LootTable> listLootTablesFoundByName = this.listScriptableObjectsLootTables.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsLootTablesFiltered.AddRange(listLootTablesFoundByName);

            if (GameCreatorFinder2WindowPreferencesInventory.ScriptableObjectsLootTableNames)
            {
                List<LootTable> listLootTablesFoundByLootNames = new List<LootTable>();

                for (int i = 0; i < this.listScriptableObjectsLootTables.Count; i++)
                {
                    List<bool> lootTablesNames = new List<bool>();

                    SerializedObject so = new SerializedObject(this.listScriptableObjectsLootTables[i]);
                    var list = so.FindProperty("m_LootList").FindPropertyRelative("m_List");
                    Loot[] lootList = list.GetValue<Loot[]>();

                    for (int j = 0; j < lootList.Length; j++)
                    {
                        if (lootList[j].IsItem) lootTablesNames.Add(lootList[j].Item.ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                        else lootTablesNames.Add(lootList[j].Currency.ToString().ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()));
                    }

                    if (lootTablesNames.Contains(true)) listLootTablesFoundByLootNames.Add(this.listScriptableObjectsLootTables[i]);
                }

                this.listScriptableObjectsLootTablesFiltered.AddRange(listLootTablesFoundByLootNames);
            }

            this.listScriptableObjectsLootTablesFiltered = this.listScriptableObjectsLootTablesFiltered.Distinct().ToList();
            this.listScriptableObjectsLootTablesFiltered = this.sortScriptableObjectsAZ ? this.listScriptableObjectsLootTablesFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsLootTablesFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsLootTablesFiltered.Count > 0)
            {
                this.listViewScriptableObjectsLootTables.itemsSource = this.listScriptableObjectsLootTablesFiltered;
                this.listViewScriptableObjectsLootTables.Rebuild();

                this.foldoutScriptableObjectsLootTables.Add(this.listViewScriptableObjectsLootTables);

                this.content.Add(this.foldoutScriptableObjectsLootTables);
            }
        }

        protected override void ShowAllComponents()
        {
            this.foldoutComponentsBags.value = true;
            this.foldoutComponentsMerchants.value = true;
        }

        protected override void HideAllComponents()
        {
            this.foldoutComponentsBags.value = false;
            this.foldoutComponentsMerchants.value = false;
        }

        protected override void ShowAllScriptableObjects()
        {
            this.foldoutScriptableObjectsBagSkins.value = true;
            this.foldoutScriptableObjectsMerchantSkins.value = true;
            this.foldoutScriptableObjectsTinkerSkins.value = true;
            this.foldoutScriptableObjectsCurrencies.value = true;
            this.foldoutScriptableObjectsEquipments.value = true;
            this.foldoutScriptableObjectsItems.value = true;
            this.foldoutScriptableObjectsLootTables.value = true;
        }

        protected override void HideAllScriptableObjects()
        {
            this.foldoutScriptableObjectsBagSkins.value = false;
            this.foldoutScriptableObjectsMerchantSkins.value = false;
            this.foldoutScriptableObjectsTinkerSkins.value = false;
            this.foldoutScriptableObjectsCurrencies.value = false;
            this.foldoutScriptableObjectsEquipments.value = false;
            this.foldoutScriptableObjectsItems.value = false;
            this.foldoutScriptableObjectsLootTables.value = false;
        }

        protected virtual void OnContentSelectItemBag(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Bag", this.listViewComponentsBags.selectedIndex);
        }

        protected virtual void OnContentSelectItemMerchant(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Merchant", this.listViewComponentsMerchants.selectedIndex);
        }

        protected virtual void OnContentSelectItemBagSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Bag Skin", this.listViewScriptableObjectsBagSkins.selectedIndex);
        }

        protected virtual void OnContentSelectItemMerchantSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Merchant Skin", this.listViewScriptableObjectsMerchantSkins.selectedIndex);
        }

        protected virtual void OnContentSelectItemTinkerSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Tinker Skin", this.listViewScriptableObjectsTinkerSkins.selectedIndex);
        }

        protected virtual void OnContentSelectItemCurrency(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Currency", this.listViewScriptableObjectsCurrencies.selectedIndex);
        }

        protected virtual void OnContentSelectItemEquipment(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Equipment", this.listViewScriptableObjectsEquipments.selectedIndex);
        }

        protected virtual void OnContentSelectItemItem(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Item", this.listViewScriptableObjectsItems.selectedIndex);
        }

        protected virtual void OnContentSelectItemLootTable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Loot Table", this.listViewScriptableObjectsLootTables.selectedIndex);
        }

        protected virtual void OnContentChooseItemBag(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Bag", this.listViewComponentsBags.selectedIndex);
        }

        protected virtual void OnContentChooseItemMerchant(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Merchant", this.listViewComponentsMerchants.selectedIndex);
        }

        protected virtual void OnContentChooseItemBagSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Bag Skin", this.listViewScriptableObjectsBagSkins.selectedIndex);
        }

        protected virtual void OnContentChooseItemMerchantSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Merchant Skin", this.listViewScriptableObjectsMerchantSkins.selectedIndex);
        }

        protected virtual void OnContentChooseItemTinkerSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Tinker Skin", this.listViewScriptableObjectsTinkerSkins.selectedIndex);
        }

        protected virtual void OnContentChooseItemCurrency(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Currency", this.listViewScriptableObjectsTinkerSkins.selectedIndex);
        }

        protected virtual void OnContentChooseItemEquipment(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Equipment", this.listViewScriptableObjectsEquipments.selectedIndex);
        }

        protected virtual void OnContentChooseItemItem(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Item", this.listViewScriptableObjectsItems.selectedIndex);
        }

        protected virtual void OnContentChooseItemLootTable(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Loot Table", this.listViewScriptableObjectsLootTables.selectedIndex);
        }
    }
}