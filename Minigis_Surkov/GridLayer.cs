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
using System.IO;

namespace Minigis_Surkov
{
    public class GridLayer: Layer
    {
        private GridGeometry geometry;
        private Bitmap gradientMap;
        private float[] anchorPoint;
        private float[] sides;
        private double? maxNodeValue;
        private double? minNodeValue;

        public double?[,] mesh;
        public GridGeometry Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }
        internal override void drawLayer(PaintEventArgs e)
        {
        
            map.gridColors.IsModified = true;
            var start = map.translateMapToScreen(new GeoPoint(bounds.minX, bounds.minY));
            var end = map.translateMapToScreen(new GeoPoint(bounds.maxX, bounds.maxY));

            anchorPoint = new float[] { start.X, end.Y };
            sides = new float[] {end.X - start.X, start.Y - end.Y};

            //if (map.Width - anchorPoint[0] < sides[0]) { sides[0] = map.Width - anchorPoint[0] - 1; } // Правая граница
            //if (map.Height - anchorPoint[1] < sides[1]) { sides[1] = map.Height - anchorPoint[1] - 1; } // Нижняя граница
            //if (anchorPoint[0] < 0) { sides[0] += anchorPoint[0]; anchorPoint[0] = 0; }
            //if (anchorPoint[1] < 0) { sides[1] += anchorPoint[1]; anchorPoint[1] = 1; }

            if (map.gridColors.IsModified)
            {
                renderGrid();
            }
            

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

        public GridLayer(string filename)
        {
            string data = File.ReadAllText(filename);
            string[] s = data.Split('\n');

            string header = s[0];
            data = s[1];
            string[] data_ = data.Split(':');

            string[] headers = header.Split(';');

            this.name = headers[0];

            this.geometry = new GridGeometry(
                //count
                int.Parse(headers[1]), 
                int.Parse(headers[2]), 
                
                //dist
                int.Parse(headers[3]),
                
                // origin
                double.Parse(headers[4], System.Globalization.CultureInfo.InvariantCulture), 
                double.Parse(headers[5], System.Globalization.CultureInfo.InvariantCulture)
                );

            int ptr = 0;
            for (int x = 0; x < this.geometry.countX; x++)
            {
                for (int y = 0; y < this.geometry.countY; y++)
                {
                    if (data_[ptr] != string.Empty)
                    {
                        this.geometry.nodeValues[x, y] = double.Parse(data_[ptr], System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        this.geometry.nodeValues[x, y] = null;
                    }

                    ptr++;
                }
            }
            bounds = new GeoRect(geometry.originX, geometry.originY, geometry.maxX, geometry.maxY);
            this.findMinMaxNodeValue();
        }

        public double? getValue(GeoPoint location)
        {
            if (!GeoRect.isIntersect(bounds, location)) { return null; }

            double[] nearest = geometry.findNearest(location);

            //double minX = nearest[0];
            //double maxX = nearest[1];
            //double minY = nearest[2];
            //double maxY = nearest[3];

            //double x = location.x;
            //double y = location.y;

            //double w1 = (maxX - x) * (y - minY);
            //double w2 = (x - minX) * (y - minY);
            //double w3 = (maxX - x) * (maxY - y);
            //double w4 = (x - minX) * (maxY - y);

            //double sumWeights = w1 + w2 + w3 + w4;

            //w1 /= sumWeights;
            //w2 /= sumWeights;
            //w3 /= sumWeights;
            //w4 /= sumWeights;

            //double? result = w1 * geometry.nodeValues[nearest[0], nearest[2]] + w2 * geometry.nodeValues[nearest[1], nearest[3]] 
            //    + w3 * geometry.nodeValues[nearest[2], nearest[0]] + w4 * geometry.nodeValues[nearest[3], nearest[1]];
            double? z1 = geometry.nodeValues[(int)nearest[0], (int)nearest[2]];
            double? z2 = geometry.nodeValues[(int)nearest[1], (int)nearest[2]];
            double? z3 = geometry.nodeValues[(int)nearest[0], (int)nearest[3]];
            double? z4 = geometry.nodeValues[(int)nearest[1], (int)nearest[3]];

            double? z5 = (nearest[5] * (z2 - z1)) / geometry.distance + z1;
            double? z6 = (nearest[5] * (z4 - z3)) / geometry.distance + z3;

            double? result = (nearest[4] * (z6 - z5)) / geometry.distance + z5;


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
                    Color c = map.gridColors.interpolateColor(val, minNodeValue, maxNodeValue);

                    int trueH = geometry.countY - h -1;                    
                    gradientMap.SetPixel(w, trueH, c);
                }
            }


            map.gridColors.IsModified = false;
        }

        public static GridLayer restoreGrid(VectorLayer layer, double cellSize = 10.0, GridGeometry geometry = null, double radius = 100, int power = 2)
        {
            if (geometry == null)
            {
                GeoPoint topLeft = new GeoPoint(layer.bounds.minX, layer.bounds.minY);
                GeoPoint botRight = new GeoPoint(layer.bounds.maxX, layer.bounds.maxY);
                double boundsWidth = botRight.x - topLeft.x;
                double boundsHeight = botRight.y - topLeft.y;

                geometry = new GridGeometry(
                        distance: cellSize,
                        originX: topLeft.x,
                        originY: topLeft.y,
                        countX: (int) (boundsWidth / cellSize) + 1,
                        countY: (int) (boundsHeight / cellSize) + 1
                );
            }

            GridLayer generated = new GridLayer(geometry);
            
            for (int x = 0; x < generated.geometry.countX; x++)
            {
                for (int y = 0; y < generated.geometry.countY; y++)
                {

                    generated.geometry.nodeValues[x, y] = findValueInRadius(
                        new GeoPoint(
                            (int) (generated.geometry.originX + generated.geometry.distance * x),
                            (int) (generated.geometry.originY + generated.geometry.distance * y)
                        ), 
                        layer, radius: radius, power: power);
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
                    Color c = map.gridColors.interpolateColor(val, minNodeValue, maxNodeValue);

                    gradientMap.SetPixel(i, j, c);
                }
            }
        }

        internal void writeToFile(string name)
        {
            string data = "";
            data += this.name; data += ";";
            data += Geometry.countX; data += ";";
            data += Geometry.countY; data += ";";
            data += Geometry.distance.ToString(System.Globalization.CultureInfo.InvariantCulture); data += ";";
            data += Geometry.originX.ToString(System.Globalization.CultureInfo.InvariantCulture); data += ";";
            data += Geometry.originY.ToString(System.Globalization.CultureInfo.InvariantCulture); data += "\n";

            for (int x = 0; x < Geometry.countX; x++)
            {
                for (int y = 0; y < Geometry.countY; y++)
                {
                    if (Geometry.nodeValues[x, y] != null) {
                        double val = (double)Geometry.nodeValues[x, y];
                        data += val.ToString(System.Globalization.CultureInfo.InvariantCulture); 
                    }

                data += ":";
                }
            }

            var file = File.CreateText(name);
            file.Write(data);
            file.Close();
            
        }
    }
}
