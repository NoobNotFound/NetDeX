using Solitaire.Games.Omi.Core;
using Solitaire.Games;
using System;
using static System.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Solitaire.App.Console.OnDevice;
class Program
{
    public static Engine OmiEngine = new Engine(Solitaire.Games.Omi.Enums.Players.Four);
    public static int ReadNum()
    {
        int n;
        while (!int.TryParse(ReadLine(), out n))
        {
            WriteLine("Invalid input");
        }
        return n;
    }
    public static T ParseEnum<T>() where T : struct
    {
        T n;
        while (!Enum.TryParse<T>(ReadLine() ?? "",true, out n))
        {
            WriteLine("Invalid input");
        }
        return n;
    }
    static void Main(string[] args)
    {
        WriteLine("Initializing");
        OmiEngine.Initialize();

        WriteLine("Creating a new gmae");
        OmiEngine.NewGame();

        WriteLine("Enter how many times to shuffle: ");
        OmiEngine.Shuffle(ReadNum());

        OmiEngine.Share();

        WriteLine("Set trump\n" + string.Join('\n', Enum.GetValues<Solitaire.Games.Enums.Types>()));
        OmiEngine.Trump = ParseEnum<Solitaire.Games.Enums.Types>();

        OmiEngine.Share();
        ReadLine();
    }
}