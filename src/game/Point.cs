
using System;

namespace csrogue
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Point Zero
        {
            get { return new Point(0, 0); }
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Delta(int dx, int dy)
        {
            return new Point(X + dx, Y + dy);
        }

        public double DistanceTo2(Point other)
        {
            return Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2);
        }

        public double DistanceTo(Point other)
        {
            return Math.Sqrt(DistanceTo2(other));
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return other != null && X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X + 5000 * Y;
        }
    }
}
