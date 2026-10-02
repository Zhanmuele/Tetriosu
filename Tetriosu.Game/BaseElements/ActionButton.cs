using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;

namespace Tetriosu.Game.BaseElements
{
    public abstract partial class ActionButton : Container
    {
        private Container box;
        public Box Background;
        public SpriteText Text;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    Background = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    Text = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    }
                }
            };
        }

        protected override bool OnClick(ClickEvent e)
        {
            return base.OnClick(e);
        }
    }
}
