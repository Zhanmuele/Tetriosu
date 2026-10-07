using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using Tetriosu.Game.Gameplay.Components;
using Tetriosu.Game.Graphics;

namespace Tetriosu.Game.Gameplay.Playfield
{
    public partial class PlayfieldContainer : Container
    {
        private Container playfieldBlockContainer;

        [BackgroundDependencyLoader]
        private void load()
        {
            Size = GameSetupValues.PLAYFIELD_SIZE;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Y = -GameSetupValues.BLOCK_SIZE * (GameSetupValues.ROWS - GameSetupValues.DEFAULT_VISIBLE_ROWS) / 2;

            InternalChildren = new Drawable[]
            {
                new Box()
                {
                    Size = GameSetupValues.VISIBLE_PLAYFIELD_SIZE,
                    Colour = Color4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
                playfieldBlockContainer = new Container()
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }

        public void AddBlockPiece(BlockPiece blockPiece) => playfieldBlockContainer.Add(blockPiece);
    }
}
