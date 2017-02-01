using System;

using BEM.BoundaryElements;
using BEM.Common.Points;

namespace BEM.Bounds
{
    public class Segment : Bound<Point2D>
    {
        private readonly Point2D start, end, direction;
        private readonly int n;
        private readonly double h;

        public Segment(Point2D start, Point2D end, int n)
        {
            this.n = n;
            this.start = start;
            this.end = end;
            h = start.Dist(end) / n;
            direction = (end - start) / start.Dist(end);
            Create();
        }

        public Segment(int n)
            : this(new Point2D(-0.25, -0.25), new Point2D(0.25, 0.25), n)
        {
        }

        private void Create()
        {
            var localStart = new Point2D(start);
            for (int i = 0; i < n; i++)
            {
                var localEnd = localStart + direction * h;
                var middle = localStart + direction * h / 2;
                var elem = new BoundaryElement1DSecondOrder(localStart, localEnd, middle, middle, this);
                elem.Normal = new Point2D(-direction.X2, direction.X1);
                Elements.Add(elem);
                localStart = localEnd;
            }
        }

        private void CreateNonLinear()
        {
            var localStart = new Point2D(start);
            var l = start.Dist(end) / 2;
            for (int i = 0; i < n; i++)
            {
                double h1;
                if (i == 0 || i == 1)
                {
                    h1 = l / Math.Pow(2, n - 1);
                }
                else
                {
                    h1 = l / Math.Pow(2, n - i);
                }
                var first = localStart + direction * h1;
                var second = localStart + direction * h1 / 2;
                var elem = new BoundaryElement1DSecondOrder(localStart, first, second, second, this);
                elem.Normal = new Point2D(-direction.X2, direction.X1);
                Elements.Add(elem);
                localStart = first;
            }
            for (int i = 0; i < n; i++)
            {
                double h1;
                if (i == n - 1 || i == n - 2)
                {
                    h1 = l / Math.Pow(2, n - 1);
                }
                else
                {
                    h1 = l / Math.Pow(2, i + 1);
                }
                var first = localStart + direction * h1;
                var second = localStart + direction * h1 / 2;
                var elem = new BoundaryElement1DSecondOrder(localStart, first, second, second, this);
                elem.Normal = new Point2D(-direction.X2, direction.X1);
                Elements.Add(elem);
                localStart = first;
            }
        }

        #region Overrides of Bound1D

        public override bool Inside(Point2D x)
        {
            throw new NotImplementedException();
        }

        public override Point2D BottomLeftCorner
        {
            get
            {
                return new Point2D(Math.Min(start.X1, end.X1), Math.Min(start.X2, end.X2));
            }
        }

        public override Point2D TopRightCorner
        {
            get
            {
                return new Point2D(Math.Max(start.X1, end.X1), Math.Max(start.X2, end.X2));
            }
        }

        #endregion
    }
}