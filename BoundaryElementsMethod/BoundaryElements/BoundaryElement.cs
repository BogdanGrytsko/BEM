using System.Collections.Generic;
using System.Linq;

using BEM.Bounds;
using BEM.Common;
using BEM.Common.Points;

namespace BEM.BoundaryElements
{
    public abstract class BoundaryElement<T>
        where T : IPoint
    {
        public List<T> Points { get; set; }

        public T Center { get; protected set; }

        public T Normal { get; set; }

        // TODO this property might not be set in all bounds. Refactor.
        public Bound<T> Bound { get; set; }

        private readonly SortedDictionary<int, List<IntegrationPoint<T>>> integrationPoints 
            = new SortedDictionary<int, List<IntegrationPoint<T>>>();

        public List<IntegrationPoint<T>> GetIntegrationPoints(int n)
        {
            if (!integrationPoints.ContainsKey(n))
            {
                integrationPoints.Add(n, CreateIntegrationPoints(n));
            }
            return integrationPoints[n];
        }

        protected abstract List<IntegrationPoint<T>> CreateIntegrationPoints(int n);

        public abstract double Yakobian(IPoint point);

        protected abstract T Interpolate(IPoint point);

        public override string ToString()
        {
            string result = Points.Aggregate("{", (current, point) => current + (point + " "));
            result += "C: " + Center;
            result += "}";
            return result;
        }
    }
}