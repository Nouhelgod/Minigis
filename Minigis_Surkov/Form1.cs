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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            layerControl1.map = map1;
            map1.layerControl = layerControl1;
            layerControl1.refreshList();
        }

        private void ZoomAll_Click(object sender, EventArgs e)
        {
            map1.zoomAll();
        }

        private void Pan_Click(object sender, EventArgs e)
        {
            Map.tool = ActiveTool.Pan;
            activeToolLabel.Text = "✋ | Pan";
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            Map.tool = ActiveTool.ZoomIn;
            activeToolLabel.Text = "🔍 | Zoom In";
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            Map.tool = ActiveTool.ZoomOut;
            activeToolLabel.Text = "🔎 | Zoom Out";
        }

        private void Select_Click(object sender, EventArgs e)
        {
            Map.tool = ActiveTool.Select;
            activeToolLabel.Text = "👆 | Select";
        }

        private void Measure_Click(object sender, EventArgs e)
        {
            Map.tool = ActiveTool.Measure;
            activeToolLabel.Text = "📏 | Measure";
        }

        private void map1_MouseMove(object sender, MouseEventArgs e)
        {
            GeoPoint cords = map1.translateScreenToMap(e.Location);
            statusLabelX.Text = "X: " + Convert.ToString(Math.Round(cords.x, 2));
            statusLabelY.Text = "Y: " + Convert.ToString(Math.Round(cords.y, 2)); 
        }

        private void statusLabelX_Click(object sender, EventArgs e)
        {

        }

        private void map1_Load(object sender, EventArgs e)
        {
            InitializeComponent();
        }

        private void map1_Load_1(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void layerControl1_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddLayer_Click(object sender, EventArgs e)
        {
            if (openLayerDialog.ShowDialog() == DialogResult.OK)
            {
                if (openLayerDialog.FileName != null)
                {
                    var filename = System.IO.Path.GetFileNameWithoutExtension(openLayerDialog.FileName);
                    var fileExtension = System.IO.Path.GetExtension(openLayerDialog.FileName);
                    try { 
                        
                        foreach (Layer layer in map1.layers)
                        {
                            if (layer.name == filename) { return; }
                        }
                        var filepath = openLayerDialog.FileName;

                        VectorLayer imported = null;

                        if (fileExtension.ToLower() == ".mif")
                        {
                            imported = new VectorLayer().parseMifFile(filepath);
                        }
                        else if (fileExtension.ToLower() == ".csv")
                        {
                            imported = new VectorLayer().parseCSVFile(filepath);
                        } 
                        
                        map1.appendLayer(imported);
                        map1.Refresh();
                        layerControl1.refreshList();

                    } catch (Exception error)
                    {
                        MessageBox.Show(error.ToString());
                    }
                }
            }
        }

        private void GetValue_Click(object sender, EventArgs e)
        {
            Map.tool = ActiveTool.GetValue;
            activeToolLabel.Text = "GET VALUE TOOL";
        }

        private void RestoreGrid_Click(object sender, EventArgs e)
        {
            Layer affectedLayer;
            Layer[] visibleVectorLayers = new Layer[map1.layers.Count];

            foreach (Layer layer in map1.layers)
            {
                if (layer.isVisible && layer is VectorLayer)
                {
                    visibleVectorLayers.Append(layer);
                }

                if (visibleVectorLayers[1] != null)
                {
                    // send message box
                }
            }
        }
    }

    public enum ActiveTool
    {
        Pan,
        ZoomIn,
        ZoomOut,
        Select,
        Measure,
        GetValue
    }
}
