using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface ITracksManager
    {
        List<ITrack> RemoveAt(List<ITrack> tracks, int index);

        List<ITrack> AddTrack(List<ITrack> tracks, ITrack track);



    }
}
