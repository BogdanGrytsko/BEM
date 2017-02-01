using System;
using System.Collections.Generic;
using System.Linq;

using BEM.BoundaryElements;
using BEM.Common.Points;

namespace BEM.Common.GaussIntegrator
{
    public class Integrator<T> 
        where T : IPoint
    {
        private readonly int n;

        public Integrator(int n)
        {
            this.n = n;
        } 

        public double Integrate(BoundaryElement<T> elem, T eta, Func<T, T, double> f, bool etaOnElement = false)
        {
            if (etaOnElement && eta is Point2D)
            {
                // TODO : works properly only for f == Ln(|x-y|). Improve for derivates also.
                // TODO : this is harcode. It uses fact that eta = element.Center, and hence eta=(fi1(0), fi2(0)), where fi1, fi2 - interpolation functions.
                var t = 0;
                var etaY = elem.Yakobian(new Point1D(t));
                var exactIntegralPart = GetExactPart(t);
                return elem.GetIntegrationPoints(n).Sum(p => f(eta, p.Point) * (p.Jacobian - etaY))
                       - etaY * exactIntegralPart;
            }
            return elem.GetIntegrationPoints(n).Sum(p => p.Weight * p.Jacobian * f(eta, p.Point));
        }

        private static double GetExactPart(double t)
        {
            const double a = -1, b = 1;
            return LogIntegral(b - t) + LogIntegral(t - a);
        }

        private static double LogIntegral(double t)
        {
            return t * (Math.Log(t) - 1);
        }

        public double Integrate(BoundaryElement<T> elem1, BoundaryElement<T> elem2, Func<T, T, double> f)
        {
            return
                elem1.GetIntegrationPoints(n).Sum(
                    p1 =>
                    elem2.GetIntegrationPoints(n + 2).Sum(
                        p2 => p1.Weight * p2.Weight * p1.Jacobian * p2.Jacobian * f(p1.Point, p2.Point)));
        }

        public double Integrate(BoundaryElement<T> elem, Func<T, double> f)
        {
            return elem.GetIntegrationPoints(n).Sum(p => p.Weight * p.Jacobian * f(p.Point));
        }

        public double Integrate(BoundaryElement<T> elem, T eta, Func<T, double> f, Func<T, T, double> fundamental)
        {
            return elem.GetIntegrationPoints(n).Sum(p => p.Weight * p.Jacobian * f(p.Point) * fundamental(eta, p.Point));
        }

        private IPoint Integrate(BoundaryElement<T> elem, T eta, List<Func<T, T, double>> derivateFunctions)
        {
            var derivates =
                derivateFunctions.Select(derivateFunction => Integrate(elem, eta, derivateFunction)).ToList();
            return CreatePoint(derivates);
        }

        public double IntegratedQdnx(
            BoundaryElement<T> integratedElement, BoundaryElement<T> elem2, List<Func<T, T, double>> derivateFunctions)
        {
            var derivate = Integrate(integratedElement, elem2.Center, derivateFunctions);
            return derivate.ScalarMultiply(elem2.Normal);
        }

        public double IntegratedQdny(
            BoundaryElement<T> integratedElement,
            BoundaryElement<T> elem2,
            List<Func<T, T, double>> derivateFunctions)
        {
            return IntegratedQdny(integratedElement, elem2.Center, derivateFunctions);
        }

        public double IntegratedQdny(
            BoundaryElement<T> integratedElement,
            T x,
            List<Func<T, T, double>> derivateFunctions)
        {
            var derivate = Integrate(integratedElement, x, derivateFunctions);
            return -derivate.ScalarMultiply(integratedElement.Normal);
        }

        public double IntegratedQdnxdny(
            BoundaryElement<T> integratedElement,
            BoundaryElement<T> elem2,
            List<Func<T, T, double>> derivateFunctions)
        {
            var derivStart = derivateFunctions.Select(df => df(elem2.Center, integratedElement.Points[0])).ToList();
            var derivEnd = derivateFunctions.Select(df => df(elem2.Center, integratedElement.Points[1])).ToList();
            var tangentialX = GetTangential(elem2.Normal as Point2D);
            var upper = CreatePoint(derivEnd).ScalarMultiply(tangentialX);
            var lower = CreatePoint(derivStart).ScalarMultiply(tangentialX);
            return upper - lower;
        }

        private static Point2D GetTangential(Point2D normal)
        {
            return new Point2D(normal.X2, -normal.X1);
        }

        private static IPoint CreatePoint(IList<double> values)
        {
            if (values.Count == 2)
            {
                return new Point2D(values[0], values[1]);
            }
            if (values.Count == 3)
            {
                return new Point3D(values[0], values[1], values[2]);
            }
            throw new Exception("Wrong value count");
        }
    }

