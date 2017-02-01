using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using BEM.BoundaryElements;
using BEM.Bounds;
using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;

namespace BEM.Methods
{
    public abstract class AbstractMethod<T> where T : IPoint
    {
        protected List<BoundWithCondition<T>> BoundWithConditions { get; private set; }

        protected List<InnerSourceWithFunction<T>> InnerSources { get; private set; }

        protected static Integrator<T> Integrator { get; private set; }

        protected static Func<T, T, double> FundamentalSolution { get; private set; }

        protected static List<Func<T, T, double>> Derivates { get; private set; }

        public Vector Solution { get; set; }

        protected AbstractMethod(
            List<BoundWithCondition<T>> bounds,
            List<InnerSourceWithFunction<T>> sources,
            Func<T, T, double> fundamentalSolution,
            List<Func<T, T, double>> derivates,
            Integrator<T> integrator)
        {
            BoundWithConditions = bounds;
            InnerSources = sources;
            FundamentalSolution = fundamentalSolution;
            Derivates = derivates;
            Integrator = integrator;
        }

        public void Solve()
        {
            var sw = new Stopwatch();
            sw.Start();
            var matrix = CreateMatrix();
            Console.WriteLine("Matrix Created " + sw.ElapsedMilliseconds);
            Writer.OutputIfAllowed(matrix, "Matrix.txt");
            sw.Reset();
            sw.Start();
            var vector = CreateVector();
            Console.WriteLine("Vector Created " + sw.ElapsedMilliseconds);
            Writer.OutputIfAllowed(vector, "GVector.txt");
            sw.Reset();
            sw.Start();
            Solution = matrix.Solve(vector);
            Console.WriteLine("Solution Found " + sw.ElapsedMilliseconds);
            Writer.OutputIfAllowed(Solution, "solution.txt");
        }

        private Matrix CreateMatrix()
        {
            var matrix = new Matrix();
            foreach (var bound1 in BoundWithConditions)
            {
                foreach (var elem1 in bound1.Bound.Elements)
                {
                    var vector = new Vector();
                    foreach (var bound2 in BoundWithConditions)
                    {
                        foreach (var elem2 in bound2.Bound.Elements)
                        {
                            vector.Add(CreateMatrixElement(elem1, elem2, bound1.ConditionType));
                        }
                    }
                    matrix.Add(vector);
                }
            }
            return matrix;
        }

        protected abstract double CreateMatrixElement(BoundaryElement<T> elem1, BoundaryElement<T> elem2,
                                                      ConditionType conditionType);

        private Vector CreateVector()
        {
            var vector = new Vector();
            foreach (var bound1 in BoundWithConditions)
            {
                foreach (var elem1 in bound1.Bound.Elements)
                {
                    vector.Add(CreateVectorElement(elem1, bound1.Function, bound1.ConditionType));
                }
            }
            return vector;
        }

        protected virtual double CreateVectorElement(BoundaryElement<T> elem1, Func<T, double> function, ConditionType conditionType)
        {
            return function(elem1.Center) - InnerSourceImplact(elem1.Center);
        }

        public void PlotErrorOnBound()
        {
            Writer.Output(GetErrorOnBound(), "Error.txt");
        }

        private string GetErrorOnBound()
        {
            var sb = new StringBuilder();
            foreach (var bound in BoundWithConditions)
            {
                foreach (var point in bound.Bound.ObservablePoints)
                {
                    var u = U(point);
                    var info = string.Format(
                        "{0} {1:0.000000} {2:0.000000}", point, u, Math.Abs(BoundWithConditions[0].Function(point) - u));
                    sb.AppendLine(info);
                }
            }
            return sb.ToString();
        }

        public virtual double U(T x)
        {
            return U(x, Solution);
        }

        public virtual double Q(T x)
        {
            return Q(x, Solution);
        }

        public double U(T x, Vector solution)
        {
            return CalculateSolutiuon(
                solution,
                (bound, element) => Integrator.Integrate(element, x, FundamentalSolution)) + InnerSourceImplact(x);
        }

        private double InnerSourceImplact(T x)
        {
            double res = 0;
            foreach (var innerSource in InnerSources)
            {
                foreach (var elem in innerSource.Bound.Elements)
                {
                    res += Integrator.Integrate(elem, x, innerSource.Function, FundamentalSolution);
                }
            }
            return res;
        }

        protected double CalculateSolutiuon(Vector solution, Func<Bound<T>, BoundaryElement<T>, double> funcThatCreates)
        {
            var integrals = new Vector();
            foreach (BoundWithCondition<T> bound in BoundWithConditions)
            {
                foreach (BoundaryElement<T> element in bound)
                {
                    integrals.Add(funcThatCreates(bound.Bound, element));
                }
            }
            return integrals.ScalarMultiply(solution);
        }

        protected static int Kroneker(BoundaryElement<T> elem1, BoundaryElement<T> elem2)
        {
            return elem1 == elem2 ? 1 : 0;
        }

        public double Q(T x, Vector solution)
        {
            return CalculateSolutiuon(
                solution,
                (bound, element) => Integrator.IntegratedQdny(element, x, Derivates));
        }
    }
}