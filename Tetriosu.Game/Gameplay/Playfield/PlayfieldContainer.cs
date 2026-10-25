using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osuTK.Graphics;
using Tetriosu.Game.Gameplay.Components;
using Tetriosu.Game.Gameplay.Components.BagGenerator;
using Tetriosu.Game.Gameplay.Components.GamePieces;
using Tetriosu.Game.Gameplay.GameRuleEnum;
using Tetriosu.Game.Input.Keybinds;

namespace Tetriosu.Game.Gameplay.Playfield
{
    public partial class PlayfieldContainer : CompositeDrawable, IKeyBindingHandler<FullPieceActions>
    {
        private FullPiece currentPiece;
        private Container playfieldBlockContainer;
        private BagGenerator bagGenerator;
        private List<KeyBindingPressEvent<FullPieceActions>> keyPressEventsQueue;
        private List<KeyBindingReleaseEvent<FullPieceActions>> keyReleaseEventsQueue;

        [BackgroundDependencyLoader]
        private void load()
        {
            keyPressEventsQueue = new List<KeyBindingPressEvent<FullPieceActions>>();
            keyReleaseEventsQueue = new List<KeyBindingReleaseEvent<FullPieceActions>>();

            Size = GameSetupValues.DEFAULT_PLAYFIELD_SIZE;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Y = -GameSetupValues.BLOCK_SIZE * (GameSetupValues.DEFAULT_ROWS - GameSetupValues.DEFAULT_VISIBLE_ROWS) / 2;

            InternalChildren = new Drawable[]
            {
                currentPiece = new FullPiece(120, 16.7, 0.0167, 32, RotationSystemAndKicktable.TetrIoSRS),
                bagGenerator = new BagGenerator(NewBagGenerationRules.SevenBag),
                new Box()
                {
                    Size = GameSetupValues.DEFAULT_VISIBLE_PLAYFIELD_SIZE,
                    Colour = Color4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
                playfieldBlockContainer = new Container()
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };

            currentPiece.PiecePlaced += piecePlaced;
        }

        protected override void LoadComplete()
        {
            setNewPiece();
            base.LoadComplete();
        }

        protected override void Update()
        {
            for (int pE = keyPressEventsQueue.Count - 1; pE >= 0; pE--)
            {
                handleKeybindingPressEvent(keyPressEventsQueue[pE]);
                keyPressEventsQueue.RemoveAt(pE);
            }

            for (int rE = keyReleaseEventsQueue.Count - 1; rE >= 0; rE--)
            {
                handleKeyBindingReleaseEvent(keyReleaseEventsQueue[rE]);
                keyReleaseEventsQueue.RemoveAt(rE);
            }

            base.Update();
        }

        public bool OnPressed(KeyBindingPressEvent<FullPieceActions> e)
        {
            if (e.Repeat) { return true; }
            keyPressEventsQueue.Add(e);

            return true;
        }

        public void OnReleased(KeyBindingReleaseEvent<FullPieceActions> e) => keyReleaseEventsQueue.Add(e);

        private void handleKeybindingPressEvent(KeyBindingPressEvent<FullPieceActions> e)
        {
            switch (e.Action)
            {
                case FullPieceActions.MoveLeft:
                    currentPiece.HandleMoveLeftKeyPress();
                    break;

                case FullPieceActions.MoveRight:
                    currentPiece.HandleMoveRightKeyPress();
                    break;

                case FullPieceActions.SoftDrop:
                    currentPiece.HandleSoftDropKeyPress();
                    break;

                case FullPieceActions.HardDrop:
                    currentPiece.HandleHardDropKeyPress();
                    break;

                case FullPieceActions.RotateClockWise:
                    currentPiece.HandleRotateCwKeypress();
                    break;

                case FullPieceActions.RotateCounterClockwise:
                    currentPiece.HandleRotateCcwKeypress();
                    break;

                case FullPieceActions.FlipPiece:
                    currentPiece.HandleFlipPieceKeypress();
                    break;

                default:
                    break;
            }

        }

        private void handleKeyBindingReleaseEvent(KeyBindingReleaseEvent<FullPieceActions> e)
        {
            switch (e.Action)
            {
                case FullPieceActions.MoveLeft:
                    currentPiece.HandleMoveLeftKeyRelease();
                    break;

                case FullPieceActions.MoveRight:
                    currentPiece.HandleMoveRightKeyRelease();
                    break;

                case FullPieceActions.SoftDrop:
                    currentPiece.HandleSoftDropKeyRelease();
                    break;

                case FullPieceActions.HardDrop:
                    currentPiece.HandleHardDropKeyRelease();
                    break;

                case FullPieceActions.RotateClockWise:
                    currentPiece.HandleRotateCwKeyRelease();
                    break;

                case FullPieceActions.RotateCounterClockwise:
                    currentPiece.HandleRotateCcwKeyRelease();
                    break;

                case FullPieceActions.FlipPiece:
                    currentPiece.HandleFlipPieceKeyRelease();
                    break;

                default:
                    break;
            }
        }

        private void piecePlaced(object? sender, BlockPiece? p)
        {
            RemoveBlockPiece(p);

            setNewPiece();
        }

        public void AddBlockPiece(BlockPiece blockPiece) => playfieldBlockContainer.Add(blockPiece);

        public void RemoveBlockPiece(BlockPiece? blockPiece) => playfieldBlockContainer.Remove(blockPiece, true);

        private void setNewPiece() => currentPiece.SetPiece(bagGenerator.GetNextPieceType(), this);
    }
}
