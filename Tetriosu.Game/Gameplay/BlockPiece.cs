using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Tetriosu.Game.Gameplay
{
    public partial class BlockPiece : Container
    {
        private readonly Vector2 blockSize = new Vector2(GameSetup.BLOCK_SIZE);
        private bool isPlaced = false;
        private Colour4 blockColour;
        private Colour4 blockColourWhenPlaced;
        private Container blockBox;
        private Box activeBlock;
        private Box placedBlock;
        public Vector2 BlockPosition;

        public BlockPiece(Vector2 startPos, Colour4 color, Colour4 colorWhenPlaced)
        {
            BlockPosition = startPos + new Vector2(4f, 28f);
            blockColour = color;
            blockColourWhenPlaced = colorWhenPlaced;
        }

        protected override void LoadComplete()
        {
            X = blockSize.X * BlockPosition.X;
            Y = blockSize.Y * BlockPosition.Y;
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            Children = new Drawable[]
            {
                activeBlock = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = blockSize,
                    Colour = blockColour,
                },
                placedBlock = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = blockSize,
                    Colour = blockColourWhenPlaced,
                    Alpha = 0,
                }
            };
        }
    }
}
