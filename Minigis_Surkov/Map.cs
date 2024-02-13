using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public partial class Map : UserControl
    {
        public LayerControl layerControl;
        public GeoPoint center = new GeoPoint();
        public double scale = 1;
        public List<Layer> layers = new List<Layer>();
        static public ActiveTool tool = ActiveTool.Pan;

        private System.Drawing.Point mouseDownPosition;
        private System.Drawing.Point mouseDownStartPosition;
        private bool isMouseDown;

        public Color selectedColor = Color.Green; 
        private const int SHAKE_DIST = 10;
        private const int SEARCH_RECT_DIST = 2;
        private List<MapObject> objectsCollection = new List<MapObject>();
        private ActiveTool lastTool;
        VectorLayer measures = new VectorLayer("📏 Measures");

        static GridGeometry testGeometry = new GridGeometry(2, 2, 8, 1, -1);
        GridLayer testGridLayer = new GridLayer(testGeometry, _name: "test-grid");
        

        public Map()
        {
            InitializeComponent();

            // Testing
            {
                VectorLayer testLayer = new VectorLayer();
                VectorLayer polygons = new VectorLayer(Name="Polygons");
                VectorLayer lines = new VectorLayer(Name="Lines");
                VectorLayer points = new VectorLayer(Name="Points");
                

                Line line1 = new Line(100, 0, 100, 100);
                Line line2 = new Line(100, 100, 0, 100);
                lines.append(line1);
                lines.append(line2);

                GeoPoint gp1 = new GeoPoint(150, 100);
                GeoPoint gp2 = new GeoPoint(310, 90);
                GeoPoint gp3 = new GeoPoint(90, 310);
                PLine pline1 = new PLine();
                pline1.append(gp1);
                pline1.append(gp2);
                pline1.append(gp3);
                lines.append(pline1);

                GeoPoint pgp1 = new GeoPoint(-100, 0);
                GeoPoint pgp2 = new GeoPoint(-100, -100);
                GeoPoint pgp3 = new GeoPoint(0, -100);
                GeoPoint pgp4 = new GeoPoint(0, 0);
                Polygon polygon1 = new Polygon();
                polygon1.append(pgp1);
                polygon1.append(pgp2);
                polygon1.append(pgp3);
                polygon1.append(pgp4);
                polygons.append(polygon1);

                Point p1 = new Point(0, 0);
                points.append(p1);


                GeoPoint cordXStart = new GeoPoint(-200, 0);
                GeoPoint cordXEnd = new GeoPoint(200, 0);
                GeoPoint cordYStart = new GeoPoint(0, -200);
                GeoPoint cordYEnd = new GeoPoint(0, 200);

                PLine cordX = new PLine();
                cordX.append(cordXStart);
                cordX.append(cordXEnd);

                PLine cordY = new PLine();
                cordY.append(cordYStart);
                cordY.append(cordYEnd);

                lines.append(cordX);
                lines.append(cordY);
                            

                Polygon complexPolygon = new Polygon();
                complexPolygon.append(new GeoPoint(-100 +200, 100));
                complexPolygon.append(new GeoPoint(100 + 200, 100));
                complexPolygon.append(new GeoPoint(100 + 200, 50));
                complexPolygon.append(new GeoPoint(0 + 200, 100));
                complexPolygon.append(new GeoPoint(-50 + 200, 25));
                complexPolygon.append(new GeoPoint(100 + 200, 25));
                complexPolygon.append(new GeoPoint(100 + 200, -25));
                complexPolygon.append(new GeoPoint(-100 + 200, -50));

                polygons.append(complexPolygon);


                testGridLayer.mesh = new double?[2, 2]{
                    {100, 400}, 
                    {200, 700}
                };
                testGridLayer.findMinMaxNodeValue();

                testLayer.map = this;
                appendLayer(testLayer);
                appendLayer(points);
                appendLayer(lines);
                appendLayer(polygons);
                appendLayer(measures);
                appendLayer(testGridLayer);
                refreshLayerList();


            }
            
            Refresh();
        }

        public System.Drawing.Point translateMapToScreen(GeoPoint geoPoint) 
        {
            int x = (int)(scale * (-center.x + geoPoint.x) + Width / 2 +0.5);
            int y = (int)(-scale * (geoPoint.y - center.y) + Height / 2+ 0.5);
            return new System.Drawing.Point(x, y);
        }

        public GeoPoint translateScreenToMap(System.Drawing.Point screenPoint) 
        {
            double x = (screenPoint.X - Width / 2) / scale + center.x;
            double y = -(screenPoint.Y - Height / 2) / scale + center.y;
            return new GeoPoint(x, y);
        }

        public void appendLayer(Layer obj)
        {
            layers.Add(obj);
            obj.map = this;
            refreshLayerList();
        }

        public void insertLayerAt(int index, Layer obj)
        {
            layers.Insert(index, obj);
            obj.map = this;
            refreshLayerList();
        }

        public void popLayer()
        {
            layers.Remove(layers.Last());
            refreshLayerList();
        }

        public void popLayerAt(int position)
        {
            layers.RemoveAt(position);
            refreshLayerList();
        }

        private void refreshLayerList() { 
            if (layerControl != null) { 
                layerControl.refreshList(); 
            }
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            foreach (Layer layer in layers)
            {
                if (layer.isVisible) { layer.drawLayer(e); }
            }
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            
            
            if (e.Button == MouseButtons.Middle)
            {
                mouseDownPosition = e.Location;
                lastTool = tool;
                tool = ActiveTool.Pan;
            }

            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            
            if (!isMouseDown)
            {
                mouseDownStartPosition = e.Location;
            }
            isMouseDown = true;
            mouseDownPosition = e.Location;
        }

        private void clearSelection()
        {
            foreach (MapObject mo in objectsCollection)
            {
                mo.isSelected = false;
                Refresh();
            }
            objectsCollection.Clear();
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Middle)
            {
                panTool(e);
            }
            if (!isMouseDown)
            {
                return;
            }

            switch (tool)
            {
                case ActiveTool.Pan:
                    panTool(e);
                    break;
                                   
                case ActiveTool.ZoomIn:
                    break;

                case ActiveTool.ZoomOut:
                    break;

                case ActiveTool.Select:
                    break;

                default:
                    break;
            }
        }

        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

            if (e.Button == MouseButtons.Middle)
            {
                tool = lastTool;
            }


            switch (tool)
            {
                case ActiveTool.ZoomIn:
                    zoomInTool(e);
                    break;

                case ActiveTool.ZoomOut:
                    zoomOutTool(e);
                    break;

                case ActiveTool.Select:
                    selectTool(e);
                    break;

                case ActiveTool.Measure:
                    measureTool(e);
                    break;

                case ActiveTool.GetValue:
                    getValueTool(e);
                    break;

                default:
                    break;
            }

            if (e.Button == MouseButtons.Right)
            {
                clearSelection();
            }
        }

        private void Map_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        // Tools
        private void getValueTool(MouseEventArgs e)
        {
            GeoPoint location = translateScreenToMap(e.Location);

            foreach(Layer layer in layers)
                if (layer is GridLayer)
                {
                    {
                        GridLayer gridLayer = (GridLayer)layer;
                        Console.WriteLine(gridLayer.getValue(location));
                    }
                }
        }

        private void measureTool(MouseEventArgs e)
        {
            { 
                GeoPoint first = translateScreenToMap(mouseDownPosition);
                GeoPoint last = translateScreenToMap(e.Location);
                                
                double minX = Math.Min(first.x, last.x);
                double maxX = Math.Max(first.x, last.x);
                double minY = Math.Min(first.y, last.y);
                double maxY = Math.Max(first.y, last.y);

                double k1 = maxX - minX;
                double k2 = maxY - minY;

                double centerX = (minX + maxX) / 2;
                double centerY = (minY + maxY) / 2;

                double distance = Math.Sqrt(k1 * k1 + k2 * k2);

                Line measureLine = new Line(first, last, type_: "Measure");
                Point measureLabel = new Point(centerX, centerY, text_: Math.Round(distance, 2).ToString() );

                measures.append(measureLine);
                measures.append(measureLabel);

                refreshLayerList();
            }
        }


        private void selectTool(MouseEventArgs e)
        {
            var cords = translateScreenToMap(e.Location);
            GeoRect selectorZone = new GeoRect(
                cords.x - SEARCH_RECT_DIST / scale, 
                cords.y - SEARCH_RECT_DIST / scale, 
                cords.x + SEARCH_RECT_DIST / scale, 
                cords.y + SEARCH_RECT_DIST / scale
                );

            for (int i = layers.Count - 1; i >= 0; i--)
            {
                if (!layers[i].isVisible) { break; }

                var t = layers[i].findObject(selectorZone);
                
                if (t != null)
                {
                    if (!ModifierKeys.HasFlag(Keys.Control))
                    {
                        clearSelection();
                    }

                    foreach (MapObject mo in objectsCollection)
                    {
                        if (!mo.Equals(t))
                        {
                            objectsCollection.Add(mo);
                        } else
                        {
                            mo.isSelected = false;
                            objectsCollection.Remove(mo);
                            Refresh();
                        }
                        break;
                    }

                    objectsCollection.Add(t);
                    break;
                }
            }

            foreach (MapObject mo in objectsCollection)
            {
                mo.isSelected = true;
                Refresh();
            }
        }

        private void panTool(MouseEventArgs e)
        {
            double deltaX = (e.X - mouseDownPosition.X) / scale;
            double deltaY = (e.Y - mouseDownPosition.Y) / scale;
            center.x -= deltaX;
            center.y += deltaY;
            Refresh();
            mouseDownPosition = e.Location;
        }

        private void zoomInTool(MouseEventArgs e, bool restore = false)
        {

            int deltaX = Math.Abs(mouseDownPosition.X - e.Location.X);
            int deltaY = Math.Abs(mouseDownPosition.Y - e.Location.Y);

            if (deltaX < SHAKE_DIST && deltaY < SHAKE_DIST)
            {
                center = translateScreenToMap(e.Location);
                scale *= 1.25;
                Refresh();
            }

            else
            {
                double x_, y_;

                x_ = (mouseDownPosition.X + e.Location.X) / 2;
                y_ = (mouseDownPosition.Y + e.Location.Y) / 2;

                center = translateScreenToMap(new System.Drawing.Point((int)x_, (int)y_));

                double dx = (Width / Math.Abs(e.Location.X - mouseDownPosition.X));
                double dy = (Height / Math.Abs(e.Location.Y - mouseDownPosition.Y));

                double s_ = Math.Min(dx, dy);
                scale *= s_ * .95;
                Refresh();
            }

            redrawGridLayers();
        }

        private void redrawGridLayers()
        {
            foreach (Layer layer in layers)
            {
                if (layer is GridLayer)
                {
                    GridLayer gridLayer = (GridLayer)layer;
                    gridLayer.createBitmap();
                }
            }
        }

        private void zoomOutTool(MouseEventArgs e)
        {
            mouseDownPosition = e.Location;

            int deltaX = Math.Abs(mouseDownPosition.X - e.Location.X);
            int deltaY = Math.Abs(mouseDownPosition.Y - e.Location.Y);

            if (deltaX < SHAKE_DIST && deltaY < SHAKE_DIST)
            {
                center = translateScreenToMap(e.Location);
                scale *= .75;
                Refresh();
            }
        }

        private GeoRect bounds = new GeoRect();

        public GeoRect Bounds
        {
            get
            {
                foreach (Layer layer in layers)
                {
                    if (!layer.isVisible) { break; }
                    bounds = GeoRect.union(bounds, layer.Bounds);
                }
                return bounds;
            }
        }

        public void zoomAll()
        {
            GeoRect bounds = Bounds;
            double x_ = (bounds.maxX + bounds.minX) / 2;
            double y_ = (bounds.maxY + bounds.minY) / 2;

            center.x = x_;
            center.y = y_;

            GeoPoint c1 = new GeoPoint(bounds.maxX, bounds.maxY);
            GeoPoint c2 = new GeoPoint(bounds.minX, bounds.minY);

            double dx = ((double)Width / Math.Abs(c1.x - c2.x));
            double dy = ((double)Height / Math.Abs(c1.y - c2.y));

            double s_ = Math.Min(dx, dy);
            scale = s_;
            Refresh();
        }


    }
}
