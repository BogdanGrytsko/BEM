using System.Collections.Generic;

namespace BEM.Common.Points
{
    public interface IPoint
    {
        double Dist(IPoint other);

        double ScalarMultiply(IPoint other);
    }

    public interface CopyOfIPoint
    {
        double Dist(CopyOfIPoint other);

        double ScalarMultiply(CopyOfIPoint other);
    }
}