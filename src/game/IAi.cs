
namespace csrogue
{
    // TODO - better name for this!
    public interface IAi
    {
        Entity Owner { get; set; }

        void TakeTurn(Entity target, GameMap map, EntityManager entityManager);
    }
}
