using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Methods
{
    public class CollocationPaper6Method<T> : AbstractMethod<T> where T : IPoint
    {
        private readonly double lambda;

        public CollocationPaper6Method(
             List<BoundWithCondition<T>> bound,
            List<InnerSourceWithFunction<T>> sources,
            Func<T, T, double> fundamentalSolution,
            List<Func<T, T, double>> derivates,
            Integrator<T> integrator,
            double lambda)
            : base(bound, sources, fundamentalSolution, derivates, integrator)
        {
            this.lambda = lambda;
        }

        #region Overrides of AbstractMethod<T>

        protected override double CreateMatrixElement(BoundaryElement<T> elem1, BoundaryElement<T> elem2, ConditionType conditionType)
        {
            switch (conditionType)
            {
                case ConditionType.Dirichlet:
                    if (elem1.Bound.IsOuter && elem2.Bound.IsOuter)
                    {
                        return Integrator.Integrate(elem1, elem2.Center, FundamentalSolution, elem1 == elem2);
                    }
                    if (elem1.Bound.IsOuter && !elem2.Bound.IsOuter)
                    {
                        return lambda * Integrator.Integrate(elem1, elem2.Center, FundamentalSolution, elem1 == elem2);
                    }
                    if (!elem1.Bound.IsOuter && elem2.Bound.IsOuter)
                    {
                        return Integrator.Integrate(elem1, elem2.Center, FundamentalSolution, elem1 == elem2);
                    }
                    if (!elem1.Bound.IsOuter && !elem2.Bound.IsOuter)
                    {
                        return Kroneker(elem1, elem2) + lambda * Integrator.Integrate(elem1, elem2.Center, FundamentalSolution, elem1 == elem2);
                    }
                    break;
            }
            return double.NaN;
        }

        #endregion
    }
}