using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface IConsoleRenderer
    {
        void Render(List<ITrack> tracks);
        void Render(List<ISeparationCondition> separations);
    }
}
