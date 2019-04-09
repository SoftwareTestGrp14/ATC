using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface ISeparationManager
    {


        List<ISeparationCondition> GetSeparationList();

        void RemoveAt(int index);

        void AddSeparation(ISeparationCondition track);

        bool IsNotEmpty();



    }
}
