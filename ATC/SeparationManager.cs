using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class SeparationManager : ISeparationManager
    {
        public void AddSeparation(List<ISeparationCondition> tracks, ISeparationCondition track)
        {
            tracks.Add(track);

            //return tracks;
        }

        public void RemoveAt(List<ISeparationCondition> tracks, int index)
        {
            tracks.RemoveAt(index);

            //return tracks;
        }

        public bool IsNotEmpty(List<ISeparationCondition> tracks)
        {
            return tracks.Count > 0;
        }


    }
}
