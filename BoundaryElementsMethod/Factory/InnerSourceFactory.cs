using System.Collections.Generic;

using BEM.Common;
using BEM.Common.Points;
using BEM.InnerSource;

namespace BEM.Factory
{
    public class InnerSourceFactory
    {
        public static List<InnerSourceWithFunction<Point3D>> GetSourcesPlate(InnerSource<Point3D> source)
        {
            return new List<InnerSourceWithFunction<Point3D>>
                {
                    new InnerSourceWithFunction<Point3D>(source, InnerSourcePlate.SourceFunction)
                };
        }

        public static List<InnerSourceWithFunction<Point3D>> GetSourcesParalelepiped(InnerSource<Point3D> source)
        {
            return new List<InnerSourceWithFunction<Point3D>>
                   {
                       new InnerSourceWithFunction<Point3D>(
                           source,
                           InnerSourceParallelepiped.SourceFunction)
                   };
        }
    }

    public class CopyOfInnerSourceFactory
    {
        public static List<InnerSourceWithFunction<Point3D>> GetSourcesPlate(InnerSource<Point3D> source)
        {
            return new List<InnerSourceWithFunction<Point3D>>
                {
                    new InnerSourceWithFunction<Point3D>(source, InnerSourcePlate.SourceFunction)
                };
        }

        public static List<InnerSourceWithFunction<Point3D>> GetSourcesParalelepiped(InnerSource<Point3D> source)
        {
            return new List<InnerSourceWithFunction<Point3D>>
                   {
                       new InnerSourceWithFunction<Point3D>(
                           source,
                           InnerSourceParallelepiped.SourceFunction)
                   };
        }
    }
}
