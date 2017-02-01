using System;

namespace BEM.Common.Points
{
    public class Point1D : IPoint
    {
        public double X1 { get; private set; }

        public Point1D(double x1)
        {
            X1 = x1;
        }

        public Point1D(Point1D other)
        {
            X1 = other.X1;
        }

        public double Dist(IPoint other)
        {
            var b = (Point1D)other;
            return Math.Abs(X1 - b.X1);
        }

        public double ScalarMultiply(IPoint other)
        {
            var b = (Point1D)other;
            return X1 * b.X1;
        }
    }
}
