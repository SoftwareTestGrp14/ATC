using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
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
            bool result1 = true;
            bool result2 = true;

            for (int i = 0; i < this._track1._tag.Length; i++)
            {
                if (((this._track1._tag[i] != other._track1._tag[i]) || (this._track2._tag[i] != other._track2._tag[i])))
                {
                    result1 =false;

                }
            }

            for (int i = 0; i < this._track1._tag.Length; i++)
            {
                if (((this._track1._tag[i] != other._track2._tag[i]) || (this._track2._tag[i] != other._track1._tag[i])))
                {
                    result2 = false;

                }
            }

            return result1 | result2;
        }

    }
}
