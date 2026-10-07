using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.Diagnostics.Runtime.DacInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Tetriosu.Game.Gameplay.Playfield;

namespace Tetriosu.Game.Gameplay.Components
{
    public partial class BlockPiece : Container
    {
        private readonly Vector2 blockSize = new Vector2(GameSetupValues.BLOCK_SIZE);
        private bool isPlaced = false;
        private Colour4 blockColour;
        private Colour4 blockColourWhenPlaced;
        private Box activeBlock;
        private Box placedBlock;
        public Vector2 BlockPosition;

        public BlockPiece(Vector2 startPos, Colour4 color, Colour4 colorWhenPlaced, PlayfieldContainer playfield)
        {
            playfield.AddBlockPiece(this);

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
            if (isPlaced) { return; }

            base.Update();
        }

        private bool horizontalCollisionCheck(float x, ref BlockPiece?[,] playfieldData)
        {
            if (!(0 <= x && x < GameSetupValues.DEFAULT_COLUMS))
            {
                return true;
            }

            if (playfieldData[(int)BlockPosition.Y, (int)x] != null)
            {
                return true;
            }

            return false;
        }

        private bool verticalCollisionCheck(float y, ref BlockPiece?[,] playfieldData)
        {
            if (!(0 <= y && y < GameSetupValues.ROWS))
            {
                return true;
            }

            if (playfieldData[(int)y, (int)BlockPosition.X] != null)
            {
                return true;
            }

            return false;
        }

        private Vector2 rotatePiece(Vector2 centerOfRotation, bool counterClockwise)
        {
            Vector2 offset = BlockPosition - centerOfRotation;
            Vector2 rotatedOffset = rotate90degrees(offset, counterClockwise);

            return centerOfRotation + rotatedOffset;
        }

        private static Vector2 rotate90degrees(Vector2 vector, bool counterClockwise)
        {
            if (!counterClockwise)
            {
                return new Vector2(vector.Y, -vector.X);
            }

            return new Vector2(-vector.Y, vector.X);
        }
    }
}
