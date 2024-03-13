using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            minToolStripMenuItem.BackColor = map1.gridColors.ColorMin;
            maxToolStripMenuItem.BackColor = map1.gridColors.ColorMax;
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
            formGridTune tuneForm = new formGridTune();

            if (tuneForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            VectorLayer affectedLayer;
            GridLayer generatedLayer;

            if (!(layerControl1.SelectedLayer is VectorLayer))
            {
                MessageBox.Show("Select exactly one vector layer to proceed", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }

            affectedLayer = layerControl1.SelectedLayer as VectorLayer;
            affectedLayer.bounds.minX += tuneForm.marginWest;
            affectedLayer.bounds.maxX -= tuneForm.marginEast;
            affectedLayer.bounds.minY += tuneForm.marginNorth;
            affectedLayer.bounds.maxY -= tuneForm.marginSouth;

            generatedLayer = GridLayer.restoreGrid(affectedLayer, cellSize: tuneForm.cellSize, radius: tuneForm.radius);
            generatedLayer.name = affectedLayer.name + " mesh";
            generatedLayer.map = affectedLayer.map;
            map1.layers.Add(generatedLayer);
            map1.layerControl.refreshList();
            MessageBox.Show("ok?", "ok?", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void testRun_Click(object sender, EventArgs e)
        {
            int WIDTH = 200;
            int HEIGHT = 200;
            int DISTANCE = 10;
            string FILEPATH = "points3d.csv";
            VectorLayer vectorTestLayer = new VectorLayer().parseCSVFile(FILEPATH);

            GridGeometry testGeometry = new GridGeometry(
                WIDTH, HEIGHT, DISTANCE, 
                vectorTestLayer.bounds.minX, 
                vectorTestLayer.bounds.minY);
            GridLayer gridTestLayer = new GridLayer(testGeometry);

            GridGeometry[] restoredGeometry = new GridGeometry[5];

            for (int pow = 1; pow <= 5; pow++)
            {
                gridTestLayer = GridLayer.restoreGrid(vectorTestLayer, cellSize: DISTANCE, geometry: testGeometry, radius: 300, power: pow);
                restoredGeometry[pow - 1] = gridTestLayer.Geometry;
            }

            GridGeometry finalGeometry = testGeometry;

            for (int i = 0; i < restoredGeometry.Length; i++)
            {
                for(int x = 0; x < finalGeometry.countX; x++)
                {
                    for(int y = 0; y < finalGeometry.countY; y++)
                    {
                        finalGeometry.nodeValues[x, y] += restoredGeometry[i].nodeValues[x, y];

                        if (i == 4)
                        {
                            finalGeometry.nodeValues[x, y] /= 5;
                        }
                    }
                }
            }

            gridTestLayer.Geometry = finalGeometry;
            gridTestLayer.findMinMaxNodeValue();
            gridTestLayer.name = "test-grid-layer";
            vectorTestLayer.isVisible = false;
            vectorTestLayer.name = "test-csv-layer";

            map1.appendLayer(gridTestLayer);
            map1.layerControl.refreshList();
            
        }

        private void selectColors_Click(object sender, EventArgs e)
        {

        }

        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialogMain.AllowFullOpen = true;
            colorDialogMain.Color = map1.gridColors.ColorMin;

            if (colorDialogMain.ShowDialog() == DialogResult.OK)
            {
                map1.gridColors.ColorMin = colorDialogMain.Color;
                minToolStripMenuItem.BackColor = colorDialogMain.Color;
                map1.gridColors.IsModified = true;
            }
        }

        private void maxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialogMain.AllowFullOpen = true;
            colorDialogMain.Color = map1.gridColors.ColorMax;

            if (colorDialogMain.ShowDialog() == DialogResult.OK)
            {
                map1.gridColors.ColorMax = colorDialogMain.Color;
                maxToolStripMenuItem.BackColor = colorDialogMain.Color;
                map1.gridColors.IsModified = true;
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
