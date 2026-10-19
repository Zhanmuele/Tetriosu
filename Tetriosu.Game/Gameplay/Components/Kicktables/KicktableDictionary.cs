using osuTK;

namespace Tetriosu.Game.Gameplay.Components.Kicktables
{
    public class KicktableDictionary
    {
        // Kicktable to use for J, L, S, T, Z pieces in SRS and variants 
        public static readonly Vector2[][] CW_KICKTABLE_SRS = new Vector2[4][]
        {
            // target rotation 3 -> 0
            new Vector2[4] { new Vector2(-1,0), new Vector2(-1,+1), new Vector2(0,-2), new Vector2(-1,-2) },
            // target rotation 0 -> 1
            new Vector2[4] { new Vector2(-1,0), new Vector2(-1,-1), new Vector2(0,+2), new Vector2(-1,+2) },
            // target rotation 1 -> 2
            new Vector2[4] { new Vector2(+1,0), new Vector2(+1,+1), new Vector2(0,-2), new Vector2(+1,-2) },
            // target rotation 2 -> 3
            new Vector2[4] { new Vector2(+1,0), new Vector2(+1,-1), new Vector2(0,+2), new Vector2(+1,+2) },
        };

        public static readonly Vector2[][] CCW_KICKTABLE_SRS = new Vector2[4][]
        {
            // target rotation 1 -> 0
            new Vector2[4] { new Vector2(+1,0), new Vector2(+1,+1), new Vector2(0,-2), new Vector2(+1,-2) },
            // target rotation 2 -> 1
            new Vector2[4] { new Vector2(-1,0), new Vector2(-1,-1), new Vector2(0,+2), new Vector2(-1,+2) },
            // target rotation 3 -> 2
            new Vector2[4] { new Vector2(-1,0), new Vector2(-1,+1), new Vector2(0,-2), new Vector2(-1,-2) },
            // target rotation 0 -> 3
            new Vector2[4] { new Vector2(+1,0), new Vector2(+1,-1), new Vector2(0,+2), new Vector2(+1,+2) },
        };

        // Kicktable to use for the I piece in Classic SRS

        public static readonly Vector2[][] CW_I_PIECE_KICKTABLE_CLASSIC_SRS = new Vector2[4][]
        {
            // target rotation 3 -> 0
            new Vector2[4] { new Vector2(+1,0), new Vector2(-2,0), new Vector2(+1,+2), new Vector2(-2,-1) },
            // target rotation 0 -> 1
            new Vector2[4] { new Vector2(-2,0), new Vector2(+1,0), new Vector2(-2,+1), new Vector2(+1,-2) },
            // target rotation 1 -> 2
            new Vector2[4] { new Vector2(-1,0), new Vector2(+2,0), new Vector2(-1,-2), new Vector2(+2,+1) },
            // target rotation 2 -> 3
            new Vector2[4] { new Vector2(+2,0), new Vector2(-1,0), new Vector2(+2,-1), new Vector2(-1,+2) },
        };

        public static readonly Vector2[][] CCW_I_PIECE_KICKTABLE_CLASSIC_SRS = new Vector2[4][]
        {
            // target rotation 1 -> 0
            new Vector2[4] { new Vector2(+2,0), new Vector2(-1,0), new Vector2(+2,-1), new Vector2(-1,+2) },
            // target rotation 2 -> 1
            new Vector2[4] { new Vector2(+1,0), new Vector2(-2,0), new Vector2(+1,+2), new Vector2(-2,-1) },
            // target rotation 3 -> 2
            new Vector2[4] { new Vector2(-2,0), new Vector2(+1,0), new Vector2(-2,+1), new Vector2(+1,-2) },
            // target rotation 0 -> 3
            new Vector2[4] { new Vector2(-1,0), new Vector2(+2,0), new Vector2(-1,-2), new Vector2(+2,+1) },
        };

        // Kicktable to use for the I piece in Arika SRS

        public static readonly Vector2[][] CW_I_PIECE_KICKTABLE_ARIKA_SRS = new Vector2[4][]
        {
            // target rotation 3 -> 0
            new Vector2[4] { new Vector2(-2,0), new Vector2(+1,0), new Vector2(-2,-1), new Vector2(+1,+2) },
            // target rotation 0 -> 1
            new Vector2[4] { new Vector2(-2,0), new Vector2(+1,0), new Vector2(+1,-2), new Vector2(-2,+1) },
            // target rotation 1 -> 2
            new Vector2[4] { new Vector2(-1,0), new Vector2(+2,0), new Vector2(-1,-2), new Vector2(+2,+1) },
            // target rotation 2 -> 3
            new Vector2[4] { new Vector2(+2,0), new Vector2(-1,0), new Vector2(+2,-1), new Vector2(-1,+1) },
        };

        public static readonly Vector2[][] CCW_I_PIECE_KICKTABLE_ARIKA_SRS = new Vector2[4][]
        {
            // target rotation 1 -> 0
            new Vector2[4] { new Vector2(+2,0), new Vector2(-1,0), new Vector2(+2,-1), new Vector2(-1,+2) },
            // target rotation 2 -> 1
            new Vector2[4] { new Vector2(-2,0), new Vector2(+1,0), new Vector2(-2,-1), new Vector2(+1,+1) },
            // target rotation 3 -> 2
            new Vector2[4] { new Vector2(+1,0), new Vector2(-2,0), new Vector2(+1,-2), new Vector2(-2,+1) },
            // target rotation 0 -> 3
            new Vector2[4] { new Vector2(+2,0), new Vector2(-1,0), new Vector2(-1,-2), new Vector2(+2,+1) },
        };
    }
}
