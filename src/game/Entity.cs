using System;

namespace csrogue
{
    public class Entity
    {
        private Fighter fighter;
        private IAi ai;

        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }
        public char Glyph { get; set; }
        public ConsoleColor Color { get; set; }
        public Point Position { get; set; }
        public bool Blocks { get; set; }
        public string Name { get; set; }

        public Entity(Point pos, char glyph, ConsoleColor color, string name, bool blocks)
        {
            Position = pos;
            Glyph = glyph;
            Color = color;
            Name = name;
            Blocks = blocks;
        }

        public void Move(int dx, int dy)
        {
            Position.X += dx;
            Position.Y += dy;
        }

        public void MoveTowards(Entity target, GameMap map, EntityManager entityManager)
        {
            double distance = Position.DistanceTo(target.Position);
            int dx = (int)Math.Round((target.Position.X - Position.X) / distance);
            int dy = (int)Math.Round((target.Position.Y - Position.Y) / distance);

            Point dest = new Point(Position.X + dx, Position.Y + dy);

            // TODO - add point-based map accessor!
            if (!(map[dest.X, dest.Y].Blocked || entityManager.IsOccupied(dest)))
            {
                Move(dx, dy);
            }
        }

        public Fighter Fighter {
            get
            {
                return fighter;
            }

            set
            {
                fighter = value;
                fighter.Owner = this;
            }
        }

        public IAi Ai
        {
            get
            {
                return ai;
            }

            set
            {
                ai = value;
                ai.Owner = this;
            }
        }
    }
}
