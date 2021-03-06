﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public interface IPlaneTracker
    {

        List<ITrack> _tracks { get; }
        IAirSpaceTracker _airSpaceTracker { get; }
        ICalculator _calc { get; }
        void Update(string data);

        string[] ConvertTransponderData(string data);
    }
}
