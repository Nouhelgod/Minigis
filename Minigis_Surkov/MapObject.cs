using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public abstract class MapObject
    {
        public VectorLayer layer;
        public bool isSelected = false;
        public GeoRect bounds
        {
            get
            {
                return getBounds();
            }
        }

        internal abstract void drawObject(PaintEventArgs e);
        internal abstract MapObject findObject(GeoRect zone);
        internal abstract GeoRect getBounds();
    }
}
