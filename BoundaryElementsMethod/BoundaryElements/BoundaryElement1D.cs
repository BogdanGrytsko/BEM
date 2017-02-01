using System;
using System.Collections.Generic;
using System.Linq;

using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.BoundaryElements
{
    public abstract class BoundaryElement1D : BoundaryElement<Point2D>
    {
        public override double Yakobian(IPoint point)
        {
            var u = ((Point1D)point).X1;
            var x1 = Points.Select(p => p.X1).ToList();
            var x2 = Points.Select(p => p.X2).ToList();
            var g1 = DxDu(x1, u);
            var g2 = DxDu(x2, u);
            return Math.Sqrt(g1 * g1 + g2 * g2);
        }

        protected override List<IntegrationPoint<Point2D>> CreateIntegrationPoints(int n)
        {
            var points = new List<IntegrationPoint<Point2D>>();
            var gaussPoints = GaussPointsFactory.GetPoints(n);
            foreach (var gaussPoint in gaussPoints)
            {
                points.Add(new IntegrationPoint<Point2D>(
                        Interpolate(gaussPoint.Point), Yakobian(gaussPoint.Point), gaussPoint.Weight));
            }
            return points;
        }

        protected override Point2D Interpolate(IPoint point)
        {
            var u = ((Point1D)point).X1;
            var func = CalculateInterpolationFunction(u);
            var result = new Point2D();
            for (int i = 0; i < Points.Count; i++)
            {
                result += func[i] * Points[i];
            }
            return result;
        }

        protected abstract List<double> CalculateInterpolationFunction(double u);

        protected abstract double DxDu(IList<double> x, double u);
    }
}