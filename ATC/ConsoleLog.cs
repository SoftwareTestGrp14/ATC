using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public class ConsoleLog : ILogger
    {
        public void Write(string textString)
        {
            Console.WriteLine(textString);
        }
    }
}
