using UnityEngine.UIElements;

namespace MiTschMR.Finder2
{
    public abstract class GameCreatorFinder2WindowContent : VisualElement
    {
        protected const int FIXED_PANEL_INDEX = 0;
        protected const float FIXED_PANEL_WIDTH = 350f;

        // MEMBERS: -------------------------------------------------------------------------------

        private readonly GameCreatorFinder2WindowTemplate window;

        protected TwoPaneSplitView splitView;

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameCreatorFinder2WindowContentList ContentList { get; protected set; }
        public GameCreatorFinder2WindowContentDetails ContentDetails { get; protected set; }

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public GameCreatorFinder2WindowContent(GameCreatorFinder2WindowTemplate window)
        {
            this.window = window;
        }

        public virtual void OnEnable()
        {
            this.AddTwoPaneSplitView();
        }

        protected virtual void AddTwoPaneSplitView()
        {
            this.splitView = new TwoPaneSplitView(
                FIXED_PANEL_INDEX,
                FIXED_PANEL_WIDTH,
                TwoPaneSplitViewOrientation.Horizontal
            );

            this.Add(this.splitView);
        }

        internal virtual void OnDisable()
        {
            this.ContentList?.OnDisable();
            this.ContentDetails?.OnDisable();
        }
    }
}