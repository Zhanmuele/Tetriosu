using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osuTK.Graphics;
using osuTK.Input;
using Tetriosu.Game.Gameplay.Components;
using Tetriosu.Game.Gameplay.Components.BagGenerator;
using Tetriosu.Game.Gameplay.GameRuleEnum;
using Tetriosu.Game.Input.Keybinds;

namespace Tetriosu.Game.Gameplay.Playfield
{
    public partial class PlayfieldContainer : Container, IKeyBindingHandler<FullPieceActions>
    {
        private FullPiece currentPiece;
        private Container playfieldBlockContainer;
        private BagGenerator bagGenerator;
        private List<KeyBindingPressEvent<FullPieceActions>> fullPiecePressEventsQueue;
        private List<KeyBindingReleaseEvent<FullPieceActions>> fullPieceReleaseEventsQueue;

        [BackgroundDependencyLoader]
        private void load()
        {
            fullPiecePressEventsQueue = new List<KeyBindingPressEvent<FullPieceActions>>();
            fullPieceReleaseEventsQueue = new List<KeyBindingReleaseEvent<FullPieceActions>>();

            Size = GameSetupValues.PLAYFIELD_SIZE;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Y = -GameSetupValues.BLOCK_SIZE * (GameSetupValues.ROWS - GameSetupValues.DEFAULT_VISIBLE_ROWS) / 2;

            InternalChildren = new Drawable[]
            {
                currentPiece = new FullPiece(167, 16.7),
                bagGenerator = new BagGenerator(NewBagGenerationRules.SevenBag),
                new Box()
                {
                    Size = GameSetupValues.VISIBLE_PLAYFIELD_SIZE,
                    Colour = Color4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
                playfieldBlockContainer = new Container()
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }

        protected override void LoadComplete()
        {
            currentPiece.SetPiece(bagGenerator.GetNextPieceType(), this);
            base.LoadComplete();
        }

        public void AddBlockPiece(BlockPiece blockPiece) => playfieldBlockContainer.Add(blockPiece);

        public bool OnPressed(KeyBindingPressEvent<FullPieceActions> e)
        {
            if (e.Repeat) { return true; }
            fullPiecePressEventsQueue.Add(e);
            return true;
        }

        public void OnReleased(KeyBindingReleaseEvent<FullPieceActions> e) => fullPieceReleaseEventsQueue.Add(e);

        protected override void Update()
        {
            for (int pE = fullPiecePressEventsQueue.Count - 1 ; pE >= 0; pE--)
            {
                handleKeybindingPressEvent(fullPiecePressEventsQueue[pE]);
                fullPiecePressEventsQueue.RemoveAt(pE);
            }

            for (int rE = fullPieceReleaseEventsQueue.Count - 1; rE >= 0; rE--)
            {
                handleKeyBindingReleaseEvent(fullPieceReleaseEventsQueue[rE]);
                fullPieceReleaseEventsQueue.RemoveAt(rE);
            }

            base.Update();
        }

        private void handleKeybindingPressEvent(KeyBindingPressEvent<FullPieceActions> e)
        {
            switch (e.Action)
            {
                case FullPieceActions.MoveLeft:
                    currentPiece.HandleMoveLeftPress();
                    break;

                case FullPieceActions.MoveRight:
                    currentPiece.HandleMoveRightPress();
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
                    currentPiece.HandleMoveLeftRelease();
                    break;

                case FullPieceActions.MoveRight:
                    currentPiece.HandleMoveRightRelease();
                    break;

                default:
                    break;
            }
        }
    }
}
