using System;
using System.Collections.Generic;

using BEM.Common;
using BEM.Common.GaussIntegrator;
using BEM.Common.Points;
using BEM.Methods;

namespace BEM.Factory
{
    public class MethodFactory
    {
        private static readonly List<Func<Point2D, Point2D, double>> Derivates2D = new List<Func<Point2D, Point2D, double>>
            {
                FunctionFactory.Q1,
                FunctionFactory.Q2,
            };

        private static readonly List<Func<Point3D, Point3D, double>> Derivates3D = new List<Func<Point3D, Point3D, double>>
            {
                FunctionFactory.Q1,
                FunctionFactory.Q2,
                FunctionFactory.Q3
            };

        public static AbstractMethod<Point2D> GetCollocationMethod(
            List<BoundWithCondition<Point2D>> bound, List<InnerSourceWithFunction<Point2D>> sources)
        {
            return new CollocationMethod<Point2D>(bound, sources, FunctionFactory.Q, Derivates2D, new Integrator<Point2D>(4));
        }

        public static AbstractMethod<Point2D> GetCollocationPaper4Method(List<BoundWithCondition<Point2D>> bound, double lambda1, double lambda2)
        {
            return new CollocationPaper4Method<Point2D>(
                bound,
                new List<InnerSourceWithFunction<Point2D>>(),
                FunctionFactory.Q,
                Derivates2D,
                new Integrator<Point2D>(4),
                lambda1,
                lambda2);
        }

        public static AbstractMethod<Point2D> GetCollocationPaper6Method(List<BoundWithCondition<Point2D>> bound, double lambda)
        {
            return new CollocationPaper6Method<Point2D>(
                bound,
                new List<InnerSourceWithFunction<Point2D>>(),
                FunctionFactory.Q,
                Derivates2D,
                new Integrator<Point2D>(4),
                lambda);
        }

        public static AbstractMethod<Point2D> GetCollocationPaper7Method(List<BoundWithCondition<Point2D>> bound, double lambda)
        {
            return new CollocationPaper7Method<Point2D>(
                bound,
                new List<InnerSourceWithFunction<Point2D>>(),
                FunctionFactory.Q,
                Derivates2D,
                new Integrator<Point2D>(4),
                lambda);
        }

