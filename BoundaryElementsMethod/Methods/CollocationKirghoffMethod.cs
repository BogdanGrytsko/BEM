using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Methods
{

    public class CollocationKirghoffMethod<T> : CollocationMethodNearBoundary<T> where T : IPoint

    {
        private readonly Func<double, double> transformation, converseTransformation;

        public CollocationKirghoffMethod(
            List<BoundWithCondition<T>> bounds,
            List<InnerSourceWithFunction<T>> sources,
            Func<T, T, double> fundamentalSolution,
            List<Func<T, T, double>> derivates,
            Integrator<T> integrator,
            Func<double, double> transformation,
            Func<double, double> converseTransformation)
            : base(bounds, sources, fundamentalSolution, derivates, integrator)
        {
            this.transformation = transformation;
            this.converseTransformation = converseTransformation;
        }

        protected override double CreateVectorElement(BoundaryElement<T> elem1, Func<T, double> function, ConditionType conditionType)
        {
            return transformation(base.CreateVectorElement(elem1, function, conditionType));
        }

        public override double U(T x)
        {
            return converseTransformation(base.U(x));
        }
    }
}