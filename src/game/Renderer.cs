using System;
using System.Collections.Generic;

namespace csrogue
{
    public class Renderer
    {
        public Renderer()
        {
        }

        public void RenderAll(List<Entity> entities, GameMap map)
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    if (map[x, y].Blocked)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write('#');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('.');
                    }
                }
            }

            foreach (var entity in entities)
            {
                DrawEntity(entity);
            }
        }

        public void ClearAll(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                ClearEntity(entity);
            }
        }

        public void DrawEntity(Entity entity)
        {
            Console.SetCursorPosition(entity.X, entity.Y);
            Console.ForegroundColor = entity.Color;
            Console.Write(entity.Glyph);
        }

        public void ClearEntity(Entity entity)
        {
            // TODO - how to know the "default" background color?
            Console.SetCursorPosition(entity.X, entity.Y);
            Console.Write(' ');
        }
    }
}
