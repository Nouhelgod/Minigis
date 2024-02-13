using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public class Line: MapObject
    {
        public GeoPoint start = new GeoPoint();
        public GeoPoint end = new GeoPoint();
        public LineStyle visual = new LineStyle();
        public string type;
        public Line(double x1, double y1, double x2, double y2, string type_ = "Default") 
        {
            start.x = x1;
            start.y = y1;

            end.x = x2;
            end.y = y2;

            type = type_;
        }


        public Line(GeoPoint _start, GeoPoint _end, string type_ = "Default")
        {
            start = _start;
            end = _end;

            type = type_;
        }

        override internal void drawObject(PaintEventArgs e)
        {

            LineStyle style = new LineStyle();

            var col = style.color;

            if (isSelected)
            {
                col = Color.DarkGreen;
            }

            Pen pen = new Pen(col, style.size); ;

            if (type == "Default")
            {
                pen = new Pen(col, style.size);

            } else if (type == "Measure")
            {
                if (!isSelected) { col = Color.LightGray; }
                else { col = Color.LightGreen; }
                pen = new Pen(col, style.size);
                pen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
            }

            e.Graphics.DrawLine(
                pen,
                layer.map.translateMapToScreen(start),
                layer.map.translateMapToScreen(end)
                );

        }

        internal override GeoRect getBounds()
        {
            var maxX = Math.Max(start.x, end.x);
            var maxY = Math.Max(start.y, end.y);
            var minX = Math.Min(start.x, end.x);
            var minY = Math.Min(start.y, end.y);

            return new GeoRect(maxX, maxY, minX, minY);
        }

        public static bool isCrossed (Line A, Line B)
        {
            GeoPoint A1 = A.start; GeoPoint A2 = A.end; GeoPoint B1 = B.start; GeoPoint B2 = B.end;
            double x1 = A1.x;
            double y1 = A1.y;
            double x2 = A2.x;
            double y2 = A2.y;
            double x3 = B1.x;
            double y3 = B1.y;
            double x4 = B2.x;
            double y4 = B2.y;

            double determinant = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (determinant == 0)
            {
                // проверить по уравнению 
                // Прямые параллельны и не пересекаются
                return false;
            }
            else
            {
                double x = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / determinant;
                double y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / determinant;

                if (x >= Math.Min(x1, x2) && x <= Math.Max(x1, x2) && x >= Math.Min(x3, x4) && x <= Math.Max(x3, x4) &&
                    y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2) && y >= Math.Min(y3, y4) && y <= Math.Max(y3, y4))
                {
                    // Прямые пересекаются, и их пересечение - это точка (x, y)
                    return true;
                }
                else
                {
                    // Прямые не пересекаются
                    return false;
                }
            }
        }

        internal override MapObject findObject(GeoRect zone)
        {
            Line zoneTop = new Line(new GeoPoint(zone.minX, zone.maxY), new GeoPoint(zone.maxX, zone.maxY));
            Line zoneBot = new Line(new GeoPoint(zone.minX, zone.minY), new GeoPoint(zone.maxX, zone.minY));
            Line zoneLeft = new Line(new GeoPoint(zone.minX, zone.minY), new GeoPoint(zone.minX, zone.maxY));
            Line zoneRight = new Line(new GeoPoint(zone.maxX, zone.minY), new GeoPoint(zone.maxX, zone.maxY));

            if (isCrossed(this, zoneTop) | isCrossed(this, zoneBot) | isCrossed(this, zoneRight) | isCrossed(this, zoneLeft)) { return this; }
            return null;
        }
    }
}
