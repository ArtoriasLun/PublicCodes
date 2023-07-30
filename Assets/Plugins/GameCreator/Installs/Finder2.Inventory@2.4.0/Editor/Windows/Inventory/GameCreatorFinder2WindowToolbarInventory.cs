namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowToolbarInventory : GameCreatorFinder2WindowToolbar
    {
        private readonly GameCreatorFinder2WindowInventory window;

        public GameCreatorFinder2WindowToolbarInventory(GameCreatorFinder2WindowInventory window) : base(window)
        {
            this.window = window;
        }

        protected override void SetupTabDropdown()
        {
            base.SetupTabDropdown();

            this.menuTabs.menu.AppendAction(
                "Preferences",
                action => GameCreatorFinder2WindowPreferencesInventory.OpenWindow()
            );
        }
    }
}