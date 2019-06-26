
namespace csrogue
{
    // Recursive shadow-casting based on rot.js implementation:
    //    https://github.com/ondras/rot.js/blob/master/src/fov/recursive-shadowcasting.ts
    public class FieldOfView
    {
        private readonly Octant[] octants;

        public FieldOfView()
        {
            octants = new Octant[8];
            octants[0] = new Octant(-1, 0, 0, 1);
            octants[1] = new Octant(0, -1, 1, 0);
            octants[2] = new Octant(0, -1, -1, 0);
            octants[3] = new Octant(-1, 0, 0, -1);
            octants[4] = new Octant(1, 0, 0, -1);
            octants[5] = new Octant(0, 1, -1, 0);
            octants[6] = new Octant(0, 1, 1, 0);
            octants[7] = new Octant(1, 0, 0, 1);
        }

        public void Calc(GameMap map, Point pos, int radius)
        {
            // Reset current FOV
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map[x, y].Visible = false;
                }
            }

            // You can always see your own tile
            Visible(map[pos.X, pos.Y]);

            foreach (var octant in octants)
            {
                RenderOctant(map, pos, octant, radius);
            }
        }

        private void RenderOctant(GameMap map, Point pos, Octant octant, int radius)
        {
            // Radius incremented by 1 to provide same coverage area as other shadowcasting radiuses
            CastVisibility(map, pos, 1, 1.0, 0.0, radius + 1, octant);
        }

        private void CastVisibility(GameMap map, Point pos, int row,
                                    double visSlopeStart, double visSlopeEnd,
                                    int radius, Octant octant)
        {
            if (visSlopeStart < visSlopeEnd)
            {
                return;
            }

            for (int i = row; i <= radius; i++)
            {
                int dx = -i - 1;
                int dy = -i;
                bool blocked = false;
                double newStart = 0;

                // 'Row' could be column, names here assume octant 0 and would be flipped for half the octants
                while (dx <= 0)
                {
                    dx += 1;

                    // Translate from relative coordinates to map coordinates
                    int mapX = pos.X + dx * octant.XX + dy * octant.XY;
                    int mapY = pos.Y + dx * octant.YX + dy * octant.YY;

                    // Range of the row
                    double slopeStart = (dx - 0.5) / (dy + 0.5);
                    double slopeEnd = (dx + 0.5) / (dy - 0.5);

                    // Ignore if not yet at left edge of Octant
                    if (slopeEnd > visSlopeStart) 
                    {
                        continue;
                    }

                    // Done if past right edge
                    if (slopeStart < visSlopeEnd)
                    {
                        break;
                    }

                    // If it's in range, it's visible
                    if ((dx * dx + dy * dy) < (radius * radius))
                    {
                        Visible(map[mapX, mapY]);
                    }

                    if (!blocked)
                    {
                        // If tile is a blocking tile, cast around it
                        if (map[mapX, mapY].Blocked && i < radius)
                        {
                            blocked = true;
                            CastVisibility(map, pos, i + 1, visSlopeStart, slopeStart, radius, octant);
                            newStart = slopeEnd;
                        }
                    }
                    else
                    {
                        // Keep narrowing if scanning across a block
                        if (map[mapX, mapY].Blocked)
                        {
                            newStart = slopeEnd;
                            continue;
                        }

                        // Block has ended
                        blocked = false;
                        visSlopeStart = newStart;
                    }
                }

                if (blocked)
                {
                    break;
                }

            }
        }

        private void Visible(Tile tile)
        {
            tile.Visible = true;
            tile.Explored = true;
        }


        private class Octant
        {
            public int XX { get; private set; }
            public int XY { get; private set; }
            public int YX { get; private set; }
            public int YY { get; private set; }

            public Octant(int xx, int xy, int yx, int yy)
            {
                XX = xx;
                XY = xy;
                YX = yx;
                YY = yy;
            }

            public override string ToString()
            {
                return string.Format("[{0},{1},{2},{3}]", XX, XY, YX, YY);
            }
        }
    }
}
