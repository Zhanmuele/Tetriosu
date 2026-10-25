using System;
using System.Collections.Generic;
using System.ComponentModel;
using osu.Framework.Graphics;
using osuTK;
using Tetriosu.Game.Gameplay.Components.Kicktables;
using Tetriosu.Game.Gameplay.GameRuleEnum;
using Tetriosu.Game.Gameplay.Playfield;

namespace Tetriosu.Game.Gameplay.Components.GamePieces
{
    public partial class FullPiece : Drawable
    {
        public string HeldPieceType = "";
        public bool HasSwapped = false;
        private int autoLockCountdown = 15;
        private string pieceType = "";
        private List<BlockPiece> blockPieces = new List<BlockPiece>(8);
        private List<Vector2> blockStartingPositions = new List<Vector2>(8);
        private readonly List<BlockPiece?[]> emptyField;
        private List<BlockPiece?[]> playfieldData;

        #region timers (should really create a class for this :P)

        private readonly double dasDelay;
        private readonly double arrDelay;
        private readonly double lockDelay = 500;
        private double? dasDelayTimer;
        private double? arrDelayTimer;
        private double? lockDelayTimer;
        private readonly double gConstant = 16.7;
        private readonly double startingGravityG;
        private double gravityG;
        private readonly double softDropMultiplier;
        private double gravityTimeMs;
        private double softDropGravityTimeMs;
        private double? gravityTimer;

        #endregion

        #region movement variables

        private bool isMoving = false;
        private bool leftMovementKeyHeld = false;
        private bool rightMovementKeyHeld = false;
        private bool rightMovementKeyIsHeldLast = false;
        private bool isSoftDropKeyHeld = false;
        private bool isSoftDropping = false;
        private bool isHardDropKeyHeld = false;
        private bool hasHardDropped = false;

        #endregion

        #region rotation variables

        private int rotationState = 0;
        private bool rotateCwKeyHeld = false;
        private bool rotateCcwKeyHeld = false;
        private bool flipPieceKeyHeld = false;
        private bool hasRotatedCW = false;
        private bool hasRotatedCCW = false;
        private bool hasFlippedPiece = false;

        #endregion

        #region kicktable variables

        private Vector2[][] kicktableCW;
        private Vector2[][] kicktableCCW;
        private Vector2[][] iPieceKicktableCW;
        private Vector2[][] iPieceKicktableCCW;

        #endregion

        #region events

        public event EventHandler<BlockPiece?> PiecePlaced;

        #endregion

        public FullPiece(double dasDelayValue, double arrDelayValue, double startingGravityValue, double softDropMultiplierValue, RotationSystemAndKicktable kicktableRule)
        {
            emptyField = new List<BlockPiece?[]>(50);
            dasDelay = dasDelayValue;
            arrDelay = arrDelayValue;
            gravityG = startingGravityG = startingGravityValue;
            softDropMultiplier = softDropMultiplierValue;

            gravityTimeMs = calculateGravityTime();
            softDropGravityTimeMs = calculateSoftDropGravityTime();

            for (int i = 0; i < GameSetupValues.DEFAULT_ROWS; i++)
            {
                emptyField.Add(new BlockPiece?[(int)GameSetupValues.DEFAULT_COLUMS]);
            }

            playfieldData = [.. emptyField];

            switch (kicktableRule)
            {
                case RotationSystemAndKicktable.ClassicSRS:
                case RotationSystemAndKicktable.TetrIoSRS:
                    kicktableCW = KicktableDictionary.CW_KICKTABLE_SRS;
                    kicktableCCW = KicktableDictionary.CCW_KICKTABLE_SRS;
                    break;

                case RotationSystemAndKicktable.None:
                    break;

                default: throw new InvalidEnumArgumentException();
            }

            switch (kicktableRule)
            {
                case RotationSystemAndKicktable.ClassicSRS:
                    iPieceKicktableCW = KicktableDictionary.CW_I_PIECE_KICKTABLE_CLASSIC_SRS;
                    iPieceKicktableCCW = KicktableDictionary.CCW_I_PIECE_KICKTABLE_CLASSIC_SRS;
                    break;

                case RotationSystemAndKicktable.TetrIoSRS:
                    iPieceKicktableCW = KicktableDictionary.CW_I_PIECE_KICKTABLE_TETRIO_SRS;
                    iPieceKicktableCCW = KicktableDictionary.CCW_I_PIECE_KICKTABLE_TETRIO_SRS;
                    break;

                case RotationSystemAndKicktable.None:
                    break;

                default: throw new InvalidEnumArgumentException();
            }
        }

