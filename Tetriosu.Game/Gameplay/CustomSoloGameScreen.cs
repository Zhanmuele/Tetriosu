using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using Tetriosu.Game.Gameplay.Playfield;
using Tetriosu.Game.Graphics;
using Tetriosu.Game.Input.Keybinds;
using Tetriosu.Game.Screens;

namespace Tetriosu.Game.Gameplay
{
    public partial class CustomSoloGameScreen : TetriosuScreen
    {
        private const double fade_animation_duration = 150;
        private Container box;
        private PlayfieldContainer playfieldContainer;
        private FullPieceKeybindsContainer fullPieceKeybindsContainer;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = box = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    new Background
                    {
                        Colour = Colour4.DarkGray
                    },
                    fullPieceKeybindsContainer = new FullPieceKeybindsContainer()
                    {
                        Child = playfieldContainer = new PlayfieldContainer()
                    },
                }
            };
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            this.FadeInFromZero(fade_animation_duration, Easing.In);
            base.OnEntering(e);
        }

        public override bool OnExiting(ScreenExitEvent e)
        {
            this.FadeOut(fade_animation_duration, Easing.Out);
            return base.OnExiting(e);
        }

        public override void OnSuspending(ScreenTransitionEvent e)
        {
            this.FadeOut(fade_animation_duration, Easing.Out);
            base.OnSuspending(e);
        }

        public override void OnResuming(ScreenTransitionEvent e)
        {
            this.FadeIn(fade_animation_duration, Easing.In);
            base.OnResuming(e);
        }
    }
}
