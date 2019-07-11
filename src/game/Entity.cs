using System;
using System.Collections.Generic;

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

        public void MoveTo(Point p)
        {
            Position = p;
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

        public void MoveAStar(Entity target, GameMap map, EntityManager entityManager)
        {
            // Get the distance to the point
            Func<Point, Point, double> distance = (a, b) => a.DistanceTo(b);

            // Estimate distance to target
            Func<Point, double> estimate = a => a.DistanceTo(target.Position);

            // Get the path
            Path<Point> path = AStar.FindPath(Position, target.Position, distance, estimate, x => GetNeighbors(map, entityManager, target.Position, x));

            if (path != null)
            {
                Logger.WriteLine("Found a path! Moving from {0} to {1}!", Position, path.FirstStep);
                MoveTo(path.FirstStep);
            }
        }

        private IEnumerable<Point> GetNeighbors(GameMap map, EntityManager entityManager, Point target, Point point)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    Point delta = point.Delta(dx, dy);

                    bool occupied = false;
                    if (entityManager.IsOccupied(delta) && (delta.X != target.X || delta.Y != target.Y))
                    {
                        occupied = true;
                    }

                    // TODO - add point-based accessor to map
                    if (!map[delta.X, delta.Y].Blocked && !occupied)
                    {
                        yield return delta;
                    }
                }
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
