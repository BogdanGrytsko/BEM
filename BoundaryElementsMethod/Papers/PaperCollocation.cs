using System;
using System.Collections.Generic;
using System.Diagnostics;
using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;
using BEM.Factory;
using BEM.InnerSource;
using BEM.Plotter;

namespace BEM.Papers
{
    public class PaperCollocation
    {
        public void DoWork()
        {
            var bound = new ParallelepipedNearBound();
            // var bound = new Parallelepiped();
            // Writer.OutputIfAllowed(bound, "bound.txt");
            //var boundWithCondition = ConditionSetter.SetMixedCondition(bound);
            var boundWithCondition = ConditionSetter.SetKirghoffCondition(bound);
            // var source = new InnerSourcePlate();
            var source = new List<InnerSourceWithFunction<Point3D>>(); //no innersors CollocationKirghoffMethod
            // var innerSource = InnerSourceFactory.GetSourcesPlate(source);
            //var method = MethodFactory.GetCollocationMethodNearBoundary(boundWithCondition);//, innerSource);
            var method = MethodFactory.GetCollocationKirghoffMethod(
                boundWithCondition,
                source,
                KirghoffTransformation.Exp,
                KirghoffTransformation.ConverseExp);
            method.Solve();
            method.PlotErrorOnBound();
            var plotter = PlotterFactory.GetPlotter(bound, method.U);
            plotter.Plot();
        }
    }

    public class CopyOfPaperCollocation
    {
        public void DoWork()
        {
            var bound = new ParallelepipedNearBound();
            // var bound = new Parallelepiped();
            // Writer.OutputIfAllowed(bound, "bound.txt");
            //var boundWithCondition = ConditionSetter.SetMixedCondition(bound);
            var boundWithCondition = ConditionSetter.SetKirghoffCondition(bound);
            // var source = new InnerSourcePlate();
            var source = new List<InnerSourceWithFunction<Point3D>>(); //no innersors CollocationKirghoffMethod
            // var innerSource = InnerSourceFactory.GetSourcesPlate(source);
            //var method = MethodFactory.GetCollocationMethodNearBoundary(boundWithCondition);//, innerSource);
            var method = MethodFactory.GetCollocationKirghoffMethod(
                boundWithCondition,
                source,
                KirghoffTransformation.Exp,
                KirghoffTransformation.ConverseExp);
            method.Solve();
            method.PlotErrorOnBound();
            var plotter = PlotterFactory.GetPlotter(bound, method.U);
            plotter.Plot();
        }
    }
}
