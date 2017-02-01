using System;

using BEM.Bounds;
using BEM.Common.Points;

namespace BEM.InnerSource
{
    internal class InnerSourceParallelepiped : InnerSource<Point3D>
    {
        private static double a1 = 0.25, a2 = -0.25, b1 = 0.75, b2 = -0.75, c1 = 0.75, c2 = -0.75;

        private static int n1 = 2, n2 = 2, n3 = 2;

        public InnerSourceParallelepiped()
        {
            var p = new Parallelepiped(a1, a2, b1, b2, c1, c2, n1, n2, n3);
            Add(p);
        }

        public static double SourceFunction(Point3D x)
        {
            var psig =-10;
            var xcenter = new Point3D((a1 + a2) / 2, (b1 + b2) / 2, (c1 + c2) / 2);
            var a = x.X1 - xcenter.X1;
            var b = x.X2 - xcenter.X2;
            var c = x.X3 - xcenter.X3;
            var l1 = a1 - xcenter.X1;
            var l2 = b1 - xcenter.X2;
            var l3 = c1 - xcenter.X3;
            return psig * (1 + Math.Cos(Math.PI * a / l1))
                   * (1 + Math.Cos(Math.PI * b / l2) * (1 + Math.Cos(Math.PI * c / l3)));
        }
    }
}