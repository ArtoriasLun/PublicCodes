namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentInventory : GameCreatorFinder2WindowContent
    {
        private readonly GameCreatorFinder2WindowInventory window;

        public GameCreatorFinder2WindowContentInventory(GameCreatorFinder2WindowInventory window) : base(window)
        {
            this.window = window;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            this.ContentList = new GameCreatorFinder2WindowContentListInventory(this.window);
            this.ContentDetails = new GameCreatorFinder2WindowContentDetailsInventory(this.window);

            this.splitView.Add(this.ContentList);
            this.splitView.Add(this.ContentDetails);

            this.ContentList.OnEnable();
            this.ContentDetails.OnEnable();
        }
    }
}