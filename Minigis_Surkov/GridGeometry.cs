﻿namespace Minigis_Surkov
{
    public class GridGeometry
    {
        public int countX {  get; }
        public int countY { get; }
        public double distance {  get; }
        public double originX { get; }
        public double originY { get; }
        public double maxX => originX + distance * (countX - 1);
        public double maxY => (originY + distance * (countY - 1));

        public GeoPoint[,] nodeCoords;
        public double?[,] nodeValues;
        public GridGeometry (int countX, int countY, double distance, double originX, double originY)
        {
            this.countX = countX;
            this.countY = countY;
            this.distance = distance;
            this.originX = originX;
            this.originY = originY;

            // this.nodeCoords = new GeoPoint[countX, countY];
            this.nodeValues = new double?[countX, countY];

            //for (int x = 0; x < countX; x++)
            //{
            //    for (int y = 0; y < countY; y++)
            //    {
            //        double x_ = originX + distance * x;
            //        double y_ = originY + distance * y;
            //        nodeCoords[x, y] = new GeoPoint(x_, y_);
            //    }
            //}
        }

        public double[] findNearest(GeoPoint point)
        {

            double[] nearest = new double[4];

            // ===  

            //double nodeX = originX;
            //double nodeY = originY;

            //while (nodeX < point.x )
            //{
            //    nearest[0] = nodeX;
            //    nodeX += distance;
            //    nearest[1] = nodeX;
            //}

            //while (nodeY < point.y)
            //{
            //    nearest[2] = nodeY;
            //    nodeY += distance;
            //    nearest[3] = nodeY;
            //}


            //(x - xmin) / dist

            double dX = point.x - originX;
            double dY = point.y - originY;
            nearest[0] = dX / distance;
            nearest[1] = nearest[0] + distance;
            nearest[2] = dY / distance;
            nearest[3] = nearest[2] + distance;

            return nearest;
        }
    }
}