        // go to heck truncated modulo
        public static int MathModuloByFour(int i) => ((i % 4) + 4) % 4;

        public void SetPiece(string blocktype, PlayfieldContainer playfield)
        {
            if (!isSoftDropping) { activateGravityTimer(); }
            else { activateSoftDropGravityTimer(); }

            pieceType = blocktype;
            isMoving = false;
            rotationState = 0;
            autoLockCountdown = 15;

            blockPieces.Clear();
            blockStartingPositions.Clear();
            blockStartingPositions.AddRange(GameSetupValues.SRS_FULL_BLOCK_SHAPE[pieceType]);

            int i = 0;
            foreach (Vector2 blockPos in blockStartingPositions)
            {
                blockPieces.Add(new BlockPiece(blockPos,
                    GameSetupValues.DEFAULT_FULL_BLOCK_COLORS[pieceType],
                    GameSetupValues.DEFAULT_FULL_BLOCK_COLORS_WHEN_PLACED[pieceType],
                    playfield, i));

                i++;
            }
        }

        protected override void Update()
        {
            handleHardDrop();
            handlePieceRotation();
            handleVerticalMovement();
            handleHorizontalMovement();
            updateTimers();

            base.Update();
        }

        #region gravity calculation

        private double calculateGravityTime() => Math.Floor(gConstant / gravityG);

        private double calculateSoftDropGravityTime() => Math.Floor(gravityTimeMs / softDropMultiplier);

        #endregion

        #region movement key events

        public void HandleMoveLeftKeyPress()
        {
            leftMovementKeyHeld = true;
            rightMovementKeyIsHeldLast = false;
        }

        public void HandleMoveLeftKeyRelease() => leftMovementKeyHeld = false;

        public void HandleMoveRightKeyPress()
        {
            rightMovementKeyHeld = true;
            rightMovementKeyIsHeldLast = true;
        }

        public void HandleMoveRightKeyRelease() => rightMovementKeyHeld = false;

        public void HandleSoftDropKeyPress() => isSoftDropKeyHeld = true;

        public void HandleSoftDropKeyRelease() => isSoftDropKeyHeld = false;

        #endregion

        #region hard drop, hold piece key events

        public void HandleHardDropKeyPress() => isHardDropKeyHeld = true;

        public void HandleHardDropKeyRelease()
        {
            isHardDropKeyHeld = false;
            hasHardDropped = false;
        }

        #endregion

        #region rotation key events

        public void HandleRotateCwKeypress() => rotateCwKeyHeld = true;

        public void HandleRotateCcwKeypress() => rotateCcwKeyHeld = true;

        public void HandleFlipPieceKeypress() => flipPieceKeyHeld = true;

        public void HandleRotateCwKeyRelease()
        {
            rotateCwKeyHeld = false;
            hasRotatedCW = false;
        }

        public void HandleRotateCcwKeyRelease()
        {
            rotateCcwKeyHeld = false;
            hasRotatedCCW = false;
        }

        public void HandleFlipPieceKeyRelease()
        {
            flipPieceKeyHeld = false;
            hasFlippedPiece = false;
        }

        #endregion

        #region horizontal movement 

