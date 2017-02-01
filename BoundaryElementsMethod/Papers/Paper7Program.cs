using BEM.Bounds;
using BEM.Common.Points;
using BEM.Factory;
using BEM.Plotter;

namespace BEM.Papers
{
    public class Paper7Program
    {
        public void DoWork()
        {
            ////var bound = new Circle(800, 2) { IsOuter = true };

            var bound = new Rectangle(new Point2D(-1, -1), new Point2D(1, 1), 160) { IsOuter = true };
            ////var segment1 = new Segment(new Point2D(-0.75, 0.1), new Point2D(0, 0.9), 80);
            ////var segment2 = new Segment(new Point2D(0, 0.9), new Point2D(0.75, 0.1), 80);

            ////var segment3 = new Segment(new Point2D(0, -0.9), new Point2D(-0.75, -0.1), 80);
            ////var segment4 = new Segment(new Point2D(0.75, -0.1), new Point2D(0, -0.9), 80);
            ////var boundWithCondition = ConditionSetter.SetDirichletCondition(segment1, segment2, segment3, segment4, bound);

            var segment1 = new Segment(new Point2D(-0.95, 0), new Point2D(0.95, 0), 160);
            var boundWithCondition = ConditionSetter.SetDirichletCondition(segment1, bound);
            var method = MethodFactory.GetCollocationPaper7Method(boundWithCondition, 1);
            method.Solve();
            var plotter = PlotterFactory.GetPlotter(bound, method.U, 40);
            plotter.Plot();
        } 
    }
}