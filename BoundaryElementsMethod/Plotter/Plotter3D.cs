using System;

using BEM.Bounds;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Plotter
{
    public abstract class Plotter3D : AbstractPlotter<Point3D>
    {
        protected Plotter3D(Bound<Point3D> bound, Func<Point3D, double> solution)
            : base(bound, solution, "Plot3D.txt")
        {
            Integrator = new Integrator<Point3D>(4);
        }
    }

}