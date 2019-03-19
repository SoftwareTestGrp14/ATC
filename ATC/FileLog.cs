using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ATC
{
    public class FileLog : ILogger
    {
        // Ikke i stand til at teste - det er en del af en integrationstest
        public void Write(string textString)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
               w.WriteLine(textString);
            }

        }
    }
}
