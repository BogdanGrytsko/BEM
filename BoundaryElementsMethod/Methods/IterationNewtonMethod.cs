using System;
using System.Collections.Generic;
using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;
using BEM.Factory;

namespace BEM.Methods
{
    internal class IterationNewtonMethod<T> : AbstractMethod<T> where T : IPoint
    {

        public IterationNewtonMethod(List<BoundWithCondition<T>> bounds,
                                     List<InnerSourceWithFunction<T>> sources,
                                     Func<T, T, double> fundamentalSolution,
                                     List<Func<T, T, double>> derivates,
                                     Integrator<T> integrator)
            : base(bounds, sources, fundamentalSolution, derivates, integrator)
        {
        }

        protected override double CreateMatrixElement(
            BoundaryElement<T> elem1,
            BoundaryElement<T> elem2,
            ConditionType conditionType)
        {
            double sum = 1;
            switch (conditionType)
            {
                case ConditionType.Pow:
                    var derivate2 = Integrator.Integrate(elem1, elem2.Center, Derivates);
                    var enumerator = Integrator.Integrate(elem1, elem2.Center, FundamentalSolution);
                    var denumerator =
                        Math.Sqrt(
                            1
                            + 2 * KirghoffTransformation.BETALAMDA * sum
                            / (KirghoffTransformation.LAMDA0 * KirghoffTransformation.U0));
                    return KirghoffTransformation.Nuv(elem2.Center) * enumerator
                           / (KirghoffTransformation.LAMDA0 * denumerator) - 0.5 * Kroneker(elem1, elem2)
                           + derivate2.ScalarMultiply(elem1.Normal);
                case ConditionType.Exp:
                    return 0;
            }
            return double.NaN;
        }

        protected override double CreateVectorElement(BoundaryElement<T> elem1, Func<T, double> function)
        {
            throw new NotImplementedException();
        }
    }

}
