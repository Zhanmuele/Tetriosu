using osu.Framework.Extensions.ObjectExtensions;
using osu.Framework.Graphics;
using osuTK;
using Tetriosu.Game.Gameplay.Playfield;

namespace Tetriosu.Game.Gameplay.Components
{
    public partial class FullPiece : Drawable
    {
        private readonly int dasDelay;
        private readonly int arrDelay;
        private double? dasDelayTimer;
        private double? arrDelayTimer;
        private int rotationState = 0;
        private int autoLockCounter = 15;
        private string blockType = "";
        private bool isMoving = false;
        private bool leftMovementKeyHeld = false;
        private bool rightMovementKeyHeld = false;
        private bool rightMovementKeyIsHeldLast = false;
        private BlockPiece[] blockPieces = new BlockPiece[4];
        private float[,] blockPositions = new float[4, 2];
        private BlockPiece?[,] playfieldData = new BlockPiece?[GameSetupValues.ROWS, GameSetupValues.DEFAULT_COLUMS];

        public FullPiece(int dasDelayValue, int arrDelayValue)
        {
            dasDelay = dasDelayValue;
            arrDelay = arrDelayValue;
        }

        public void SetPiece(string blocktype, PlayfieldContainer playfield)
        {
            blockType = blocktype;
            blockPositions = GameSetupValues.FULL_BLOCK_SHAPE[blockType];

            deactivateDasTimer();
            deactivateArrTimer();

            isMoving = false;
            rotationState = 0;
            autoLockCounter = 15;
            leftMovementKeyHeld = false;
            rightMovementKeyHeld = false;
            rightMovementKeyIsHeldLast = false;

            for (int i = 0; i < 4; i++)
            {
                blockPieces[i] = new BlockPiece(
                    new Vector2(blockPositions[i, 0], blockPositions[i, 1]),
                    GameSetupValues.FULL_BLOCK_COLORS[blockType],
                    GameSetupValues.FULL_BLOCK_COLORS_WHEN_PLACED[blockType],
                    playfield);
            }
        }

        protected override void Update()
        {
            updateTimers();
            handleHorizontalMovement();

            base.Update();
        }

        public void HandleMoveLeftRelease() => leftMovementKeyHeld = false;

        public void HandleMoveRightRelease() => rightMovementKeyHeld = false;

        public void HandleMoveLeftPress()
        {
            leftMovementKeyHeld = true;
            rightMovementKeyIsHeldLast = false;
        }

        public void HandleMoveRightPress()
        {
            rightMovementKeyHeld = true;
            rightMovementKeyIsHeldLast = true;
        }

        private void handleHorizontalMovement()
        {
            if (!leftMovementKeyHeld && !rightMovementKeyHeld)
            {
                isMoving = false;
                return;
            }

            int i = handleHorizontalMovementKeypressPriority();

            if (i == 0) { return; }

            if (!isMoving)
            {
                activateDasTimer();
                handleMovePiece(i);
            }

            else if (dasDelayTimer.IsNull() && arrDelayTimer.IsNull())
            {
                activateArrTimer();
                handleMovePiece(i);
            }
        }

        private int handleHorizontalMovementKeypressPriority()
        {
            if (leftMovementKeyHeld && rightMovementKeyHeld)
            {
                return rightMovementKeyIsHeldLast ? 1 : -1;
            }

            if (leftMovementKeyHeld) { return -1; }
            if (rightMovementKeyHeld) { return 1; }
            return 0;
        }

        private void handleMovePiece(int i)
        {
            if (!horizontalCollisionCheck(i))
            {
                movePiece(i);
            }
        }

        private bool horizontalCollisionCheck(int x)
        {
            foreach (BlockPiece p in blockPieces)
            {
                if (p.HorizontalCollisionCheck(p.BlockPosition.X + x, ref playfieldData))
                {
                    return true;
                }
            }

            return false;
        }

        private void movePiece(int x)
        {
            isMoving = true;

            foreach (BlockPiece p in blockPieces)
            {
                p.BlockPosition.X += x;
            }
        }

        private void activateDasTimer() => dasDelayTimer = dasDelay;

        private void activateArrTimer() => arrDelayTimer = arrDelay;

        private void deactivateDasTimer() => dasDelayTimer = null;

        private void deactivateArrTimer() => arrDelayTimer = null;

        private void updateTimers()
        {
            if (dasDelayTimer.IsNotNull()) { dasDelayTimer -= Time.Elapsed; }
            if (arrDelayTimer.IsNotNull()) { arrDelayTimer -= Time.Elapsed; }

            if (dasDelayTimer <= 0) { deactivateDasTimer(); }
            if (arrDelayTimer <= 0) { deactivateArrTimer(); }
        }
    }
}
