using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetriosu.Game.Gameplay;
using Tetriosu.Game.Graphics;

namespace Tetriosu.Game.Screens
{
    public partial class SoloGameSelectionScreen : Screen
    {
        private const double fade_animation_duration = 100;
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Background
                {
                    Colour = Colour4.DarkGreen
                },
                new ActionButton(Colour4.MediumSeaGreen, Colour4.Black, "40 Lines", 32, (_, _) => loadSoloGameScreen())
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = -50
                },
                new ActionButton(Colour4.MediumSeaGreen, Colour4.Black, "Blitz / Ultra", 32, (_, _) => loadSoloGameScreen())
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 50
                },
                new ActionButton(Colour4.MediumPurple, Colour4.Black, "Back", 32, (_, _) => this.Exit())
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 150
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

        private void loadSoloGameScreen() => this.Push(new SoloGameScreen());
    }
}
