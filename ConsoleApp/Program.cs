using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATC;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PlaneTracker pt = new PlaneTracker(new AirSpace(), new AirSpaceTracker(), new List<ISeparationCondition>(), new List<ITrack>(), new ConsoleLog(), new FileLog(), new Calculator());
            pt.Update("ATR423; 39045; 12932; 14000; 20151006213456789");
            pt.Update("ATR423; 39050; 12932; 14000; 20151006213656789");


            pt.Update("ABC123; 38800; 12500; 13900; 20151006213456789");
            pt.Update("ABC123; 38800; 12510; 13900; 20151006213656789");

            pt.Update("ABC123; 38800; 12512; 13900; 20151006213856789");
        }
    }
}
