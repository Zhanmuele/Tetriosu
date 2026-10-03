using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Tetriosu.Game.Gameplay.Components
{
    public partial class FullBlock
    {
        private char blockType;
        private bool isMoving = false;
        private int rotationState = 0;
        private int autoLockCounter = 15;
        private BlockPiece[] blockPieces = new BlockPiece[4];

        public FullBlock(char blockType)
        {
            this.blockType = blockType;
            
        }
    }
}
