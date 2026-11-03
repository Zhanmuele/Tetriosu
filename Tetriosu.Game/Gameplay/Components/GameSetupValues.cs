using System.Collections.Generic;
using osu.Framework.Graphics;
using osuTK;

namespace Tetriosu.Game.Gameplay.Components
{
    public class GameSetupValues
    {
        public const float DEFAULT_ROWS = 50f;
        public const float DEFAULT_VISIBLE_ROWS = 20f;
        public const float DEFAULT_COLUMS = 10f;
        public const float BLOCK_SIZE = 25f;
        public static readonly Vector2 BLOCK_SIZE_V = new Vector2(25f);
        public static readonly Vector2 DEFAULT_PLAYFIELD_SIZE = new Vector2(DEFAULT_COLUMS * BLOCK_SIZE, DEFAULT_ROWS * BLOCK_SIZE);
        public static readonly Vector2 DEFAULT_VISIBLE_PLAYFIELD_SIZE = new Vector2(DEFAULT_COLUMS * BLOCK_SIZE, DEFAULT_VISIBLE_ROWS * BLOCK_SIZE);

        public static readonly Dictionary<string, Vector2[]> DEFAULT_FULL_BLOCK_SHAPE_POS = new Dictionary<string, Vector2[]>()
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
            // HEX: #b30000
            { "Z", new Colour4(179, 0, 0, 255) },
            // HEX: #cc6600
            { "L", new Colour4(204, 102, 0, 255) },
            // HEX: #cccc00
            { "O", new Colour4(204, 204, 0, 255) },
            // HEX: #269900
            { "S", new Colour4(38, 153, 0, 255) },
            // HEX: #00cccc
            { "I", new Colour4(0, 204, 204, 255) },
            // HEX: #005AB2
            { "J", new Colour4(0, 90, 178, 255) },
            // HEX: #B231B2
            { "T", new Colour4(178, 49, 178, 255) },

        };

        public static readonly Dictionary<string, Colour4> DEFAULT_FULL_BLOCK_COLORS_WHEN_PLACED = new Dictionary<string, Colour4>()
        {
            // HEX: #962828
            { "Z", new Colour4(150, 40, 40, 255) },
            // HEX: #b36b24
            { "L", new Colour4(179, 107, 36, 255) },
            // HEX: #b3b324
            { "O", new Colour4(179, 179, 36, 255) },
            // HEX: #258019
            { "S", new Colour4(51, 128, 25, 255) },
            // HEX: #24b3b3
            { "I", new Colour4(36, 179, 179, 255) },
            // HEX: #194d80
            { "J", new Colour4(25, 77, 128, 255) },
            // HEX: #990099
            { "T", new Colour4(153, 0, 153, 255) },
        };
    }
}
