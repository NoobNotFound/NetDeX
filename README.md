# NetDeX

NetDeX is a a LAN-based multiplayer and single-player card game engine developed in .NET for local gaming enjoyment (especially for Sri Lankans). Play with your friends over a local network or challenge computer-controlled opponents for a solo gaming experience.

## Features

- Supports Omi, 304 is planned to implement.
- **LAN Multiplayer:** Connect with friends over a local area network for exciting multiplayer matches.
- **Single-Player Mode:** Enjoy the game even when playing solo with computer-controlled opponents. (not planned yet)
- **Intuitive User Interface:** A user-friendly interface for an enjoyable gaming experience. (to do)
- **Customizable Settings:** Tailor the game to your preferences with customizable settings.

## Getting Started

Clone the repository to your local machine.
   ```bash
   git clone https://github.com/NoobNotFound/Solitaire.git
   ```

Alternatively, you can [go to releases](https://github.com/NoobNotFound/Solitaire/releases) to download the latest version of the game.

**OmiEngine**
```C#
var OmiEngine = new Solitaire.Games.Omi.Core.Engine(Games.Omi.Enums.Players.Four);
OmiEngine.Initialize();

OmiEngine.NewGame();  //Start a new game
OmiEngine.Shuffle(5);  //Shuffle 5 times
OmiEngine.Share();   //Start share
//so on
```
**Game (LAN)**
```C#
var game = new Game();

game.Host("192.168.0.1", 12345);
game.Join("192.168.0.1", 12345); //No need to do this if you are the host
game.RequestPlayer(1);
game.Reset();
game.NewGame();
game.Shuffle(3);
game.Share();
game.SetTrump(Types.Diamond);
//so on
game.JoinPlayerSuccess += (sender, playerPosition) =>
{
    Console.WriteLine($"Player {playerPosition} joined successfully!");
};

game.Engine.EngineData.DataChanged += (sender, engineData) =>
{
    Console.WriteLine("Engine Data Changed:");
    // Handle the updated engine data
};

game.Engine.TeamData.DataChanged += (sender, teamData) =>
{
    Console.WriteLine("Team Data Changed:");
    // Handle the updated team data
};

```
this example is an all in one so do not copy paste because it may not work.

## Feedback

Your feedback is valuable! If you encounter any issues or have suggestions for improvement, please create an issue in the [GitHub repository](https://github.com/NoobNotFound/Solitaire/issues).

## License

This project is licensed under the [GNU General Public License v3.0 (GPL-3.0)](LICENSE).

Enjoy playing Omi!


---

*This README was generated with the assistance of AI.*
