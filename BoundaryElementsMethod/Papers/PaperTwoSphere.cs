using System.Collections.Generic;
using System.Linq;
using BEM.Bounds;
using BEM.Common.Points;
using BEM.Enum;
using BEM.Factory;
using BEM.Plotter;

namespace BEM.Papers
{
    public class PaperTwoSphere
    {
        public void DoWork()
        {
            var compositePlotter = new CompositePlotter();
            foreach (var useCase in UseCases)
            {
                compositePlotter.FileNames.Add(CalcOneMethod(useCase));
            }
            compositePlotter.Plot();
        }

        private IEnumerable<SemiSpaceParameters> UseCases
        {
            get
            {

             //   yield return new SemiSpaceParameters(1, 2, 2);
              //  yield return new SemiSpaceParameters(1, 5, 5);
              //  yield return new SemiSpaceParameters(1, 7, 10);
                yield return new SemiSpaceParameters(1, 1, 1);
                yield return new SemiSpaceParameters(1, 0.01, 1);
                yield return new SemiSpaceParameters(1, 1, 0.01);
                yield return new SemiSpaceParameters(1, 0.01, 0.01);
            //    yield return new SemiSpaceParameters(1, 1, 0.000001);
            //    yield return new SemiSpaceParameters(1, 0.000001, 0.000001);
            }
        }

        private static string CalcOneMethod(SemiSpaceParameters parameters)
        {
            var bound12 = new Parallelepiped(-1, 1, -1, 1, FunctionsForSemiSpace.H, 1, 4, 4, 4) { Name = BoundNumber.Bound12 };
            var bound2 = new Parallelepiped(-1, 1, -1, 1, FunctionsForSemiSpace.H, 1, 4, 4, 4) { Name = BoundNumber.Bound2 };
            var bound13 = new Parallelepiped(-1, 1, 0, 1, -1, FunctionsForSemiSpace.H, 4, 4, 4) { Name = BoundNumber.Bound13 };
            var bound3 = new Parallelepiped(-1, 1, 0, 1, -1, FunctionsForSemiSpace.H, 4, 4, 4) { Name = BoundNumber.Bound3 };
         //   var bound12 = new Sphere(new Point3D(5, 0, FunctionsForSemiSpace.H)) { Name = BoundNumber.Bound12 };
          //var bound2 = new Sphere(new Point3D(5, 0, FunctionsForSemiSpace.H)) { Name = BoundNumber.Bound2 };
          //  var bound13 = new Sphere(new Point3D(5, 0, FunctionsForSemiSpace.H)) { Name = BoundNumber.Bound13 };
          //  var bound3 = new Sphere(new Point3D(5, 0, FunctionsForSemiSpace.H)) { Name = BoundNumber.Bound3 };
           var boundWithCondition = ConditionSetter.SetBoundSphere(bound12, bound2, bound13, bound3).ToList();
       //   var boundWithCondition = ConditionSetter.SetBoundSphere(bound12, bound2).ToList();
            var method = MethodFactory.GetSphereMethod(boundWithCondition, null, parameters);
            method.Solve();
            var plotter = PlotterFactory.GetPlotter(method.U, parameters);
            plotter.Plot();
            return plotter.FormatedFileName;
        }
    }
}
