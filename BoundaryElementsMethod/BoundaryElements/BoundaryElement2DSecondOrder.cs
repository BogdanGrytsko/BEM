using System;
using System.Collections.Generic;

using BEM.Common.Points;

using BEM.Common;

namespace BEM.BoundaryElements
{
    public class BoundaryElement2DSecondOrder : BoundaryElement2D
    {
        private const int POINTS_NUMBER = 8;

        public BoundaryElement2DSecondOrder(List<Point3D> points, Point3D center)
        {
            if (points.Count != POINTS_NUMBER)
            {
                throw new Exception("Wrong number of points for element");
            }
            Points = points;
            Center = center;
        }

        #region Overrides of BoundaryElement2D

        protected override List<double> CalculateInterpolationFunction(double u, double v)
        {
            var interpolatingFunctionsFor2D = new List<double>
                {
                    (1 - u) * (1 - v) * (-1 - u - v) * 0.25,
                    (1 + u) * (1 - v) * (-1 + u - v) * 0.25,
                    (1 + u) * (1 + v) * (-1 + u + v) * 0.25,
                    (1 - u) * (1 + v) * (-1 - u + v) * 0.25,
                    (1 - u * u) * (1 - v) * 0.5,
                    (1 - v * v) * (1 + u) * 0.5,
                    (1 - u * u) * (1 + v) * 0.5,
                    (1 - v * v) * (1 - u) * 0.5
                };
            return interpolatingFunctionsFor2D;
        }

        protected override double DxDu(IList<double> x, double u, double v)
        {
            double cxu1 = (x[5] - x[7]) / 2;
            double cxuu = (x[0] + x[1] + x[2] + x[3]) / 2 - x[4] - x[6];
            double cxuv = (x[0] - x[1] + x[2] - x[3]) / 4;
            double cxuuv = (-x[0] - x[1] + x[2] + x[3]) / 2 + x[4] - x[6];
            double cxuuu = 0;
            double cxuvv = (-x[0] + x[1] + x[2] - x[3]) / 4 + (x[7] - x[5]) / 2;
            return 1 * cxu1 + u * cxuu + v * cxuv + u * v * cxuuv + u * u * cxuuu + v * v * cxuvv;
        }

        protected override double DxDv(IList<double> x, double u, double v)
        {
            double cxv1 = (x[6] - x[4]) / 2;
            double cxvu = (x[0] - x[1] + x[2] - x[3]) / 4;
            double cxvv = (x[0] + x[1] + x[2] + x[3]) / 2 - x[5] - x[7];
            double cxvuv = (-x[0] + x[1] + x[2] - x[3]) / 2 + x[7] - x[5];
            double cxvuu = (-x[0] - x[1] + x[2] + x[3]) / 4 + (x[4] - x[6]) / 2;
            double cxvvv = 0;
            return 1 * cxv1 + u * cxvu + v * cxvv + u * v * cxvuv + u * u * cxvuu + v * v * cxvvv;
        }

        #endregion
    }
}