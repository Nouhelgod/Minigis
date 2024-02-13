using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public class PLine: MapObject
    {
        public List<GeoPoint> nodes = new List<GeoPoint>();
        public LineStyle visual = new LineStyle();


        public void append(GeoPoint point)
        {
            nodes.Add(point);
        }

        public void insertAt(int index, GeoPoint point)
        {
            nodes.Insert(index, point);
        }

        public void pop()
        {
            nodes.Remove(nodes.Last());
        }

        public void popAt(int position)
        {
            nodes.RemoveAt(position);
        }


        internal override void drawObject(PaintEventArgs e)
        {
            List<System.Drawing.Point> points = convertPoints();

            LineStyle style = new LineStyle();
            var col = style.color;

            if (isSelected)
            {
                col = Color.DarkGreen;
            }

            Pen pen = new Pen(col, style.size);
            e.Graphics.DrawLines(pen, points.ToArray());
        }


        public List<System.Drawing.Point> convertPoints()
        {
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            foreach (GeoPoint node in nodes)
            {
                points.Add(layer.map.translateMapToScreen(node));
            }

            return points;
        }

        internal override GeoRect getBounds()
        {
            double maxX = nodes[0].x;
            double maxY = nodes[0].y;
            double minX = nodes[0].x;
            double minY = nodes[0].y;

            foreach (GeoPoint point in nodes)
            {
                if (point.x > maxX)
                {
                    maxX = point.x;
                }

                if (point.x < minX)
                {
                    minX = point.x;
                }

                if (point.y > maxY)
                {
                    maxY = point.y;
                }

                if (point.y < minY)
                {
                    minY = point.y;
                }
            }

            return new GeoRect(minX, minY, maxX, maxY);
        }

        internal override MapObject findObject(GeoRect zone)
        {
            Line zoneTop = new Line(new GeoPoint(zone.minX, zone.maxY), new GeoPoint(zone.maxX, zone.maxY));
            Line zoneBot = new Line(new GeoPoint(zone.minX, zone.minY), new GeoPoint(zone.maxX, zone.minY));
            Line zoneLeft = new Line(new GeoPoint(zone.minX, zone.minY), new GeoPoint(zone.minX, zone.maxY));
            Line zoneRight = new Line(new GeoPoint(zone.maxX, zone.minY), new GeoPoint(zone.maxX, zone.maxY));

            for (int i = 1; i < nodes.Count; i ++)
            {
                Line l = new Line(nodes[i - 1], nodes[i]);
                if (Line.isCrossed(l, zoneTop) | Line.isCrossed(l, zoneBot) | Line.isCrossed(l, zoneRight) | Line.isCrossed(l, zoneLeft)) { return this; }
            }
            return null;
        }
    }
}
