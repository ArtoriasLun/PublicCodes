namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowToolbarStats : GameCreatorFinder2WindowToolbar
    {
        private readonly GameCreatorFinder2WindowStats window;

        public GameCreatorFinder2WindowToolbarStats(GameCreatorFinder2WindowStats window) : base(window)
        {
            this.window = window;
        }

        protected override void SetupTabDropdown()
        {
            base.SetupTabDropdown();

            this.menuTabs.menu.AppendAction(
                "Preferences",
                action => GameCreatorFinder2WindowPreferencesStats.OpenWindow()
            );
        }
    }
}