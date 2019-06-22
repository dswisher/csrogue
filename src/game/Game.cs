using System;
using System.Collections.Generic;

namespace csrogue
{
    public class Game
    {
        private int height;
        private int width;

        public Game(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public void Run()
        {
            Renderer renderer = new Renderer();

            Entity player = new Entity(width / 2, height / 2, '@', ConsoleColor.White);
            Entity npc = new Entity(width / 2 - 5, height / 2 - 5, '@', ConsoleColor.Yellow);

            List<Entity> entities = new List<Entity> { player, npc };

            renderer.RenderAll(entities);

            bool done = false;
            while (!done)
            {
                // Get a key
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Clear everything
                renderer.ClearAll(entities);

                // Process the input
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        done = true;
                        break;

                    case ConsoleKey.H:
                    case ConsoleKey.LeftArrow:
                        if (player.X > 0)
                        {
                            player.Move(-1, 0);
                        }
                        break;

                    case ConsoleKey.J:
                    case ConsoleKey.DownArrow:
                        if (player.Y < height - 1)
                        {
                            player.Move(0, 1);
                        }
                        break;

                    case ConsoleKey.K:
                    case ConsoleKey.UpArrow:
                        if (player.Y > 0)
                        {
                            player.Move(0, -1);
                        }
                        break;

                    case ConsoleKey.L:
                    case ConsoleKey.RightArrow:
                        if (player.X < width - 1)
                        {
                            player.Move(1, 0);
                        }
                        break;
                }

                // Render the updated entities
                renderer.RenderAll(entities);
            }

        }
    }
}
