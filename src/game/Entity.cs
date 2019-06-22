using System;

namespace csrogue
{
    public class Entity
    {
        private static int nextId = 1;

        public string Id { get; private set; }

        public Entity()
        {
            Id = (nextId++).ToString();
        }
    }
}
