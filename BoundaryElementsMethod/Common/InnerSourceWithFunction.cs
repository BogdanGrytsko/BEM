using System;

using BEM.Common.Points;
using BEM.InnerSource;

namespace BEM.Common
{
    public class InnerSourceWithFunction<T> where T : IPoint
    {
        public InnerSourceWithFunction(InnerSource<T> bound, Func<T, double> function)
        {
            Bound = bound;
            Function = function;
        }

        public InnerSource<T> Bound { get; private set; }

        public Func<T, double> Function { get; private set; }
    }

    public class CopyOfInnerSourceWithFunction<T> where T : IPoint
    {
        public CopyOfInnerSourceWithFunction(InnerSource<T> bound, Func<T, double> function)
        {
            Bound = bound;
            Function = function;
        }

        public InnerSource<T> Bound { get; private set; }

        public Func<T, double> Function { get; private set; }
    }
}
