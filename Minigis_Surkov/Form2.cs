using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public partial class formGridTune : Form
    {

        public double marginNorth;
        public double marginSouth;
        public double marginWest;
        public double marginEast;
        public double radius;
        public double cellSize;

        public formGridTune()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            marginNorth = double.Parse(textNorth.Text);
            marginEast = double.Parse(textEast.Text);
            marginSouth = double.Parse(textSouth.Text);
            marginWest = double.Parse(textWest.Text);   
            radius = double.Parse(textRadius.Text);
            cellSize = double.Parse(textCellSize.Text);
        }
    }
}
