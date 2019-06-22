using System;

namespace csrogue
{
    public class RenderSystem
    {
        private readonly EntityManager entityManager;

        public RenderSystem(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public void Process()
        {
            // Get all entities that have a position component
            // TODO
        }
    }
}
