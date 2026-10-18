using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;

namespace Tetriosu.Game.Screens
{
    public partial class TetriosuScreen : Screen
    {
        // Plz help me idk on how to exit game on fullscreen/borderless
        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.Escape)
            {
                this.Exit();
            }

            return base.OnKeyDown(e);
        }
    }
}
