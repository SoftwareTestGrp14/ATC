using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface ISeparationCondition
    {

        ITrack _track1 { get; }
        ITrack _track2 { get; }
        DateTime Timestamp { get; }

        bool Equals(SeparationCondition other);

    }
}
