using osu.Framework.Platform;
using osu.Framework;
using Tetriosu.Game;

namespace Tetriosu.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Tetriosu"))
            using (osu.Framework.Game game = new TetriosuGame())
                host.Run(game);
        }
    }
}
