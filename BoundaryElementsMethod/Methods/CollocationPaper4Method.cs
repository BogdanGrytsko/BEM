using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Bounds;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Methods
{
    public class CollocationPaper4Method<T> : AbstractMethod<T>
        where T : IPoint
    {
        private readonly double lambda1, lambda2;

        public CollocationPaper4Method(
            List<BoundWithCondition<T>> bounds,
            List<InnerSourceWithFunction<T>> sources,
            Func<T, T, double> fundamentalSolution,
            List<Func<T, T, double>> derivates,
            Integrator<T> integrator,
            double lambda1,
            double lambda2)
            : base(bounds, sources, fundamentalSolution, derivates, integrator)
        {
            this.lambda1 = lambda1;
            this.lambda2 = lambda2;
        }

        protected override double CreateMatrixElement(
            BoundaryElement<T> elem1, BoundaryElement<T> elem2, ConditionType conditionType)
        {
            switch (conditionType)
            {
                case ConditionType.Dirichlet:
                    if (elem1.Bound.IsOuter && elem2.Bound.IsOuter)
                    {
                        return Kroneker(elem1, elem2) / 2 -Integrator.IntegratedQdny(elem1, elem2, Derivates);
                    }
                    if (elem1.Bound.IsOuter && !elem2.Bound.IsOuter)
                    {
                        return Integrator.Integrate(elem1, elem2.Center, FundamentalSolution);
                    }
                    if (!elem1.Bound.IsOuter && elem2.Bound.IsOuter)
                    {
                        return -(lambda1 - lambda2) * Integrator.IntegratedQdnx(elem1, elem2, Derivates);
                    }
                    if (!elem1.Bound.IsOuter && !elem2.Bound.IsOuter)
                    {
                        var first = Kroneker(elem1, elem2) * (lambda1 + lambda2) / 2;
                        return first + (lambda1 - lambda2) * Integrator.IntegratedQdnx(elem1, elem2, Derivates);
                    }
                    break;
            }
            return double.NaN;
        }

        public override double U(T x)
        {
            return CalculateSolutiuon(Solution,
                (bound, element) =>
                    {
                        if (bound.IsOuter)
                        {
                            return -1 * Integrator.IntegratedQdny(element, x, Derivates);
                        }
                        return Integrator.Integrate(element, x, FundamentalSolution);
                    });
        }
    }
}