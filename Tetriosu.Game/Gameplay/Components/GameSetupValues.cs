using System.Collections.Generic;
using osu.Framework.Graphics;
using osuTK;

namespace Tetriosu.Game.Gameplay.Components
{
    public class GameSetupValues
    {
        public const int ROWS = 50;
        public const int DEFAULT_VISIBLE_ROWS = 20;
        public const int DEFAULT_COLUMS = 10;
        public const float BLOCK_SIZE = 25f;
        public static readonly Vector2 DEFAULT_PLAYFIELD_SIZE = new Vector2(DEFAULT_COLUMS * BLOCK_SIZE, ROWS * BLOCK_SIZE);
        public static readonly Vector2 DEFAULT_VISIBLE_PLAYFIELD_SIZE = new Vector2(DEFAULT_COLUMS * BLOCK_SIZE, DEFAULT_VISIBLE_ROWS * BLOCK_SIZE);

        public static readonly Dictionary<string, Vector2[]> SRS_FULL_BLOCK_SHAPE = new Dictionary<string, Vector2[]>()
        {
            { "Z", new Vector2[5] { Vector2.Zero, Vector2.Zero, new Vector2(+1,0), new Vector2(0,-1), new Vector2(-1,-1) }},
            { "L", new Vector2[5] { Vector2.Zero, Vector2.Zero, new Vector2(-1,0), new Vector2(+1,0), new Vector2(+1,-1) }},
            { "O", new Vector2[5] { new Vector2(0.5f,-0.5f), Vector2.Zero, new Vector2(+1,0), new Vector2(0,-1), new Vector2(+1,-1) }},
            { "S", new Vector2[5] { Vector2.Zero, Vector2.Zero, new Vector2(-1,0), new Vector2(0,-1), new Vector2(+1,-1) }},
            { "I", new Vector2[5] { new Vector2(0.5f,+0.5f), Vector2.Zero, new Vector2(-1,0), new Vector2(+1,0), new Vector2(+2,+0) }},
            { "J", new Vector2[5] { Vector2.Zero, Vector2.Zero, new Vector2(-1,0), new Vector2(+1,0), new Vector2(-1,-1) }},
            { "T", new Vector2[5] { Vector2.Zero, Vector2.Zero, new Vector2(-1,0), new Vector2(+1,0), new Vector2(+0,-1) }},
        };

        public static readonly Dictionary<string, Colour4> DEFAULT_FULL_BLOCK_COLORS = new Dictionary<string, Colour4>()
        {
            // HEX: #B32424   HSV 0°, 80%, 70%
            { "Z", new Colour4(179, 36, 36, 255) },
            // HEX: #CC7B29   HSV 30°, 80%, 80%
            { "L", new Colour4(204, 123, 41, 255) },
            // HEX: #CCCC29   HSV 60°, 80%, 80%
            { "O", new Colour4(204, 204, 41, 255) },
            // HEX: #3DBD1F   HSV 105°, 80%, 60%
            { "S", new Colour4(61, 153, 31, 255) },
            // HEX: #29CCCC   HSV 180°, 80%, 80%
            { "I", new Colour4(41, 204, 204, 255) },
            // HEX: #1F3DBD   HSV 225°, 80%, 60%
            { "J", new Colour4(31, 61, 153, 255) },
            // HEX: #BD1FBD   HSV 300°, 80%, 60%
            { "T", new Colour4(153, 31, 153, 255) },
        };

        public static readonly Dictionary<string, Colour4> DEFAULT_FULL_BLOCK_COLORS_WHEN_PLACED = new Dictionary<string, Colour4>()
        {
            // HEX: #A62121   HSV 0°, 80%, 70%
            { "Z", new Colour4(166, 33, 33, 255) },
            // HEX: #BF7326   HSV 30°, 80%, 75%
            { "L", new Colour4(191, 115, 38, 255) },
            // HEX: #BFBF26   HSV 60°, 80%, 75%
            { "O", new Colour4(191, 191, 38, 255) },
            // HEX: #38BF26   HSV 105°, 80%, 55%
            { "S", new Colour4(56, 140, 28, 255) },
            // HEX: #26BFBF   HSV 180°, 80%, 80%
            { "I", new Colour4(38, 191, 191, 255) },
            // HEX: #2638BF   HSV 225°, 80%, 55%
            { "J", new Colour4(28, 56, 140, 255) },
            // HEX: #BF26BF   HSV 300°, 80%, 55%
            { "T", new Colour4(140, 28, 140, 255) },
        };
    }
}
