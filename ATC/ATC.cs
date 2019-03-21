using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public class ATC
    {
        public IAtm ATM { get; set; }

        public ATC(IAtm atm)
        {
            ATM = atm;
        }
    }
}
    