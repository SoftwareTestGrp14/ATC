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

            Track track1 = new Track("ABC123", 100, 100, 1000, 1, 1, DateTime.Now);
            Track track2 = new Track("DEF123", 90, 90, 900, 1, 1, DateTime.Now);

            Track track3 = new Track("ABC123", 101, 101, 1000, 1, 1, DateTime.Now);
            Track track4 = new Track("DEF123", 91, 91, 900, 1, 1, DateTime.Now);

            SeparationCondition sep1 = new SeparationCondition(track1, track2);

            SeparationCondition sep2 = new SeparationCondition(track1, track3);



            Console.WriteLine("Comparing tracks:");
            Console.WriteLine($"Track1: {track1.Equals(track3)}  Track2: {track2.Equals(track4)}");


            Console.WriteLine("Comparing seps:");
            Console.WriteLine(sep1.Equals(sep2));
        }
    }
}
