
namespace csrogue
{
    public class BasicMonster : IAi
    {
        public Entity Owner { get; set; }

        public BasicMonster()
        {
        }

        public void TakeTurn(Entity target, GameMap map, EntityManager entityManager    )
        {
            Entity monster = Owner;

            if (map[monster.X, monster.Y].Visible)
            {
                if (monster.Position.DistanceTo(target.Position) >= 2)
                {
                    monster.MoveTowards(target, map, entityManager);
                }
                else if (monster.Fighter.Hp > 0)
                {
                    // TODO - fight
                    Logger.WriteLine("The {0} insults you! Your ego is damaged!", Owner.Name);
                }
            }
        }
    }
}
