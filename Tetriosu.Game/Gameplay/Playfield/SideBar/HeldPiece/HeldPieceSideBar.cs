using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using Tetriosu.Game.Gameplay.Components;
using Tetriosu.Game.Gameplay.Components.BagGenerator;

namespace Tetriosu.Game.Gameplay.SideBar.HeldPiece
{
    public partial class HeldPieceSideBar : CompositeDrawable
    {
        private Container heldPieceContainer;

        public HeldPieceSideBar()
        {
            AutoSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                new Box()
                {
                    Colour = Color4.Black,
                    Width = GameSetupValues.BLOCK_SIZE * 6,
                    Height = GameSetupValues.BLOCK_SIZE * 4,
                },
                heldPieceContainer = new Container()
                {
                    Width = GameSetupValues.BLOCK_SIZE * 6,
                    Height = GameSetupValues.BLOCK_SIZE * 4,
                },
            };
        }

        public void UpdateShownPiece(string heldPieceType)
        {
            heldPieceContainer.Clear(true);
            heldPieceContainer.Add(new SideBarFullPiece(heldPieceType)
            {
                Y = GameSetupValues.BLOCK_SIZE
            });
        }
    }
}
