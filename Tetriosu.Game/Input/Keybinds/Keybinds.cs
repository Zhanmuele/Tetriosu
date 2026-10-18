using System.Collections.Generic;
using osu.Framework.Input.Bindings;

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
        FlipPiece,
        HoldPiece,
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
             new KeyBinding(InputKey.Z, FullPieceActions.FlipPiece),
             new KeyBinding(InputKey.C, FullPieceActions.HoldPiece),
         };
    }
}
