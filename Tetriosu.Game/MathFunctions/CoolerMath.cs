namespace Tetriosu.Game.CoolerMathFunctions
{
    public static class CoolerMath
    {
        // go to heck truncated modulo
        public static int RealIntModulo(int dividend, int divisor) => ((dividend % divisor) + divisor) % divisor;
    }
}
