using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public abstract class Layer
    {
        public bool isVisible = true;
        public string name;
        public Map map;

        internal GeoRect bounds = new GeoRect();

        internal abstract void drawLayer(PaintEventArgs e);
        internal abstract MapObject findObject(GeoRect zone);
        internal GeoRect Bounds
        {
            get { return bounds; }
        }
    }
}