        public static AbstractMethod<Point2D> GetGalerkinMethod(
            List<BoundWithCondition<Point2D>> bound, List<InnerSourceWithFunction<Point2D>> sources)
        {
            return new GalerkinMethod<Point2D>(bound, sources, FunctionFactory.Q, Derivates2D, new Integrator<Point2D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationMethod(
            List<BoundWithCondition<Point3D>> bound)
        {
            return new CollocationMethod<Point3D>(
                bound,
                new List<InnerSourceWithFunction<Point3D>>(),
                FunctionFactory.Q,
                Derivates3D,
                new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationMethod(
            List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources)
        {
            return new CollocationMethod<Point3D>(bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetGalerkinMethod(
            List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources)
        {
            return new GalerkinMethod<Point3D>(bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationMethodNearBoundary(
    List<BoundWithCondition<Point3D>> bound)
        {
            return new CollocationMethodNearBoundary<Point3D>(
                bound,
                new List<InnerSourceWithFunction<Point3D>>(),
                FunctionFactory.Q,
                Derivates3D,
                new Integrator<Point3D>(4));
        }
        public static AbstractMethod<Point3D> GetCollocationMethodNearBoundary(

            List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources)
        {
            return new CollocationMethodNearBoundary<Point3D>(bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationKirghoffMethod(
            List<BoundWithCondition<Point3D>> bound,
            List<InnerSourceWithFunction<Point3D>> sources,
            Func<double, double> transformation,
            Func<double, double> converseTransformation)
        {
            return new CollocationKirghoffMethod<Point3D>(
                bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4), transformation, converseTransformation);
        }

        public static AbstractMethod<Point3D> GetJakobiMethod(
            List<BoundWithCondition<Point3D>> bound,
            List<InnerSourceWithFunction<Point3D>> sources,
           Vector solution)
        {
            return new JakobiNewtonMethod<Point3D>(
                bound,
                sources,
                FunctionFactory.Q,
                Derivates3D,
                new Integrator<Point3D>(4),
                solution);
        }

        public static AbstractMethod<Point3D> GetSphereMethod(List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources, SemiSpaceParameters useCase)
        {
            return new TwoSphereMethod(
                bound,
                sources,
                new Integrator<Point3D>(4),
                useCase);
        }

    }

    public class CopyOfMethodFactory
    {
        private static readonly List<Func<Point2D, Point2D, double>> Derivates2D = new List<Func<Point2D, Point2D, double>>
            {
                FunctionFactory.Q1,
                FunctionFactory.Q2,
            };

        private static readonly List<Func<Point3D, Point3D, double>> Derivates3D = new List<Func<Point3D, Point3D, double>>
            {
                FunctionFactory.Q1,
                FunctionFactory.Q2,
                FunctionFactory.Q3
            };

        public static AbstractMethod<Point2D> GetCollocationMethod(
            List<BoundWithCondition<Point2D>> bound, List<InnerSourceWithFunction<Point2D>> sources)
        {
            return new CollocationMethod<Point2D>(bound, sources, FunctionFactory.Q, Derivates2D, new Integrator<Point2D>(4));
        }

        public static AbstractMethod<Point2D> GetCollocationPaper4Method(List<BoundWithCondition<Point2D>> bound, double lambda1, double lambda2)
        {
            return new CollocationPaper4Method<Point2D>(
                bound,
                new List<InnerSourceWithFunction<Point2D>>(),
                FunctionFactory.Q,
                Derivates2D,
                new Integrator<Point2D>(4),
                lambda1,
                lambda2);
        }

        public static AbstractMethod<Point2D> GetCollocationPaper6Method(List<BoundWithCondition<Point2D>> bound, double lambda)
        {
            return new CollocationPaper6Method<Point2D>(
                bound,
                new List<InnerSourceWithFunction<Point2D>>(),
                FunctionFactory.Q,
                Derivates2D,
                new Integrator<Point2D>(4),
                lambda);
        }

        public static AbstractMethod<Point2D> GetCollocationPaper7Method(List<BoundWithCondition<Point2D>> bound, double lambda)
        {
            return new CollocationPaper7Method<Point2D>(
                bound,
                new List<InnerSourceWithFunction<Point2D>>(),
                FunctionFactory.Q,
                Derivates2D,
                new Integrator<Point2D>(4),
                lambda);
        }

        public static AbstractMethod<Point2D> GetGalerkinMethod(
            List<BoundWithCondition<Point2D>> bound, List<InnerSourceWithFunction<Point2D>> sources)
        {
            return new GalerkinMethod<Point2D>(bound, sources, FunctionFactory.Q, Derivates2D, new Integrator<Point2D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationMethod(
            List<BoundWithCondition<Point3D>> bound)
        {
            return new CollocationMethod<Point3D>(
                bound,
                new List<InnerSourceWithFunction<Point3D>>(),
                FunctionFactory.Q,
                Derivates3D,
                new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationMethod(
            List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources)
        {
            return new CollocationMethod<Point3D>(bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetGalerkinMethod(
            List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources)
        {
            return new GalerkinMethod<Point3D>(bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationMethodNearBoundary(
    List<BoundWithCondition<Point3D>> bound)
        {
            return new CollocationMethodNearBoundary<Point3D>(
                bound,
                new List<InnerSourceWithFunction<Point3D>>(),
                FunctionFactory.Q,
                Derivates3D,
                new Integrator<Point3D>(4));
        }
        public static AbstractMethod<Point3D> GetCollocationMethodNearBoundary(

            List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources)
        {
            return new CollocationMethodNearBoundary<Point3D>(bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4));
        }

        public static AbstractMethod<Point3D> GetCollocationKirghoffMethod(
            List<BoundWithCondition<Point3D>> bound,
            List<InnerSourceWithFunction<Point3D>> sources,
            Func<double, double> transformation,
            Func<double, double> converseTransformation)
        {
            return new CollocationKirghoffMethod<Point3D>(
                bound, sources, FunctionFactory.Q, Derivates3D, new Integrator<Point3D>(4), transformation, converseTransformation);
        }

        public static AbstractMethod<Point3D> GetJakobiMethod(
            List<BoundWithCondition<Point3D>> bound,
            List<InnerSourceWithFunction<Point3D>> sources,
           Vector solution)
        {
            return new JakobiNewtonMethod<Point3D>(
                bound,
                sources,
                FunctionFactory.Q,
                Derivates3D,
                new Integrator<Point3D>(4),
                solution);
        }

        public static AbstractMethod<Point3D> GetSphereMethod(List<BoundWithCondition<Point3D>> bound, List<InnerSourceWithFunction<Point3D>> sources, SemiSpaceParameters useCase)
        {
            return new TwoSphereMethod(
                bound,
                sources,
                new Integrator<Point3D>(4),
                useCase);
        }

    }
}
