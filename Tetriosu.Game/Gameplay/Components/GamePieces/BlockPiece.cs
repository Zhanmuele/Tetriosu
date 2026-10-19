using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Tetriosu.Game.Gameplay.Playfield;

namespace Tetriosu.Game.Gameplay.Components.GamePieces
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
        public readonly bool IsHidden;

        public BlockPiece(Vector2 startPos, Colour4 color, Colour4 colorWhenPlaced, PlayfieldContainer playfield, int i)
        {
            playfield.AddBlockPiece(this);

            BlockPosition = startPos + new Vector2((float)Math.Round(GameSetupValues.DEFAULT_COLUMS / 2, MidpointRounding.AwayFromZero) - 1, 28f);
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

            if (i == 0)
            {
                IsHidden = true;
                activeBlock.Alpha = 0;
            }
            else { IsHidden = false; }
        }

        protected override void Update()
        {
            if (isPlaced) { return; }

            UpdatePosition();

            base.Update();
        }

        public void UpdatePosition() => Position = BlockPosition * blockSize;

        public bool HorizontalCollisionCheck(float x, ref List<BlockPiece?[]> playfieldData)
        {
            if (IsHidden) { return false; }

            if (!(0 <= x && x < GameSetupValues.DEFAULT_COLUMS))
            {
                return true;
            }

            if (playfieldData[(int)BlockPosition.Y][(int)x] != null)
            {
                return true;
            }

            return false;
        }

        public bool VerticalCollisionCheck(float y, ref List<BlockPiece?[]> playfieldData)
        {
            if (IsHidden) { return false; }

            if (!(0 <= y && y < GameSetupValues.DEFAULT_ROWS))
            {
                return true;
            }

            if (playfieldData[(int)y][(int)BlockPosition.X] != null)
            {
                return true;
            }

            return false;
        }

        public Vector2 RotatePiece(Vector2 centerOfRotation, bool CCW, bool flipPiece = false)
        {
            Vector2 offset = BlockPosition - centerOfRotation;

            if (offset == Vector2.Zero) { return centerOfRotation; }

            Vector2 rotatedOffset;

            if (!flipPiece)
            {
                rotatedOffset = rotate90degrees(offset, CCW);
            }
            else
            {
                rotatedOffset = rotate180degrees(offset);
            }

            return centerOfRotation + rotatedOffset;
        }

        private static Vector2 rotate90degrees(Vector2 vector, bool CCW)
        {
            if (CCW) { return new Vector2(vector.Y, -vector.X); }

            return new Vector2(-vector.Y, vector.X);
        }

        private static Vector2 rotate180degrees(Vector2 vector) => -vector;
    }
}
