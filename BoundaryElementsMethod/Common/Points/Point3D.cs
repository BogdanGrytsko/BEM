using System;

namespace BEM.Common.Points
{
    public class Point3D : IPoint
    {
        public double X1 { get; private set; }

        public double X2 { get; private set; }

        public double X3 { get; private set; }

        public Point3D()
        {
        }

        public Point3D(double x1, double x2, double x3)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
        }

        public Point3D(Point3D other)
        {
            X1 = other.X1;
            X2 = other.X2;
            X3 = other.X3;
        }

        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X1 + p2.X1, p1.X2 + p2.X2, p1.X3 + p2.X3);
        }

        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X1 - p2.X1, p1.X2 - p2.X2, p1.X3 - p2.X3);
        }

        public static Point3D operator *(double a, Point3D p2)
        {
            return new Point3D(a * p2.X1, a * p2.X2, a * p2.X3);
        }

        public static Point3D operator *(Point3D p2, double a)
        {
            return a * p2;
        }

        public override string ToString()
        {
            return string.Format("[{0,7:0.0000} {1,7:0.0000} {2,7:0.0000}]", X1, X2, X3);
        }

        public double Dist(IPoint other)
        {
            var b = (Point3D)other;
            var l1 = X1 - b.X1;
            var l2 = X2 - b.X2;
            var l3 = X3 - b.X3;
            return Math.Sqrt(l1 * l1 + l2 * l2 + l3 * l3);
        }
        public double Dist1(IPoint other)
        {
            var b = (Point3D)other;
            var l1 = X1 - b.X1;
            var l2 = X2 - b.X2;
            var l3 = X3 + b.X3;
            return Math.Sqrt(l1 * l1 + l2 * l2 + l3 * l3);
        }

        public void ShiftToB(Point3D b, double shift)
        {
            X1 += (b.X1 - X1)*shift/Dist(b);
            X2 += (b.X2 - X2)*shift/Dist(b);
            X3 += (b.X3 - X3)*shift/Dist(b);
        }

        public double ScalarMultiply(IPoint other)
        {
            var b = (Point3D)other;
            return X1 * b.X1 + X2 * b.X2 + X3 * b.X3;
        }

        public Bounds.Parallelepiped Bound { get; set; }
    }

    public class CopyOfPoint3D : IPoint
    {
        public double X1 { get; private set; }

        public double X2 { get; private set; }

        public double X3 { get; private set; }

        public CopyOfPoint3D()
        {
        }

        public CopyOfPoint3D(double x1, double x2, double x3)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
        }

        public CopyOfPoint3D(CopyOfPoint3D other)
        {
            X1 = other.X1;
            X2 = other.X2;
            X3 = other.X3;
        }

        public static CopyOfPoint3D operator +(CopyOfPoint3D p1, CopyOfPoint3D p2)
        {
            return new CopyOfPoint3D(p1.X1 + p2.X1, p1.X2 + p2.X2, p1.X3 + p2.X3);
        }

        public static CopyOfPoint3D operator -(CopyOfPoint3D p1, CopyOfPoint3D p2)
        {
            return new CopyOfPoint3D(p1.X1 - p2.X1, p1.X2 - p2.X2, p1.X3 - p2.X3);
        }

        public static CopyOfPoint3D operator *(double a, CopyOfPoint3D p2)
        {
            return new CopyOfPoint3D(a * p2.X1, a * p2.X2, a * p2.X3);
        }

        public static CopyOfPoint3D operator *(CopyOfPoint3D p2, double a)
        {
            return a * p2;
        }

        public override string ToString()
        {
            return string.Format("[{0,7:0.0000} {1,7:0.0000} {2,7:0.0000}]", X1, X2, X3);
        }

        public double Dist(IPoint other)
        {
            var b = (CopyOfPoint3D)other;
            var l1 = X1 - b.X1;
            var l2 = X2 - b.X2;
            var l3 = X3 - b.X3;
            return Math.Sqrt(l1 * l1 + l2 * l2 + l3 * l3);
        }
        public double Dist1(IPoint other)
        {
            var b = (CopyOfPoint3D)other;
            var l1 = X1 - b.X1;
            var l2 = X2 - b.X2;
            var l3 = X3 + b.X3;
            return Math.Sqrt(l1 * l1 + l2 * l2 + l3 * l3);
        }

        public void ShiftToB(CopyOfPoint3D b, double shift)
        {
            X1 += (b.X1 - X1) * shift / Dist(b);
            X2 += (b.X2 - X2) * shift / Dist(b);
            X3 += (b.X3 - X3) * shift / Dist(b);
        }

        public double ScalarMultiply(IPoint other)
        {
            var b = (CopyOfPoint3D)other;
            return X1 * b.X1 + X2 * b.X2 + X3 * b.X3;
        }

        public Bounds.Parallelepiped Bound { get; set; }
    }
}
