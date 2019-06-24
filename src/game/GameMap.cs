
using System;
using System.Collections.Generic;

namespace csrogue
{
    public class GameMap
    {
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

        public Point MakeMap()
        {
            const int roomMaxSize = 10;
            const int roomMinSize = 6;
            const int maxRooms = 30;

            Logger.WriteLine("MakeMap, width={0}, height={1}", Width, Height);

            Random chaos = new Random();
            List<Rect> rooms = new List<Rect>();
            Point playerPosition = null;

            for (int r = 0; r < maxRooms; r++)
            {
                // Random width and height
                int w = chaos.Next(roomMinSize, roomMaxSize);
                int h = chaos.Next(roomMinSize, roomMaxSize);

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
                        playerPosition = center;
                    }
                    else
                    {
                        Point prev = rooms[rooms.Count - 1].Center();

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

                    rooms.Add(newRoom);
                }
            }

            return playerPosition;
        }

        public void CreateRoom(Rect rect)
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
            for (int x = Math.Min(x1, x2); x < Math.Max(x1, x2); x++)
            {
                tiles[x, y].Blocked = false;
                tiles[x, y].BlocksSight = false;
            }
        }

        private void CreateVerticalTunnel(int y1, int y2, int x)
        {
            for (int y = Math.Min(y1, y2); y < Math.Max(y1, y2); y++)
            {
                tiles[x, y].Blocked = false;
                tiles[x, y].BlocksSight = false;
            }
        }
    }
}
