
namespace csrogue
{
    public class Tile
    {
        public bool Blocked { get; set; }
        public bool BlocksSight { get; set; }

        public Tile() : this(false, false) {}

        public Tile(bool blocked) : this(blocked, false) {}

        public Tile(bool blocked, bool blocksSight)
        {
            Blocked = blocked;
            BlocksSight = blocksSight;
        }
    }
}
