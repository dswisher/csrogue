
using System;

namespace csrogue
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Logger.Open())
            {
                Exception boom = null;

                // Initialize the screen
                Renderer renderer = new Renderer();

                renderer.Initialize();

                try
                {
                    new Game(renderer).Run();
                }
                catch (Exception ex)
                {
                    boom = ex;
                }
                finally
                {
                    renderer.Restore();
                }

                if (boom != null)
                {
                    Console.WriteLine("Unhandled exception!");
                    Console.WriteLine(boom);
                }
            }
        }
    }
}
