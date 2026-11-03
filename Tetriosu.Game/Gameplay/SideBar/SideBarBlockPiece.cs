using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Tetriosu.Game.Gameplay.Components;

namespace Tetriosu.Game.Gameplay.SideBar
{
    public partial class SideBarBlockPiece : Box
    {
        private static readonly Vector2 block_size = GameSetupValues.BLOCK_SIZE_V;

        public SideBarBlockPiece(Vector2 startPos, Colour4 colour)
        {
            Colour = colour;
            Position = block_size * startPos;
        }
    }
}
