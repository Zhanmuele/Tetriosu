using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Tetriosu.Game.Graphics
{
    public partial class ActionButton : Container
    {
        private readonly float animationDuration = 150;
        private readonly Vector2 buttonSize = new Vector2(250, 75);
        private Box background;
        private SpriteText text;
        private readonly Action<ActionButton, UIEvent>? clickAction;

        public ActionButton(Color4 backgroundColour, Color4 textColour, string buttonText, float fontSize, Action<ActionButton, UIEvent>? clickAction = null)
        {
            this.clickAction = clickAction;
            AutoSizeAxes = Axes.Both;

            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Children = new Drawable[]
                    {
                        background = new Box
                        {
                            Size =  buttonSize,
                            Colour = backgroundColour,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        },
                        text = new SpriteText
                        {
                            Colour = textColour,
                            Text = buttonText,
                            Font = FontUsage.Default.With(size: fontSize),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        }
                    }
                },
            });
        }

        protected override bool OnClick(ClickEvent e)
        {
            clickAction?.Invoke(this, e);
            return base.OnClick(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            background.ResizeTo(buttonSize * new Vector2(1.5f, 1), animationDuration, Easing.InSine);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            background.ResizeTo(buttonSize, animationDuration, Easing.OutSine);
            base.OnHoverLost(e);
        }
    }
}
