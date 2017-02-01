using System;

using BEM.Bounds;
using BEM.Common.Points;

namespace BEM.InnerSource
{
    public class InnerSource<T> : Bound<T> where T : IPoint
    {
        public override bool Inside(T x)
        {
            throw new NotImplementedException();
        }

        public override T BottomLeftCorner
        {
            get { throw new NotImplementedException(); }
        }

        public override T TopRightCorner
        {
            get { throw new NotImplementedException(); }
        }
    }
}
