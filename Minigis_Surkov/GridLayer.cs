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
       

        public double?[,] mesh;

        internal override void drawLayer(PaintEventArgs e)
        {
            var start = map.translateMapToScreen(new GeoPoint(bounds.minX, bounds.minY));
            var end = map.translateMapToScreen(new GeoPoint(bounds.maxX, bounds.maxY));

            Pen pen = new Pen(Color.Red, 1);
            e.Graphics.DrawRectangle(pen,
                start.X, start.Y,
                start.X - end.X,
                start.Y + end.Y);

            if (gradientMap == null)
            {
                gradientMap = new Bitmap(geometry.countX, geometry.countY);
            }

            if (colors.IsModified)
            {
                renderGrid();
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
            if (!GeoRect.isIntersect(bounds, location)) { return 0; }

            double minX = (location.x - geometry.originX) / geometry.distance;
            double maxX = (geometry.maxX - location.x) / geometry.distance;
            double maxY = (location.y - geometry.originY) / geometry.distance;
            double minY = (geometry.maxY - location.y) / geometry.distance;

            double[,] nodes =
            {
                {minX, minY}, {minX, maxY},
                {maxX, minY}, {maxX, maxY},
            };

            double?[] values =
            {
                mesh[(int) minX, (int) minY], mesh[(int) minX, (int) maxY],
                mesh[(int) maxX, (int) minY], mesh[(int) maxX, (int) maxY],
            };

            double? r1 = lerp(minX, values[0], maxX, values[2], location.x);
            double? r2 = lerp(minX, values[1], maxX, values[3], location.x);
            double? result = lerp(minY, r1, maxY - location.y, r2, location.y);

            Console.WriteLine(r1);
            Console.WriteLine(r2);
                
            return result;
        }


        private double? lerp(double? x0, double? y0, double? x1, double? y1, double? x)
        {
            return y0 + ((y1 - y0) / (x1 - x0)) * (x - x0);
        }


        private void renderGrid()
        {
            if (gradientMap == null) { return; }

            for (int i = 0; i < geometry.countX; i++)
            {
                for (int j = 0; j < geometry.countY; j++)
                {
                    double? targetValue = mesh[i, j];
                    Color? color = getColor(targetValue);
                    gradientMap.SetPixel(
                        geometry.countY - j - 1, 
                        geometry.countX - i - 1, 
                        color.Value
                        );
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
