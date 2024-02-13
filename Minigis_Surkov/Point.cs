using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Minigis_Surkov
{
    public class Point: MapObject
    {
        public GeoPoint location;
        public Symbol visual = new Symbol();
        private string text;

        public Point(double x, double y, string text_ = "Default")
        {
            location = new GeoPoint();
            location.x = x;
            location.y = y;
            text = text_;
        }

        internal override void drawObject(PaintEventArgs e)
        {
            System.Drawing.Point p = layer.map.translateMapToScreen(location);
            Color col;
            var font = new Font(visual.font, visual.size);
            string ch = Convert.ToChar(visual.number).ToString();
            if (text != "Default")
            {
                ch = text;
                font = new Font("Bahnschrift", 12);

            }
            var size = e.Graphics.MeasureString(ch, font);
            
            
            if (isSelected)
            {
                col = layer.map.selectedColor;
            } else
            {
                col = visual.color;
            }
            Brush color = new SolidBrush(col);
            e.Graphics.DrawString(
                ch,
                font,
                color,
                p.X - (int) (size.Width / 2),
                p.Y - (int) (size.Height / 2)

            );
        }

        internal override MapObject findObject(GeoRect selectRect)
        {
            var graphics = layer.map.CreateGraphics();
            var scale = layer.map.scale;
            var ch = Convert.ToChar(visual.number).ToString();
            var font = new Font(visual.font, visual.size);
            var size = graphics.MeasureString(ch, font);

            GeoRect charRect = new GeoRect(
                location.x - size.Width / 2 / scale,
                location.y - size.Height / 2 / scale,
                location.x + size.Width / 2 / scale,
                location.y + size.Height / 2 / scale
                );

            if (GeoRect.isIntersect(selectRect, charRect))
            {
                return this;
            }

            return null;
        }

        internal override GeoRect getBounds()
        {
            return new GeoRect(location.x, location.y, location.x, location.y);
        }
    }
}
