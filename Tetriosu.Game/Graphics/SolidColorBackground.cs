using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace Tetriosu.Game.Graphics
{
    public partial class SolidColorBackground : Box
    {
        public SolidColorBackground()
        {
            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }
    }
}
