using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;
using BEM.Factory;

namespace BEM.Methods
{
    internal class JakobiNewtonMethod<T> : AbstractMethod<T>
        where T : IPoint
    {
        private readonly Vector solution;

        public JakobiNewtonMethod(
            List<BoundWithCondition<T>> bounds,
            List<InnerSourceWithFunction<T>> sources,
            Func<T, T, double> fundamentalSolution,
            List<Func<T, T, double>> derivates,
            Integrator<T> integrator,
            Vector solution)
            : base(bounds, sources, fundamentalSolution, derivates, integrator)
        {
            this.solution = solution;
        }

        protected override double CreateMatrixElement(
            BoundaryElement<T> elem1,
            BoundaryElement<T> elem2,
            ConditionType conditionType)
        {
            
            if (conditionType == ConditionType.Robin)
            {
                var IsExponental = true;
                if (IsExponental)
                {
                    var enumerator = KirghoffTransformation.U0 * KirghoffTransformation.U0*KirghoffTransformation.LAMDA0 *
                                    Integrator.Integrate(elem1, elem2.Center, FundamentalSolution);
                    var denumerator = KirghoffTransformation.BETALAMDA * KirghoffTransformation.BETALAMDA
                                      *( U(elem2.Center, solution)+1);
                    return KirghoffTransformation.Nuv(elem2.Center) * enumerator /  denumerator
                           - 0.5 * Kroneker(elem1, elem2) + Integrator.IntegratedQdnx(elem1, elem2, Derivates);
                }
                else
                {
                    var enumerator = Integrator.Integrate(elem1, elem2.Center, FundamentalSolution);
                    var denumerator =
                        Math.Sqrt(
                            1
                            + 2 * KirghoffTransformation.BETALAMDA * U(elem2.Center, solution)
                            / (KirghoffTransformation.LAMDA0 * KirghoffTransformation.U0));
                    return KirghoffTransformation.Nuv(elem2.Center) * enumerator / (KirghoffTransformation.LAMDA0 * denumerator)
                           - 0.5 * Kroneker(elem1, elem2) + Integrator.IntegratedQdnx(elem1, elem2, Derivates);
                }
            }
            else return 0;
        }

        protected override double CreateVectorElement(BoundaryElement<T> elem1, Func<T, double> function, ConditionType conditionType)
        {
            switch (conditionType)
            {
                case (ConditionType.Robin) :
                    {
                        var IsExponental = true;
                        if (IsExponental)
                        {
                            var ln =
                                Math.Log(
                                    KirghoffTransformation.BETALAMDA
                                    / (KirghoffTransformation.LAMDA0 * KirghoffTransformation.U0)
                                    * U(elem1.Center, solution) + 1);
                            return KirghoffTransformation.Nuv(elem1.Center) * (KirghoffTransformation.U0 + (KirghoffTransformation.U0 / KirghoffTransformation.BETALAMDA)*ln) 
                                + Q(elem1.Center, solution)
                                - KirghoffTransformation.Nuv(elem1.Center)
                                * base.CreateVectorElement(elem1, function, conditionType);
                        }
                        else
                        {
                            var sqrt =
                                Math.Sqrt(
                                    1
                                    + 2 * KirghoffTransformation.BETALAMDA * U(elem1.Center, solution)
                                    / (KirghoffTransformation.LAMDA0 * KirghoffTransformation.U0));
                            return KirghoffTransformation.Nuv(elem1.Center)
                                   * (KirghoffTransformation.U0
                                      + KirghoffTransformation.U0 / KirghoffTransformation.BETALAMDA * (sqrt - 1))
                                   + Q(elem1.Center, solution)
                                   - KirghoffTransformation.Nuv(elem1.Center)
                                   * base.CreateVectorElement(elem1, function, conditionType);
                        }
                        break;
                    }
            }
                return 0;

        }
    }
}

     