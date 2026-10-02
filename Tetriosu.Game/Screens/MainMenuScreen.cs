using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using Tetriosu.Game.Graphics;

namespace Tetriosu.Game.Screens
{
    public partial class MainMenuScreen : Screen
    {
        public Action? OnPlayGameButtonClicked;
        private const double fade_animation_duration = 100;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Background
                {
                    Colour = Colour4.DarkOrange,
                },
                new SpriteText
                {
                    Y = 20,
                    Text = "Tetriosu",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 48),
                },
                new SpinningBox
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                new ActionButton(Colour4.MediumPurple, Colour4.Black, "Play game", 32, (_, _) => loadGameSelectionScreen())
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 250
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

        private void loadGameSelectionScreen() => this.Push(new GameSelectionScreen());
    }
}
