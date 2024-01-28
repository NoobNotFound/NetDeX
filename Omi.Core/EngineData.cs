using Solitaire.Games.Enums;
using Solitaire.Games.Omi.Core.Helpers;
using Solitaire.Games.Omi.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security;


namespace Solitaire.Games.Omi.Core;
public class EngineData
{
    public Players PlayersCount { get; internal set; }
    public ObservableCollection<(Card[] round, Guid TeamWon, int PlayerWon)> OldRounds { get; internal set; } = new();
    public ObservableCollection<(Card card, int player)> CurrentRound { get; private set; } = new();
    public ObservableCollection<((Card[] round, int PlayerWon)[], Guid TeamWon)> OldGames { get; internal set; } = new();
    public int CurrentPlayerPosition { get; internal set; }

    
    public int WhoSaidTrump { get; set; } = 1;
    public int WhoShared => WhoSaidTrump == 1 ? (int)PlayersCount : WhoSaidTrump - 1;

    public Types Trump { get; set; } = Types.Undefined;
    public Types CurrentRoundType { get; internal set; } = Types.Undefined;
    public Card[] UnSharedCards { get; internal set; } = [];
    public EngineData(Players playersCount)
    {
        PlayersCount = playersCount;
    }
}