using GameCreator.Runtime.Melee;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentDetailsMelee : GameCreatorFinder2WindowContentDetails
    {
        private readonly GameCreatorFinder2WindowMelee window;

        public GameCreatorFinder2WindowContentDetailsMelee(GameCreatorFinder2WindowMelee window) : base(window)
        {
            this.window = window;
        }

        protected override void ShowComponent(string foldout, int index)
        {
            switch (foldout)
            {
                case "Striker":
                    this.ShowComponentStriker(index);
                    break;
            }
        }

        protected virtual void ShowComponentStriker(int index)
        {
            GameCreatorFinder2WindowContentMelee windowContent = (GameCreatorFinder2WindowContentMelee)this.window.Content;
            GameCreatorFinder2WindowContentListMelee windowContentList = (GameCreatorFinder2WindowContentListMelee)windowContent.ContentList;

            VisualElement strikerObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Striker),
                value = windowContentList.listComponentsStrikersFiltered[index]
            };
            this.content.Add(strikerObjectField);

            VisualElement strikerInspector = new InspectorElement();
            strikerInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListMelee)this.window.Content.ContentList).listComponentsStrikersFiltered[index]));
            this.content.Add(strikerInspector);
        }

        protected override void ShowScriptableObject(string foldout, int index)
        {
            switch (foldout)
            {
                case "Combo":
                    this.ShowScriptableObjectCombo(index);
                    break;
                case "Reaction":
                    this.ShowScriptableObjectReaction(index);
                    break;
                case "Shield":
                    this.ShowScriptableObjectShield(index);
                    break;
                case "Skill":
                    this.ShowScriptableObjectSkill(index);
                    break;
                case "Weapon":
                    this.ShowScriptableObjectWeapon(index);
                    break;
            }
        }

        protected virtual void ShowScriptableObjectCombo(int index)
        {
            GameCreatorFinder2WindowContentMelee windowContent = (GameCreatorFinder2WindowContentMelee)this.window.Content;
            GameCreatorFinder2WindowContentListMelee windowContentList = (GameCreatorFinder2WindowContentListMelee)windowContent.ContentList;

            VisualElement comboObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Combos),
                value = windowContentList.listScriptableObjectsCombosFiltered[index]
            };
            this.content.Add(comboObjectField);

            VisualElement comboInspector = new InspectorElement();
            comboInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListMelee)this.window.Content.ContentList).listScriptableObjectsCombosFiltered[index]));
            this.content.Add(comboInspector);
        }

        protected virtual void ShowScriptableObjectReaction(int index)
        {
            GameCreatorFinder2WindowContentMelee windowContent = (GameCreatorFinder2WindowContentMelee)this.window.Content;
            GameCreatorFinder2WindowContentListMelee windowContentList = (GameCreatorFinder2WindowContentListMelee)windowContent.ContentList;

            VisualElement reactionObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(MeleeReaction),
                value = windowContentList.listScriptableObjectsReactionsFiltered[index]
            };
            this.content.Add(reactionObjectField);

            VisualElement reactionInspector = new InspectorElement();
            reactionInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListMelee)this.window.Content.ContentList).listScriptableObjectsReactionsFiltered[index]));
            this.content.Add(reactionInspector);
        }

        protected virtual void ShowScriptableObjectShield(int index)
        {
            GameCreatorFinder2WindowContentMelee windowContent = (GameCreatorFinder2WindowContentMelee)this.window.Content;
            GameCreatorFinder2WindowContentListMelee windowContentList = (GameCreatorFinder2WindowContentListMelee)windowContent.ContentList;

            VisualElement shieldObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Shield),
                value = windowContentList.listScriptableObjectsShieldsFiltered[index]
            };
            this.content.Add(shieldObjectField);

            VisualElement shieldInspector = new InspectorElement();
            shieldInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListMelee)this.window.Content.ContentList).listScriptableObjectsShieldsFiltered[index]));
            this.content.Add(shieldInspector);
        }

        protected virtual void ShowScriptableObjectSkill(int index)
        {
            GameCreatorFinder2WindowContentMelee windowContent = (GameCreatorFinder2WindowContentMelee)this.window.Content;
            GameCreatorFinder2WindowContentListMelee windowContentList = (GameCreatorFinder2WindowContentListMelee)windowContent.ContentList;

            VisualElement skillsObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(Skill),
                value = windowContentList.listScriptableObjectsSkillsFiltered[index]
            };
            this.content.Add(skillsObjectField);

            VisualElement skillInspector = new InspectorElement();
            skillInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListMelee)this.window.Content.ContentList).listScriptableObjectsSkillsFiltered[index]));
            this.content.Add(skillInspector);
        }

        protected virtual void ShowScriptableObjectWeapon(int index)
        {
            GameCreatorFinder2WindowContentMelee windowContent = (GameCreatorFinder2WindowContentMelee)this.window.Content;
            GameCreatorFinder2WindowContentListMelee windowContentList = (GameCreatorFinder2WindowContentListMelee)windowContent.ContentList;

            VisualElement weaponObjectField = new ObjectField()
            {
                allowSceneObjects = false,
                name = NAME_OBJECTFIELD,
                objectType = typeof(MeleeWeapon),
                value = windowContentList.listScriptableObjectsWeaponsFiltered[index]
            };
            this.content.Add(weaponObjectField);

            VisualElement weaponInspector = new InspectorElement();
            weaponInspector.Bind(new SerializedObject(((GameCreatorFinder2WindowContentListMelee)this.window.Content.ContentList).listScriptableObjectsWeaponsFiltered[index]));
            this.content.Add(weaponInspector);
        }
    }
}