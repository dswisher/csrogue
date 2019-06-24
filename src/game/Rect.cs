
namespace csrogue
{
    public class Rect
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public Rect(int x, int y, int width, int height)
        {
            X1 = x;
            Y1 = y;
            X2 = x + width;
            Y2 = y + height;
        }

        public Point Center()
        {
            int x = (X1 + X2) / 2;
            int y = (Y1 + Y2) / 2;

            return new Point(x, y);
        }

        public bool Intersects(Rect other)
        {
            return X1 < other.X2 && X2 > other.X1 && Y1 < other.Y2 && Y2 > other.Y1;
        }
    }
}
