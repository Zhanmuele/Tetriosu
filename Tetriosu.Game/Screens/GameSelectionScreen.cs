using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetriosu.Game.Graphics;

namespace Tetriosu.Game.Screens
{
    public partial class GameSelectionScreen : TetriosuScreen
    {
        private const double fade_animation_duration = 100;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Background
                {
                    Colour = Colour4.MediumPurple
                },
                new ActionButton(Colour4.DarkGreen, Colour4.Black, "Solo game", 32, (_, _) => loadSoloGameSelectionScreen())
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = -50
                },
                new ActionButton(Colour4.DarkOrange, Colour4.Black, "Back", 32, (_, _) => this.Exit())
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 50
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

        private void loadSoloGameSelectionScreen() => this.Push(new SoloGameSelectionScreen());
    }
}
