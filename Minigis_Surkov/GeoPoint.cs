using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public class GeoPoint
    {
        public double x;
        public double y;

        public GeoPoint()
        {
            x = 0;
            y = 0;
        }

        public GeoPoint(double x_, double y_)
        {
            x = x_;
            y = y_;
        }
    }
}
