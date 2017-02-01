using BEM.Common.Points;

namespace BEM.Common.GaussIntegrator
{
    public class GaussPoint<T> where T: IPoint
    {
        public T Point { get; private set; }

        public double Weight { get; private set; }

        public GaussPoint(T point, double weight)
        {
            Point = point;
            Weight = weight;
        }

        public override string ToString()
        {
            return string.Format("{0,5:0.00} {1,5:0.00}", Point, Weight);
        }
    }

    public class CopyOfGaussPoint<T> where T : IPoint
    {
        public T Point { get; private set; }

        public double Weight { get; private set; }

        public CopyOfGaussPoint(T point, double weight)
        {
            Point = point;
            Weight = weight;
        }

        public override string ToString()
        {
            return string.Format("{0,5:0.00} {1,5:0.00}", Point, Weight);
        }
    }
}