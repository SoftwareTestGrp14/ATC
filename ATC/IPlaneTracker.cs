using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public interface IPlaneTracker
    {

        void Update(string data);

        string[] ConvertTransponderData(string data);
    }
}
