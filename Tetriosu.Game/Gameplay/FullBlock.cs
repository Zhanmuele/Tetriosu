
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Tetriosu.Game.Gameplay
{
    public partial class FullBlock
    {
        private char blockType;
        private bool isMoving = false;
        private int rotationState = 0;
        private int autoLockCounter = 15;

        public FullBlock(Vector2 startPos, char blockType)
        {
            this.blockType = blockType;
        }

        [BackgroundDependencyLoader]
        private void load()
        {

        }
    }
}
