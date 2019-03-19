using System;

namespace ATC
{
    public class ATC
    {

        private ATM _atm;

        public ATM ATM
        {
            get { return _atm; }
            set { _atm = value; }
        }

        public ATC(ATM atm)
        {
            ATM = atm;
        }
    }
}
    