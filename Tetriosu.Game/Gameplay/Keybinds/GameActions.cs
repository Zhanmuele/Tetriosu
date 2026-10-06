using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Input.Bindings;

namespace Tetriosu.Game.Gameplay.Keybinds
{
    public enum GameAction
    {
        MoveLeft,
        MoveRight,
        SoftDrop,
        HardDrop,
        RotateClockWise,
        RotateCounterClockwise,
        Rotate180Degrees,
        HoldPiece,
        ExitGame,
    }

    public partial class GameKeybinds : KeyBindingContainer
    {
        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
        {
             new KeyBinding(new[] {InputKey.Left}, GameAction.MoveLeft),
             new KeyBinding(new[] {InputKey.Right}, GameAction.MoveRight),
             new KeyBinding(new[] {InputKey.Down}, GameAction.SoftDrop),
             new KeyBinding(new[] {InputKey.Space}, GameAction.HardDrop),
             new KeyBinding(new[] {InputKey.Up}, GameAction.RotateClockWise),
             new KeyBinding(new[] {InputKey.X}, GameAction.RotateCounterClockwise),
             new KeyBinding(new[] {InputKey.Z}, GameAction.Rotate180Degrees),
             new KeyBinding(new[] {InputKey.C}, GameAction.HoldPiece),
             new KeyBinding(new[] {InputKey.Escape}, GameAction.ExitGame),
         };
    }
}
