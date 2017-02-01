using System;
using System.Text;

using BEM.Bounds;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Plotter
{
    public class Plotter2D : AbstractPlotter<Point2D>
    {
        private readonly int n;

        public Plotter2D(Bound<Point2D> bound, Func<Point2D, double> solution, string fileName, int n)
            : base(bound, solution, fileName)
        {
            Integrator = new Integrator<Point2D>(4);
            this.n = n;
        }

        public Plotter2D(Bound<Point2D> bound, Func<Point2D, double> solution, int n)
            : this(bound, solution, "plot2D.txt", n)
        {
        }

        public override void Plot()
        {
            Writer.Output(GetPlot(), FileName);
        }

        private string GetPlot()
        {
            var sb = new StringBuilder();
            var leftCorner = Bound.BottomLeftCorner;
            var rightCorner = Bound.TopRightCorner;
            var h1 = (rightCorner.X1 - leftCorner.X1) / n;
            var h2 = (rightCorner.X2 - leftCorner.X2) / n;
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    var point = new Point2D(leftCorner.X1 + i * h1, leftCorner.X2 + j * h2);
                    var sol = Bound.Inside(point) ? Solution(point) : 0;
                    var text = string.Format("{0} {1} {2}", point.X1, point.X2, sol);
                    sb.AppendLine(text);
                }
            }
            return sb.ToString();
        }
    }
}