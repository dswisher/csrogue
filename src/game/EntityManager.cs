using System;
using System.Collections.Generic;

namespace csrogue
{
    public class EntityManager
    {
        private List<Entity> entities = new List<Entity>();

        public EntityManager()
        {
        }

        public Entity CreateEntity()
        {
            Entity entity = new Entity();

            entities.Add(entity);

            return entity;
        }

        public IEnumerable<Entity> FindEntities()
        {
            foreach (Entity entity in entities)
            {
                // TODO - filter
                yield return entity;
            }
        }
    }
}
