using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public class Polygon: PLine 
    {
        static private Color color = Color.MediumTurquoise;

        public Brush brush = new SolidBrush(color);
    
        override internal void drawObject(PaintEventArgs e)
        {
            List<System.Drawing.Point> points = convertPoints();
            LineStyle style = new LineStyle();
            Color col = style.color;
            Brush fillBrush = brush;
            if (isSelected)
            {
                col = Color.DarkGreen;
                fillBrush = new SolidBrush(Color.Orange);
            }

            Pen pen = new Pen(col, style.size);
            
            e.Graphics.DrawPolygon(pen, points.ToArray());
            e.Graphics.FillPolygon(fillBrush, points.ToArray());
        }

        internal override MapObject findObject(GeoRect zone)
        {
            MapObject isOnBorder = base.findObject(zone);
            MapObject isInside = null;

            Line checker = new Line(
                zone.getCenter(), 
                new GeoPoint(
                    getBounds().maxX + 1,
                    zone.getCenter().y)
                );

            int overlaps = 0;

            for (int i = 1; i < nodes.Count; i++)
            {
                Line l = new Line(nodes[i - 1], nodes[i]);
                if (Line.isCrossed(l, checker)) { overlaps ++; }
            }

            Line connector = new Line(nodes[0], nodes[nodes.Count - 1]);
            if (Line.isCrossed(checker, connector)) { overlaps ++; }
           

            if (overlaps % 2 != 0)
            {
                isInside = this;
            }



            if (isOnBorder != null | isInside != null) { return this; }
            return null;
        }
    }
}
