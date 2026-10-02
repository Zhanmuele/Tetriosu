using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetriosu.Game.Screens;

namespace Tetriosu.Game
{
    public partial class TetriosuGame : TetriosuGameBase
    {
        public static ScreenStack ScreenStack;
        public static MainScreen MainScreen;

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            ScreenStack = new ScreenStack();
            MainScreen = new MainScreen();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddRange(new Drawable[]
            {
                ScreenStack
            });

            ScreenStack.Push(MainScreen);
        }
    }
}
