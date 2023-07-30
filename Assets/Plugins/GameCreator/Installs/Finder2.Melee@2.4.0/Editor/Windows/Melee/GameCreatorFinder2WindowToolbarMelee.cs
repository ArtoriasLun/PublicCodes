namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowToolbarMelee : GameCreatorFinder2WindowToolbar
    {
        private readonly GameCreatorFinder2WindowMelee window;

        public GameCreatorFinder2WindowToolbarMelee(GameCreatorFinder2WindowMelee window) : base(window)
        {
            this.window = window;
        }

        protected override void SetupTabDropdown()
        {
            base.SetupTabDropdown();

            this.menuTabs.menu.AppendAction(
                "Preferences",
                action => GameCreatorFinder2WindowPreferencesMelee.OpenWindow()
            );
        }
    }
}