# Omi Card Game

Welcome to Omi, a LAN-based multiplayer and single-player card game developed in .NET for local gaming enjoyment. Play with your friends over a local network or challenge computer-controlled opponents for a solo gaming experience.

## Features

- **LAN Multiplayer:** Connect with friends over a local area network for exciting multiplayer matches.
- **Single-Player Mode:** Enjoy the game even when playing solo with computer-controlled opponents.
- **Intuitive User Interface:** A user-friendly interface for an enjoyable gaming experience.
- **Scalable Architecture:** Built with scalability in mind to accommodate future updates and additional features.
- **Graphics and Animations:** Enhance your gaming experience with visually appealing graphics and animations.
- **Customizable Settings:** Tailor the game to your preferences with customizable settings.

## Getting Started

Clone the repository to your local machine.
   ```bash
   git clone https://github.com/NoobNotFound/Solitaire.git
   ```

Alternatively, you can [go to releases](https://github.com/NoobNotFound/Solitaire/releases) to download the latest version of the game.

```C#
var OmiEngine = new Solitaire.Games.Omi.Core.Engine(Games.Omi.Enums.Players.Four);
OmiEngine.Initialize();

OmiEngine.NewGame();  //Start a new game
OmiEngine.Shuffle(5);  
OmiEngine.Share();
```

## Feedback

Your feedback is valuable! If you encounter any issues or have suggestions for improvement, please create an issue in the [GitHub repository](https://github.com/NoobNotFound/Solitaire/issues).

## License

This project is licensed under the [GNU General Public License v3.0 (GPL-3.0)](LICENSE).

Enjoy playing Omi!
