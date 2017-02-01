using System;

using BEM.BoundaryElements;
using BEM.Common.Points;

namespace BEM.InnerSource
{
    public class InnerSourcePlate : InnerSource<Point3D>
    {
        private static double a1 = 0.5, a2 = -0.5, c1 = 0.5, c2 = -0.5, b = 0.25;
        private static int n1 = 2, n2 = 2;
        private double h1, h2;

        public InnerSourcePlate()
        {
            h1 = (a1 - a2) / n1;
            h2 = (c1 - c2) / n2;

            CreatePlataElements();
        }

        private void CreatePlataElements()
        {
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    var start = new Point3D(a2 + i * h1, b, c1 - j * h2);
                    Elements.Add(GetElementPlata(start));
                }
            }
        }

        private BoundaryElement2DFirstOrder GetElementPlata(Point3D start)
        {
            var firstPoint3D = start + new Point3D(h1, 0, 0);
            var secondPoint3D = start + new Point3D(h1, 0, -h2);
            var thirdPoint3D = start + new Point3D(0, 0, -h2);
            var center = start + new Point3D(h1 / 2, 0, -h2 / 2);
            var element = new BoundaryElement2DFirstOrder(
                start, firstPoint3D, secondPoint3D, thirdPoint3D, center, null);
            return element;
        }

        public static double SourceFunction(Point3D x)
        {
            var psig =10;
            var xcenter = new Point3D(
                (a1 + a2) / 2, 0, (c1 + c2) / 2);
            var a = x.X1 - xcenter.X1;
            var b = x.X3 - xcenter.X3;
            var l1 = a1 - xcenter.X1;
            var l2 = c1 - xcenter.X3;
            return psig * (1 + Math.Cos(Math.PI * a / l1)) * (1 + Math.Cos(Math.PI * b / l2));
        }
    }
}
