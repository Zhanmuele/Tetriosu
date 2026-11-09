namespace Tetriosu.Game.MathFunctions
{
    public static class CoolerMath
    {
        // go to heck truncated modulo
        public static int RealIntModulo(int dividend, int divisor) => ((dividend % divisor) + divisor) % divisor;
    }
}
