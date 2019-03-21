using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public interface IAirSpaceTracker
    {
        bool IsInAirSpace(ITrack track);
    }
}
