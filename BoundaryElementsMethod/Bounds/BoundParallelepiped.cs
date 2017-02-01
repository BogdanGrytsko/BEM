using System;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Common.Points;

namespace BEM.Bounds
{
    public class BoundParallelepiped : Bound<Point3D>
    {
        private const int N = 22;
        private double a1 = 0.5, a2 = -0.5, b1 = 1, b2 = -1, c1 = 1, c2 = -1;
        private static int n1 = 2, n2 = 4, n3 = 4;
        private static int frontBackEnd = n1 * n2 * 2;
        private static int leftRightEnd = frontBackEnd + n2 * n3 * 2;
        private int topBottomEnd = leftRightEnd + n3 * n1 * 2;
        private double h1, h2, h3, hRightLeft, hFrontBack, hBottomTop;

        public BoundParallelepiped()
        {
            h1 = (a1 - a2) / n1;
            h2 = (b1 - b2) / n2;
            h3 = (c1 - c2) / n3;
            hRightLeft = 0.01;
            hFrontBack = 0.01;
            hBottomTop = 0.01;

         CreateBoundaryElements();

    // CreateNearBoundaryElements();
        }

        public IEnumerable<BoundaryElement<Point3D>> TopPane
        {
            get 
            {
                return BoundaryElements(leftRightEnd + 1, topBottomEnd);
            }
        }

        public IEnumerable<BoundaryElement<Point3D>> BottomPane
        {
            get
            {
                return BoundaryElements(leftRightEnd, topBottomEnd);
            }
        }

        public IEnumerable<BoundaryElement<Point3D>> RightPane
        {
            get
            {
                return BoundaryElements(frontBackEnd, leftRightEnd);
            }
        }

        public IEnumerable<BoundaryElement<Point3D>> LeftPane
        {
            get
            {
                return BoundaryElements(frontBackEnd + 1, leftRightEnd);
            }
        }

        public IEnumerable<BoundaryElement<Point3D>> FrontPane
        {
            get 
            {
                return BoundaryElements(0, frontBackEnd);
            }
        }

        public IEnumerable<BoundaryElement<Point3D>> BackPane
        {
            get
            {
                return BoundaryElements(1, frontBackEnd);
            }
        }

        private IEnumerable<BoundaryElement<Point3D>> BoundaryElements(int start, int end)
        {
            var elems = new List<BoundaryElement<Point3D>>();
            for (int i = start; i < end; i = i + 2)
            {
                elems.Add(Elements[i]);
            }
            return elems;
        }

