using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;
using BEM.Factory;
using BEM.Plotter;

namespace BEM.Papers
{
    public class Paper6Program
    {
        public void DoWork()
        {
            var bound = new Circle(800, 2) { IsOuter = true };

            ////var bound = new Rectangle(new Point2D(-1, -1), new Point2D(1, 1), 160) { IsOuter = true };
            var segment1 = new Segment(new Point2D(-1.1, -1.1), new Point2D(1.1, 1.1), 160);
            ////var segment1 = new Segment(new Point2D(-0.75, 0.1), new Point2D(0, 0.9), 80);
            ////var segment2 = new Segment(new Point2D(-0.75, -0.1), new Point2D(0, -0.9), 80);
            ////var segment3 = new Segment(new Point2D(0.75, 0.1), new Point2D(0, 0.9), 80);
            ////var segment4 = new Segment(new Point2D(0.75, -0.1), new Point2D(0, -0.9), 80);
            var boundWithCondition = ConditionSetter.SetDirichletCondition(segment1, bound);
            var method = MethodFactory.GetCollocationPaper6Method(boundWithCondition, 1);
            method.Solve();
            var plotter = PlotterFactory.GetPlotter(bound, method.U, 40);
            plotter.Plot();
        }
    }
}