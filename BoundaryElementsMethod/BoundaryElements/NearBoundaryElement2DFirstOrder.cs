using System;
using System.Collections.Generic;
using System.Linq;

using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.BoundaryElements
{
    public class NearBoundaryElement2DFirstOrder : BoundaryElement<Point3D>
    {
        public override double Yakobian(IPoint point)
        {
            var point3D = (Point3D)point;
            // u - equals nju1, v equals nju2, w equals nju3
            var u = point3D.X1;
            var v = point3D.X2;
            var w = point3D.X3;
            var x1 = Points.Select(p => p.X1).ToList();
            var x2 = Points.Select(p => p.X2).ToList();
            var x3 = Points.Select(p => p.X3).ToList();
            double g1 = DxDu(x1, u, v, w)
                        * (DxDv(x2, u, v, w) * DxDw(x3, u, v, w) - DxDv(x3, u, v, w) * DxDw(x2, u, v, w));
            double g2 = DxDu(x2, u, v, w)
                        * (DxDv(x1, u, v, w) * DxDw(x3, u, v, w) - DxDv(x3, u, v, w) * DxDw(x1, u, v, w));
            double g3 = DxDu(x3, u, v, w)
                        * (DxDv(x1, u, v, w) * DxDw(x2, u, v, w) - DxDv(x2, u, v, w) * DxDw(x1, u, v, w));
            return Math.Abs(g1 - g2 + g3);
        }

        protected override Point3D Interpolate(IPoint point)
        {
            var p = (Point3D)point;
            var func = CalculateInterpolationFunction(p.X1, p.X2, p.X3);
            var result = new Point3D();
            for (int i = 0; i < Points.Count; i++)
            {
                result += func[i] * Points[i];
            }
            return result;
        }

        public NearBoundaryElement2DFirstOrder(
            Point3D p1, Point3D p2, Point3D p3, Point3D p4, Point3D p5, Point3D p6, Point3D p7, Point3D p8, Point3D center, Point3D normal)
        {
            Points = new List<Point3D> { p1, p2, p3, p4, p5, p6, p7, p8 };
            Center = center;
            Normal = normal;
        }

        #region Overrides of BoundaryElement<Point3D>

        protected override List<IntegrationPoint<Point3D>> CreateIntegrationPoints(int n)
        {
            var points = new List<IntegrationPoint<Point3D>>();
            var gaussPoints = GaussPointsFactory.GetPoints3D(n);
            foreach (var gaussPoint in gaussPoints)
            {
                points.Add(new IntegrationPoint<Point3D>(
                        Interpolate(gaussPoint.Point), Yakobian(gaussPoint.Point), gaussPoint.Weight));
            }
            return points;
        }

        protected List<double> CalculateInterpolationFunction(double u, double v, double w)
        {
            var interpolatingFunctionsFor3D = new List<double>
                                                  {
                                                      (1 - u) * (1 - v) * (1 - w) * 0.125,
                                                      (1 + u) * (1 - v) * (1 - w) * 0.125,
                                                      (1 + u) * (1 + v) * (1 - w) * 0.125,
                                                      (1 - u) * (1 + v) * (1 - w) * 0.125,
                                                      (1 - u) * (1 - v) * (1 + w) * 0.125,
                                                      (1 + u) * (1 - v) * (1 + w) * 0.125,
                                                      (1 + u) * (1 + v) * (1 + w) * 0.125,
                                                      (1 - u) * (1 + v) * (1 + w) * 0.125,
                                                  };
            return interpolatingFunctionsFor3D;
        }

        protected double DxDu(IList<double> x, double u, double v, double w)
        {
            return 0.125
                   * ((1 - v) * (1 - w) * (x[1] - x[0]) + (1 + v) * (1 - w) * (x[2] - x[3])
                      + (1 - v) * (1 + w) * (x[5] - x[4]) + (1 + v) * (1 + w) * (x[6] - x[7]));
        }

        protected double DxDv(IList<double> x, double u, double v, double w)
        {
            return 0.125
                   * ((1 - u) * (1 - w) * (x[3] - x[0]) + (1 + u) * (1 - w) * (x[2] - x[1])
                      + (1 - u) * (1 + w) * (x[7] - x[4]) + (1 + u) * (1 + w) * (x[6] - x[5]));
        }

        protected double DxDw(IList<double> x, double u, double v, double w)
        {
            return 0.125
                   * ((1 - u) * (1 - v) * (x[4] - x[0]) + (1 + u) * (1 - v) * (x[5] - x[1])
                      + (1 + u) * (1 + v) * (x[7] - x[2]) + (1 - u) * (1 + v) * (x[7] - x[3]));
        }

        #endregion
    }
}