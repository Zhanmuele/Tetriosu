using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Tetriosu.Game.Gameplay.Playfield;
using Tetriosu.Game.MathFunctions;

namespace Tetriosu.Game.Gameplay.Components.GamePieces
{
    public partial class BlockPiece : Container<Box>
    {
        private static readonly Vector2 block_size = GameSetupValues.BLOCK_SIZE_V;
        private bool isPlaced = false;
        private Box activeBlock;
        private Box placedBlock;
        public Vector2 BlockPosition;
        public readonly bool IsHidden;

        public BlockPiece(Vector2 startPos, Colour4 colour, Colour4 colourWhenPlaced, PlayfieldContainer playfield, int i)
        {
            BlockPosition = startPos + new Vector2((float)Math.Ceiling(GameSetupValues.DEFAULT_COLUMS / 2) - 1, GameSetupValues.DEFAULT_ROWS - 22f);
            Position = BlockPosition * block_size;
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;

            AddRange(new Box[]
            {
                activeBlock = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = block_size,
                    Colour = colour,
                },
                placedBlock = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = block_size,
                    Colour = colourWhenPlaced,
                    Alpha = 0,
                }
            });

            if (i == 0)
            {
                IsHidden = true;
                activeBlock.Alpha = 0;
            }
            else { IsHidden = false; }

            playfield.AddBlockPiece(this);
        }

        public void PlacePiece()
        {
            isPlaced = true;
            placedBlock.Alpha = 1;
            activeBlock.Alpha = 0;
        }

        public void UpdatePosition() => Position = BlockPosition * block_size;

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

        public Vector2 RotatePosition(Vector2 centerOfRotation, bool CCW, bool flipPiece = false)
        {
            Vector2 offset = BlockPosition - centerOfRotation;

            if (offset == Vector2.Zero) { return centerOfRotation; }

            Vector2 rotatedOffset;

            if (!flipPiece)
            {
                rotatedOffset = Vector2Math.RotateVector2(offset, CCW);
            }
            else
            {
                rotatedOffset = Vector2Math.Rotate180degrees(offset);
            }

            return centerOfRotation + rotatedOffset;
        }
    }
}
