
using System;
using System.Collections.Generic;
using System.Diagnostics;
using BEM.BoundaryElements;
using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;
using BEM.Factory;
using BEM.InnerSource;
using BEM.Methods;
using BEM.Plotter;

namespace BEM.Papers
{
    internal class PaperNewtonMethod
    {

        public void DoWork()
        {
            var sw = new Stopwatch();
            sw.Start();
          //  var bound = new ParallelepipedNearBound();
            var bound = new Parallelepiped();
            var boundWithCondition = ConditionSetter.SetKirghoffCondition(bound);
            var source = new InnerSourcePlate();
          //  var innerSource = new List<InnerSourceWithFunction<Point3D>>(); //no innersors CollocationKirghoffMethod
             var innerSource = InnerSourceFactory.GetSourcesParalelepiped(source);

            var eps = 1;

            var paperKirghoff = new PaperKirghoff1UmovaProgram(bound, innerSource);
            var dPrevious = paperKirghoff.GetSolutionVector();
            Vector dCurrent;
            while (true)
            {
                var method1 = MethodFactory.GetJakobiMethod(boundWithCondition, innerSource, dPrevious);
                method1.Solve();
                dCurrent = dPrevious + method1.Solution;
                dPrevious = dCurrent;
                Console.WriteLine("d: " + method1.Solution.Norma());
                if (method1.Solution.Norma() < eps)
                {
                    break;
                }
            }
            var method = MethodFactory.GetCollocationKirghoffMethod(
                boundWithCondition,
                innerSource,
                KirghoffTransformation.Pow,
                KirghoffTransformation.ConversePow);

            method.Solution = dCurrent;

            var plotter = PlotterFactory.GetPlotter(bound, method.U);
            plotter.Plot();
            // Console.WriteLine("Total time: " + sw.ElapsedMilliseconds);

            /*f-1*f(d0)=x 
            d1=d0-x,
            f-1*f(d1)=x 
            d2=d1-x*/
        }
    }
}
