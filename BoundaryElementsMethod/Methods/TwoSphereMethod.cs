using System;
using System.Collections.Generic;
using System.Linq;
using BEM.BoundaryElements;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;
using BEM.Enum;
using BEM.Factory;

namespace BEM.Methods
{
    public class TwoSphereMethod : AbstractMethod<Point3D>
    {
        private readonly FunctionsForSemiSpace functionsForSemiSpace;
        private readonly SemiSpaceParameters parameters;

        public TwoSphereMethod(List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources, Integrator<Point3D> integrator, SemiSpaceParameters parameters)
            : base(bound, sources, null, null, integrator)
        {
            this.parameters = parameters;
            functionsForSemiSpace = new FunctionsForSemiSpace(parameters);
        }

        protected override double CreateMatrixElement(BoundaryElement<Point3D> elem1, BoundaryElement<Point3D> elem2,
            ConditionType conditionType)
        {
            if (elem1.Bound.Name == BoundNumber.Bound12 && elem2.Bound.Name == BoundNumber.Bound12)
            {
                return Integrator.Integrate(elem1, elem2.Center, functionsForSemiSpace.U1);
            }
            if (elem1.Bound.Name == BoundNumber.Bound12 && elem2.Bound.Name == BoundNumber.Bound2)
            {
                return -Integrator.Integrate(elem1, elem2.Center, functionsForSemiSpace.U2);
            }
            if (elem1.Bound.Name == BoundNumber.Bound12 && elem2.Bound.Name == BoundNumber.Bound13)
            {
                return Integrator.Integrate(elem1, elem2.Center, functionsForSemiSpace.U1);
            }
            if (elem1.Bound.Name == BoundNumber.Bound12 && elem2.Bound.Name == BoundNumber.Bound3)
            {
                return 0;
            }
            //2-----------------------------------------------
            if (elem1.Bound.Name == BoundNumber.Bound2 && elem2.Bound.Name == BoundNumber.Bound12)
            {
                return Integrator.IntegratedQdnx(elem1, elem2, FunctionsForSemiSpace.Derivates);
            }
            if (elem1.Bound.Name == BoundNumber.Bound2 && elem2.Bound.Name == BoundNumber.Bound2)
            {
                return -Integrator.IntegratedQdnx(elem1, elem2, FunctionFactory.Derivates);
            }
            if (elem1.Bound.Name == BoundNumber.Bound2 && elem2.Bound.Name == BoundNumber.Bound13)
            {
                return Integrator.IntegratedQdnx(elem1, elem2, FunctionsForSemiSpace.Derivates);
            }
            if (elem1.Bound.Name == BoundNumber.Bound2 && elem2.Bound.Name == BoundNumber.Bound3)
            {
                return 0;
            }
            //3-----------------------------------------------
            if (elem1.Bound.Name == BoundNumber.Bound13 && elem2.Bound.Name == BoundNumber.Bound12)
            {
                return Integrator.Integrate(elem1, elem2.Center, functionsForSemiSpace.U1);
            }
            if (elem1.Bound.Name == BoundNumber.Bound13 && elem2.Bound.Name == BoundNumber.Bound2)
            {
                return 0;
            }
            if (elem1.Bound.Name == BoundNumber.Bound13 && elem2.Bound.Name == BoundNumber.Bound13)
            {
                return Integrator.Integrate(elem1, elem2.Center, functionsForSemiSpace.U1);
            }
            if (elem1.Bound.Name == BoundNumber.Bound13 && elem2.Bound.Name == BoundNumber.Bound3)
            {
                return -(Integrator.Integrate(elem1, elem2.Center, functionsForSemiSpace.U3));
            }
            //4------------------------------------------------
            if (elem1.Bound.Name == BoundNumber.Bound3 && elem2.Bound.Name == BoundNumber.Bound12)
            {
                return Integrator.IntegratedQdnx(elem1, elem2, FunctionsForSemiSpace.Derivates);
            }
            if (elem1.Bound.Name == BoundNumber.Bound3 && elem2.Bound.Name == BoundNumber.Bound2)
            {
                return 0;
            }
            if (elem1.Bound.Name == BoundNumber.Bound3 && elem2.Bound.Name == BoundNumber.Bound13)
            {
                return Integrator.IntegratedQdnx(elem1, elem2, FunctionsForSemiSpace.Derivates);
            }
            if (elem1.Bound.Name == BoundNumber.Bound3 && elem2.Bound.Name == BoundNumber.Bound3)
            {
                return -Integrator.IntegratedQdnx(elem1, elem2, FunctionFactory.Derivates);
            }

            throw new Exception("Invalid boundaries");
        }

        protected override double CreateVectorElement(BoundaryElement<Point3D> elem1, Func<Point3D, double> function,
            ConditionType conditionType)
        {

            switch (elem1.Bound.Name)
            {
                case BoundNumber.Bound12:
                    return -parameters.BPotential*functionsForSemiSpace.U1(elem1.Center, parameters.B) -
                           parameters.APotential*functionsForSemiSpace.U1(elem1.Center, parameters.A);
                case BoundNumber.Bound2:
                    var fbBound2 =
                        elem1.Normal.ScalarMultiply(FunctionsForSemiSpace.FSemiSpace(elem1.Normal, parameters.B));
                    var faBound2 =
                        elem1.Normal.ScalarMultiply(FunctionsForSemiSpace.FSemiSpace(elem1.Normal, parameters.A));
                    return -parameters.APotential*faBound2 - parameters.BPotential*fbBound2;
                case BoundNumber.Bound13:
                    return -parameters.BPotential*functionsForSemiSpace.U1(elem1.Center, parameters.B) -
                           parameters.APotential*functionsForSemiSpace.U1(elem1.Center, parameters.A);
                case BoundNumber.Bound3:
                    var fbBound3 =
                        elem1.Normal.ScalarMultiply(FunctionsForSemiSpace.FSemiSpace(elem1.Normal, parameters.B));
                    var faBound3 =
                        elem1.Normal.ScalarMultiply(FunctionsForSemiSpace.FSemiSpace(elem1.Normal, parameters.A));
                    return -parameters.APotential*faBound3 - parameters.BPotential*fbBound3;
            }

            return base.CreateVectorElement(elem1, function, conditionType);
        }

        public override double U(Point3D x)
        {
            double sum = 0;
            for (int i = 0; i < BoundWithConditions.Count; i++)
            {
                var bound = BoundWithConditions[i];
                for (int j = 0; j < bound.Count(); j++)
                {
                    sum += U(x, bound.Bound.Elements[j], i, j, bound.Count());
                }
            }
            return sum + parameters.BPotential * functionsForSemiSpace.U1(x, parameters.B) + parameters.APotential * functionsForSemiSpace.U1(x, parameters.A);
        }

        public double U(Point3D x, BoundaryElement<Point3D> elem, int boundNumber, int boundElem, int count)
        {
            if (elem.Bound.Name == BoundNumber.Bound12)
            {
                return Integrator.Integrate(elem, x, functionsForSemiSpace.U1) * Solution[boundElem];
            }
            if (elem.Bound.Name == BoundNumber.Bound13)
            {
                return Integrator.Integrate(elem, x, functionsForSemiSpace.U1) * Solution[boundNumber * count + boundElem];
            }
            return 0;
        }
    }
}
