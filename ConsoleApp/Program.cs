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
            Console.WriteLine(DateTime.Now);

            ATC.ATC atc=new ATC.ATC(new ATM());

            while (true) { }
        }
    }
}
