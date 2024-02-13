using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minigis_Surkov
{
    internal class GridColors
    {
        private Color colorMin;
        private Color colorMax;

        private bool isModified = true;

        public GridColors() {
            colorMin = Color.Blue;
            colorMax = Color.Red;
        }

        public GridColors(Color colorMin, Color colorMax)
        {
            ColorMin = colorMin;
            ColorMax = colorMax;
        }

        public Color ColorMin
        {
            get => colorMin;

            set
            {
                colorMin = value;
                isModified = true;
            }
        }

        public bool IsModified
        {
            set => isModified = value;
            get => isModified;
        }

        public Color ColorMax
        {
            get => colorMax;

            set
            {
                colorMax = value;
                isModified = true;
            }
        }
    }
}
