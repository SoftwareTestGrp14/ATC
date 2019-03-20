using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface ICalculator
    {
        double CalcVelocity(int xCord1, int xCord2, int yCord1, int yCord2, DateTime time1, DateTime time2);
        double CalcCourse(int xCord1, int xCord2, int yCord1, int yCord2);
        bool IsSeparation(ITrack track1, ITrack track2);


    }
}
