using System;
using System.Text;

using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;

namespace BEM.Plotter
{
    public class LinePlotter2D : AbstractPlotter<Point2D>
    {
        protected const int N = 50;

        public LinePlotter2D(
            Bound<Point2D> bound, Func<Point2D, double> solution, string fileName)
            : base(bound, solution, fileName)
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
            var middleX1 = (leftCorner.X1 + rightCorner.X1) / 2;
            ////var h1 = (rightCorner.X1 - leftCorner.X1) / N;
            var h2 = (rightCorner.X2 - leftCorner.X2) / N;
            for (int i = N/2; i < N; i++)
            {
                var point = new Point2D(middleX1, leftCorner.X2 + i * h2);
                var sol = Bound.Inside(point) ? Solution(point) : 0;
                var text = string.Format("{0} {1:F5}", point.X2, sol);
                sb.AppendLine(text);
            }
            return sb.ToString();
        }
    }
}