using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public class AirSpaceTracker : IAirSpaceTracker
    {
        public bool IsInAirSpace(IAirSpace airSpace, ITrack track)
        {
            if (track._alt < airSpace.MinAltitude || track._alt > airSpace.MaxAltitude)
            { 
                return false;
            }

            if (track._xCord < airSpace.XStartPoint || track._xCord > airSpace.GetXEndPoint())
            {
                return false;
            }

            if (track._yCord < airSpace.YStartPoint || track._yCord > airSpace.GetYEndPoint())
            {
                return false;
            }

            return true;
        }
    }
}
