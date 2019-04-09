using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface ISeparationManager
    {
        void RemoveAt(List<ISeparationCondition> tracks, int index);

        void AddSeparation(List<ISeparationCondition> tracks, ISeparationCondition track);

        bool IsNotEmpty(List<ISeparationCondition> tracks);



    }
}
