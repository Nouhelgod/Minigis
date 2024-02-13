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

        public GridGeometry (int countX, int countY, double distance, double originX, double originY)
        {
            this.countX = countX;
            this.countY = countY;
            this.distance = distance;
            this.originX = originX;
            this.originY = originY;
        }
    }
}