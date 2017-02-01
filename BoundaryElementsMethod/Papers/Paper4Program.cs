using BEM.Bounds;
using BEM.Common;
using BEM.Factory;
using BEM.Plotter;

namespace BEM.Papers
{
    public class Paper4Program
    {
        public void DoWork()
        {
            double lambda1 = 10, lambda2 = 1;
            int circleN = 128, squareN = 32;
            var circle = new Circle(circleN, 1) { IsOuter = true };
            var square = new Rectangle(squareN / 4, squareN / 4);
            var boundWithCondition = ConditionSetter.SetDirichletCondition(square, circle);
            var method = MethodFactory.GetCollocationPaper4Method(boundWithCondition, lambda1, lambda2);
            method.Solve();
            string fileName = string.Format("plot2D;Lambda1={0};Lambda2={1};f=0;g=x2;.txt", lambda1, lambda2);
            var plotter = PlotterFactory.GetPlotter(circle, method.U, fileName, 40);
            plotter.Plot();
            var lineFileName = string.Format(
                "NCircle={0};NSquare={1};L1=10;L2=1;f=0;g=x2;.txt", circleN, squareN);
            var linePloter = new LinePlotter2D(circle, method.U, lineFileName);
            linePloter.Plot();
        }
    }
}