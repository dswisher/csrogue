
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

            // TODO - hack in a test map
            tiles[30, 22].Blocked = true;
            tiles[30, 22].BlocksSight = true;

            tiles[31, 22].Blocked = true;
            tiles[31, 22].BlocksSight = true;

            tiles[32, 22].Blocked = true;
            tiles[32, 22].BlocksSight = true;
        }
    }
}
