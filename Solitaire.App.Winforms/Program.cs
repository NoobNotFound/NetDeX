using Solitaire.Games.Omi.Core;

namespace Solitaire.App.Winforms
{
    internal static class Program
    {
        public static Game ServerWithClient { get; private set; } = new();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Start());
        }
    }
}