using System;
using System.Text;

using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;
using BEM.Factory;

namespace BEM.Plotter
{
    public class PlotterParallelepiped : Plotter3D
    {
        private const int N = 8;
 

        public PlotterParallelepiped(Parallelepiped bound, Func<Point3D, double> solution)
            : base(bound, solution)
        {
        }

        public override void Plot()
        {
            //Writer.Output(GetPlotAnalitSoluyionInside(), "GetPlotAnalitSoluyionInside.txt");
            // Writer.Output(GetPlotTemperature(), "HeatPowBEM,Q0=300,BETALAMDA = 0.1,LAMDA0 = 1,vcudyumova 400+100x3,bezinnersourse,oblast[-0.5,05;-1,1;-1,1].txt");
            //Writer.Output(GetPlotFrontBack(Bound.BottomLeftCorner.X3), "QFront.txt");
            //Writer.Output(GetPlotBottomTop(Bound.BottomLeftCorner.X2), "QBottom .txt");
            //Writer.Output(GetPlotRightLeft(Bound.BottomLeftCorner.X1), "QLeft.txt");
            //Writer.Output(GetPlotFrontBack(Bound.TopRightCorner.X3), "QBack.txt");
            var fileName = string.Format(
                "HeatPow_withtInnerSource3D-10.txt",
                KirghoffTransformation.U0, KirghoffTransformation.BETALAMDA,KirghoffTransformation.LAMDA0,KirghoffTransformation.NLAMDA);
            Writer.Output(GetPlotBottomTop(Bound.TopRightCorner.X2), fileName);
            //Writer.Output(GetPlotRightLeft(Bound.TopRightCorner.X1), "QRight.txt"); 

/*
           //Writer.Output(GetPlotAnalitSoluyionInside(), "GetPlotAnalitSoluyionInside.txt");
           // Writer.Output(GetPlotTemperature(), "HeatPowBEM,Q0=300,BETALAMDA = 0.1,LAMDA0 = 1,vcudyumova 400+100x3,bezinnersourse,oblast[-0.5,05;-1,1;-1,1].txt");
           //Writer.Output(GetPlotFrontBack(Bound.BottomLeftCorner.X3), "QFront.txt");
           //Writer.Output(GetPlotBottomTop(Bound.BottomLeftCorner.X2), "QBottom .txt");
           //Writer.Output(GetPlotRightLeft(Bound.BottomLeftCorner.X1), "QLeft.txt");
           //Writer.Output(GetPlotFrontBack(Bound.TopRightCorner.X3), "QBack.txt");
            Writer.Output(GetPlotBottomTop(Bound.TopRightCorner.X2), "1DHeatPowBEM, Q0=20,BETALAMDA = -0.01,LAMDA0 = 1, nlamda=1, vsudy 100+10*x3, bezinnersourse,oblast[-1,1;-1,1;-1,1]x2const 0.5.txt");
           //Writer.Output(GetPlotRightLeft(Bound.TopRightCorner.X1), "QRight.txt"); 

*/
        }

        private string GetPlotAnalitSoluyionInside()
        {
            var sb = new StringBuilder();
            var leftCorner = Bound.BottomLeftCorner;
            var rightCorner = Bound.TopRightCorner;
            var h1 = (rightCorner.X1 - leftCorner.X1) / N;
            var h2 = (rightCorner.X2 - leftCorner.X2) / N;
            var h3 = (rightCorner.X3 - leftCorner.X3) / N;
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    for (int k = 0; k <= N; k++)
                    {
                        var point = new Point3D(leftCorner.X1 + i * h1, leftCorner.X2 + j * h2, leftCorner.X3 + k * h3);
                        if (Bound.Inside(point))
                        {
                            var u = Solution(point);
                            var analytical = ((Parallelepiped)Bound).AnalyticalSolution(point);
                            var error = Math.Abs(u - analytical);
                            var text = string.Format(
                                "{0} {1} {2} {3} {4} {5} {6} {7}",
                                "[",
                                point.X1,
                                point.X2,
                                point.X3,
                                "]",
                                u,
                                analytical,
                                error);
                            sb.AppendLine(text);
                        }
                    }
                }
            }
            return sb.ToString();
        }

        private string GetPlotTemperature()
        {
            var sb = new StringBuilder();
            var leftCorner = Bound.BottomLeftCorner;
            var rightCorner = Bound.TopRightCorner;
            var h1 = (rightCorner.X1 - leftCorner.X1) / N;
            var h2 = (rightCorner.X2 - leftCorner.X2) / N;
            var h3 = (rightCorner.X3 - leftCorner.X3) / N;
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    for (int k = 0; k <= N; k++)
                    {
                        var point = new Point3D(leftCorner.X1 + i * h1, leftCorner.X2 + j * h2, leftCorner.X3 + k * h3);

                        var u = Solution(point);
                        //  var error = Math.Abs(u - point.X3);
                        var text = string.Format("{0} {1} {2} {3} ", point.X1, point.X2, point.X3, u);
                        sb.AppendLine(text);

                    }
                }
            }
            return sb.ToString();
        }

        private string GetPlotSection(double x2)
        {
            var sb = new StringBuilder();
            return sb.ToString();
        }

        private string GetPlotFrontBack(double x3)
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
                    var point = new Point3D(leftCorner.X1 + i * h1, leftCorner.X2 + j * h2, x3);
                    var text = string.Format("{0} {1} {2}", point.X1, point.X2, Solution(point));
                    sb.AppendLine(text);
                }
            }
            return sb.ToString();
        }

        private string GetPlotBottomTop(double x2)
        {
            var sb = new StringBuilder();
            var leftCorner = Bound.BottomLeftCorner;
            var rightCorner = Bound.TopRightCorner;

            var h1 = (rightCorner.X1 - leftCorner.X1) / N;
            var h3 = (rightCorner.X3 - leftCorner.X3) / N;

            for (int i = 0; i <= N; i++)

            {

                for (int k = 0; k <= N; k++)
                {
                   var point = new Point3D(leftCorner.X1 + i * h1, 0.5, leftCorner.X3 + k * h3);
                   var text = string.Format("{0} {1} {2}", point.X1, point.X3, Math.Abs(Solution(point)));
                // var point = new Point3D(0, 0.5, leftCorner.X3 + k * h3);
               //  var text = string.Format("{0} {1} ", point.X3, Solution(point));
                    sb.AppendLine(text);
                }
            }
            return sb.ToString();
        }


        private string GetPlotRightLeft(double x1)
        {
            var sb = new StringBuilder();
            var leftCorner = Bound.BottomLeftCorner;
            var rightCorner = Bound.TopRightCorner;
            var h2 = (rightCorner.X2 - leftCorner.X2) / N;
            var h3 = (rightCorner.X3 - leftCorner.X3) / N;

            for (int j = 0; j <= N; j++)
            {
                for (int k = 0; k <= N; k++)
                {
                    var point = new Point3D(x1, leftCorner.X2 + j * h2, leftCorner.X3 + k * h3);
                    var text = string.Format("{0} {1} {2}", point.X2, point.X3, Solution(point));
                    sb.AppendLine(text);
                }
            }
            return sb.ToString();
        }
    }
}