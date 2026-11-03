using osuTK;

namespace Tetriosu.Game.MathFunctions
{
    public static class Vector2Math
    {
        public static Vector2 RotateVector2(Vector2 vector, bool CCW)
        {
            if (CCW) { return new Vector2(vector.Y, -vector.X); }

            return new Vector2(-vector.Y, vector.X);
        }

        public static Vector2 Rotate180degrees(Vector2 vector) => -vector;
    }
}
