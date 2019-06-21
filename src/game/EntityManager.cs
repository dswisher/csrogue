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
    }
}
