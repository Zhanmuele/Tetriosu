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
            Size = GameSetup.PLAYFIELD_SIZE;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Y = -GameSetup.BLOCK_SIZE * (GameSetup.ROWS - GameSetup.VISIBLE_ROWS) / 2;

            InternalChildren = new Drawable[]
            {
                new Box()
                {
                    Size = GameSetup.VISIBLE_PLAYFIELD_SIZE,
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
    }
}