    public class CopyOfIntegrator<T>
           where T : IPoint
    {
        private readonly int n;

        public CopyOfIntegrator(int n)
        {
            this.n = n;
        }

        public double Integrate(BoundaryElement<T> elem, T eta, Func<T, T, double> f, bool etaOnElement = false)
        {
            if (etaOnElement && eta is Point2D)
            {
                // TODO : works properly only for f == Ln(|x-y|). Improve for derivates also.
                // TODO : this is harcode. It uses fact that eta = element.Center, and hence eta=(fi1(0), fi2(0)), where fi1, fi2 - interpolation functions.
                var t = 0;
                var etaY = elem.Yakobian(new Point1D(t));
                var exactIntegralPart = GetExactPart(t);
                return elem.GetIntegrationPoints(n).Sum(p => f(eta, p.Point) * (p.Jacobian - etaY))
                       - etaY * exactIntegralPart;
            }
            return elem.GetIntegrationPoints(n).Sum(p => p.Weight * p.Jacobian * f(eta, p.Point));
        }

        private static double GetExactPart(double t)
        {
            const double a = -1, b = 1;
            return LogIntegral(b - t) + LogIntegral(t - a);
        }

        private static double LogIntegral(double t)
        {
            return t * (Math.Log(t) - 1);
        }

        public double Integrate(BoundaryElement<T> elem1, BoundaryElement<T> elem2, Func<T, T, double> f)
        {
            return
                elem1.GetIntegrationPoints(n).Sum(
                    p1 =>
                    elem2.GetIntegrationPoints(n + 2).Sum(
                        p2 => p1.Weight * p2.Weight * p1.Jacobian * p2.Jacobian * f(p1.Point, p2.Point)));
        }

        public double Integrate(BoundaryElement<T> elem, Func<T, double> f)
        {
            return elem.GetIntegrationPoints(n).Sum(p => p.Weight * p.Jacobian * f(p.Point));
        }

        public double Integrate(BoundaryElement<T> elem, T eta, Func<T, double> f, Func<T, T, double> fundamental)
        {
            return elem.GetIntegrationPoints(n).Sum(p => p.Weight * p.Jacobian * f(p.Point) * fundamental(eta, p.Point));
        }

        private IPoint Integrate(BoundaryElement<T> elem, T eta, List<Func<T, T, double>> derivateFunctions)
        {
            var derivates =
                derivateFunctions.Select(derivateFunction => Integrate(elem, eta, derivateFunction)).ToList();
            return CreatePoint(derivates);
        }

        public double IntegratedQdnx(
            BoundaryElement<T> integratedElement, BoundaryElement<T> elem2, List<Func<T, T, double>> derivateFunctions)
        {
            var derivate = Integrate(integratedElement, elem2.Center, derivateFunctions);
            return derivate.ScalarMultiply(elem2.Normal);
        }

        public double IntegratedQdny(
            BoundaryElement<T> integratedElement,
            BoundaryElement<T> elem2,
            List<Func<T, T, double>> derivateFunctions)
        {
            return IntegratedQdny(integratedElement, elem2.Center, derivateFunctions);
        }

        public double IntegratedQdny(
            BoundaryElement<T> integratedElement,
            T x,
            List<Func<T, T, double>> derivateFunctions)
        {
            var derivate = Integrate(integratedElement, x, derivateFunctions);
            return -derivate.ScalarMultiply(integratedElement.Normal);
        }

        public double IntegratedQdnxdny(
            BoundaryElement<T> integratedElement,
            BoundaryElement<T> elem2,
            List<Func<T, T, double>> derivateFunctions)
        {
            var derivStart = derivateFunctions.Select(df => df(elem2.Center, integratedElement.Points[0])).ToList();
            var derivEnd = derivateFunctions.Select(df => df(elem2.Center, integratedElement.Points[1])).ToList();
            var tangentialX = GetTangential(elem2.Normal as Point2D);
            var upper = CreatePoint(derivEnd).ScalarMultiply(tangentialX);
            var lower = CreatePoint(derivStart).ScalarMultiply(tangentialX);
            return upper - lower;
        }

        private static Point2D GetTangential(Point2D normal)
        {
            return new Point2D(normal.X2, -normal.X1);
        }

        private static IPoint CreatePoint(IList<double> values)
        {
            if (values.Count == 2)
            {
                return new Point2D(values[0], values[1]);
            }
            if (values.Count == 3)
            {
                return new Point3D(values[0], values[1], values[2]);
            }
            throw new Exception("Wrong value count");
        }
    }
}
