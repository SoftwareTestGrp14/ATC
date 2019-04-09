using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class TracksManager:ITracksManager
    {
        public TracksManager()
        {

        }

        public List<ITrack> RemoveAt(List<ITrack> tracks, int index)
        {
            tracks.RemoveAt(index);

            return tracks;
        }

        public List<ITrack> AddTrack(List<ITrack> tracks, ITrack track)
        {
            tracks.Add(track);

            return tracks;

        }


    }
}
