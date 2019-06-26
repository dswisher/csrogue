using System;
using System.Collections.Generic;

namespace csrogue
{
    public class Game
    {
        private int height;
        private int width;
        private GameMap map;
        private Renderer renderer;
        private FieldOfView fov;

        public Game(Renderer renderer)
        {
            this.renderer = renderer;

            // TODO - width and height should be independent of screen size
            height = renderer.Height;
            width = renderer.Width;

            map = new GameMap(width, height);
            fov = new FieldOfView();
        }

        public void Run()
        {
            Point playerPosition = map.MakeMap();
            Point npcPosition = new Point(playerPosition.X - 2, playerPosition.Y);

            Entity player = new Entity(playerPosition, '@', ConsoleColor.White);
            Entity npc = new Entity(npcPosition, '@', ConsoleColor.Yellow);

            List<Entity> entities = new List<Entity> { player, npc };

            bool done = false;
            while (!done)
            {
                // Reset current FOV and calc new
                const int radius = 5;

                // TODO - only do this after move
                fov.Calc(map, playerPosition, radius);

                // Draw everything
                renderer.RenderAll(entities, map);

                // Get a key
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Clear everything
                // TODO - only clear on move
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
            }

        }
    }
}
