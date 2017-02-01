using System;

using BEM.Bounds;
using BEM.Common.Points;
using BEM.Factory;
using BEM.Papers;

namespace BEM.Plotter
{
    public class PlotterFactory
    {
        public static AbstractPlotter<Point2D> GetPlotter(
            Bound<Point2D> bound, Func<Point2D, double> solution, string fileName, int n)
        {
            return new Plotter2D(bound, solution, fileName, n);
        }

        public static AbstractPlotter<Point2D> GetPlotter(
            Bound<Point2D> bound, Func<Point2D, double> solution, int n)
        {
            return new Plotter2D(bound, solution, n);
        }

        public static AbstractPlotter<Point3D> GetPlotter(
            Parallelepiped bound, Func<Point3D, double> solution)
        {
            return new PlotterParallelepiped(bound, solution);
        }

        public static PlotterTwoSphere GetPlotter(Func<Point3D, double> solution, SemiSpaceParameters parameters)
        {
            return new PlotterTwoSphere(solution, parameters);
        }

        public static AbstractPlotter<Point3D> GetPlotter(
            Sphere bound, Func<Point3D, double> solution)
        {
            return new PlotterOneSphere(bound, solution);
        }

    }

    public class CopyOfPlotterFactory
    {
        public static AbstractPlotter<Point2D> GetPlotter(
            Bound<Point2D> bound, Func<Point2D, double> solution, string fileName, int n)
        {
            return new Plotter2D(bound, solution, fileName, n);
        }

        public static AbstractPlotter<Point2D> GetPlotter(
            Bound<Point2D> bound, Func<Point2D, double> solution, int n)
        {
            return new Plotter2D(bound, solution, n);
        }

        public static AbstractPlotter<Point3D> GetPlotter(
            Parallelepiped bound, Func<Point3D, double> solution)
        {
            return new PlotterParallelepiped(bound, solution);
        }

        public static PlotterTwoSphere GetPlotter(Func<Point3D, double> solution, SemiSpaceParameters parameters)
        {
            return new PlotterTwoSphere(solution, parameters);
        }

        public static AbstractPlotter<Point3D> GetPlotter(
            Sphere bound, Func<Point3D, double> solution)
        {
            return new PlotterOneSphere(bound, solution);
        }

    }
}