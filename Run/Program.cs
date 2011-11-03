using System;

namespace Run
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Base game = new Base())
            {
                game.Run();
            }
        }
    }
}

