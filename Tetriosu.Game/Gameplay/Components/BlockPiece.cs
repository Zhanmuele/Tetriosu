using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Tetriosu.Game.Gameplay.Playfield;

namespace Tetriosu.Game.Gameplay.Components
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

        public BlockPiece(Vector2 startPos, Colour4 color, Colour4 colorWhenPlaced, PlayfieldContainer playfield)
        {
            playfield.Add(this);

            BlockPosition = startPos + new Vector2(4f, 28f);
            blockColour = color;
            blockColourWhenPlaced = colorWhenPlaced;
            Position = BlockPosition * blockSize;
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;

            AddRange(new Drawable[]
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
            });
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
