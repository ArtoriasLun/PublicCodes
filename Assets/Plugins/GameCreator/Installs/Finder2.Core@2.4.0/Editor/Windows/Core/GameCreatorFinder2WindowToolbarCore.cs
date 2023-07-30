namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowToolbarCore : GameCreatorFinder2WindowToolbar
    {
        private readonly GameCreatorFinder2WindowCore window;

        public GameCreatorFinder2WindowToolbarCore(GameCreatorFinder2WindowCore window) : base(window)
        {
            this.window = window;
        }

        protected override void SetupTabDropdown()
        {
            base.SetupTabDropdown();

            this.menuTabs.menu.AppendAction(
                "Preferences",
                action => GameCreatorFinder2WindowPreferencesCore.OpenWindow()
            );
        }
    }
}