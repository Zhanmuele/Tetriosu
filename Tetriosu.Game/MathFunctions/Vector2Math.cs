using System.Collections.Generic;
using osuTK;

namespace Tetriosu.Game.MathFunctions
{
    public static class Vector2Math
    {
        public static Vector2 RotateVectorBy90degrees(Vector2 vector, bool CCW)
        {
            if (CCW) { return new Vector2(vector.Y, -vector.X); }

            return new Vector2(-vector.Y, vector.X);
        }

        public static Vector2 RotateVectorBy180degrees(Vector2 vector) => -vector;

        /// <summary>
        /// Adds a Vector2 to every Vector2 inside the List.
        /// The List is modified in place.
        /// </summary>
        /// <param name="vectorList">The List with the Vectors to update</param>
        /// <param name="vectorToSumWith">The Vector to add to each Vector in the List</param>
        public static void AddVector2ToVector2List(List<Vector2> vectorList, Vector2 vectorToSumWith)
        {
            for (int i = 0; i < vectorList.Count; i++)
            {
                vectorList[i] += vectorToSumWith;
            }
        }

        /// <summary>
        /// Adds a Vector2 to every Vector2 inside the Array.
        /// The Array is passed by ref and will be modified in place.
        /// </summary>
        /// <param name="vectorArray">The List with the Vectors to update</param>
        /// <param name="vectorToSumWith">The Vector to add to each Vector in the Array</param>
        public static void AddVector2ToVector2Array(ref Vector2[] vectorArray, Vector2 vectorToSumWith)
        {
            for (int i = 0; i < vectorArray.Length; i++)
            {
                vectorArray[i] += vectorToSumWith;
            }
        }
    }
}
