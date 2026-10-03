using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Input.Bindings;

namespace Tetriosu.Game.Gameplay.Keybinds
{
    public enum Keybinds
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

    public partial class KeybindsContainer : KeyBindingContainer
    {
        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
        {
             new KeyBinding(new[] {InputKey.Left}, Keybinds.MoveLeft),
             new KeyBinding(new[] {InputKey.Right}, Keybinds.MoveRight),
             new KeyBinding(new[] {InputKey.Down}, Keybinds.SoftDrop),
             new KeyBinding(new[] {InputKey.Space}, Keybinds.HardDrop),
             new KeyBinding(new[] {InputKey.Up}, Keybinds.RotateClockWise),
             new KeyBinding(new[] {InputKey.X}, Keybinds.RotateCounterClockwise),
             new KeyBinding(new[] {InputKey.Z}, Keybinds.Rotate180Degrees),
             new KeyBinding(new[] {InputKey.C}, Keybinds.HoldPiece),
             new KeyBinding(new[] {InputKey.Escape}, Keybinds.ExitGame),
         };
    }
}
