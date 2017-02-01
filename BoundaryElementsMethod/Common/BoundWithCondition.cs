using System;
using System.Collections;
using System.Collections.Generic;

using BEM.BoundaryElements;
using BEM.Bounds;
using BEM.Common.Points;

namespace BEM.Common
{
    public class BoundWithCondition<T> : IEnumerable<BoundaryElement<T>>
        where T : IPoint 
    {
        public BoundWithCondition(Bound<T> bound, ConditionType conditionType, Func<T, double> function)
        {
            Bound = bound;
            ConditionType = conditionType;
            Function = function;
        }

        public Bound<T> Bound { get; private set; }

        public ConditionType ConditionType { get; private set; }

        public Func<T, double> Function { get; private set; }

        public IEnumerator<BoundaryElement<T>> GetEnumerator()
        {
            foreach (var boundaryElement in Bound.Elements)
            {
                yield return boundaryElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Copy1OfBoundWithCondition<T> : IEnumerable<BoundaryElement<T>>
           where T : IPoint
    {
        public Copy1OfBoundWithCondition(Bound<T> bound, ConditionType conditionType, Func<T, double> function)
        {
            Bound = bound;
            ConditionType = conditionType;
            Function = function;
        }

        public Bound<T> Bound { get; private set; }

        public ConditionType ConditionType { get; private set; }

        public Func<T, double> Function { get; private set; }

        public IEnumerator<BoundaryElement<T>> GetEnumerator()
        {
            foreach (var boundaryElement in Bound.Elements)
            {
                yield return boundaryElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CopyOfBoundWithCondition<T> : IEnumerable<BoundaryElement<T>>
           where T : IPoint
    {
        public CopyOfBoundWithCondition(Bound<T> bound, ConditionType conditionType, Func<T, double> function)
        {
            Bound = bound;
            ConditionType = conditionType;
            Function = function;
        }

        public Bound<T> Bound { get; private set; }

        public ConditionType ConditionType { get; private set; }

        public Func<T, double> Function { get; private set; }

        public IEnumerator<BoundaryElement<T>> GetEnumerator()
        {
            foreach (var boundaryElement in Bound.Elements)
            {
                yield return boundaryElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
