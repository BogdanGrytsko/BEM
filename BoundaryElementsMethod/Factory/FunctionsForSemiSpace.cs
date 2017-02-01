using System;
using System.Collections.Generic;
using BEM.Common.Points;

namespace BEM.Factory
{
    public class FunctionsForSemiSpace
    {
        public const double H = -5;
        private const double mult = 1/(4*Math.PI);

        private readonly SemiSpaceParameters parameters;

        public FunctionsForSemiSpace(SemiSpaceParameters parameters)
        {
            this.parameters = parameters;
        }

        public static double K(Point3D a, Point3D b, Point3D m, Point3D n)
        {
            double res = 2 * Math.PI;
            res /= (1 / a.Dist(m) - 1 / a.Dist(n) - 1 / b.Dist(m) + 1 / b.Dist(n));
            return res;
        }

        public double U1(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return mult * (1 / r + 1 / RMirrored(x, y)) *1/ parameters.Sigma1;
        }

        public double U2(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return mult*(1 / (parameters.Sigma2* r));
        }

        public double U3(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return mult *(1/ (parameters.Sigma3 * r));
        }

        public static Point3D FSemiSpace(Point3D x, Point3D y)
        {
            return new Point3D(F1SemiSpace(x, y), F2SemiSpace(x, y), F3SemiSpace(x, y));
        }

        public static double F1SemiSpace(Point3D x, Point3D y)
        {
            return FPart(x, y)*(x.X1 - y.X1);
        }

        private static double FPart(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            var r3 = r * r * r;
            var totalR = 1 / r3 + 1 / RMirrorCube(x, y);
            return mult * totalR;
        }

        public static double RMirrorCube(Point3D x, Point3D y)
        {
            return RMirrored(x, y)*RMirrored(x, y)*RMirrored(x, y);
        }

        public static double F2SemiSpace(Point3D x, Point3D y)
        {
            return FPart(x, y)*(x.X2 - y.X2);
        }
        public static double F3SemiSpace(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            var r3 = r * r * r;
            var d = mult * ((x.X3 - y.X3) / r3 + (x.X3 + y.X3) / RMirrorCube(x, y));
            return d;
        }

        public static List<Func<Point3D, Point3D, double>> Derivates
        {
            get
            {
                return new List<Func<Point3D, Point3D, double>>
                {
                    F1SemiSpace,
                    F2SemiSpace,
                    F3SemiSpace
                };
            }
        }

        private static double RMirrored(Point3D x, Point3D y)
        {
            var l1 = x.X1 - y.X1;
            var l2 = x.X2 - y.X2;
            var l3 = x.X3 + y.X3;
            return Math.Sqrt(l1*l1 + l2*l2 + l3*l3);
        }
    }

    public class CopyOfFunctionsForSemiSpace
    {
        public const double H = -2;
        private const double mult = 1 / (4 * Math.PI);

        private readonly SemiSpaceParameters parameters;

        public CopyOfFunctionsForSemiSpace(SemiSpaceParameters parameters)
        {
            this.parameters = parameters;
        }

        public static double K(Point3D a, Point3D b, Point3D m, Point3D n)
        {
            double res = 2 * Math.PI;
            res /= (1 / a.Dist(m) - 1 / a.Dist(n) - 1 / b.Dist(m) + 1 / b.Dist(n));
            return res;
        }

        public double U1(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return mult * (1 / r + 1 / RMirrored(x, y)) * 1 / parameters.Sigma1;
        }

        public double U2(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return mult * (1 / (parameters.Sigma2 * r));
        }

        public double U3(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return mult * (1 / (parameters.Sigma3 * r));
        }

        public static Point3D FSemiSpace(Point3D x, Point3D y)
        {
            return new Point3D(F1SemiSpace(x, y), F2SemiSpace(x, y), F3SemiSpace(x, y));
        }

        public static double F1SemiSpace(Point3D x, Point3D y)
        {
            return FPart(x, y) * (x.X1 - y.X1);
        }

        private static double FPart(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            var r3 = r * r * r;
            var totalR = 1 / r3 + 1 / RMirrorCube(x, y);
            return mult * totalR;
        }

        public static double RMirrorCube(Point3D x, Point3D y)
        {
            return RMirrored(x, y) * RMirrored(x, y) * RMirrored(x, y);
        }

        public static double F2SemiSpace(Point3D x, Point3D y)
        {
            return FPart(x, y) * (x.X2 - y.X2);
        }
        public static double F3SemiSpace(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            var r3 = r * r * r;
            var d = mult * ((x.X3 - y.X3) / r3 + (x.X3 + y.X3) / RMirrorCube(x, y));
            return d;
        }

        public static List<Func<Point3D, Point3D, double>> Derivates
        {
            get
            {
                return new List<Func<Point3D, Point3D, double>>
                {
                    F1SemiSpace,
                    F2SemiSpace,
                    F3SemiSpace
                };
            }
        }

        private static double RMirrored(Point3D x, Point3D y)
        {
            var l1 = x.X1 - y.X1;
            var l2 = x.X2 - y.X2;
            var l3 = x.X3 + y.X3;
            return Math.Sqrt(l1 * l1 + l2 * l2 + l3 * l3);
        }
    }
}