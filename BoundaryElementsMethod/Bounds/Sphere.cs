using System;
using System.Collections.Generic;
using BEM.BoundaryElements;
using BEM.Common.Points;

namespace BEM.Bounds
{
    public class Sphere : Bound<Point3D>
    {
        private readonly int k, m;

        private readonly double r;

        private readonly Point3D center;

        public Sphere()
            :this(new Point3D())
        {
        }

        public Sphere(Point3D center)
        {
            k = 2;
            m = 4 * k;
            r = 1;
            this.center = center;
            Create();
        }

        private void Create()
        {
            double hp = m / (Math.Pow(k + 1, 2) * 2 + m);
            double deltafi = 2 * Math.PI / (4 * (k + 1)), deltapsi = 2 * Math.Asin((r - hp) / r) / (k + 1);
            var fi = GetFi(deltafi);
            var psi = GetPsi(hp, deltapsi);

            for (int i = 0; i < k + 1; i++)
            {
                for (int j = 0; j < (k + 1) * 4; j++)
                {
                    var points = new List<Point3D>();
                    for (int l = 0; l < 8; l++)
                    {
                        points.Add(GetSpherePoint(fi[l], psi[l]));
                    }
                    var elem = new BoundaryElement2DSecondOrder(points, GetSpherePoint(fi[8], psi[8]));
                    elem.Normal = NormalInPoint(elem.Center);
                    elem.Bound = this;
                    Elements.Add(elem);
                    for (int l = 0; l < 9; l++)
                        fi[l] += deltafi;
                }
                for (int l = 0; l < 9; l++)
                    psi[l] -= deltapsi;
            }

            // Hats on poles
            deltafi = 2 * Math.PI / m;
            deltapsi = Math.PI / 2 - Math.Asin(1 - hp);
            fi = GetFiOnPole(deltafi);
            psi = GetPsiOnPole(deltapsi);

            for (int i = 0; i < m; i++)
            {
                var points = new List<Point3D>();
                for (int l = 0; l < 8; l++)
                {
                    points.Add(GetSpherePoint(fi[l], psi[l]));
                }
                var elem = new BoundaryElement2DSecondOrder(points, GetSpherePoint(fi[8], psi[8]));
                elem.Normal = NormalInPoint(elem.Center);
                elem.Bound = this;
                Elements.Add(elem);

                points = new List<Point3D>();
                for (int l = 0; l < 8; l++)
                {
                    points.Add(GetBottomSpherePoint(fi[l], psi[l]));
                }
                var elem2 = new BoundaryElement2DSecondOrder(points, GetBottomSpherePoint(fi[8], psi[8]));
                elem2.Normal = NormalInPoint(elem.Center);
                elem2.Bound = this;
                Elements.Add(elem2);

                for (int l = 5; l < 9; l++)
                    fi[l] += deltafi;
                fi[2] += deltafi;
                fi[3] += deltafi;
                
            }
        }

        private static double[] GetFi(double deltafi)
        {
            var fi = new double[9];
            fi[0] = 0; 
            fi[1] = fi[0] + deltafi; 
            fi[2] = fi[1]; 
            fi[3] = fi[0]; 
            fi[4] = fi[0] + deltafi / 2;
            fi[5] = fi[1]; 
            fi[6] = fi[4]; 
            fi[7] = fi[0]; 
            fi[8] = fi[4];
            return fi;
        }

        private double[] GetPsi(double hp, double deltapsi)
        {
            var psi = new double[9];
            psi[0] = Math.Asin(1 - hp);
            psi[1] = psi[0];
            psi[2] = psi[0] - deltapsi;
            psi[3] = psi[2];
            psi[4] = psi[0];
            psi[5] = psi[0] - deltapsi / 2;
            psi[6] = psi[2];
            psi[7] = psi[5];
            psi[8] = psi[5];
            return psi;
        }

        private static double[] GetFiOnPole(double deltafi)
        {
            var fi = new double[9];
            fi[0] = 0;
            fi[1] = fi[0];
            fi[2] = 0 + deltafi;
            fi[3] = 0;
            fi[4] = fi[0];
            fi[5] = fi[2];
            fi[6] = fi[3] + deltafi / 2;
            fi[7] = fi[3];
            fi[8] = fi[6];
            return fi;
        }

        private static double[] GetPsiOnPole(double deltapsi)
        {
            var psi = new double[9];
            psi[0] = Math.PI / 2;
            psi[1] = psi[0];
            psi[2] = Math.PI / 2 - deltapsi;
            psi[3] = psi[2];
            psi[4] = psi[0];
            psi[5] = psi[2] + deltapsi / 2;
            psi[6] = psi[2];
            psi[7] = psi[5];
            psi[8] = psi[5];
            return psi;
        }

        private Point3D GetSpherePoint(double fi, double psi)
        {
            var x1 = r * Math.Cos(psi) * Math.Sin(fi) + center.X1;
            var x2 = r * Math.Cos(psi) * Math.Cos(fi) + center.X2;
            var x3 = r * Math.Sin(psi) + center.X3;
            return new Point3D(x1, x2, x3);
        }

        private Point3D GetBottomSpherePoint(double fi, double psi)
        {
            var x1 = r * Math.Cos(psi) * Math.Sin(fi) + center.X1;
            var x2 = r * Math.Cos(psi) * Math.Cos(fi) + center.X2;
            var x3 = -r * Math.Sin(psi) + center.X3;
            return new Point3D(x1, x2, x3);
        }

        private Point3D NormalInPoint(Point3D x)
        {
            var x1 = (x.X1 - center.X1) / r;
            var x2 = (x.X2 - center.X2) / r;
            var x3 = (x.X3 - center.X3) / r;
            return new Point3D(x1, x2, x3);
        }

        #region Overrides of Bound2D

        public override bool Inside(Point3D x)
        {
            return x.Dist(center) <= r;
        }

        public override Point3D BottomLeftCorner
        {
            get
            {
                var x = new Point3D();
                var x1 = x.X1 + r;
                var x2 = x.X2 + r;
                var x3 = x.X3 - r;
                return new Point3D(x1, x2, x3);
            }
        }

        public override Point3D TopRightCorner
        {
            
            get
            {
                var x=new Point3D();
                var x1 = x.X1 - r;
                var x2 = x.X2 - r;
                var x3 = x.X3 + r;
                return new Point3D(x1, x2, x3);
            }
        }

        public override IEnumerable<Point3D> ObservablePoints
        {
            get { yield break; }
        }

        #endregion
    }
}