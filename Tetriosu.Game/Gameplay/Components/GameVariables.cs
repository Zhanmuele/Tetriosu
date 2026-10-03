using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace Tetriosu.Game.Gameplay.Components
{
    public partial class GameVariables : Component
    {
        public Bindable<char> HeldPieceType = new Bindable<char>(' ');
        public BindableBool HasSwapped = new BindableBool(false);
        public BindableBool IsSoftDropping = new BindableBool(false);
        public BlockPiece?[,] PlayfieldData = new BlockPiece?[GameSetup.ROWS, GameSetup.COLUMS];

    }
}
