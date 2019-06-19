using System;

namespace csrogue
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = Console.BufferHeight;
            int width = Console.BufferWidth;

            // TODO - how to restore the console when we're done?
            Console.Clear();
            Console.CursorVisible = false;

            try
            {
                int x = width / 2;
                int y = height / 2;

                bool done = false;
                while (!done)
                {
                    // Draw character
                    Console.SetCursorPosition(x, y);
                    Console.Write('@');

                    // Get a key
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    bool redraw = true;
                    int lastX = x;
                    int lastY = y;
                    // TODO - add a point class to lump X and Y together!
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Escape:
                            done = true;
                            redraw = false;
                            break;

                        case ConsoleKey.H:
                        case ConsoleKey.LeftArrow:
                            if (x > 0)
                            {
                                x -= 1;
                            }
                            break;

                        case ConsoleKey.J:
                        case ConsoleKey.DownArrow:
                            if (y < height - 1)
                            {
                                y += 1;
                            }
                            break;

                        case ConsoleKey.K:
                        case ConsoleKey.UpArrow:
                            if (y > 0)
                            {
                                y -= 1;
                            }
                            break;

                        case ConsoleKey.L:
                        case ConsoleKey.RightArrow:
                            if (x < width - 1)
                            {
                                x += 1;
                            }
                            break;
                    }

                    if (redraw)
                    {
                        Console.SetCursorPosition(lastX, lastY);
                        Console.Write(' ');
                    }
                }
            }
            finally
            {
                Console.CursorVisible = true;   // TODO - put this in a finally block!
                Console.SetCursorPosition(0, height - 1);
            }

            Console.WriteLine("Game Over.");
        }
    }
}
