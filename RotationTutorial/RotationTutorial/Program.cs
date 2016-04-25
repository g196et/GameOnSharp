using System;

namespace RotationTutorial
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameStateManager game = new GameStateManager())
            {
                game.Run();
            }
        }
    }
#endif
}

