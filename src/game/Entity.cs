using System;

namespace csrogue
{
    public class Entity
    {
        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }
        public char Glyph { get; set; }
        public ConsoleColor Color { get; set; }
        public Point Position { get; set; }

        public Entity(Point pos, char glyph, ConsoleColor color)
        {
            Position = pos;
            Glyph = glyph;
            Color = color;
        }

        public void Move(int dx, int dy)
        {
            Position.X += dx;
            Position.Y += dy;
        }
    }
}
