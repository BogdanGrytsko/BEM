using BEM.BoundaryElements;
using BEM.Common.Points;

namespace BEM.Bounds
{
    public class ParallelepipedNearBound : Parallelepiped
    {
        private double hRightLeft, hFrontBack, hBottomTop;


        public ParallelepipedNearBound(double a1, double a2, double b1, double b2, double c1, double c2, int n1, int n2, int n3)
            : base(a1, a2, b1, b2, c1, c2, n1, n2, n3, false)
        {
            CreateElements();
        }

        public ParallelepipedNearBound()
            : this(1, -1, 1, -1, 1, -1, 2, 2, 2)
        {
        }

        private void CreateElements()
        {

            hRightLeft = 0.01;
            hFrontBack = 0.01;
            hBottomTop = 0.01;

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

    }
}