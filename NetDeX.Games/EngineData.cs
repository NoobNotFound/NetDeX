using CommunityToolkit.Mvvm.ComponentModel;
using NetDeX.Games.Enums;
using NetDeX.Games.Helpers;
using NetDeX.Games.Omi.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security;


namespace NetDeX.Games.Omi.Core;
public class EngineSimpleData
{
    public Players PlayersCount { get; set; }

    public (Card[] round, Guid TeamWon, int PlayerWon)[] OldRounds = [];

    public (Card card, int player)[] CurrentRound = [];

    public ((Card[] round, int PlayerWon)[], Guid TeamWon)[] OldGames = [];

    public int CurrentPlayerPosition = 1;

    public int WhoSaidTrump = 1;

    public Types Trump = Types.Undefined;

    public Types CurrentRoundType = Types.Undefined;

    public Card[] UnSharedCards = [];
}
public partial class EngineData : ObservableObject
{
    public event EventHandler<EngineSimpleData>? DataChanged;
    public Players PlayersCount { get; set; }

    public ObservableCollection<(Card[] round, Guid TeamWon, int PlayerWon)> OldRounds { get; internal set; } = new();
    public ObservableCollection<(Card card, int player)> CurrentRound { get; private set; } = new();
    public ObservableCollection<((Card[] round, int PlayerWon)[], Guid TeamWon)> OldGames { get; internal set; } = new();

    [ObservableProperty]
    private int _CurrentPlayerPosition = 1;

    [ObservableProperty]
    private int _WhoSaidTrump = 1;
    public int WhoShared => WhoSaidTrump == 1 ? (int)PlayersCount : WhoSaidTrump - 1;

    [ObservableProperty]
    private Types _Trump = Types.Undefined;

    [ObservableProperty]
    private Types _CurrentRoundType = Types.Undefined;

    [ObservableProperty]
    private Card[] _UnSharedCards = [];

    public EngineData(Players playersCount)
    {
        PlayersCount = playersCount;

        this.OldRounds.CollectionChanged += CollectionsChanged;
        this.CurrentRound.CollectionChanged += CollectionsChanged;
        this.OldGames.CollectionChanged += CollectionsChanged;

        this.PropertyChanged += (s, e) => ChangeData();
    }

    private void CollectionsChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        ChangeData();

    }

    int dataChangeCount = 0;
    //waits 50 ms to get more data changes then invoke
    private async void ChangeData()
    {
        dataChangeCount++;
        var d = dataChangeCount;
        await Task.Delay(50);

        if (d == dataChangeCount)
            DataChanged?.Invoke(this, this.ToSimpleData());
    }
    public EngineSimpleData ToSimpleData()
    {
        return new EngineSimpleData
        {
            PlayersCount = PlayersCount,
            CurrentRound = CurrentRound.ToArray(),
            OldRounds = OldRounds.ToArray(),
            OldGames = OldGames.ToArray(),
            CurrentPlayerPosition = CurrentPlayerPosition,
            WhoSaidTrump = WhoSaidTrump,
            CurrentRoundType = CurrentRoundType,
            Trump = Trump,
            UnSharedCards = UnSharedCards
        };
    }
}