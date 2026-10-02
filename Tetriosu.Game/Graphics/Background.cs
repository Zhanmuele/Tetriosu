using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace Tetriosu.Game.Graphics
{
    public partial class Background : Box
    {
        public Background()
        {
            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }
    }
}
