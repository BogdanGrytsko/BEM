using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Common.Points;

namespace BEM.Bounds
{
    public class Circle : Bound<Point2D>
    {
        private readonly int n;

        private int nObs = 10;

        private readonly double r;

        private readonly Point2D center;

        public Circle() : this(50, 1)
        {
        }

        public Circle(int n, double r)
        {
            this.n = n;
            this.r = r;
            center = new Point2D();
            Create();
        }

        private void Create()
        {
            double deltafi = 2 * Math.PI / n;
            var fi = GetFi(deltafi);
            for (int i = 0; i < n; i++)
            {
                var points = new List<Point2D>();
                for (int j = 0; j < 3; j++)
                {
                    points.Add(GetCirclePoint(fi[j]));
                }
                var element = new BoundaryElement1DSecondOrder(points, GetCirclePoint(fi[2]), this);
                element.Normal = new Point2D(Math.Cos(fi[2]), Math.Sin(fi[2]));
                Elements.Add(element);
                for (int j = 0; j < 3; j++)
                {
                    fi[j] += deltafi;
                }
            }
        }

        private static double[] GetFi(double deltafi)
        {
            var fi = new double[3];
            fi[0] = 0;
            fi[1] = fi[0] + deltafi;
            fi[2] = fi[0] + deltafi / 2;
            return fi;
        }

        private Point2D GetCirclePoint(double fi)
        {
            var x1 = r * Math.Cos(fi) + center.X1;
            var x2 = r * Math.Sin(fi) + center.X2;
            return new Point2D(x1, x2);
        }

        private IEnumerable<Point2D> CreateObservable()
        {
            var observable = new List<Point2D>(nObs);
            double deltafi = 2 * Math.PI / nObs;
            var fi = deltafi;
            for (int i = 0; i < nObs; i++)
            {
                observable.Add(GetCirclePoint(fi));
                fi += deltafi;
            }
            return observable;
        }

        #region Overrides of Bound1D

        public override bool Inside(Point2D x)
        {
            return x.Dist(center) < r;
        }

        public override Point2D BottomLeftCorner
        {
            get
            {
                return center - new Point2D(r, r);
            }
        }

        public override Point2D TopRightCorner
        {
            get
            {
                return center + new Point2D(r, r);
            }
        }

        public override IEnumerable<Point2D> ObservablePoints
        {
            get { return CreateObservable(); }
        }

        #endregion
    }
}