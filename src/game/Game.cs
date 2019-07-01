using System;
using System.Collections.Generic;

namespace csrogue
{
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn
    }

    public class Game
    {
        private int height;
        private int width;
        private GameMap map;
        private Renderer renderer;
        private FieldOfView fov;
        private bool redraw;
        private EntityManager entityManager;
        private Entity player;
        private bool done;
        private GameState gameState;

        public Game(Renderer renderer)
        {
            this.renderer = renderer;

            // TODO - width and height should be independent of screen size
            height = renderer.Height;
            width = renderer.Width;

            map = new GameMap(width, height);
            fov = new FieldOfView();

            player = new Entity(Point.Zero, '@', ConsoleColor.White, "Player", true);
            entityManager = new EntityManager();
            entityManager.AddPlayer(player);

            map.MakeMap(entityManager);
        }

        public void Run()
        {
            done = false;
            gameState = GameState.PlayerTurn;

            while (!done)
            {
                switch (gameState)
                {
                    case GameState.PlayerTurn:
                        PlayerTurn();
                        gameState = GameState.EnemyTurn;
                        break;

                    case GameState.EnemyTurn:
                        foreach (Entity entity in entityManager.Entities)
                        {
                            if (entity != entityManager.Player)
                            {
                                // TODO - let the mob take its turn
                            }
                        }
                        gameState = GameState.PlayerTurn;
                        break;
                }
            }
        }

        private void PlayerTurn()
        {
            // Reset current FOV and calc new
            const int radius = 6;

            // TODO - only do this after move
            fov.Calc(map, player.Position, radius);

            // Draw everything
            renderer.RenderAll(entityManager, map, redraw);

            redraw = false;

            // Get a key
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // Clear everything
            // TODO - only clear on move
            renderer.ClearAll(entityManager.Entities);

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

                case ConsoleKey.Spacebar:
                    // TODO - make this a "command", invoked by :explore or some such
                    ExploreAll();
                    break;

                case ConsoleKey.R:
                    // TODO - make this a "command", invoked by :redraw or some such
                    redraw = true;
                    break;

                case ConsoleKey.N:
                    // TODO - make this a "command", invoked by :newmap or some such
                    ResetAllTiles();
                    entityManager.Reset();
                    map.MakeMap(entityManager);
                    redraw = true;
                    break;
            }

            // Move?
            if (dx != 0 || dy != 0)
            {
                // TODO - better way of constructing point + delta
                Point newPos = new Point(player.X + dx, player.Y + dy);

                // TODO - add point-based map accessor
                if (!map[player.X + dx, player.Y + dy].Blocked)
                {
                    Entity mob = entityManager.GetBlockingEntitiesAtLocation(newPos);

                    if (mob != null)
                    {
                        // TODO - need a better way to show output to the player!
                        Console.Write("You kick the {0} in the shins.", mob.Name);
                    }
                    else
                    {
                        player.Move(dx, dy);
                    }
                }
            }
        }

        private void ExploreAll()
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map[x, y].Explored = true;
                }
            }
        }

        private void ResetAllTiles()
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map[x, y].Reset();
                }
            }
        }
    }
}
