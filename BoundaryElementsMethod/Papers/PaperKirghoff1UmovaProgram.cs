using System.Collections.Generic;
using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;
using BEM.Factory;
using BEM.Methods;

namespace BEM.Papers
{
    public class PaperKirghoff1UmovaProgram
    {
        private readonly AbstractMethod<Point3D> method;

        public PaperKirghoff1UmovaProgram(Bound<Point3D> bound, List<InnerSourceWithFunction<Point3D>> source)
        {
            var boundWithCondition = ConditionSetter.SetDirichletConditionSphere(bound);
            method = MethodFactory.GetCollocationKirghoffMethod(
                boundWithCondition,
                source,
                KirghoffTransformation.Pow,
                KirghoffTransformation.ConversePow);
            method.Solve();
        }

        public Vector GetSolutionVector()
        {
            return method.Solution;
        }
    }
}
