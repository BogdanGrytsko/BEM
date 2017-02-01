using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Methods
{
    public class CollocationMethodNearBoundary<T> : AbstractMethod<T> where T : IPoint
    {
        public CollocationMethodNearBoundary(
            List<BoundWithCondition<T>> bound,
            List<InnerSourceWithFunction<T>> sources,
            Func<T, T, double> fundamentalSolution,
            List<Func<T, T, double>> derivates,
            Integrator<T> integrator)
            : base(bound, sources, fundamentalSolution, derivates, integrator)
        {
        }

        protected override double CreateMatrixElement(BoundaryElement<T> elem1, BoundaryElement<T> elem2, ConditionType conditionType)
        {
            switch (conditionType)
            {
                case ConditionType.Dirichlet:
                    return Integrator.Integrate(elem1, elem2.Center, FundamentalSolution);
                case ConditionType.Neumann:
                    return Integrator.IntegratedQdnx(elem1, elem2, Derivates);
                case ConditionType.Robin:
                    return Integrator.Integrate(elem1, elem2.Center, FundamentalSolution)
                           - Integrator.IntegratedQdnx(elem1, elem2, Derivates);
            }
            return double.NaN;
        }
    }
}