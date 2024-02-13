using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minigis_Surkov
{
    public class GeoRect
    {
        public double minX;
        public double minY;
        public double maxX;
        public double maxY;

        public GeoRect (double minX = 0, double minY = 0, double maxX = 0, double maxY = 0)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public GeoRect()
        {
            this.minX = 0;
            this.minY = 0;
            this.maxX = 0;
            this.maxY = 0;
        }

        public bool isExist()
        {
            return !(minX == 0 && minY == 0 && maxX == 0 && maxY == 0);
        }

        public static GeoRect union(GeoRect A, GeoRect B)
        {
            if (!A.isExist())
            {
                return B;
            }

            if (!B.isExist())
            {
                return A;
            }

            var _minX = Math.Min(A.minX, B.minX);
            var _minY = Math.Min(A.minY, B.minY);
            var _maxX = Math.Max(A.maxX, B.maxX);
            var _maxY = Math.Max(A.maxY, B.maxY);

            return new GeoRect(_minX, _minY, _maxX, _maxY);
        }

        private static bool checkIntersect(GeoRect A, GeoRect B)
        {
            if (A.minX <= B.minX && B.minX <= A.maxX && A.minY <= B.minY && B.minY <= A.maxY) { return true; }
            if (A.minX <= B.minX && B.minX <= A.maxX && A.minY <= B.maxY && B.maxY <= A.maxY) { return true; }
            if (A.minX <= B.maxX && B.maxX <= A.maxX && A.minY <= B.maxY && B.maxY <= A.maxY) { return true; }
            if (A.minX <= B.maxX && B.maxX <= A.maxX && A.minY <= B.minY && B.minY <= A.maxY) { return true; }

            return false;
        }

        public static bool isIntersect(GeoRect A, GeoRect B)
        {
            return checkIntersect(A, B) | checkIntersect(B, A);
        }

        public static bool isIntersect(GeoRect rect, GeoPoint point)
        {
            Console.WriteLine("point-x: " + point.x + " point-y: " + point.y);
            Console.WriteLine("min X: " + rect.minX + " Y: " + rect.minY);
            Console.WriteLine("max X: " + rect.maxX + " Y: " + rect.maxY);
            if (point.x < rect.minX) { return false; }
            if (point.x > rect.maxX) { return false; }
            if (point.y < rect.minY) { return false; }
            if (point.y > rect.maxY) { return false; }
                
            return true;
        }

        public GeoPoint getCenter()
        {
            return new GeoPoint((maxX + minX) / 2, (maxY + minY) / 2);
        }

    }
}