        private void handleHorizontalMovement()
        {
            if (!leftMovementKeyHeld && !rightMovementKeyHeld)
            {
                deactivateArrTimer();
                isMoving = false;
                return;
            }

            int i = handleHorizontalMovementKeypressPriority();

            if (i == 0) { return; }

            if (!isMoving)
            {
                activateDasTimer();
                attemptHorizontalMovement(i);
            }
            else if ((dasDelayTimer <= 0 || dasDelayTimer == null) && (arrDelayTimer <= 0 || arrDelayTimer == null))
            {
                if (dasDelayTimer is not null)
                {
                    activateArrTimer();
                    arrDelayTimer -= dasDelayTimer;
                    deactivateDasTimer();
                }

                arrDelayTimer += arrDelay;
                attemptHorizontalMovement(i);
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

        private bool attemptHorizontalMovement(int x)
        {
            if (!horizontalCollisionCheck(x))
            {
                movePieceHorizontally(x);
                verticalCollisiionCheck(1);

                return true;
            }

            return false;
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

        private void movePieceHorizontally(int x)
        {
            isMoving = true;

            foreach (BlockPiece p in blockPieces)
            {
                p.BlockPosition.X += x;
            }
        }

        #endregion

        #region vertical movement

        private void handleVerticalMovement()
        {
            if (gravityTimeMs <= 2 || (isSoftDropKeyHeld && softDropGravityTimeMs <= 2))
            {
                pushPieceToBottomOfPlayfield();
                return;
            }

            if (isSoftDropKeyHeld && !isSoftDropping)
            {
                attemptVerticalMovement(1);
                activateSoftDropGravityTimer();
                isSoftDropping = true;
            }
            else if (!isSoftDropKeyHeld && isSoftDropping)
            {
                isSoftDropping = false;
                gravityTimer = gravityTimeMs - gravityTimer;
            }

            if (gravityTimer <= 0)
            {
                if (isSoftDropping)
                {
                    gravityTimer += softDropGravityTimeMs;
                }
                else { gravityTimer += gravityTimeMs; }

                attemptVerticalMovement(1);
            }
        }

        private void pushPieceToBottomOfPlayfield()
        {
            while (attemptVerticalMovement(1)) { }
        }

        private bool attemptVerticalMovement(int y)
        {
            if (!verticalCollisiionCheck(y))
            {
                movePieceVertically(y);
                verticalCollisiionCheck(y);

                return true;
            }

            return false;
        }

        private bool verticalCollisiionCheck(int y)
        {
            foreach (BlockPiece p in blockPieces)
            {
                if (p.VerticalCollisionCheck(p.BlockPosition.Y + y, ref playfieldData))
                {
                    if (lockDelayTimer == null) { activateLoclDelayTimer(); }
                    deactivateGravityTimer();

                    return true;
                }
            }

            deactivateLockDelayTimer();
            if (gravityTimer == null)
            {
                if (isSoftDropping) { activateSoftDropGravityTimer(); }
                else { activateGravityTimer(); }
            }

            return false;
        }

        private void movePieceVertically(int y)
        {
            foreach (BlockPiece p in blockPieces)
            {
                p.BlockPosition.Y += y;
            }
        }

        #endregion

        #region hard drop, hold piece and piece locking

        protected void OnPiecePlaced(BlockPiece? p)
        {
            PiecePlaced?.Invoke(this, p);
        }

        private void handleHardDrop()
        {
            if (hasHardDropped || !isHardDropKeyHeld) { return; }

            hasHardDropped = true;
            pushPieceToBottomOfPlayfield();
            placePiece();
        }

        private void placePiece()
        {
            BlockPiece? pToRemove = null;

            foreach (BlockPiece p in blockPieces)
            {
                if (p.IsHidden)
                {
                    pToRemove = p;
                    continue;
                }

                playfieldData[(int)p.BlockPosition.Y][(int)p.BlockPosition.X] = p;
                p.UpdatePosition();
                p.PlacePiece();
            }

            OnPiecePlaced(pToRemove);
        }

        #endregion

        #region piece rotation

        private void handlePieceRotation()
        {
            if (!rotateCwKeyHeld && !rotateCcwKeyHeld && !flipPieceKeyHeld) { return; }

            if (rotateCwKeyHeld && !hasRotatedCW)
            {
                attemptClockwiseRotation();
            }

            if (rotateCcwKeyHeld && !hasRotatedCCW)
            {
                attemptCounterClockwiseRotation();
            }
        }

        private void attemptClockwiseRotation()
        {
            bool isCollidingAfterRotation = true;
            int failedRotationAttempts = -1;
            rotationState += 1;
            hasRotatedCW = true;

            Vector2 appliedKick;
            List<Vector2> rotatedPositions = rotatePiece(false);

            while (isCollidingAfterRotation)
            {
                isCollidingAfterRotation = rotatedPositionCollisionCheck(rotatedPositions);

                if (isCollidingAfterRotation)
                {
                    failedRotationAttempts += 1;
                    rotatedPositions = rotatePiece(false);

                    try
                    {
                        if (pieceType != "I")
                        {
                            appliedKick = kicktableCW[MathModuloByFour(rotationState)][failedRotationAttempts];
                        }
                        else { appliedKick = iPieceKicktableCW[MathModuloByFour(rotationState)][failedRotationAttempts]; }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        rotationState -= 1;
                        return;
                    }

                    applyRotationKicktable(ref rotatedPositions, appliedKick);
                }
            }

            updateRotatedPiecePositions(ref blockPieces, rotatedPositions);
            verticalCollisiionCheck(1);
        }

        private void attemptCounterClockwiseRotation()
        {
            bool isCollidingAfterRotation = true;
            int failedRotationAttempts = -1;
            rotationState -= 1;
            hasRotatedCCW = true;

            Vector2 appliedKick;
            List<Vector2> rotatedPositions = rotatePiece(true);

            while (isCollidingAfterRotation)
            {
                isCollidingAfterRotation = rotatedPositionCollisionCheck(rotatedPositions);

                if (isCollidingAfterRotation)
                {
                    failedRotationAttempts += 1;
                    rotatedPositions = rotatePiece(true);

                    try
                    {
                        if (pieceType != "I")
                        {
                            appliedKick = kicktableCCW[MathModuloByFour(rotationState)][failedRotationAttempts];
                        }
                        else { appliedKick = iPieceKicktableCCW[MathModuloByFour(rotationState)][failedRotationAttempts]; }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        rotationState += 1;
                        return;
                    }

                    applyRotationKicktable(ref rotatedPositions, appliedKick);
                }
            }

            updateRotatedPiecePositions(ref blockPieces, rotatedPositions);
            verticalCollisiionCheck(1);
        }

        private List<Vector2> rotatePiece(bool CCW, bool flipPiece = false)
        {
            Vector2 centerOfRotation = blockPieces[0].BlockPosition;
            List<Vector2> rotatedPositions = new List<Vector2>(8);

            foreach (BlockPiece p in blockPieces)
            {
                Vector2 rotPos = p.RotatePosition(centerOfRotation, CCW, flipPiece);
                rotatedPositions.Add(rotPos);
            }

            return rotatedPositions;
        }

        private bool rotatedPositionCollisionCheck(List<Vector2> rotatedPositions)
        {
            foreach (Vector2 pos in rotatedPositions)
            {
                try
                {
                    if (pos.X < 0 || pos.X >= GameSetupValues.DEFAULT_COLUMS || pos.Y < 0 || pos.Y >= GameSetupValues.DEFAULT_ROWS)
                    {
                        return true;
                    }

                    if (playfieldData[(int)pos.Y][(int)pos.X] is not null)
                    {
                        return true;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    return true;
                }
            }

            return false;
        }

        private void applyRotationKicktable(ref List<Vector2> rotatedPositions, Vector2 appliedKick)
        {
            for (int i = 0; i < rotatedPositions.Count; i++)
            {
                rotatedPositions[i] += appliedKick;
            }
        }

        private void updateRotatedPiecePositions(ref List<BlockPiece> blockPieces, List<Vector2> rotatedPositions)
        {
            for (int i = 0; i < blockPieces.Count; i++)
            {
                blockPieces[i].BlockPosition = rotatedPositions[i];
            }
        }

        #endregion

        #region timer handling (should really create a class for this :P)

        private void activateDasTimer() => dasDelayTimer = dasDelay;

        private void activateArrTimer() => arrDelayTimer = arrDelay;

        private void activateLoclDelayTimer() => lockDelayTimer = lockDelay;

        private void activateGravityTimer() => gravityTimer = gravityTimeMs;

        private void activateSoftDropGravityTimer() => gravityTimer = softDropGravityTimeMs;

        private void deactivateDasTimer() => dasDelayTimer = null;

        private void deactivateArrTimer() => arrDelayTimer = null;

        private void deactivateLockDelayTimer() => lockDelayTimer = null;

        private void deactivateGravityTimer() => gravityTimer = null;

        private void updateTimers()
        {
            gravityTimer -= Time.Elapsed;
            if (dasDelayTimer is not null) { dasDelayTimer -= Time.Elapsed; }
            if (arrDelayTimer is not null) { arrDelayTimer -= Time.Elapsed; }
            if (lockDelayTimer is not null) { lockDelayTimer -= Time.Elapsed; }
        }

        #endregion
    }
}
