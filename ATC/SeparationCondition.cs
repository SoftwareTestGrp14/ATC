using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public class SeparationCondition:ISeparationCondition
    {
        public ITrack _track1 { get; }
        public ITrack _track2 { get; }
        public DateTime Timestamp { get; }

        public SeparationCondition(ITrack track1, ITrack track2)
        {
            _track1 = track1;
            _track2 = track2;

            if (track2._timestamp > track1._timestamp)
                Timestamp = track2._timestamp;
            else
                Timestamp = track1._timestamp;
            
        }


      

        public bool Equals(SeparationCondition other)
        {
            // Would still want to check for null etc. first.
            return ((this._track1 == other._track1 && this._track2 == other._track2)||(this._track1 == other._track2 && this._track2 == other._track1));
        }

    }
}