        public double AnalyticalSolution(Point3D p)
        {
            double sum = 0;
            double v1 = 2;
            double v2 = 10;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    double l1 = ((2 * i + 1) * Math.PI) / b1;
                    double l2 = ((2 * j + 1) * Math.PI) / c1;
                    double l = Math.Sqrt(l1 * l1 + l2 * l2);
                    double d1 = v1 * Math.Sinh(l * (a1 - p.X1));
                    double d2 = v2 * Math.Sinh(l * p.X1);
                    double d3 = Math.Sin(l1 * p.X2);
                    double d4 = Math.Sin(l2 * p.X3);
                    double d5 = (2 * i + 1) * (2 * j + 1) * Math.Sinh(l * a1);

                    sum += (d1 + d2) * d3 * d4 / d5;
                }
            }
            double d6 = 16 / (Math.PI * Math.PI);
            return sum * d6;
        }

        private void CreateBoundaryElements()
        {
            // C = const FrontBack
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    var start1 = new Point3D(a2 + i * h1, b2 + j * h2, c1);
                    var start2 = new Point3D(a2 + j * h1, b2 + i * h2, c2);
                    Elements.Add(GetElementC(start1, false));
                    Elements.Add(GetElementC(start2, true));
                }
            }

            // A = const RightLeft
            for (int i = 0; i < n2; i++)
            {
                for (int j = 0; j < n3; j++)
                {
                    var start1 = new Point3D(a1, b2 + i * h2, c1 - j * h3);
                    var start2 = new Point3D(a2, b2 + i * h2, c1 - j * h3);
                    Elements.Add(GetElementA(start1, false));
                    Elements.Add(GetElementA(start2, true));
                }
            }

            // B = const BottomTop
            for (int i = 0; i < n3; i++)
            {
                for (int j = 0; j < n1; j++)
                {
                    var start1 = new Point3D(a2 + j * h1, b2, c1 - i * h3);
                    var start2 = new Point3D(a2 + j * h1, b1, c1 - i * h3);
                    Elements.Add(GetElementB(start1, true));
                    Elements.Add(GetElementB(start2, false));
                }
            }
        }

        private BoundaryElement2DFirstOrder GetElementA(Point3D start, bool isInverted)
        {
            var firstPoint3D = start + new Point3D(0, 0, -h3);
            var secondPoint3D = start + new Point3D(0, h2, -h3);
            var thirdPoint3D = start + new Point3D(0, h2, 0);
            var center = start + new Point3D(0, h2 / 2, -h3 / 2);
            var normal = new Point3D(1, 0, 0);
            if (isInverted)
            {
                normal *= -1;
            }
            var element = new BoundaryElement2DFirstOrder(
                start, firstPoint3D, secondPoint3D, thirdPoint3D, center, normal);
            return element;
        }

        private BoundaryElement2DFirstOrder GetElementB(Point3D start, bool isInverted)
        {
            var firstPoint3D = start + new Point3D(h1, 0, 0);
            var secondPoint3D = start + new Point3D(h1, 0, -h3);
            var thirdPoint3D = start + new Point3D(0, 0, -h3);
            var center = start + new Point3D(h1 / 2, 0, -h3 / 2);
            var normal = new Point3D(0, 1, 0);
            if (isInverted)
            {
                normal *= -1;
            }
            var element = new BoundaryElement2DFirstOrder(
                start, firstPoint3D, secondPoint3D, thirdPoint3D, center, normal);
            return element;
        }

        private BoundaryElement2DFirstOrder GetElementC(Point3D start, bool isInverted)
        {
            var firstPoint3D = start + new Point3D(h1, 0, 0);
            var secondPoint3D = start + new Point3D(h1, h2, 0);
            var thirdPoint3D = start + new Point3D(0, h2, 0);
            var center = start + new Point3D(h1 / 2, h2 / 2, 0);
            var normal = new Point3D(0, 0, 1);
            if (isInverted)
            {
                normal *= -1;
            }
            var element = new BoundaryElement2DFirstOrder(
                start, firstPoint3D, secondPoint3D, thirdPoint3D, center, normal);
            return element;
        }

        private void CreateNearBoundaryElements()
        {
            // C = const FrontBack
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    var start1 = new Point3D(a2 + i * h1, b2 + j * h2, c1);
                    var start2 = new Point3D(a2 + j * h1, b2 + i * h2, c2);
                    Elements.Add(NearGetElementC(start1, false));
                    Elements.Add(NearGetElementC(start2, true));
                }
            }

            // A = const  RightLeft
            for (int i = 0; i < n2; i++)
            {
                for (int j = 0; j < n3; j++)
                {
                    var start1 = new Point3D(a1, b2 + i * h2, c1 - j * h3);
                    var start2 = new Point3D(a2, b2 + i * h2, c1 - j * h3);
                    Elements.Add(NearGetElementA(start1, false));
                    Elements.Add(NearGetElementA(start2, true));
                }
            }

            // B = const  BottomTop
            for (int i = 0; i < n3; i++)
            {
                for (int j = 0; j < n1; j++)
                {
                    var start1 = new Point3D(a2 + j * h1, b2, c1 - i * h3);
                    var start2 = new Point3D(a2 + j * h1, b1, c1 - i * h3);
                    Elements.Add(NearGetElementB(start1, true));
                    Elements.Add(NearGetElementB(start2, false));
                }
            }
        }

        private NearBoundaryElement2DFirstOrder NearGetElementA(Point3D start, bool isInverted)
        {
            var vectorNearElementA = new Point3D(hRightLeft, 0, 0);
            if (isInverted)
            {
                vectorNearElementA *= -1;
            }
            var firstPoint3D = start + new Point3D(0, 0, -h3);
            var secondPoint3D = start + new Point3D(0, h2, -h3);
            var thirdPoint3D = start + new Point3D(0, h2, 0);
            var fourthPoint3D = start + vectorNearElementA;
            var fifthPoint3D = firstPoint3D + vectorNearElementA;
            var sixthPoint3D = secondPoint3D + vectorNearElementA;
            var seventhPoint3D = thirdPoint3D + vectorNearElementA;
            var center = start + new Point3D(0, h2 / 2, -h3 / 2);
            var normal = new Point3D(1, 0, 0);
            if (isInverted)
            {
                normal *= -1;
            }
            var element = new NearBoundaryElement2DFirstOrder(
                start,
                firstPoint3D,
                secondPoint3D,
                thirdPoint3D,
                fourthPoint3D,
                fifthPoint3D,
                sixthPoint3D,
                seventhPoint3D,
                center,
                normal);
            return element;
        }

        private NearBoundaryElement2DFirstOrder NearGetElementB(Point3D start, bool isInverted)
        {
            var vectorNearElementB = new Point3D(0, hBottomTop, 0);
            if (isInverted)
            {
                vectorNearElementB *= -1;
            }
            var firstPoint3D = start + new Point3D(h1, 0, 0);
            var secondPoint3D = start + new Point3D(h1, 0, -h3);
            var thirdPoint3D = start + new Point3D(0, 0, -h3);
            var fourthPoint3D = start + vectorNearElementB;
            var fifthPoint3D = firstPoint3D + vectorNearElementB;
            var sixthPoint3D = secondPoint3D + vectorNearElementB;
            var seventhPoint3D = thirdPoint3D + vectorNearElementB;
            var center = start + new Point3D(h1 / 2, 0, -h3 / 2);
            var normal = new Point3D(0, 1, 0);
            if (isInverted)
            {
                normal *= -1;
            }
            var element = new NearBoundaryElement2DFirstOrder(
                start,
                firstPoint3D,
                secondPoint3D,
                thirdPoint3D,
                fourthPoint3D,
                fifthPoint3D,
                sixthPoint3D,
                seventhPoint3D,
                center,
                normal);
            return element;
        }

        private NearBoundaryElement2DFirstOrder NearGetElementC(Point3D start, bool isInverted)
        {
            var vectorNearElementC = new Point3D(0, 0, hFrontBack);
            if (isInverted)
            {
                vectorNearElementC *= -1;
            }
            var firstPoint3D = start + new Point3D(h1, 0, 0);
            var secondPoint3D = start + new Point3D(h1, h2, 0);
            var thirdPoint3D = start + new Point3D(0, h2, 0);
            var fourthPoint3D = start + vectorNearElementC;
            var fifthPoint3D = firstPoint3D + vectorNearElementC;
            var sixthPoint3D = secondPoint3D + vectorNearElementC;
            var seventhPoint3D = thirdPoint3D + vectorNearElementC;
            var center = start + new Point3D(h1 / 2, h2 / 2, 0);
            var normal = new Point3D(0, 0, 1);
            if (isInverted)
            {
                normal *= -1;
            }
            var element = new NearBoundaryElement2DFirstOrder(
                start,
                firstPoint3D,
                secondPoint3D,
                thirdPoint3D,
                fourthPoint3D,
                fifthPoint3D,
                sixthPoint3D,
                seventhPoint3D,
                center,
                normal);
            return element;
        }

        #region Overrides of Bound2D

        public override bool Inside(Point3D x)
        {
            return a1 > x.X1 && a2 < x.X1 && b1 > x.X2 && b2 < x.X2 && c1 > x.X3 && c2 < x.X3;
        }

        public override Point3D BottomLeftCorner
        {
            get { return new Point3D(Math.Min(a1, a2), Math.Min(b1, b2), Math.Min(c1, c2)); }
        }

        public override Point3D TopRightCorner
        {
            get { return new Point3D(Math.Max(a1, a2), Math.Max(b1, b2), Math.Max(c1, c2)); }
        }

        #endregion
    }
}