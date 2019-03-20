using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public interface IPlaneTracker
    {

        List<ITrack> _tracks { get; }
        IAirSpaceTracker _airSpaceTracker { get; }
        IAirSpace _airSpace { get; }
        List<ISeparationCondition> _currentSeparations { get; }
        ConsoleLog _cLog { get; }
        FileLog _fLog { get; }
        void Update(string data);

        string[] ConvertTransponderData(string data);
    }
}
