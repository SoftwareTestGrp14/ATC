using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public class AirSpaceTracker : IAirSpaceTracker
    {
        public bool IsInAirSpace(IAirSpace airSpace, ITrack track)
        {
            Console.WriteLine("TEST1TestTest");
            if (track._alt < airSpace.MinAltitude || track._alt > airSpace.MaxAltitude)
            { 
                Console.WriteLine("TEST1");
                return false;
            }

            if (track._xCord < airSpace.XStartPoint || track._xCord > airSpace.GetXEndPoint())
            {
                Console.WriteLine("TEST2");
                return false;
            }

            if (track._yCord < airSpace.YStartPoint || track._yCord > airSpace.GetYEndPoint())
            {
                Console.WriteLine("TEST3");
                return false;
            }
            Console.WriteLine("TEST4");
            return true;
        }
    }
}
