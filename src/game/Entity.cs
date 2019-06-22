using System;

namespace csrogue
{
    public class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Glyph { get; set; }
        public ConsoleColor Color { get; set; }

        public Entity(int x, int y, char glyph, ConsoleColor color)
        {
            X = x;
            Y = y;
            Glyph = glyph;
            Color = color;
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
