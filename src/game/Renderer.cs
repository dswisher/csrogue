using System;
using System.Collections.Generic;

namespace csrogue
{
    public class Renderer
    {
        private ConsoleColor defaultBackground;
        private ConsoleColor defaultForeground;
        private readonly Cell[,] cells;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Renderer()
        {
            defaultBackground = Console.BackgroundColor;
            defaultForeground = Console.ForegroundColor;

            Width = Console.WindowWidth;
            Height = Console.WindowHeight;

            cells = new Cell[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    cells[x, y] = new Cell();
                }
            }
        }

        public void Initialize()
        {
            Console.Clear();
            Console.CursorVisible = false;
        }

        public void Restore()
        {
            // TODO - how to restore the prior console contents when we're done?
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.SetCursorPosition(0, Height - 1);
        }

        public void RenderAll(EntityManager entityManager, GameMap map, bool redraw)
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    bool isWall = map[x, y].Blocked;

                    if (map[x, y].Visible)
                    {
                        if (isWall)
                        {
                            PutChar(x, y, '#', ConsoleColor.White);
                        }
                        else
                        {
                            PutChar(x, y, '.', ConsoleColor.White);
                        }
                    }
                    else if (map[x, y].Explored)
                    {
                        if (isWall)
                        {
                            PutChar(x, y, '#', ConsoleColor.DarkGray);
                        }
                        else
                        {
                            PutChar(x, y, '.', ConsoleColor.DarkGray);
                        }
                    }
                    else
                    {
                        PutChar(x, y, ' ');
                    }
                }
            }

            foreach (var entity in entityManager.Entities)
            {
                if (map[entity.X, entity.Y].Visible)
                {
                    DrawEntity(entity);
                }
            }

            Blit(redraw);
        }

        public void ClearAll(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                ClearEntity(entity);
            }
        }

        public void DrawEntity(Entity entity)
        {
            PutChar(entity.X, entity.Y, entity.Glyph, entity.Color);
        }

        public void ClearEntity(Entity entity)
        {
            PutChar(entity.X, entity.Y, ' ', defaultBackground);
        }

        private void PutChar(int x, int y, char c)
        {
            PutChar(x, y, c, defaultForeground, defaultBackground);
        }

        private void PutChar(int x, int y, char c, ConsoleColor foreground)
        {
            PutChar(x, y, c, foreground, defaultBackground);
        }

        private void PutChar(int x, int y, char c, ConsoleColor foreground, ConsoleColor background)
        {
            Cell cell = cells[x, y];

            cell.Foreground = foreground;
            cell.Background = background;
            cell.Char = c;
            cell.Dirty = true;
        }

        private void Blit(bool redraw)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cell cell = cells[x, y];
                    if (cell.Dirty || redraw)
                    {
                        Console.ForegroundColor = cell.Foreground;
                        Console.BackgroundColor = cell.Background;
                        Console.SetCursorPosition(x, y);
                        Console.Write(cell.Char);

                        cell.Dirty = false;
                    }
                }
            }
        }


        private class Cell
        {
            public char Char { get; set; }
            public ConsoleColor Foreground { get; set; }
            public ConsoleColor Background { get; set; }
            public bool Dirty { get; set; }
        }
    }
}
