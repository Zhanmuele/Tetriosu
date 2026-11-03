using System.Collections.Generic;
using ManagedBass.Fx;
using OpenTabletDriver.Native.Windows.USB;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using osuTK.Graphics;
using Tetriosu.Game.Gameplay.Components;

namespace Tetriosu.Game.Gameplay.SideBar
{
    public partial class SideBarFullPiece : Container
    {
        private Vector2[] positions;
        private Container box;
        private readonly string pieceType;

        public SideBarFullPiece(string pieceType)
        {
            this.pieceType = pieceType;
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;

            InternalChild = box = new Container()
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
            };

            positions = new Vector2[GameSetupValues.DEFAULT_FULL_BLOCK_SHAPE_POS[pieceType].Length];
            GameSetupValues.DEFAULT_FULL_BLOCK_SHAPE_POS[pieceType].CopyTo(positions, 0);

            bool hasNegativeX = true;
            while (hasNegativeX)
            {
                hasNegativeX = vector2ArrayHasNegativeX(ref positions);

                if (hasNegativeX)
                {
                    for (int i = 0; i < positions.Length; i++)
                    {
                        positions[i] += Vector2.UnitX;
                    }
                }
            }

            bool hasNegativeY = true;
            while (hasNegativeY)
            {
                hasNegativeY = vector2ArrayHasNegativeY(ref positions);

                if (hasNegativeY)
                {
                    for (int i = 0; i < positions.Length; i++)
                    {
                        positions[i] += Vector2.UnitY;
                    }
                }
            }

            int x = 0;
            foreach (Vector2 pos in positions)
            {
                if (x == 0) { x++; continue; }

                box.Add(new SideBarBlockPiece(pos, GameSetupValues.DEFAULT_FULL_BLOCK_COLORS_WHEN_PLACED[pieceType])
                {
                    Size = GameSetupValues.BLOCK_SIZE_V,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                });
            }
        }

        private static bool vector2ArrayHasNegativeX(ref Vector2[] positions)
        {
            foreach (Vector2 pos in positions)
            {
                if (pos.X < 0) { return true; }
            }

            return false;
        }

        private static bool vector2ArrayHasNegativeY(ref Vector2[] positions)
        {
            foreach (Vector2 pos in positions)
            {
                if (pos.Y < 0) { return true; }
            }

            return false;
        }

        public override string ToString()
        {
            return $"{base.ToString()}_Type={pieceType}";
        }
    }
}
