using BEM.Common.Points;

namespace BEM.Factory
{
    public class SemiSpaceParameters
    {
        public double Sigma1 { get; private set; }

        public double Sigma2 { get; private set; }

        public double Sigma3 { get; private set; }

        public double APotential { get; private set; }

        public Point3D A { get; private set; }

        public double BPotential { get; private set; }

        public Point3D B { get; private set; }

        public SemiSpaceParameters(double sigma1, double sigma2, double sigma3)
        {
            Sigma1 = sigma1;
            Sigma2 = sigma2;
            Sigma3 = sigma3;

            A = new Point3D(-20, 0, 0);
            B = new Point3D(20, 0, 0);
            APotential = 1;
            BPotential = -1;
        }
    }
}