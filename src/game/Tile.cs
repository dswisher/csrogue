
namespace csrogue
{
    public class Tile
    {
        public bool Blocked { get; set; }
        public bool BlocksSight { get; set; }
        public bool Visible { get; set; }
        public bool Explored { get; set; }

        public Tile() : this(true, false) {}

        public Tile(bool blocked) : this(blocked, false) {}

        public Tile(bool blocked, bool blocksSight)
        {
            Blocked = blocked;
            BlocksSight = blocksSight;
        }
    }
}
