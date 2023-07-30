using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentDetailsInventory : GameCreatorFinder2WindowContentDetails
    {
        private readonly GameCreatorFinder2WindowInventory window;

        public GameCreatorFinder2WindowContentDetailsInventory(GameCreatorFinder2WindowInventory window) : base(window)
        {
            this.window = window;
        }

        protected override void ShowComponent(string foldout, int index)
        {
            switch (foldout)
            {
                case "Bag":
                    this.ShowComponentBag(index);
                    break;
                case "Merchant":
                    this.ShowComponentMerchant(index);
                    break;
            }
        }

        protected virtual void ShowComponentBag(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement bagObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Bag),
                value = windowContentList.listComponentsBagsFiltered[index]
            };
            this.content.Add(bagObjectField);

            VisualElement bagInspector = new InspectorElement();
            bagInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listComponentsBagsFiltered[index]));
            this.content.Add(bagInspector);
        }

        protected virtual void ShowComponentMerchant(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement merchantObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Merchant),
                value = windowContentList.listComponentsMerchantsFiltered[index]
            };
            this.content.Add(merchantObjectField);

            VisualElement merchantInspector = new InspectorElement();
            merchantInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listComponentsMerchantsFiltered[index]));
            this.content.Add(merchantInspector);
        }

        protected override void ShowScriptableObject(string foldout, int index)
        {
            switch (foldout)
            {
                case "Bag Skin":
                    this.ShowScriptableObjectBagSkin(index);
                    break;
                case "Merchant Skin":
                    this.ShowScriptableObjectMerchantSkin(index);
                    break;
                case "Tinker Skin":
                    this.ShowScriptableObjectTinkerSkin(index);
                    break;
                case "Currency":
                    this.ShowScriptableObjectCurrency(index);
                    break;
                case "Equipment":
                    this.ShowScriptableObjectEquipment(index);
                    break;
                case "Item":
                    this.ShowScriptableObjectItem(index);
                    break;
                case "Loot Table":
                    this.ShowScriptableObjectLootTable(index);
                    break;
            }
        }

        protected virtual void ShowScriptableObjectBagSkin(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement bagSkinObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(BagSkin),
                value = windowContentList.listScriptableObjectsBagSkinsFiltered[index]
            };
            this.content.Add(bagSkinObjectField);

            VisualElement bagSkinInspector = new InspectorElement();
            bagSkinInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listScriptableObjectsBagSkinsFiltered[index]));
            this.content.Add(bagSkinInspector);
        }

        protected virtual void ShowScriptableObjectMerchantSkin(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement merchantSkinObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(MerchantSkin),
                value = windowContentList.listScriptableObjectsMerchantSkinsFiltered[index]
            };
            this.content.Add(merchantSkinObjectField);

            VisualElement merchantSkinInspector = new InspectorElement();
            merchantSkinInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listScriptableObjectsMerchantSkinsFiltered[index]));
            this.content.Add(merchantSkinInspector);
        }

        protected virtual void ShowScriptableObjectTinkerSkin(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement tinkerSkinObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(TinkerSkin),
                value = windowContentList.listScriptableObjectsTinkerSkinsFiltered[index]
            };
            this.content.Add(tinkerSkinObjectField);

            VisualElement tinkerSkinInspector = new InspectorElement();
            tinkerSkinInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listScriptableObjectsTinkerSkinsFiltered[index]));
            this.content.Add(tinkerSkinInspector);
        }

        protected virtual void ShowScriptableObjectCurrency(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement currencyObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Currency),
                value = windowContentList.listScriptableObjectsCurrenciesFiltered[index]
            };
            this.content.Add(currencyObjectField);

            VisualElement currencyInspector = new InspectorElement();
            currencyInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listScriptableObjectsCurrenciesFiltered[index]));
            this.content.Add(currencyInspector);
        }

        protected virtual void ShowScriptableObjectEquipment(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement equipmentObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Equipment),
                value = windowContentList.listScriptableObjectsEquipmentsFiltered[index]
            };
            this.content.Add(equipmentObjectField);

            VisualElement equipmentInspector = new InspectorElement();
            equipmentInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listScriptableObjectsEquipmentsFiltered[index]));
            this.content.Add(equipmentInspector);
        }

        protected virtual void ShowScriptableObjectItem(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement itemObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Item),
                value = windowContentList.listScriptableObjectsItemsFiltered[index]
            };
            this.content.Add(itemObjectField);

            VisualElement itemInspector = new InspectorElement();
            itemInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listScriptableObjectsItemsFiltered[index]));
            this.content.Add(itemInspector);
        }

        protected virtual void ShowScriptableObjectLootTable(int index)
        {
            GameCreatorFinder2WindowContentInventory windowContent = (GameCreatorFinder2WindowContentInventory)this.window.Content;
            GameCreatorFinder2WindowContentListInventory windowContentList = (GameCreatorFinder2WindowContentListInventory)windowContent.ContentList;

            VisualElement lootTableObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(LootTable),
                value = windowContentList.listScriptableObjectsLootTablesFiltered[index]
            };
            this.content.Add(lootTableObjectField);

            VisualElement lootTableInspector = new InspectorElement();
            lootTableInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListInventory)this.window.Content.ContentList).listScriptableObjectsLootTablesFiltered[index]));
            this.content.Add(lootTableInspector);
        }
    }
}