using osu.Framework.iOS;
using Tetriosu.Game;

namespace Tetriosu.iOS
{
    /// <inheritdoc />
    public class AppDelegate : GameApplicationDelegate
    {
        /// <inheritdoc />
        protected override osu.Framework.Game CreateGame() => new TetriosuGame();
    }
}
