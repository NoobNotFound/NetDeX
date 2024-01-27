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
        while (!Enum.TryParse<T>(ReadLine() ?? "", true, out n))
        {
            WriteLine("Invalid input");
        }
        return n;
    }
    public static Game OmiGame { get; set; }
    static void Main(string[] args)
    {
        OmiGame = new Game();
        OmiGame.Host(System.Net.IPAddress.Parse("127.0.0.1"), 56044);
    }
}