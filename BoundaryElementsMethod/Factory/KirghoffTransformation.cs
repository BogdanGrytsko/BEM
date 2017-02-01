using System;

using BEM.Common.Points;

namespace BEM.Factory
{
    public static class KirghoffTransformation
    {
        public const double LAMDA0 = 1;

        public const double U0 = 2;
        public const double BETALAMDA =1;
        public const double NLAMDA = 2;

        public static double Nuv<T>(T p) where T : IPoint
        {
            return 1;
        }

        public static double Exp(double p)
        {
            return LAMDA0 * U0 / BETALAMDA * (Math.Exp(BETALAMDA * (p - U0) / U0) - 1);
        }

        public static double ConverseExp(double p)
        {

            return U0 + U0 / BETALAMDA * Math.Log10(p * BETALAMDA / (LAMDA0 * U0) + 1);
        }

        public static double Pow(double p)
        {
            var enumerator = LAMDA0 * U0;
            var denumerator = (NLAMDA + 1) * BETALAMDA;
            return enumerator / denumerator * (Math.Pow(1 + BETALAMDA * (p - U0) / U0, NLAMDA + 1) - 1);
        }

        public static double ConversePow(double p)
        {
            return U0
                   + U0 / BETALAMDA
                   * (Math.Pow(1 + (p * (NLAMDA + 1) * BETALAMDA / (LAMDA0 * U0)), 1 / (NLAMDA + 1)) - 1);
        }
    }
}