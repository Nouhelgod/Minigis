using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public class VectorLayer: Layer
    {
        public List<MapObject> objects = new List<MapObject>();

        public VectorLayer()
        {
            name = "default-vector";
        }

        public VectorLayer(string name_)
        {
            name = name_;
        }

        public void append(MapObject obj)
        {
            //objects.Append(obj);
            objects.Add(obj);
            bounds = GeoRect.union(obj.bounds, bounds);
            obj.layer = this;
        }

        public void insertAt(int index, MapObject obj)
        {
            objects.Insert(index, obj);
            bounds = GeoRect.union(obj.bounds, bounds);
            obj.layer = this;
        }

        public void pop()
        {
            // Fix bounds
            objects.Remove(objects.Last());
        }

        public void popAt(int position)
        {
            // Fix bounds
            objects.RemoveAt(position);
        }

        internal override void drawLayer(PaintEventArgs e)
        {
            foreach (MapObject object_ in objects)
            {
                object_.drawObject(e);
            }
        }

        override internal MapObject findObject(GeoRect zone)
        {
            MapObject result = null;
            for (int i = objects.Count -1; i >= 0; i--)
            {
                result = objects[i].findObject(zone);
                if (result != null)
                {
                    break;
                }
            }

            return result;
        }

        internal VectorLayer parseCSVFile(string filename_)
        {
            char[] separators = {';', ',', '\t'};
            var filename = Path.GetFileNameWithoutExtension(filename_);
            string[] lines = File.ReadAllLines(filename_);
            VectorLayer parsedLayer = new VectorLayer(filename);
                       
            foreach (string line in lines)
            {
                string[] cordStr = line.Split(separators);
                double[] cordDouble = new double[cordStr.Length];
                for (int i = 0; i < cordStr.Length; i++)
                {
                    cordStr[i] = cordStr[i].Replace('.', ',');
                    cordDouble[i] = double.Parse(cordStr[i]);
                }

                Point p = new Point(cordDouble[0], cordDouble[1], cordDouble[2]);
                parsedLayer.append(p);
            }
         

            return parsedLayer;
        }

        internal VectorLayer parseMifFile(string filename_)
        {
            var filename = Path.GetFileNameWithoutExtension(filename_);
            string[] lines = File.ReadAllLines(filename_);
            VectorLayer parsedLayer = new VectorLayer(filename);

            bool plining = false;
            List<PLine> plines = new List<PLine>();
            int plindex = -1;

            bool polygoning = false;
            List<Polygon> polygons = new List<Polygon>();
            int polyindex = -1;
            foreach (string line in lines)
            {
                string[] words = line.Split(new char[] { ' ' });

                if (line.StartsWith("POINT"))
                {
                    plining = false;
                    polygoning = false;
                    double x = double.Parse(words[1]);
                    double y = double.Parse(words[2]);
                    Point imported = new Point(x, y);
                    
                    parsedLayer.append(imported);
                }

                else if (line.StartsWith("LINE")) {
                    plining = false;
                    polygoning = false;
                    double x1 = double.Parse(words[1]);
                    double y1 = double.Parse(words[2]);
                    double x2 = double.Parse(words[3]);
                    double y2 = double.Parse(words[4]);
                    Line imported = new Line(x1, y1, x2, y2);

                    parsedLayer.append(imported);
                }

                else if (line.StartsWith("PLINE"))
                {
                    plining = true;
                    polygoning = false;
                    plines.Add(new PLine());
                    plindex++;
                }

                else if (plining)
                {
                    if (line != "")
                    {
                        double x = double.Parse(words[0]);
                        double y = double.Parse(words[1]);
                        plines[plindex].append(new GeoPoint(x, y));
                    } else
                    {
                        plining = false;
                    }
                }

                else if (line.StartsWith("REGION"))
                {
                    plining = false;
                    polygoning = true;
                    polygons.Add(new Polygon());
                    polyindex++;
                }

                else if (polygoning)
                {
                    if (line != "")
                    {
                        double x = double.Parse(words[0]);
                        double y = double.Parse(words[1]);
                        polygons[polyindex].append(new GeoPoint(x, y));
                    } else
                    {
                        polygoning = false;
                    }
                }

            }

            foreach (PLine line in plines)
            {
                parsedLayer.append(line);
            }

            foreach(Polygon polygon in polygons)
            {
                parsedLayer.append(polygon);
            }

            return parsedLayer;
        }
    }
}
