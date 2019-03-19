using System;
using System.Collections.Generic;
using System.Text;

namespace ATC
{
    public interface IAirSpace
    {
        double MinAltitude { get; set; }
        double MaxAltitude { get; set; }
        double XSideLength { get; set; }
        double XStartPoint { get; set; }
        double YSideLength { get; set; }
        double YStartPoint { get; set; }
        double GetXEndPoint();
        double GetYEndPoint();
    }
}
