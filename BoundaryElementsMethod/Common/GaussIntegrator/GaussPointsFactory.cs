using System.Collections.Generic;
using System.Linq;

using BEM.Common.Points;

namespace BEM.Common.GaussIntegrator
{
    public class GaussPointsFactory
    {
        private static readonly Dictionary<int, List<GaussPoint<Point1D>>> PointsCache =
            new Dictionary<int, List<GaussPoint<Point1D>>>();

        private static readonly Dictionary<int, List<GaussPoint<Point2D>>> Points2DCache =
            new Dictionary<int, List<GaussPoint<Point2D>>>();

        private static readonly Dictionary<int, List<GaussPoint<Point3D>>> Points3DCache =
            new Dictionary<int, List<GaussPoint<Point3D>>>();

        public static List<GaussPoint<Point1D>> GetPoints(int n)
        {
            if (!PointsCache.ContainsKey(n))
            {
                PointsCache.Add(n, CreatePoints(n));
            }
            return PointsCache[n];
        }

        public static List<GaussPoint<Point2D>> GetPoints2D(int n)
        {
            if (!Points2DCache.ContainsKey(n))
            {
                Points2DCache.Add(n, CreatePoints2D(n));
            }
            return Points2DCache[n];
        }

        public static List<GaussPoint<Point3D>> GetPoints3D(int n)
        {
            if (!Points3DCache.ContainsKey(n))
            {
                Points3DCache.Add(n, CreatePoints3D(n));
            }
            return Points3DCache[n];
        }

        private static List<GaussPoint<Point3D>> CreatePoints3D(int i)
        {
            var points = GetPoints(i);
            return (from point1 in points
                from point2 in points
                from point3 in points
                select
                    new GaussPoint<Point3D>(
                        new Point3D(point1.Point.X1, point2.Point.X1, point3.Point.X1),
                        point1.Weight * point2.Weight * point3.Weight)).ToList();
        } 

