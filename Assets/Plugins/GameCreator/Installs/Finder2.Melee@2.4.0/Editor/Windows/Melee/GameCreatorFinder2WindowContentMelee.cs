namespace MiTschMR.Finder2
{
    public class GameCreatorFinder2WindowContentMelee : GameCreatorFinder2WindowContent
    {
        private readonly GameCreatorFinder2WindowMelee window;

        public GameCreatorFinder2WindowContentMelee(GameCreatorFinder2WindowMelee window) : base(window)
        {
            this.window = window;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            this.ContentList = new GameCreatorFinder2WindowContentListMelee(this.window);
            this.ContentDetails = new GameCreatorFinder2WindowContentDetailsMelee(this.window);

            this.splitView.Add(this.ContentList);
            this.splitView.Add(this.ContentDetails);

            this.ContentList.OnEnable();
            this.ContentDetails.OnEnable();
        }
    }
}