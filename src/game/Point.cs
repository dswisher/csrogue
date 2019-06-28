
using System;

namespace csrogue
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo2(Point other)
        {
            return Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
}
