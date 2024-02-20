namespace Minigis_Surkov
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
        public double[,] nodeValues;
        public GridGeometry (int countX, int countY, double distance, double originX, double originY)
        {
            this.countX = countX;
            this.countY = countY;
            this.distance = distance;
            this.originX = originX;
            this.originY = originY;

            this.nodeCoords = new GeoPoint[countX, countY];

            for (int x = 0; x < countX; x++)
            {
                for (int y = 0; y < countY; y++)
                {
                    double x_ = originX + distance * x;
                    double y_ = originY + distance * y;
                    nodeCoords[x, y] = new GeoPoint(x_, y_);
                }
            }
        }

        public double[] findNearest(GeoPoint point)
        {
            double[] nearest = new double[4];
            for (int x = 0; x < countX; x++)
            {
                for (int y = 0; y < countY; y++)
                {
                    double nextNodeX = double.NaN;
                    double nextNodeY = double.NaN;
                    double nodeX = nodeCoords[x, y].x;
                    double nodeY = nodeCoords[x, y].y;

                    if (y + 1 != countY && x + 1 != countX)
                    {
                        nextNodeX = nodeCoords[x + 1, y].x;
                        nextNodeY = nodeCoords[x, y + 1].y;
                    }

                    if (point.x >= nodeX && nextNodeX >= point.x)
                    {
                        nearest[0] = nodeX;
                        nearest[1] = nextNodeX;
                    }

                    if (point.y >= nodeY && nextNodeY >= point.y)
                    {
                        nearest[2] = nodeY;
                        nearest[3] = nextNodeY;
                    }
                }
            }
            return nearest;
        }
    }
}