using System;

using BEM.Common.Points;

namespace BEM.Bounds
{
    public class Rectangle : Bound<Point2D>
    {
        private readonly double a1 = 0.25, a2 = -0.25, b1 = 0.25, b2 = -0.25;

        private readonly int n1, n2;

        public Rectangle(Point2D bottomLeft, Point2D topRight, int n1, int n2)
        {
            this.n1 = n1;
            this.n2 = n2;
            a1 = topRight.X1;
            a2 = bottomLeft.X1;
            b1 = topRight.X2;
            b2 = bottomLeft.X2;
            Create();
        }

        public Rectangle(Point2D bottomLeft, Point2D topRight, int n)
            :this(bottomLeft, topRight, n ,n)
        {
        }

        public Rectangle(int n1, int n2)
            : this(new Point2D(-0.25, -0.25), new Point2D(0.25, 0.25), n1, n2)
        {
        }

        private void Create()
        {
            var top = new Segment(new Point2D(a2, b1), new Point2D(a1, b1), n1);
            var right = new Segment(new Point2D(a1, b1), new Point2D(a1, b2), n2);
            var bottom = new Segment(new Point2D(a1, b2), new Point2D(a2, b2), n1);
            var left = new Segment(new Point2D(a2, b2), new Point2D(a2, b1), n2);
            Add(top, right, bottom, left);
        }

        #region Overrides of Bound1D

        public override bool Inside(Point2D x)
        {
            return x.X1 >= BottomLeftCorner.X1 && x.X2 >= BottomLeftCorner.X2 && x.X1 <= TopRightCorner.X1
                   && x.X2 <= TopRightCorner.X2;
        }

        public override Point2D BottomLeftCorner
        {
            get
            {
                return new Point2D(Math.Min(a1, a2), Math.Min(b1, b2));
            }
        }

        public override Point2D TopRightCorner
        {
            get
            {
                return new Point2D(Math.Max(a1, a2), Math.Max(b1, b2));
            }
        }

        #endregion
    }
}