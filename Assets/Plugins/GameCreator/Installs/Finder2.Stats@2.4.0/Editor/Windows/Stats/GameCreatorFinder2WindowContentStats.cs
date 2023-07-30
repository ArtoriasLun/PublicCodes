namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentStats : GameCreatorFinder2WindowContent
    {
        private readonly GameCreatorFinder2WindowStats window;

        public GameCreatorFinder2WindowContentStats(GameCreatorFinder2WindowStats window) : base(window)
        {
            this.window = window;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            this.ContentList = new GameCreatorFinder2WindowContentListStats(this.window);
            this.ContentDetails = new GameCreatorFinder2WindowContentDetailsStats(this.window);

            this.splitView.Add(this.ContentList);
            this.splitView.Add(this.ContentDetails);

            this.ContentList.OnEnable();
            this.ContentDetails.OnEnable();
        }
    }
}