using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osuTK;
using Tetriosu.Game.Gameplay.Keybinds;
using Tetriosu.Game.Gameplay.Playfield;

namespace Tetriosu.Game.Gameplay.Components
{
    public partial class FullPiece : GameKeybinds
    {
        private char blockType;
        private bool isMoving = false;
        private int rotationState = 0;
        private int autoLockCounter = 15;
        private BlockPiece[] blockPieces = new BlockPiece[4];
        private float[,] blockPositions = new float[4,2];

        public FullPiece(char blockType, PlayfieldContainer playfield)
        {
            this.blockType = blockType;
            blockPositions = GameSetup.FULL_BLOCK_SHAPE[blockType];

            for (int i = 0; i < 4; i++)
            {
                blockPieces[i] = new BlockPiece(
                    new Vector2(blockPositions[i, 0], blockPositions[i, 1]),
                    GameSetup.FULL_BLOCK_COLORS[blockType],
                    GameSetup.FULL_BLOCK_COLORS_WHEN_PLACED[blockType],
                    playfield);
            }
        }
    }
}
