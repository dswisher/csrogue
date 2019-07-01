
using System;
using System.Collections.Generic;

namespace csrogue
{
    public class GameMap
    {
        private const int RoomMaxHeight = 10;
        private const int RoomMaxWidth = 15;
        private const int RoomMinSize = 6;
        private const int MaxRooms = 40;
        private const int MaxMonstersPerRoom = 3;

        private Random chaos = new Random();    // TODO - allow a seed to be specified

        private Tile[,] tiles;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Tile this[int x, int y]
        {
            get
            {
                return tiles[x, y];
            }
        }

        public GameMap(int width, int height)
        {
            Width = width;
            Height = height;

            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new Tile();
                }
            }
        }

        public void MakeMap(EntityManager entityManager)
        {
            Logger.WriteLine("MakeMap, width={0}, height={1}", Width, Height);

            List<Rect> rooms = new List<Rect>();

            for (int r = 0; r < MaxRooms; r++)
            {
                // Random width and height
                int w = chaos.Next(RoomMinSize, RoomMaxWidth);
                int h = chaos.Next(RoomMinSize, RoomMaxHeight);

                // Random position
                int x = chaos.Next(0, Width - w - 1);
                int y = chaos.Next(0, Height - h - 1);

                // Make a room
                Rect newRoom = new Rect(x, y, w, h);

                bool ok = true;
                foreach (Rect other in rooms)
                {
                    if (newRoom.Intersects(other))
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    CreateRoom(newRoom);

                    Point center = newRoom.Center();

                    if (rooms.Count == 0)
                    {
                        entityManager.Player.Position = center;
                    }
                    else
                    {
                        Point prev = FindClosestRoom(rooms, center);

                        if (chaos.NextDouble() > 0.5)
                        {
                            // First move horizontally, then vertically
                            CreateHorizontalTunnel(prev.X, center.X, prev.Y);
                            CreateVerticalTunnel(prev.Y, center.Y, center.X);
                        }
                        else
                        {
                            // First move vertically, then horizontally
                            CreateVerticalTunnel(prev.Y, center.Y, prev.X);
                            CreateHorizontalTunnel(prev.X, center.X, center.Y);
                        }
                    }

                    PlaceEntities(entityManager, newRoom);

                    rooms.Add(newRoom);
                }
            }
        }

        private void PlaceEntities(EntityManager entityManager, Rect room)
        {
            // Place a random number of monsters
            int numberOfMonsters = chaos.Next(0, MaxMonstersPerRoom);

            for (int i = 0; i < numberOfMonsters; i++)
            {
                // Choose a random location in the room
                Point pos = new Point(chaos.Next(room.X1 + 1, room.X2 - 1), chaos.Next(room.Y1 + 1, room.Y2 - 1));

                if (!entityManager.IsOccupied(pos))
                {
                    Entity monster;
                    if (chaos.Next(0, 100) < 80)
                    {
                        // Add Orc
                        monster = new Entity(pos, 'o', ConsoleColor.Red, "Orc", true);
                    }
                    else
                    {
                        // Add troll
                        monster = new Entity(pos, 'T', ConsoleColor.Red, "Troll", true);
                    }

                    entityManager.AddNonPlayer(monster);
                }
            }
        }

        private Point FindClosestRoom(List<Rect> rooms, Point p)
        {
            Point closest = null;
            double closestDist = 1e6;   // arbitrary large number

            foreach (Rect r in rooms)
            {
                Point c = r.Center();
                double dist2 = c.DistanceTo2(p);

                if (dist2 < closestDist)
                {
                    closest = c;
                    closestDist = dist2;
                }
            }

            return closest;
        }

        private void CreateRoom(Rect rect)
        {
            for (int x = rect.X1 + 1; x < rect.X2; x++)
            {
                for (int y = rect.Y1 + 1; y < rect.Y2; y++)
                {
                    tiles[x, y].Blocked = false;
                    tiles[x, y].BlocksSight = false;
                }
            }
        }

        private void CreateHorizontalTunnel(int x1, int x2, int y)
        {
            for (int x = Math.Min(x1, x2); x < Math.Max(x1, x2) + 1; x++)
            {
                tiles[x, y].Blocked = false;
                tiles[x, y].BlocksSight = false;
            }
        }

        private void CreateVerticalTunnel(int y1, int y2, int x)
        {
            for (int y = Math.Min(y1, y2); y < Math.Max(y1, y2) + 1; y++)
            {
                tiles[x, y].Blocked = false;
                tiles[x, y].BlocksSight = false;
            }
        }
    }
}
