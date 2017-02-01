using BEM.Common.Points;

namespace BEM.Common
{
    public class IntegrationPoint<T>
        where T : IPoint
    {
        public IntegrationPoint(T point, double jacobian, double weight)
        {
            Point = point;
            Jacobian = jacobian;
            Weight = weight;
        } 

        public T Point { get; private set; }

        public double Jacobian { get; private set; }

        public double Weight { get; private set; }
    }

    public class CopyOfIntegrationPoint<T>
           where T : IPoint
    {
        public CopyOfIntegrationPoint(T point, double jacobian, double weight)
        {
            Point = point;
            Jacobian = jacobian;
            Weight = weight;
        }

        public T Point { get; private set; }

        public double Jacobian { get; private set; }

        public double Weight { get; private set; }
    }
}