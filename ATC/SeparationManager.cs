using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class SeparationManager : ISeparationManager
    {
        private readonly List<ISeparationCondition> _currentSeparations;

        public SeparationManager()
        {
            _currentSeparations = new List<ISeparationCondition>();
        }

        public void AddSeparation(ISeparationCondition track)
        {
            _currentSeparations.Add(track);

            //return tracks;
        }

        public void RemoveAt(int index)
        {
            _currentSeparations.RemoveAt(index);

            //return tracks;
        }

        public bool IsNotEmpty()
        {
            return _currentSeparations.Count > 0;
        }

        public List<ISeparationCondition> GetSeparationList()
        {
            return _currentSeparations;
        }


    }
}
