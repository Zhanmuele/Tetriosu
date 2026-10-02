using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using Tetriosu.Game.BaseElements;
using Tetriosu.Game.Screens;

namespace Tetriosu.Game.Graphics.Buttons
{
    public partial class SoloPlayButton : ActionButton
    {
        private readonly float animationDuration = 150;
        private readonly Vector2 buttonSize = new Vector2(200, 75);
        private readonly float fontSize = 32;

        public SoloPlayButton()
        {
        }

        protected override void LoadComplete()
        {
            AutoSizeAxes = Axes.Both;
            Background.Size = buttonSize;
            Background.Colour = Colour4.MediumPurple;
            Text.Colour = Colour4.Black;
            Text.Text = "Single";
            Text.Font = FontUsage.Default.With(size: fontSize);
            base.LoadComplete();
        }

        protected override bool OnClick(ClickEvent e)
        {
            TetriosuGame.ScreenStack.Push(new SoloGameScreen());
            return base.OnClick(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            Background.ResizeTo(buttonSize * new Vector2(1.5f, 1), animationDuration, Easing.InSine);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            Background.ResizeTo(buttonSize, animationDuration, Easing.OutSine);
            base.OnHoverLost(e);
        }
    }
}
