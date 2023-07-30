namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentCore : GameCreatorFinder2WindowContent
    {
        private readonly GameCreatorFinder2WindowCore window;

        public GameCreatorFinder2WindowContentCore(GameCreatorFinder2WindowCore window) : base(window)
        {
            this.window = window;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            this.ContentList = new GameCreatorFinder2WindowContentListCore(this.window);
            this.ContentDetails = new GameCreatorFinder2WindowContentDetailsCore(this.window);

            this.splitView.Add(this.ContentList);
            this.splitView.Add(this.ContentDetails);

            this.ContentList.OnEnable();
            this.ContentDetails.OnEnable();
        }
    }
}