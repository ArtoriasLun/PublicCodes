using GameCreator.Runtime.Common;
using GameCreator.Runtime.Melee;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentListMelee : GameCreatorFinder2WindowContentList
    {
        protected static readonly IIcon ICON_STRIKER = new IconMeleeSword(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_COMBOS = new IconMeleeCombo(ColorTheme.Type.Purple);
        protected static readonly IIcon ICON_REACTION = new IconReaction(ColorTheme.Type.Red);
        protected static readonly IIcon ICON_SHIELD = new IconShieldOutline(ColorTheme.Type.Red);
        protected static readonly IIcon ICON_SKILL = new IconMeleeSkill(ColorTheme.Type.Green);
        protected static readonly IIcon ICON_WEAPON = new IconMeleeSword(ColorTheme.Type.Blue);

        private readonly GameCreatorFinder2WindowMelee window;

        internal List<Striker> listComponentsStrikers = new List<Striker>();
        internal List<Striker> listComponentsStrikersFiltered = new List<Striker>();

        internal List<Combos> listScriptableObjectsCombos = new List<Combos>();
        internal List<Combos> listScriptableObjectsCombosFiltered = new List<Combos>();
        internal List<MeleeReaction> listScriptableObjectsReactions = new List<MeleeReaction>();
        internal List<MeleeReaction> listScriptableObjectsReactionsFiltered = new List<MeleeReaction>();
        internal List<Shield> listScriptableObjectsShields = new List<Shield>();
        internal List<Shield> listScriptableObjectsShieldsFiltered = new List<Shield>();
        internal List<Skill> listScriptableObjectsSkills = new List<Skill>();
        internal List<Skill> listScriptableObjectsSkillsFiltered = new List<Skill>();
        internal List<MeleeWeapon> listScriptableObjectsWeapons = new List<MeleeWeapon>();
        internal List<MeleeWeapon> listScriptableObjectsWeaponsFiltered = new List<MeleeWeapon>();

        protected ListView listViewComponentsStrikers;

        protected ListView listViewScriptableObjectsCombos;
        protected ListView listViewScriptableObjectsReactions;
        protected ListView listViewScriptableObjectsShields;
        protected ListView listViewScriptableObjectsSkills;
        protected ListView listViewScriptableObjectsWeapons;

        protected Foldout foldoutComponentsStrikers = new Foldout() { text = "Striker", value = false };

        protected Foldout foldoutScriptableObjectsCombos = new Foldout() { text = "Combos", value = false };
        protected Foldout foldoutScriptableObjectsReactions = new Foldout() { text = "Reactions", value = false };
        protected Foldout foldoutScriptableObjectsShields = new Foldout() { text = "Shields", value = false };
        protected Foldout foldoutScriptableObjectsSkills = new Foldout() { text = "Skills", value = false };
        protected Foldout foldoutScriptableObjectsWeapons = new Foldout() { text = "Weapons", value = false };

        public GameCreatorFinder2WindowContentListMelee(GameCreatorFinder2WindowMelee window) : base(window)
        {
            this.window = window;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            this.listViewComponentsStrikers.selectionChanged -= OnContentSelectItemStriker;

            this.listViewScriptableObjectsCombos.selectionChanged -= OnContentSelectItemCombo;
            this.listViewScriptableObjectsReactions.selectionChanged -= OnContentSelectItemReaction;
            this.listViewScriptableObjectsShields.selectionChanged -= OnContentSelectItemShield;
            this.listViewScriptableObjectsSkills.selectionChanged -= OnContentSelectItemDialogueSkin;
            this.listViewScriptableObjectsWeapons.selectionChanged -= OnContentSelectItemSpeechSkin;

            this.listViewComponentsStrikers.itemsChosen -= OnContentChooseItemStriker;

            this.listViewScriptableObjectsCombos.itemsChosen -= OnContentChooseItemCombo;
            this.listViewScriptableObjectsReactions.itemsChosen -= OnContentChooseItemReaction;
            this.listViewScriptableObjectsShields.itemsChosen -= OnContentChooseItemShield;
            this.listViewScriptableObjectsSkills.itemsChosen -= OnContentChooseItemSkill;
            this.listViewScriptableObjectsWeapons.itemsChosen -= OnContentChooseItemWeapon;
        }

        protected override void SetupListComponents()
        {
            this.InitializeListComponents();

            this.SetupListComponentsStrikers();
        }

        protected virtual void SetupListComponentsStrikers()
        {
            this.listViewComponentsStrikers = new ListView(this.listComponentsStrikersFiltered, 24, this.MakeItem, this.BindItemStriker)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewComponentsStrikers.selectionChanged += OnContentSelectItemStriker;
            this.listViewComponentsStrikers.itemsChosen += OnContentChooseItemStriker;

            this.foldoutComponentsStrikers.Add(this.listViewComponentsStrikers);
        }

        protected override void SetupListScriptableObjects()
        {
            this.InitializeListScriptableObjects();

            this.SetupListScriptableObjectsCombos();
            this.SetupListScriptableObjectsReactions();
            this.SetupListScriptableObjectsShields();
            this.SetupListScriptableObjectsSkills();
            this.SetupListScriptableObjectsWeapons();
        }

        protected virtual void SetupListScriptableObjectsCombos()
        {
            this.listViewScriptableObjectsCombos = new ListView(this.listScriptableObjectsCombosFiltered, 24, this.MakeItem, this.BindItemCombo)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsCombos.selectionChanged += OnContentSelectItemCombo;
            this.listViewScriptableObjectsCombos.itemsChosen += OnContentChooseItemCombo;

            this.foldoutScriptableObjectsCombos.Add(this.listViewScriptableObjectsCombos);
        }

        protected virtual void SetupListScriptableObjectsReactions()
        {
            this.listViewScriptableObjectsReactions = new ListView(this.listScriptableObjectsReactionsFiltered, 24, this.MakeItem, this.BindItemReaction)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsReactions.selectionChanged += OnContentSelectItemReaction;
            this.listViewScriptableObjectsReactions.itemsChosen += OnContentChooseItemReaction;

            this.foldoutScriptableObjectsReactions.Add(this.listViewScriptableObjectsReactions);
        }

        protected virtual void SetupListScriptableObjectsShields()
        {
            this.listViewScriptableObjectsShields = new ListView(this.listScriptableObjectsShieldsFiltered, 24, this.MakeItem, this.BindItemShield)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsShields.selectionChanged += OnContentSelectItemShield;
            this.listViewScriptableObjectsShields.itemsChosen += OnContentChooseItemShield;

            this.foldoutScriptableObjectsShields.Add(this.listViewScriptableObjectsShields);
        }

        protected virtual void SetupListScriptableObjectsSkills()
        {
            this.listViewScriptableObjectsSkills = new ListView(this.listScriptableObjectsSkillsFiltered, 24, this.MakeItem, this.BindItemSkill)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsSkills.selectionChanged += OnContentSelectItemDialogueSkin;
            this.listViewScriptableObjectsSkills.itemsChosen += OnContentChooseItemSkill;

            this.foldoutScriptableObjectsSkills.Add(this.listViewScriptableObjectsSkills);
        }

        protected virtual void SetupListScriptableObjectsWeapons()
        {
            this.listViewScriptableObjectsWeapons = new ListView(this.listScriptableObjectsWeaponsFiltered, 24, this.MakeItem, this.BindItemWeapon)
            {
                name = NAME_CONTENT_LIST,
                reorderable = false,
                focusable = true,
                selectionType = SelectionType.Single
            };

            this.listViewScriptableObjectsWeapons.selectionChanged += OnContentSelectItemSpeechSkin;
            this.listViewScriptableObjectsWeapons.itemsChosen += OnContentChooseItemWeapon;

            this.foldoutScriptableObjectsWeapons.Add(this.listViewScriptableObjectsWeapons);
        }

        protected virtual void BindItemStriker(VisualElement element, int index)
        {
            IIcon icon = this.listComponentsStrikersFiltered[index].GetType().Name switch
            {
                "Striker" => ICON_STRIKER,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listComponentsStrikersFiltered[index].name} (ID {this.listComponentsStrikersFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemCombo(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsCombosFiltered[index].GetType().Name switch
            {
                "Combos" => ICON_COMBOS,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsCombosFiltered[index].name} (ID {this.listScriptableObjectsCombosFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemReaction(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsReactionsFiltered[index].GetType().Name switch
            {
                "MeleeReaction" => ICON_REACTION,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsReactionsFiltered[index].name} (ID {this.listScriptableObjectsReactionsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemShield(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsShieldsFiltered[index].GetType().Name switch
            {
                "Shield" => ICON_SHIELD,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsShieldsFiltered[index].name} (ID {this.listScriptableObjectsShieldsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemSkill(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsSkillsFiltered[index].GetType().Name switch
            {
                "Skill" => ICON_SKILL,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsSkillsFiltered[index].name} (ID {this.listScriptableObjectsSkillsFiltered[index].GetInstanceID()})";
        }

        protected virtual void BindItemWeapon(VisualElement element, int index)
        {
            IIcon icon = this.listScriptableObjectsWeaponsFiltered[index].GetType().Name switch
            {
                "MeleeWeapon" => ICON_WEAPON,
                _ => ICON_NONE
            };

            element.Q<Image>(NAME_ELEMENT_ICON).image = icon.Texture;
            element.Q<Label>(NAME_ELEMENT_TITLE).text = $"{this.listScriptableObjectsWeaponsFiltered[index].name} (ID {this.listScriptableObjectsWeaponsFiltered[index].GetInstanceID()})";
        }

        public override void InitializeListComponents()
        {
            this.listComponentsStrikers.AddRange(GameCreatorFinder2WindowMelee.STRIKER_FOUND);
        }

        public override void InitializeListScriptableObjects()
        {
            this.listScriptableObjectsCombos.AddRange(GameCreatorFinder2WindowMelee.COMBOS_FOUND);
            this.listScriptableObjectsReactions.AddRange(GameCreatorFinder2WindowMelee.REACTIONS_FOUND);
            this.listScriptableObjectsShields.AddRange(GameCreatorFinder2WindowMelee.SHIELDS_FOUND);
            this.listScriptableObjectsSkills.AddRange(GameCreatorFinder2WindowMelee.SKILLS_FOUND);
            this.listScriptableObjectsWeapons.AddRange(GameCreatorFinder2WindowMelee.WEAPONS_FOUND);
        }

        public override void RemoveListComponents()
        {
            this.listComponentsStrikers = new List<Striker>();
        }

        protected override void RemoveListScriptableObjects()
        {
            this.listScriptableObjectsCombos = new List<Combos>();
            this.listScriptableObjectsReactions = new List<MeleeReaction>();
            this.listScriptableObjectsShields = new List<Shield>();
            this.listScriptableObjectsSkills = new List<Skill>();
            this.listScriptableObjectsWeapons = new List<MeleeWeapon>();
        }

        protected override void RemoveListComponentsElements()
        {
            try { this.content.Remove(this.foldoutComponentsStrikers); } catch { }
            try { this.foldoutComponentsStrikers.Remove(this.listViewComponentsStrikers); } catch { }
        }

        protected override void RemoveListScriptableObjectsElements()
        {
            try { this.content.Remove(this.foldoutScriptableObjectsCombos); } catch { }
            try { this.foldoutScriptableObjectsCombos.Remove(this.listViewScriptableObjectsCombos); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsReactions); } catch { }
            try { this.foldoutScriptableObjectsReactions.Remove(this.listViewScriptableObjectsReactions); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsShields); } catch { }
            try { this.foldoutScriptableObjectsShields.Remove(this.listViewScriptableObjectsShields); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsSkills); } catch { }
            try { this.foldoutScriptableObjectsSkills.Remove(this.listViewScriptableObjectsSkills); } catch { }

            try { this.content.Remove(this.foldoutScriptableObjectsWeapons); } catch { }
            try { this.foldoutScriptableObjectsWeapons.Remove(this.listViewScriptableObjectsWeapons); } catch { }
        }

        public override void UpdateListComponents()
        {
            this.RemoveListElements();

            this.UpdateListComponentsStrikers();
        }

        protected virtual void UpdateListComponentsStrikers()
        {
            this.listComponentsStrikers = this.sortComponentsAZ ? this.listComponentsStrikers.OrderBy(go => go.name).ToList() : this.listComponentsStrikers.OrderByDescending(go => go.name).ToList();

            this.listComponentsStrikersFiltered = this.listComponentsStrikers.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listComponentsStrikersFiltered.Count > 0)
            {
                this.listViewComponentsStrikers.itemsSource = this.listComponentsStrikersFiltered;
                this.listViewComponentsStrikers.Rebuild();

                this.foldoutComponentsStrikers.Add(this.listViewComponentsStrikers);

                this.content.Add(this.foldoutComponentsStrikers);
            }
        }

        public override void UpdateListScriptableObjects()
        {
            this.RemoveListElements();

            this.UpdateListScriptableObjectsCombos();
            this.UpdateListScriptableObjectsReactions();
            this.UpdateListScriptableObjectsShields();
            this.UpdateListScriptableObjectsSkills();
            this.UpdateListScriptableObjectsWeapons();
        }

        protected virtual void UpdateListScriptableObjectsCombos()
        {
            this.listScriptableObjectsCombos = this.sortScriptableObjectsAZ ? this.listScriptableObjectsCombos.OrderBy(go => go.name).ToList() : this.listScriptableObjectsCombos.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsCombosFiltered = this.listScriptableObjectsCombos.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsCombosFiltered.Count > 0)
            {
                this.listViewScriptableObjectsCombos.itemsSource = this.listScriptableObjectsCombosFiltered;
                this.listViewScriptableObjectsCombos.Rebuild();

                this.foldoutScriptableObjectsCombos.Add(this.listViewScriptableObjectsCombos);

                this.content.Add(this.foldoutScriptableObjectsCombos);
            }
        }

        protected virtual void UpdateListScriptableObjectsReactions()
        {
            this.listScriptableObjectsReactions = this.sortScriptableObjectsAZ ? this.listScriptableObjectsReactions.OrderBy(go => go.name).ToList() : this.listScriptableObjectsReactions.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsReactionsFiltered = this.listScriptableObjectsReactions.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsReactionsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsReactions.itemsSource = this.listScriptableObjectsReactionsFiltered;
                this.listViewScriptableObjectsReactions.Rebuild();

                this.foldoutScriptableObjectsReactions.Add(this.listViewScriptableObjectsReactions);

                this.content.Add(this.foldoutScriptableObjectsReactions);
            }
        }

        protected virtual void UpdateListScriptableObjectsShields()
        {
            this.listScriptableObjectsShields = this.sortScriptableObjectsAZ ? this.listScriptableObjectsShields.OrderBy(go => go.name).ToList() : this.listScriptableObjectsShields.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsShieldsFiltered = this.listScriptableObjectsShields.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            if (this.listScriptableObjectsShieldsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsShields.itemsSource = this.listScriptableObjectsShieldsFiltered;
                this.listViewScriptableObjectsShields.Rebuild();

                this.foldoutScriptableObjectsShields.Add(this.listViewScriptableObjectsShields);

                this.content.Add(this.foldoutScriptableObjectsShields);
            }
        }

        protected virtual void UpdateListScriptableObjectsSkills()
        {
            this.listScriptableObjectsSkills = this.sortScriptableObjectsAZ ? this.listScriptableObjectsSkills.OrderBy(go => go.name).ToList() : this.listScriptableObjectsSkills.OrderByDescending(go => go.name).ToList();
            
            this.listScriptableObjectsSkillsFiltered.Clear();

            List<Skill> listSkillsFoundByName = this.listScriptableObjectsSkills.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsSkillsFiltered.AddRange(listSkillsFoundByName);

            if (GameCreatorFinder2WindowPreferencesMelee.ScriptableObjectsSkillsNames)
            {
                List<Skill> listSkillsFoundBySkillNames = new List<Skill>();

                for (int i = 0; i < this.listScriptableObjectsSkills.Count; i++)
                {
                    if (this.listScriptableObjectsSkills[i].GetName(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()))
                    {
                        listSkillsFoundBySkillNames.Add(this.listScriptableObjectsSkills[i]);
                    }
                }

                this.listScriptableObjectsSkillsFiltered.AddRange(listSkillsFoundBySkillNames);
            }

            if (GameCreatorFinder2WindowPreferencesMelee.ScriptableObjectsSkillsDescriptions)
            {
                List<Skill> listSkillsFoundBySkillDescriptions = new List<Skill>();

                for (int i = 0; i < this.listScriptableObjectsSkills.Count; i++)
                {
                    if (this.listScriptableObjectsSkills[i].GetDescription(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()))
                    {
                        listSkillsFoundBySkillDescriptions.Add(this.listScriptableObjectsSkills[i]);
                    }
                }

                this.listScriptableObjectsSkillsFiltered.AddRange(listSkillsFoundBySkillDescriptions);
            }

            this.listScriptableObjectsSkillsFiltered = this.listScriptableObjectsSkillsFiltered.Distinct().ToList();
            this.listScriptableObjectsSkillsFiltered = this.sortScriptableObjectsAZ ? this.listScriptableObjectsSkillsFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsSkillsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsSkillsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsSkills.itemsSource = this.listScriptableObjectsSkillsFiltered;
                this.listViewScriptableObjectsSkills.Rebuild();

                this.foldoutScriptableObjectsSkills.Add(this.listViewScriptableObjectsSkills);

                this.content.Add(this.foldoutScriptableObjectsSkills);
            }
        }

        protected virtual void UpdateListScriptableObjectsWeapons()
        {
            this.listScriptableObjectsWeapons = this.sortScriptableObjectsAZ ? this.listScriptableObjectsWeapons.OrderBy(go => go.name).ToList() : this.listScriptableObjectsWeapons.OrderByDescending(go => go.name).ToList();

            this.listScriptableObjectsWeaponsFiltered.Clear();

            List<MeleeWeapon> listWeaponsFoundByName = this.listScriptableObjectsWeapons.Where(go => go.name.ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower())).ToList();

            this.listScriptableObjectsWeaponsFiltered.AddRange(listWeaponsFoundByName);

            if (GameCreatorFinder2WindowPreferencesMelee.ScriptableObjectsWeaponsNames)
            {
                List<MeleeWeapon> listWeaponsFoundByWeaponNames = new List<MeleeWeapon>();

                for (int i = 0; i < this.listScriptableObjectsWeapons.Count; i++)
                {
                    if (this.listScriptableObjectsWeapons[i].GetName(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()))
                    {
                        listWeaponsFoundByWeaponNames.Add(this.listScriptableObjectsWeapons[i]);
                    }
                }

                this.listScriptableObjectsWeaponsFiltered.AddRange(listWeaponsFoundByWeaponNames);
            }

            if (GameCreatorFinder2WindowPreferencesMelee.ScriptableObjectsWeaponsDescriptions)
            {
                List<MeleeWeapon> listWeaponsFoundByWeaponDescriptions = new List<MeleeWeapon>();

                for (int i = 0; i < this.listScriptableObjectsWeapons.Count; i++)
                {
                    if (this.listScriptableObjectsWeapons[i].GetDescription(Args.EMPTY).ToLower().Contains(this.window.Toolbar.SearchField.value.ToLower()))
                    {
                        listWeaponsFoundByWeaponDescriptions.Add(this.listScriptableObjectsWeapons[i]);
                    }
                }

                this.listScriptableObjectsWeaponsFiltered.AddRange(listWeaponsFoundByWeaponDescriptions);
            }

            this.listScriptableObjectsWeaponsFiltered = this.listScriptableObjectsWeaponsFiltered.Distinct().ToList();
            this.listScriptableObjectsWeaponsFiltered = this.sortScriptableObjectsAZ ? this.listScriptableObjectsWeaponsFiltered.OrderBy(go => go.name).ToList() : this.listScriptableObjectsWeaponsFiltered.OrderByDescending(go => go.name).ToList();

            if (this.listScriptableObjectsWeaponsFiltered.Count > 0)
            {
                this.listViewScriptableObjectsWeapons.itemsSource = this.listScriptableObjectsWeaponsFiltered;
                this.listViewScriptableObjectsWeapons.Rebuild();

                this.foldoutScriptableObjectsWeapons.Add(this.listViewScriptableObjectsWeapons);

                this.content.Add(this.foldoutScriptableObjectsWeapons);
            }
        }

        protected override void ShowAllComponents()
        {
            this.foldoutComponentsStrikers.value = true;
        }

        protected override void HideAllComponents()
        {
            this.foldoutComponentsStrikers.value = false;
        }

        protected override void ShowAllScriptableObjects()
        {
            this.foldoutScriptableObjectsCombos.value = true;
            this.foldoutScriptableObjectsReactions.value = true;
            this.foldoutScriptableObjectsShields.value = true;
            this.foldoutScriptableObjectsSkills.value = true;
            this.foldoutScriptableObjectsWeapons.value = true;
        }

        protected override void HideAllScriptableObjects()
        {
            this.foldoutScriptableObjectsCombos.value = false;
            this.foldoutScriptableObjectsReactions.value = false;
            this.foldoutScriptableObjectsShields.value = false;
            this.foldoutScriptableObjectsSkills.value = false;
            this.foldoutScriptableObjectsWeapons.value = false;
        }

        protected virtual void OnContentSelectItemStriker(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Striker", this.listViewComponentsStrikers.selectedIndex);
        }

        protected virtual void OnContentSelectItemCombo(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Combo", this.listViewScriptableObjectsCombos.selectedIndex);
        }

        protected virtual void OnContentSelectItemReaction(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Reaction", this.listViewScriptableObjectsReactions.selectedIndex);
        }

        protected virtual void OnContentSelectItemShield(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Shield", this.listViewScriptableObjectsShields.selectedIndex);
        }

        protected virtual void OnContentSelectItemDialogueSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Skill", this.listViewScriptableObjectsSkills.selectedIndex);
        }

        protected virtual void OnContentSelectItemSpeechSkin(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Weapon", this.listViewScriptableObjectsWeapons.selectedIndex);
        }

        protected virtual void OnContentChooseItemStriker(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Striker", this.listViewComponentsStrikers.selectedIndex);
        }

        protected virtual void OnContentChooseItemCombo(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Combo", this.listViewScriptableObjectsCombos.selectedIndex);
        }

        protected virtual void OnContentChooseItemReaction(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Reaction", this.listViewScriptableObjectsReactions.selectedIndex);
        }

        protected virtual void OnContentChooseItemShield(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Shield", this.listViewScriptableObjectsShields.selectedIndex);
        }

        protected virtual void OnContentChooseItemSkill(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Skill", this.listViewScriptableObjectsSkills.selectedIndex);
        }

        protected virtual void OnContentChooseItemWeapon(IEnumerable<object> list)
        {
            this.window.OnChangeSelection((GameCreatorFinder2WindowTemplate.GC2_FINDER_MODULE_TABS)this.window.tabIndex, "Weapon", this.listViewScriptableObjectsWeapons.selectedIndex);
        }
    }
}