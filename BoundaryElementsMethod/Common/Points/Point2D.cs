using System;
using System.Collections.Generic;

namespace BEM.Common.Points
{
    public class Point2D : IPoint
    {
        public double X1 { get; private set; }

        public double X2 { get; private set; }

        public Point2D()
        {
        }

        public Point2D(double x1, double x2)
        {
            X1 = x1;
            X2 = x2;
        }

        public Point2D(Point2D other)
        {
            X1 = other.X1;
            X2 = other.X2;
        }

        public static Point2D operator +(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.X1 + p2.X1, p1.X2 + p2.X2);
        }

        public static Point2D operator -(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.X1 - p2.X1, p1.X2 - p2.X2);
        }

        public static Point2D operator *(double a, Point2D p2)
        {
            return new Point2D(a * p2.X1, a * p2.X2);
        }

        public static Point2D operator *(Point2D p2, double a)
        {
            return a * p2;
        }

        public static Point2D operator /(Point2D p1, double a)
        {
            return 1 / a * p1;
        }

        public override string ToString()
        {
            return string.Format("[{0,7:0.0000} {1,7:0.0000}]", X1, X2);
        }

        public double Dist(IPoint other)
        {
            var b = (Point2D)other;
            var l1 = X1 - b.X1;
            var l2 = X2 - b.X2;
            return Math.Sqrt(l1 * l1 + l2 * l2);
        }

        public double ScalarMultiply(IPoint other)
        {
            var b = (Point2D)other;
            return X1 * b.X1 + X2 * b.X2;
        }
    }
}
