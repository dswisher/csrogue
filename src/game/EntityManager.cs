
using System.Collections.Generic;

namespace csrogue
{
    public class EntityManager
    {
        private List<Entity> entities = new List<Entity>();

        public IReadOnlyCollection<Entity> Entities
        {
            get { return entities; }
        }

        public Entity Player { get; private set; }

        public void AddPlayer(Entity player)
        {
            Player = player;
            entities.Add(player);
        }

        public void AddNonPlayer(Entity entity)
        {
            entities.Add(entity);
        }

        public void Reset()
        {
            entities.Clear();
            entities.Add(Player);
        }

        public bool IsOccupied(Point pos)
        {
            foreach (Entity e in entities)
            {
                // TODO - implement equals operator on Point!
                if (pos.X == e.Position.X && pos.Y == e.Position.Y)
                {
                    return true;
                }
            }

            return false;
        }

        public Entity GetBlockingEntitiesAtLocation(Point pos)
        {
            foreach (Entity e in entities)
            {
                // TODO - implement equals operator on Point!
                if (e.Blocks && pos.X == e.Position.X && pos.Y == e.Position.Y)
                {
                    return e;
                }
            }

            return null;
        }
    }
}
