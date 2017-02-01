using System;
using System.Collections.Generic;

using BEM.Bounds;
using BEM.Common.Points;

namespace BEM.BoundaryElements
{
    public class BoundaryElement1DSecondOrder : BoundaryElement1D
    {
        private const int POINTS_NUMBER = 3;

        public BoundaryElement1DSecondOrder(Point2D p1, Point2D p2, Point2D p3, Point2D center, Bound<Point2D> bound) :
            this(new List<Point2D>{ p1, p2, p3} , center, bound )
        {
        }

        public BoundaryElement1DSecondOrder(List<Point2D> points, Point2D center, Bound<Point2D> bound)
        {
            if (points.Count != POINTS_NUMBER)
            {
                throw new Exception("Wrong number of points for element");
            }
            Points = points;
            Center = center;
            Bound = bound;
        }

        #region Overrides of BoundaryElement1D

        protected override List<double> CalculateInterpolationFunction(double u)
        {
            var interpolatingFunctionsFor2D = new List<double>
                {
                    u * (u - 1) / 2,
                    u * (u + 1) / 2,
                    (1 - u) * (1 + u)
                };
            return interpolatingFunctionsFor2D;
        }

        protected override double DxDu(IList<double> x, double u)
        {
            var a = x[0] + x[1] - 2 * x[2];
            var b = x[0] - x[1];
            return a * u - b / 2;
        }

        #endregion
    }
}