using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;
using BEM.Factory;
using BEM.Plotter;

namespace BEM.Papers
{
  public  class PlotterOneSphere:Plotter3D
    {
      private const int N = 52;
      public PlotterOneSphere(Sphere bound, Func<Point3D, double> solution)
            : base(bound, solution)
        {

        }

        public override void Plot()
        {
            var fileName = string.Format(
                "HeatPow2+1_SphereU0 = 5.txt",
                KirghoffTransformation.U0, KirghoffTransformation.BETALAMDA, KirghoffTransformation.LAMDA0, KirghoffTransformation.NLAMDA);
            Writer.Output(GetPlot(), fileName);
        }

        private string GetPlot()
        {
            var sb = new StringBuilder();
            var leftCorner = Bound.BottomLeftCorner;
            var rightCorner = Bound.TopRightCorner;
            var h1 = (rightCorner.X1 - leftCorner.X1) / N;
            var h2 = (rightCorner.X2 - leftCorner.X2) / N;
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    var point = new Point3D(leftCorner.X2 + i * h1, leftCorner.X1 + j * h2,0);
                    var sol = Bound.Inside(point) ? Solution(point) : 0;
                    var text = string.Format("{0} {1} {2}", point.X1, point.X2, sol);
                    sb.AppendLine(text);
                }
            }
            return sb.ToString();
        }
    }
}
