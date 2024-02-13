using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Minigis_Surkov
{
    public class GridLayer: Layer
    {
        private GridGeometry geometry;
        private Bitmap gradientMap;
        private GridColors colors = new GridColors();
        private float[] anchorPoint;
        private float[] sides;
        private double? maxNodeValue;
        private double? minNodeValue;

        public double?[,] mesh;

        internal override void drawLayer(PaintEventArgs e)
        {
            var start = map.translateMapToScreen(new GeoPoint(bounds.minX, bounds.minY));
            var end = map.translateMapToScreen(new GeoPoint(bounds.maxX, bounds.maxY));
            anchorPoint = new float[] { start.X, end.Y };
            sides = new float[] {end.X - start.X, start.Y - end.Y};


            if (colors.IsModified)
            {
                renderGrid();
            }


            Pen pen = new Pen(Color.Red, 1);
            e.Graphics.DrawRectangle(pen,
                x: anchorPoint[0], y: anchorPoint[1],
                width: sides[0], height: sides[1]
                );
        }


        public void findMinMaxNodeValue()
        {
            for( int i = 0; i < mesh.GetLength(0); i++ )
            {
                for( int j = 0; j < mesh.GetLength(1); j++)
                {
                    if (mesh[i, j] != null)
                    {
                        var p = mesh[i, j];

                        if (minNodeValue == null) { minNodeValue = p; }
                        if (maxNodeValue == null) { maxNodeValue = p; }
                        if (p > maxNodeValue) { maxNodeValue = p; }
                        if (p < minNodeValue) { minNodeValue = p; }
                    }
                }
            }
        }

        internal override MapObject findObject(GeoRect zone)
        {
            return null;
        }

        public GridLayer(GridGeometry g, string _name = "default-grid")
        {
            geometry = g;
            name = _name;

            bounds = new GeoRect(g.originX, g.originY, g.maxX, g.maxY);
            mesh = new double?[g.countY, g.countX];

        }    

        public double? getValue(GeoPoint location)
        {
            if (!GeoRect.isIntersect(bounds, location)) {
                return 0; }

            double minX = geometry.originX;
            double maxX = geometry.maxX;
            double maxY = geometry.maxY;
            double minY = geometry.originY;


            double x = location.x;
            double y = location.y;

            double w1 = (maxX - x) * (y - minY);
            double w2 = (x - minX) * (y - minY);
            double w3 = (maxX - x) * (maxY - y);
            double w4 = (x - minX) * (maxY - y);

            double sumWeights = w1 + w2 + w3 + w4;
            w1 /= sumWeights;
            w2 /= sumWeights;
            w3 /= sumWeights;
            w4 /= sumWeights;

            double? result = w1 * mesh[0, 0] + w2 * mesh[0, 1] + w3 * mesh[1, 0] + w4 * mesh[1, 1];

            return result;
        }


        public void createBitmap()
        {
            gradientMap = new Bitmap(width: (int)sides[0], height: (int)sides[1]);
        }


        private void renderGrid()
        {
            createBitmap();

            for (int i = 0; i < sides[0]; i++)
            {
                for (int j = 0; j < sides[1]; j++)
                {
                    GeoPoint location = map.translateScreenToMap(
                        new System.Drawing.Point((int)anchorPoint[0], (int)anchorPoint[1])
                        );


                    double? val = getValue(
                        new GeoPoint(location.x + i, location.y + i)
                        );
                    Color c = colors.interpolateColor(val, minNodeValue, maxNodeValue);
                    gradientMap.SetPixel(i, j, c);
                }
            }
            colors.IsModified = true;
        }

        private Color? getColor(double? targetValue)
        {
            if (!targetValue.HasValue)
            { 
                return null;
            }

            return Color.DarkGreen;
        }
    }
}
