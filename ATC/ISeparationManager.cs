using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface ISeparationManager
    {
        List<ISeparationCondition> RemoveAt(List<ISeparationCondition> tracks, int index);

        List<ISeparationCondition> AddSeparation(List<ISeparationCondition> tracks, ISeparationCondition track);

        bool IsNotEmpty(List<ISeparationCondition> tracks);



    }
}