        private static List<GaussPoint<Point1D>> CreatePoints(int n)
        {
            var gaussPoints = new List<GaussPoint<Point1D>>();
            switch (n)
            {
                case 2:
                {
                    gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.5773502691), 1));
                    gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.5773502691), 1));
                        break;
                    }
                case 4:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.3399810435), 0.3478548451));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.8611363115), 0.6521451548));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.3399810435), 0.3478548451));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.8611363115), 0.6521451548));
                        break;
                    }
                case 6:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.2386191860), 0.1713244923));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.6612093864), 0.3607615730));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9324695142), 0.4679139345));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.2386191860), 0.1713244923));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.6612093864), 0.3607615730));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.9324695142), 0.4679139345));
                        break;
                    }
                case 8:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.1834346424), 0.3626837833));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.5255324099), 0.3137066458));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.7966664774), 0.2223810344));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9602898564), 0.1012285362));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.1834346424), 0.3626837833));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.5255324099), 0.3137066458));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.7966664774), 0.2223810344));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.9602898564), 0.1012285362));
                        break;
                    }
                case 10:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.1488743389), 0.2955242247));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.4333953941), 0.2692667193));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.6794095682), 0.2190863625));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.8650633666), 0.1494513491));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9739065285), 0.0666713443));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.1488743389), 0.2955242247));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.4333953941), 0.2692667193));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.6794095682), 0.2190863625));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.8650633666), 0.1494513491));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.9739065285), 0.0666713443));
                        break;
                    }
                case 12:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.1252334085), 0.2491470458));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.3678314989), 0.2334925365));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.5873179542), 0.2031674267));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.7699026741), 0.1600783285));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9041172563), 0.1069393259));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9815606342), 0.0471753363));
                        break;
                    }
            }
            return gaussPoints;
        }

        private static List<GaussPoint<Point2D>> CreatePoints2D(int n)
        {
            var points = GetPoints(n);
            return (from point1 in points
                from point2 in points
                select
                    new GaussPoint<Point2D>(
                        new Point2D(point1.Point.X1, point2.Point.X1),
                        point1.Weight * point2.Weight)).ToList();
        } 
    }

    public class CopyOfGaussPointsFactory
    {
        private static readonly Dictionary<int, List<GaussPoint<Point1D>>> PointsCache =
            new Dictionary<int, List<GaussPoint<Point1D>>>();

        private static readonly Dictionary<int, List<GaussPoint<Point2D>>> Points2DCache =
            new Dictionary<int, List<GaussPoint<Point2D>>>();

        private static readonly Dictionary<int, List<GaussPoint<Point3D>>> Points3DCache =
            new Dictionary<int, List<GaussPoint<Point3D>>>();

        public static List<GaussPoint<Point1D>> GetPoints(int n)
        {
            if (!PointsCache.ContainsKey(n))
            {
                PointsCache.Add(n, CreatePoints(n));
            }
            return PointsCache[n];
        }

        public static List<GaussPoint<Point2D>> GetPoints2D(int n)
        {
            if (!Points2DCache.ContainsKey(n))
            {
                Points2DCache.Add(n, CreatePoints2D(n));
            }
            return Points2DCache[n];
        }

        public static List<GaussPoint<Point3D>> GetPoints3D(int n)
        {
            if (!Points3DCache.ContainsKey(n))
            {
                Points3DCache.Add(n, CreatePoints3D(n));
            }
            return Points3DCache[n];
        }

        private static List<GaussPoint<Point3D>> CreatePoints3D(int i)
        {
            var points = GetPoints(i);
            return (from point1 in points
                    from point2 in points
                    from point3 in points
                    select
                        new GaussPoint<Point3D>(
                            new Point3D(point1.Point.X1, point2.Point.X1, point3.Point.X1),
                            point1.Weight * point2.Weight * point3.Weight)).ToList();
        }

        private static List<GaussPoint<Point1D>> CreatePoints(int n)
        {
            var gaussPoints = new List<GaussPoint<Point1D>>();
            switch (n)
            {
                case 2:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.5773502691), 1));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.5773502691), 1));
                        break;
                    }
                case 4:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.3399810435), 0.3478548451));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.8611363115), 0.6521451548));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.3399810435), 0.3478548451));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.8611363115), 0.6521451548));
                        break;
                    }
                case 6:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.2386191860), 0.1713244923));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.6612093864), 0.3607615730));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9324695142), 0.4679139345));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.2386191860), 0.1713244923));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.6612093864), 0.3607615730));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.9324695142), 0.4679139345));
                        break;
                    }
                case 8:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.1834346424), 0.3626837833));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.5255324099), 0.3137066458));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.7966664774), 0.2223810344));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9602898564), 0.1012285362));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.1834346424), 0.3626837833));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.5255324099), 0.3137066458));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.7966664774), 0.2223810344));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.9602898564), 0.1012285362));
                        break;
                    }
                case 10:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.1488743389), 0.2955242247));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.4333953941), 0.2692667193));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.6794095682), 0.2190863625));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.8650633666), 0.1494513491));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9739065285), 0.0666713443));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.1488743389), 0.2955242247));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.4333953941), 0.2692667193));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.6794095682), 0.2190863625));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.8650633666), 0.1494513491));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(0.9739065285), 0.0666713443));
                        break;
                    }
                case 12:
                    {
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.1252334085), 0.2491470458));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.3678314989), 0.2334925365));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.5873179542), 0.2031674267));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.7699026741), 0.1600783285));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9041172563), 0.1069393259));
                        gaussPoints.Add(new GaussPoint<Point1D>(new Point1D(-0.9815606342), 0.0471753363));
                        break;
                    }
            }
            return gaussPoints;
        }

        private static List<GaussPoint<Point2D>> CreatePoints2D(int n)
        {
            var points = GetPoints(n);
            return (from point1 in points
                    from point2 in points
                    select
                        new GaussPoint<Point2D>(
                            new Point2D(point1.Point.X1, point2.Point.X1),
                            point1.Weight * point2.Weight)).ToList();
        }
    }
}