using GameCreator.Runtime.Melee;
using UnityEditor;
using UnityEngine;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowMelee : GameCreatorFinder2WindowTemplate
    {
        protected const string MENU_ITEM = "Game Creator/Finder/Melee";
        protected const string MENU_TITLE = "GC2 Finder [Melee]";

        internal static Striker[] STRIKER_FOUND;

        internal static Combos[] COMBOS_FOUND;
        internal static MeleeReaction[] REACTIONS_FOUND;
        internal static Shield[] SHIELDS_FOUND;
        internal static Skill[] SKILLS_FOUND;
        internal static MeleeWeapon[] WEAPONS_FOUND;

        [MenuItem(MENU_ITEM)]
        public static void OpenWindow()
        {
            WINDOW = GetWindow<GameCreatorFinder2WindowMelee>();
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        protected override void Build()
        {
            this.Toolbar = new GameCreatorFinder2WindowToolbarMelee(this) { name = NAME_TOOLBAR };
            this.Content = new GameCreatorFinder2WindowContentMelee(this) { name = NAME_CONTENT };

            this.rootVisualElement.Add(this.Toolbar);
            this.rootVisualElement.Add(this.Content);

            this.Toolbar.OnEnable();
            ((GameCreatorFinder2WindowContentMelee)this.Content).OnEnable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            this.titleContent = new GUIContent(MENU_TITLE, iconWindow.Texture);
        }

        protected override void FindComponents()
        {
            this.FindStrikers();
        }

        protected override void FindScriptableObjects()
        {
            this.FindCombos();
            this.FindReactions();
            this.FindShields();
            this.FindSkills();
            this.FindWeapons();
        }

        protected virtual void FindStrikers()
        {
            STRIKER_FOUND = FindObjectsOfType<Striker>();
        }

        protected virtual void FindCombos()
        {
            string[] combosFound = AssetDatabase.FindAssets("t:Combos");

            COMBOS_FOUND = new Combos[combosFound.Length];

            for (int i = 0; i < combosFound.Length; i++)
            {
                string guid = combosFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                COMBOS_FOUND[i] = (Combos)AssetDatabase.LoadAssetAtPath(path, typeof(Combos));
            }
        }

        protected virtual void FindReactions()
        {
            string[] reactionsFound = AssetDatabase.FindAssets("t:MeleeReaction");

            REACTIONS_FOUND = new MeleeReaction[reactionsFound.Length];

            for (int i = 0; i < reactionsFound.Length; i++)
            {
                string guid = reactionsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                REACTIONS_FOUND[i] = (MeleeReaction)AssetDatabase.LoadAssetAtPath(path, typeof(MeleeReaction));
            }
        }

        protected virtual void FindShields()
        {
            string[] shieldsFound = AssetDatabase.FindAssets("t:Shield");

            SHIELDS_FOUND = new Shield[shieldsFound.Length];

            for (int i = 0; i < shieldsFound.Length; i++)
            {
                string guid = shieldsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SHIELDS_FOUND[i] = (Shield)AssetDatabase.LoadAssetAtPath(path, typeof(Shield));
            }
        }

        protected virtual void FindSkills()
        {
            string[] skillsFound = AssetDatabase.FindAssets("t:Skill");

            SKILLS_FOUND = new Skill[skillsFound.Length];

            for (int i = 0; i < skillsFound.Length; i++)
            {
                string guid = skillsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SKILLS_FOUND[i] = (Skill)AssetDatabase.LoadAssetAtPath(path, typeof(Skill));
            }
        }

        protected virtual void FindWeapons()
        {
            string[] weaponsFound = AssetDatabase.FindAssets("t:MeleeWeapon");

            WEAPONS_FOUND = new MeleeWeapon[weaponsFound.Length];

            for (int i = 0; i < weaponsFound.Length; i++)
            {
                string guid = weaponsFound[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                WEAPONS_FOUND[i] = (MeleeWeapon)AssetDatabase.LoadAssetAtPath(path, typeof(MeleeWeapon));
            }
        }
    }
}