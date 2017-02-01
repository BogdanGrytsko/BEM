using System;

using BEM.Common.Points;

namespace BEM.Bounds
{
    public class Pane : Bound<Point3D>
    {
        public override bool Inside(Point3D x)
        {
            return true;
        }

        public override Point3D BottomLeftCorner
        {
            get { throw new NotImplementedException(); }
        }

        public override Point3D TopRightCorner
        {
            get { throw new NotImplementedException(); }
        }
    }
}
