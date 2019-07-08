using System;
namespace csrogue
{
    public class Fighter
    {
        public Entity Owner { get; set; }
        public int Hp { get; set; }
        public int Defense { get; set; }
        public int Power { get; set; }

        public Fighter(int hp, int defense, int power)
        {
            Hp = hp;
            Defense = defense;
            Power = power;
        }
    }
}
