using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using Tetriosu.Game.Gameplay.Components;
using Tetriosu.Game.Gameplay.GameRuleEnum;
using Tetriosu.Game.Gameplay.Playfield;
using Tetriosu.Game.Graphics;

namespace Tetriosu.Game.Gameplay
{
    public partial class SoloGameScreen : Screen
    {
        private const double fade_animation_duration = 150;
        private Container box;
        private GameFunctions gameVariables;
        private PlayfieldContainer playfieldContainer;
        public FullPiece CurrentPiece;

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
                    gameVariables = new GameFunctions(),
                    new Background
                    {
                        Colour = Colour4.DarkGray
                    },
                    playfieldContainer = new PlayfieldContainer{}
                }
            };
        }
        
        protected override void LoadComplete()
        {
            CurrentPiece = new FullPiece(gameVariables.GetNextPieceType(NewBagGenerationRules.SevenBag), playfieldContainer);
            base.LoadComplete();
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
