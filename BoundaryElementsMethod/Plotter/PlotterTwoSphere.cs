using System;
using System.IO;
using System.Text;
using BEM.Common;
using BEM.Common.Points;
using BEM.Factory;

namespace BEM.Plotter
{
    public class PlotterTwoSphere : Plotter3D
    {
        private const int N = 52;
        public const string Directory = "PaperTwoSphere";

        private readonly SemiSpaceParameters parameters;

        public PlotterTwoSphere(Func<Point3D, double> solution, SemiSpaceParameters parameters)
            : base(null, solution)
        {
            this.parameters = parameters;
        }

        private double Tr(Point3D m, Point3D n)
        {
            return FunctionsForSemiSpace.K(parameters.A, parameters.B, m, n) * (Solution(m) - Solution(n));
        }

        public override void Plot()
        {
            FormatedFileName = string.Format("ElectroStaticsOutputFd Sigma1 = {0}, Sigma2 = {1}, Sigma3 = {2}.txt",
                parameters.Sigma1, parameters.Sigma2, parameters.Sigma3);
            Writer.Output(OutputBetweenPoints(parameters.A, parameters.B), Path.Combine(Directory, FormatedFileName));
        }

        public string FormatedFileName { get; private set; }

        private string OutputBetweenPoints(Point3D a, Point3D b)
        {
            var sb = new StringBuilder();
            double step = a.Dist(b)/(N+1);
            Point3D m = new Point3D(a);
            Point3D n = new Point3D(a);
            m.ShiftToB(b, step);
            n.ShiftToB(b, 2*step);
            for (int i = 0; i < N; i++)
            {
                sb.AppendLine(m.X1 + " " + Math.Abs(Tr(m,n)));
                m.ShiftToB(b, step);
                n.ShiftToB(b, step);
            }
            return sb.ToString();
        }
    }
}
