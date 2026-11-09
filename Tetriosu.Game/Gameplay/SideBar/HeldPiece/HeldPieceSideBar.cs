using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
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

            heldPieceContainer = new Container();

        }

        public void UpdateHeldPieceSideBar()
        {
            heldPieceContainer.Clear(true);


        }
    }
}
