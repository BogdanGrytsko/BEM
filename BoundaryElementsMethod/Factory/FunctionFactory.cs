using System;
using System.Collections.Generic;
using BEM.Common.Points;

namespace BEM.Factory
{
    public class FunctionFactory
    {
        public static double Q(Point2D x, Point2D ksi)
        {
            const double Lamda = 1;
            double r = x.Dist(ksi);
            return -Math.Log(r)/(2*Math.PI*Lamda);
        }

        // this is dQ/dx1. dQ/dy1 has opposite sign.
        public static double Q1(Point2D x, Point2D y)
        {
            double r = x.Dist(y);
            return -(x.X1 - y.X1)/(2*Math.PI*r*r);
        }

        public static double Q2(Point2D x, Point2D y)
        {
            double r = x.Dist(y);
            return -(x.X2 - y.X2)/(2*Math.PI*r*r);
        }

        public static double G(Point2D x)
        {
            ////if (x.X2 >= 0)
            ////{
            ////    return 1 - x.X2;
            ////}
            ////return -1 - x.X2;
            return 1;
        }

        public static double F(Point2D x)
        {
            return 5;
            ////if (x.X2 >= 0)
            ////{
            ////    return 3;
            ////}
            ////return -3;
        }

        public static double Q(Point3D x, Point3D y)
        {
            const double Lamda = 1;
            double r = x.Dist(y);
            return 1/(4*Math.PI*Lamda*r);
        }

        public static double Q1(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return -(x.X1 - y.X1)/(4*Math.PI*r*r*r);
        }

        public static double Q2(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return -(x.X2 - y.X2)/(4*Math.PI*r*r*r);
        }

        public static double Q3(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return -(x.X3 - y.X3)/(4*Math.PI*r*r*r);
        }

        public static double G(Point3D x)
        {
            return x.X3 + 2;
        }
        public static double GSphere(Point3D x)
        {
            return x.X2 + 2;
        }
        public static double GotherSphere(Point3D x)
        {
            return  x.X2+2;
        }

        public static double Gother(Point3D x)
        {
            if (x.X3 == 1)
            {
                return (x.X2*x.X2)/4 + 3.75;
            }
            if (x.X3 == -1)
            {
                return (x.X2*x.X2)/4 + 1.75;
            }
            else return -1;
        }


        public static List<Func<Point3D, Point3D, double>> Derivates
        {
            get
            {
                return new List<Func<Point3D, Point3D, double>>
                {
                    Q1,
                    Q2,
                    Q3
                };
            }
        }
    }

    public class CopyOfFunctionFactory
    {
        public static double Q(Point2D x, Point2D ksi)
        {
            const double Lamda = 1;
            double r = x.Dist(ksi);
            return -Math.Log(r) / (2 * Math.PI * Lamda);
        }

        // this is dQ/dx1. dQ/dy1 has opposite sign.
        public static double Q1(Point2D x, Point2D y)
        {
            double r = x.Dist(y);
            return -(x.X1 - y.X1) / (2 * Math.PI * r * r);
        }

        public static double Q2(Point2D x, Point2D y)
        {
            double r = x.Dist(y);
            return -(x.X2 - y.X2) / (2 * Math.PI * r * r);
        }

        public static double G(Point2D x)
        {
            ////if (x.X2 >= 0)
            ////{
            ////    return 1 - x.X2;
            ////}
            ////return -1 - x.X2;
            return 1;
        }

        public static double F(Point2D x)
        {
            return 5;
            ////if (x.X2 >= 0)
            ////{
            ////    return 3;
            ////}
            ////return -3;
        }

        public static double Q(Point3D x, Point3D y)
        {
            const double Lamda = 1;
            double r = x.Dist(y);
            return 1 / (4 * Math.PI * Lamda * r);
        }

        public static double Q1(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return -(x.X1 - y.X1) / (4 * Math.PI * r * r * r);
        }

        public static double Q2(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return -(x.X2 - y.X2) / (4 * Math.PI * r * r * r);
        }

        public static double Q3(Point3D x, Point3D y)
        {
            double r = x.Dist(y);
            return -(x.X3 - y.X3) / (4 * Math.PI * r * r * r);
        }

        public static double G(Point3D x)
        {
            return x.X3 + 2;
        }
        public static double GSphere(Point3D x)
        {
            return x.X2 + 2;
        }
        public static double GotherSphere(Point3D x)
        {
            return x.X2 + 2;
        }

        public static double Gother(Point3D x)
        {
            if (x.X3 == 1)
            {
                return (x.X2 * x.X2) / 4 + 3.75;
            }
            if (x.X3 == -1)
            {
                return (x.X2 * x.X2) / 4 + 1.75;
            }
            else return -1;
        }


        public static List<Func<Point3D, Point3D, double>> Derivates
        {
            get
            {
                return new List<Func<Point3D, Point3D, double>>
                {
                    Q1,
                    Q2,
                    Q3
                };
            }
        }
    }
}