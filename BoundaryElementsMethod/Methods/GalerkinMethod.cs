using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Methods
{
    public class GalerkinMethod<T> : AbstractMethod<T> where T : IPoint
    {
        public GalerkinMethod(
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
                    return Integrator.Integrate(elem1, elem2, FundamentalSolution);
            }
            return double.NaN;
        }

        protected override double CreateVectorElement(BoundaryElement<T> elem1, Func<T, double> function, ConditionType conditionType)
        {
            return Integrator.Integrate(elem1, function);
        }
    }
}