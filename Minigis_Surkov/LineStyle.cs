using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minigis_Surkov
{
    public class LineStyle
    {
        public int size;
        public System.Drawing.Color color;
        public int type;

        public LineStyle()
        {
            size = 2;
            color = System.Drawing.Color.DarkSlateBlue;
            type = 0;
        }
    }
}
