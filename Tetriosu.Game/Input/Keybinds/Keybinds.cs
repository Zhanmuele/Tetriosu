using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osuTK.Input;

namespace Tetriosu.Game.Input.Keybinds
{
    public enum FullPieceActions
    {
        MoveLeft,
        MoveRight,
        SoftDrop,
        HardDrop,
        RotateClockWise,
        RotateCounterClockwise,
        Rotate180Degrees,
        HoldPiece,
    }

    public enum GameHandleActions
    {
        ExitGame,
    }

    public partial class FullPieceKeybindsContainer(
        SimultaneousBindingMode sbm = SimultaneousBindingMode.All,
        KeyCombinationMatchingMode kcm = KeyCombinationMatchingMode.Any) :
        KeyBindingContainer<FullPieceActions>(sbm, kcm)
    {
        public override IEnumerable<IKeyBinding> DefaultKeyBindings => new[]
        {
             new KeyBinding(InputKey.Left, FullPieceActions.MoveLeft),
             new KeyBinding(InputKey.Right, FullPieceActions.MoveRight),
             new KeyBinding(InputKey.Down, FullPieceActions.SoftDrop),
             new KeyBinding(InputKey.Space, FullPieceActions.HardDrop),
             new KeyBinding(InputKey.Up, FullPieceActions.RotateClockWise),
             new KeyBinding(InputKey.X, FullPieceActions.RotateCounterClockwise),
             new KeyBinding(InputKey.Z, FullPieceActions.Rotate180Degrees),
             new KeyBinding(InputKey.C, FullPieceActions.HoldPiece),
         };
    }

    public partial class GameHandleKeybinds : KeyBindingContainer<GameHandleActions>
    {
        public override IEnumerable<IKeyBinding> DefaultKeyBindings => new[]
        {
             new KeyBinding(InputKey.Escape, GameHandleActions.ExitGame),
        };
    }
}
