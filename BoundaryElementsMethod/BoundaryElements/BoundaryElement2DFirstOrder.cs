using System.Collections.Generic;

using BEM.Common.Points;

using BEM.Common;

namespace BEM.BoundaryElements
{
    public class BoundaryElement2DFirstOrder : BoundaryElement2D
    {
        public BoundaryElement2DFirstOrder(Point3D p1, Point3D p2, Point3D p3, Point3D p4, Point3D center, Point3D normal)
        {
            Points = new List<Point3D> { p1, p2, p3, p4 };
            Center = center;
            Normal = normal;
        }

        protected override List<double> CalculateInterpolationFunction(double u, double v)
        {
            var interpolatingFunctionsFor2D = new List<double>
                                                  {
                                                      (1 - u) * (1 - v) * 0.25,
                                                      (1 + u) * (1 - v) * 0.25,
                                                      (1 + u) * (1 + v) * 0.25,
                                                      (1 - u) * (1 + v) * 0.25
                                                  };
            return interpolatingFunctionsFor2D;
        }

        protected override double DxDu(IList<double> x, double u, double v)
        {
            return 0.25 * ((1 - v) * (x[1] - x[0]) + (1 + v) * (x[2] - x[3]));
        }

        protected override double DxDv(IList<double> x, double u, double v)
        {
            return 0.25 * ((1 - u) * (x[3] - x[0]) + (1 + u) * (x[2] - x[1]));
        }
    }
}
