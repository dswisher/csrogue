
namespace csrogue
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the screen
            Renderer renderer = new Renderer();

            renderer.Initialize();

            try
            {
                new Game(renderer).Run();
            }
            finally
            {
                renderer.Restore();
            }
        }
    }
}
