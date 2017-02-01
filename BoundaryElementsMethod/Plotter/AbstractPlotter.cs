using System;

using BEM.Bounds;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Plotter
{
    public abstract class AbstractPlotter<T> where T : IPoint
    {
        protected Bound<T> Bound { get; private set; }

        protected Integrator<T> Integrator { get; set; }

        protected Func<T, double> Solution { get; private set; }

        protected string FileName { get; private set; }

        protected AbstractPlotter(
            Bound<T> bound, Func<T, double> solution, string fileName)
        {
            Bound = bound;
            Solution = solution;
            FileName = fileName;
        }

        public abstract void Plot();
    }
}