using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

            //if (map.Width - anchorPoint[0] < sides[0]) { sides[0] = map.Width - anchorPoint[0] - 1; } // Правая граница
            //if (map.Height - anchorPoint[1] < sides[1]) { sides[1] = map.Height - anchorPoint[1] - 1; } // Нижняя граница
            //if (anchorPoint[0] < 0) { sides[0] += anchorPoint[0]; anchorPoint[0] = 0; }
            //if (anchorPoint[1] < 0) { sides[1] += anchorPoint[1]; anchorPoint[1] = 1; }

            
            renderGrid();
            

            Pen pen = new Pen(Color.Red, 1);
            e.Graphics.DrawRectangle(pen,
                x: anchorPoint[0], y: anchorPoint[1],
                width: sides[0], height: sides[1]
                );

            e.Graphics.DrawImage(image: gradientMap,
                x: anchorPoint[0], y: anchorPoint[1],
                width: sides[0], height: sides[1]
                );
        }


        public void findMinMaxNodeValue()
        {
            minNodeValue = null;
            maxNodeValue = null;
            for( int i = 0; i < geometry.countX; i++ )
            {
                for( int j = 0; j < geometry.countY; j++)
                {
                    var p = geometry.nodeValues[i, j];
                    if (p != null)
                    {

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
            if (!GeoRect.isIntersect(bounds, location)) { return 0; }

            double[] nearest = geometry.findNearest(location);

            double minX = nearest[0];
            double maxX = nearest[1];
            double minY = nearest[2];
            double maxY = nearest[3];

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
            if (sides[0] <= 0 || sides[1] <= 0) { sides[0] = 1; sides[1] = 1; }
            gradientMap = new Bitmap(width: (int)sides[0], height: (int)sides[1]);
        }


        private void renderGrid()
        {
            gradientMap = new Bitmap(width: geometry.countX, height: geometry.countY);
            
            for (int w = 0; w < geometry.countX; w ++)
            {
                for (int h = 0; h < geometry.countY; h ++)
                {
                    double x = geometry.originX + geometry.distance * w;
                    double y = geometry.originY + geometry.distance * h;

                    double? val = geometry.nodeValues[w, h];
                    Color c = colors.interpolateColor(val, minNodeValue, maxNodeValue);

                    int trueH = geometry.countY - h;                    
                    gradientMap.SetPixel(w, h, c);
                }
            }

            colors.IsModified = true;
        }

        public static GridLayer restoreGrid(VectorLayer layer, double cellSize = 10.0)
        {
            GeoPoint topLeft = new GeoPoint(layer.bounds.minX, layer.bounds.minY);
            GeoPoint botRight = new GeoPoint(layer.bounds.maxX, layer.bounds.maxY);

            double boundsWidth = botRight.x - topLeft.x;
            double boundsHeight = botRight.y - topLeft.y;

            // if < 1 then width > height
            double boundsRatio = boundsHeight / boundsWidth;
            GridGeometry geometry = new GridGeometry(
                    distance: cellSize,
                    originX: topLeft.x,
                    originY: topLeft.y,
                    countX: (int) (boundsWidth / cellSize) + 1,
                    countY: (int) (boundsHeight / cellSize) + 1
                );

            GridLayer generated = new GridLayer(geometry);
            
            for (int x = 0; x < generated.geometry.countX; x++)
            {
                for (int y = 0; y < generated.geometry.countY; y++)
                {

                    generated.geometry.nodeValues[x, y] = findValueInRadius(generated.geometry.nodeCoords[x, y], layer, radius: 1500);
                }
            }

            generated.findMinMaxNodeValue();
            return generated;
        }

        private static double? findValueInRadius(GeoPoint poi, VectorLayer dataLayer, double radius = 10, double power = 2)
        {
            double accumVal = 0;
            double accumCoef = 0;
            double rSquared = radius * radius;
            bool legit = false;
            
            foreach (MapObject mo in dataLayer.objects)
            {
                if (mo is Point)
                {
                    Point p = mo as Point;
                    GeoPoint other = p.location;
                    double dist = Math.Pow(poi.x - other.x, 2) + Math.Pow(poi.y - other.y, 2);

                    if (dist <= double.Epsilon) { return p.z; }


                    if (dist < rSquared)
                    {
                        accumVal += p.z / (Math.Pow(dist, power));
                        accumCoef += 1 / (Math.Pow(dist, power));
                        legit = true;
                    }
                }
            }

            if (!legit)
            {
                return null;
            }

            return accumVal / accumCoef;
        }

        // Deprecated
        private void renderPixelPerfectGrid()
        {
            createBitmap();

            for (int i = 0; i < sides[0]; i++)
            {
                for (int j = 0; j < sides[1]; j++)
                {
                    GeoPoint location = map.translateScreenToMap(
                        new System.Drawing.Point((int)anchorPoint[0] + i, (int)anchorPoint[1] + j)
                        );

                    if (anchorPoint[0] + i > map.Width) { return; }
                    if (anchorPoint[1] + j > map.Height) { return; }

                    double? val = getValue(
                        new GeoPoint(location.x, location.y)
                        );
                    Color c = colors.interpolateColor(val, minNodeValue, maxNodeValue);

                    gradientMap.SetPixel(i, j, c);
                }
            }
        }
    }
}
