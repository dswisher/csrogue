using System;
namespace csrogue
{
    public class FieldOfView
    {
        public FieldOfView()
        {
        }

        public void Calc(GameMap map, Point pos)
        {
            // Reset current FOV
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map[x, y].Visible = false;
                }
            }

            // Calculate new FOV
            // TODO - this is a hack
            for (int x = pos.X - 2; x <= pos.X + 2; x++)
            {
                for (int y = pos.Y - 2; y <= pos.Y + 2; y++)
                {
                    if (x >= 0 && y >= 0 && x < map.Width && y < map.Height)
                    {
                        map[x, y].Visible = true;
                        map[x, y].Explored = true;
                    }
                }
            }
        }
    }
}
