using System;
using System.Collections.Generic;
using System.Linq;

using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.BoundaryElements
{
    public abstract class BoundaryElement2D : BoundaryElement<Point3D>
    {
        protected override Point3D Interpolate(IPoint point)
        {
            var p = (Point2D)point;
            var func = CalculateInterpolationFunction(p.X1, p.X2);
            var result = new Point3D();
            for (int i = 0; i < Points.Count; i++)
            {
                result += func[i] * Points[i];
            }
            return result;
        }

        public override double Yakobian(IPoint point)
        {
            var point2D = (Point2D)point;
            // u - equals nju1, v equals nju2
            var u = point2D.X1;
            var v = point2D.X2;
            var x1 = Points.Select(p => p.X1).ToList();
            var x2 = Points.Select(p => p.X2).ToList();
            var x3 = Points.Select(p => p.X3).ToList();
            double g1 = DxDu(x2, u, v) * DxDv(x3, u, v) - DxDu(x3, u, v) * DxDv(x2, u, v);
            double g2 = DxDu(x3, u, v) * DxDv(x1, u, v) - DxDu(x1, u, v) * DxDv(x3, u, v);
            double g3 = DxDu(x1, u, v) * DxDv(x2, u, v) - DxDu(x2, u, v) * DxDv(x1, u, v);
            return Math.Sqrt(g1 * g1 + g2 * g2 + g3 * g3);
        }

        protected abstract List<double> CalculateInterpolationFunction(double u, double v);

        protected abstract double DxDu(IList<double> x, double u, double v);

        protected abstract double DxDv(IList<double> x, double u, double v);

        protected override List<IntegrationPoint<Point3D>> CreateIntegrationPoints(int n)
        {
            var points = new List<IntegrationPoint<Point3D>>();
            var gaussPoints = GaussPointsFactory.GetPoints2D(n);
            foreach (var gaussPoint in gaussPoints)
            {
                points.Add(new IntegrationPoint<Point3D>(
                    Interpolate(gaussPoint.Point), Yakobian(gaussPoint.Point), gaussPoint.Weight));
            }
            return points;
        }

        public override string ToString()
        {
            string result = Points.Aggregate("{", (current, point) => current + (point + " "));
            result += "C: " + Center;
            result += "}";
            return result;
        }
    }
}