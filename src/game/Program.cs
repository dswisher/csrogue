using System;

namespace csrogue
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;

            // Initialize the screen
            Console.Clear();
            Console.CursorVisible = false;

            try
            {
                new Game(height, width).Run();
            }
            finally
            {
                // TODO - how to restore the console when we're done?
                Console.CursorVisible = true;
                Console.ResetColor();
                Console.SetCursorPosition(0, height - 1);
            }
        }
    }
}
