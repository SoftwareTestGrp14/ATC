using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public class Track: ITrack
    {
        public Track(string tag, int xC, int yC, int alt, double vel, double course, DateTime ts)
        {
            _tag = tag;
            _xCord = xC;
            _yCord = yC;
            _alt = alt;
            _velocity = vel;
            _course = course;
            _timestamp = ts;
        }

        public string _tag { get;  }
        public int _xCord { get;  }
        public int _yCord { get; }
        public int _alt { get; }
        public double _velocity { get; }
        public double _course { get; }
        public DateTime _timestamp { get; }


        public bool Equals(Track other)
        {
            // Would still want to check for null etc. first.
            return this._tag == other._tag;
        }
    }
}
