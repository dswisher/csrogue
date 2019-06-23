using System;
using System.Collections.Generic;

namespace csrogue
{
    public class Game
    {
        private int height;
        private int width;
        private GameMap map;

        public Game(int height, int width)
        {
            this.height = height;
            this.width = width;

            map = new GameMap(this.width, this.height);
        }

        public void Run()
        {
            Renderer renderer = new Renderer();

            Entity player = new Entity(width / 2, height / 2, '@', ConsoleColor.White);
            Entity npc = new Entity(width / 2 - 5, height / 2 - 5, '@', ConsoleColor.Yellow);

            List<Entity> entities = new List<Entity> { player, npc };

            renderer.RenderAll(entities, map);

            bool done = false;
            while (!done)
            {
                // Get a key
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Clear everything
                renderer.ClearAll(entities);

                int dx = 0;
                int dy = 0;

                // Process the input
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        done = true;
                        break;

                    case ConsoleKey.H:
                    case ConsoleKey.LeftArrow:
                        dx = -1;
                        break;

                    case ConsoleKey.J:
                    case ConsoleKey.DownArrow:
                        dy = 1;
                        break;

                    case ConsoleKey.K:
                    case ConsoleKey.UpArrow:
                        dy = -1;
                        break;

                    case ConsoleKey.L:
                    case ConsoleKey.RightArrow:
                        dx = 1;
                        break;
                }

                // Move?
                if (dx != 0 || dy != 0)
                {
                    if (!map[player.X + dx, player.Y + dy].Blocked)
                    {
                        player.Move(dx, dy);
                    }
                }

                // Render the updated entities
                renderer.RenderAll(entities, map);
            }

        }
    }
}
