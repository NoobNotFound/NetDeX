using Solitaire.Games.Enums;
using Solitaire.Games.Omi.Core.Helpers;
using Solitaire.Games.Omi.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security;

namespace Solitaire.Games.Omi.Core;
public class Engine(Players playersCount)
{
    public event EventHandler<(bool isDraw, Guid TeamWon)>? GameEnded;
    public event EventHandler<Guid>? MainGameEnded;
    public Values MinValue => PlayersCount == Enums.Players.Four ? Values.Seven : Values.Eight;
    public Players PlayersCount { get; private set; } = playersCount;
    public Team[] Teams { get; private set; } = [];

    public ObservableCollection<(Card[] round,Guid TeamWon,int PlayerWon)> OldRounds { get; private set; } = new();
    public ObservableCollection<(Card card,int player)> CurrentRound { get; private set; } = new();
    public ObservableCollection<((Card[] round,int PlayerWon)[], Guid TeamWon)> OldGames { get; private set; } = new();
    public int CurrentPlayerPosition { get; private set; }

    public int WhoSaidTrump { get; set; } = 1;
    public int WhoShared => WhoSaidTrump == 1 ? (int)PlayersCount : WhoSaidTrump - 1;

    public Types Trump { get; set; } = Types.Undefined;
    public Types CurrentRoundType { get; internal set; } = Types.Undefined;
    public Card[] UnSharedCards { get; private set; } = [];

    public bool IsInitialized { get; private set; } = false;
    public bool IsInGame { get; private set; } = false;
    public void Initialize()
    {
        CurrentRound.Clear();
        OldRounds.Clear();
        if (PlayersCount == Enums.Players.Four)
        {
            Teams =
            [
                new(Guid.NewGuid(),"Team 1",[
                    new Player("Player 1",Guid.NewGuid(),1),
                    new Player("Player 3",Guid.NewGuid(),3),
                ]),
                new(Guid.NewGuid(),"Team 2",[
                    new Player("Player 2",Guid.NewGuid(),2),
                    new Player("Player 4",Guid.NewGuid(),4),
                ])
            ];
            Extentions.WriteLine("Added new teams and players");

            foreach (var item in Card.AllCards.Where(x => x.Type == Types.Heart || x.Type == Types.Diamond).Where(x=> x.Value < MinValue))
                Teams[0].TradesHave.Add(item);

            foreach (var item in Card.AllCards.Where(x => x.Type == Types.Club || x.Type == Types.Spade).Where(x => x.Value < MinValue))
                Teams[1].TradesHave.Add(item);

            Extentions.WriteLine("Added trades to the teams");

        }
        else
        {
            throw new NotImplementedException();
        }
        IsInitialized = true;
    }
    public void NewGame()
    {
        IsInGame = true;
        CurrentPlayerPosition = WhoSaidTrump;
        CurrentRound.Clear();
        OldRounds.Clear();
        foreach (var item in Teams)
            item.RoundsWon.Clear();
        
        Extentions.WriteLine("Cleared the old rounds");

        foreach (var t in Teams)
        {
            t.RoundsWon.Clear();
            Extentions.WriteLine("Cleared the old rounds saved inside " + t.Name);
            foreach (var p in t.Players)
            {
                p.Deck.Clear();
                Extentions.WriteLine("Cleared " + p.Name + "'s deck");
            }
        }

        Trump = Types.Undefined;
        Extentions.WriteLine("Clear trump");

        if (PlayersCount == Enums.Players.Four)
            UnSharedCards = Card.AllCards.Where(x => x.Value >= MinValue && x.Value != Values.Undefined).ToArray();
        else
            throw new NotImplementedException();

        Extentions.WriteLine("Recreated cards");
    }

    private int PositionShared;
    public void Share()
    {
        Card[] cardsToshare = [];
        void nextPosistion()
        {
            PositionShared++;
            if (PositionShared > ((int)PlayersCount))
                PositionShared = 1;
        }
        void addCards()
        {
            foreach (var t in Teams)
                foreach (var p in t.Players)

                    if (p.Position == PositionShared)
                        foreach (var c in cardsToshare)
                        {
                            p.Deck.Add(c);
                            Extentions.WriteLine(p.Name + "'s deck + " + c);
                        }
        }
        void SplitCards()
        {
            UnSharedCards.Split(4, out cardsToshare, out var r);
            UnSharedCards = r.ToArray();
        }


        if (PlayersCount == Enums.Players.Four)
        {
            if (UnSharedCards.Count() == 32)
            {
                PositionShared = WhoSaidTrump;
                SplitCards();
                addCards();
            }
            if (Trump == Types.Undefined)
            {
                Extentions.WriteLine("Waiting for trump");
                return;
            }
            while (UnSharedCards.Length != 0)
            {
                nextPosistion();
                SplitCards();
                addCards();
            }

            foreach (var t in Teams)
                foreach (var p in t.Players)
                    foreach (var c in p.Deck)
                        c.TrumpType = Trump;
            CurrentPlayerPosition = WhoSaidTrump;
        }
        else
            throw new NotImplementedException();
    }
    public void CheckDeck()
    {
        Extentions.WriteLine("Checking current round");
        if(PlayersCount == Players.Four)
        {
            if (CurrentRound.Count != 4)
                return;

            Extentions.WriteLine("Current round's cards are full");
            var cardwon = CurrentRound[0];
            Extentions.WriteLine("Checking which card won");
            foreach (var item in CurrentRound.Skip(1))
            {
                if(item.card > cardwon.card)
                    cardwon = item;
            }
            Extentions.WriteLine("Card won: " + cardwon.card + ", Player won: " + cardwon.player);

            Extentions.WriteLine("Checking which team won");
            Guid winGuid = Guid.Empty;
            foreach (var t in Teams)
                foreach (var pl in t.Players)
                    if (pl.Position == cardwon.player)
                    {
                        t.RoundsWon.Add((CurrentRound.Select(x => x.card).ToArray(), cardwon.player));
                        winGuid = t.ID;
                        Extentions.WriteLine("Team with '" + t.ID + "' won.");
                    }

            Extentions.WriteLine("Clearing current rounds");
            OldRounds.Add((CurrentRound.Select(x => x.card).ToArray(), winGuid, cardwon.player));
            CurrentRound.Clear();
            CurrentPlayerPosition = cardwon.player;
            CheckEnd();
        }
    }
    public void CheckEnd()
    {
        Extentions.WriteLine("Checking whether game ends");
        if (PlayersCount == Players.Four)
        {
            if(OldRounds.Count != 8) 
                return;

            Extentions.WriteLine("Game ended, checking which team won the round");
            if (Teams[0].RoundsWon.Count == Teams[1].RoundsWon.Count)
            {
                Extentions.WriteLine("Game is a draw.");
                Teams[0].Draws++;
                Teams[1].Draws++;
                OldGames.Add((OldRounds.Select(x => (x.round, x.PlayerWon)).ToArray(), Guid.Empty));
                GameEnded?.Invoke(this,(true,Guid.Empty));
            }else if (Teams[0].RoundsWon.Count > Teams[1].RoundsWon.Count)
            {
                Extentions.WriteLine("Team 0 won.");
                Teams[0].Wins++;
                Teams[1].Loses++;
                OldGames.Add((OldRounds.Select(x => (x.round, x.PlayerWon)).ToArray(), Teams[0].ID));
                GameEnded?.Invoke(this, (false, Teams[0].ID));
            }
            else
            {
                Extentions.WriteLine("Team 1 won.");
                Teams[1].Wins++;
                Teams[0].Loses++;
                OldGames.Add((OldRounds.Select(x => (x.round, x.PlayerWon)).ToArray(), Teams[1].ID));
                GameEnded?.Invoke(this, (false, Teams[1].ID));
            }

            WhoSaidTrump++;
            if (WhoSaidTrump > ((int)PlayersCount))
                WhoSaidTrump = 1;
        }
    }
    public void ShareTrades()
    {
        Extentions.WriteLine("Sharing trades,will return if it's a draw");
        if (OldGames.Last().TeamWon == Guid.Empty)
            return;

        Extentions.WriteLine("Last game is not a draw");
        Extentions.WriteLine("Checking draws for double trade");
        int drawsbefore = 0;
        bool isCurrentDraw = false;
        int index = OldGames.Count - 2;
        do
        {
            if (index >= 0)
            {
                if (OldGames[index].TeamWon == Guid.Empty)
                {
                    drawsbefore--;
                    isCurrentDraw = true;
                }
                else
                    isCurrentDraw = false;
            }

        } while (isCurrentDraw);
        Extentions.WriteLine(drawsbefore + " draws found.");

        if ((drawsbefore % 2) == 0 && drawsbefore != 0)
        {
            Extentions.WriteLine("Double trading to team with '" + OldGames.Last().TeamWon.ToString() + "' guid.");
            Card[] tradesgiving = [];

            foreach (var team in Teams)
                if (team.ID != OldGames.Last().TeamWon)
                {
                    if (team.TradesHave.Count >= 2)
                    {
                        team.TradesHave.Split(2, out tradesgiving, out var left);
                        team.TradesHave.Clear();

                        foreach (var item in left)
                            team.TradesHave.Add(item);

                    }
                }

            foreach (var team in Teams)
                if (team.ID == OldGames.Last().TeamWon)
                {
                    foreach (var item in tradesgiving)
                        team.TradesGiven.Add(item);
                }

            Extentions.WriteLine("Double trading done");

        }
        else
        {
            Extentions.WriteLine("Single trading to team with '" + OldGames.Last().TeamWon.ToString() + "' guid.");
            Card[] tradesgiving = [];

            foreach (var team in Teams)
                if (team.ID != OldGames.Last().TeamWon)
                {
                    if (team.TradesHave.Count >= 1)
                    {
                        team.TradesHave.Split(1, out tradesgiving, out var left);
                        team.TradesHave.Clear();

                        foreach (var item in left)
                            team.TradesHave.Add(item);

                    }
                }

            foreach (var team in Teams)
                if (team.ID == OldGames.Last().TeamWon)
                {
                    foreach (var item in tradesgiving)
                        team.TradesGiven.Add(item);
                }

            Extentions.WriteLine("Single trading done");
        }

        Extentions.WriteLine("Checking whether a team won the whole game");
        if (Teams[0].TradesHave.Count == 0)
        {
            Extentions.WriteLine("Team 0 has no trades left");
            MainGameEnded?.Invoke(this, Teams[1].ID);
        }
        if (Teams[1].TradesHave.Count == 0)
        {
            Extentions.WriteLine("Team 1 has no trades left");
            MainGameEnded?.Invoke(this, Teams[0].ID);
        }
    }
    private void NextPlayerPosition()
    {
        CurrentPlayerPosition++;
        if (CurrentPlayerPosition > ((int)PlayersCount))
            CurrentPlayerPosition = 1;

        Extentions.WriteLine("Player" + CurrentPlayerPosition + " has to place a card");
    }
    public void PlaceCard(Card card,int p)
    {
        Extentions.WriteLine("Player " + p + " is trying to place a card");
        if (CurrentPlayerPosition != p)
            return;

        if (CurrentRound.Any(x => x.player == p))
            return;


        foreach (var t in Teams)
            foreach (var pl in t.Players)

                if (pl.Position == p)
                {
                    if(pl.Deck.Any(x=> x.Value == card.Value && x.Type == card.Type))
                    {
                        if (CurrentRound.Any())                        
                            if(CurrentRoundType != card.Type)                            
                                if(pl.Deck.Any(x=> x.Type == CurrentRoundType))
                                {
                                    Extentions.WriteLine("Player " + p + " has " + CurrentRoundType + " cards.");
                                    return;
                                }
                            
                       

                        if (!CurrentRound.Any())
                            CurrentRoundType = card.Type;

                        CurrentRound.Add((pl.Deck.First(x => x.Value == card.Value && x.Type == card.Type), p));
                        pl.Deck.Remove(x => x.Value == card.Value && x.Type == card.Type);
                        Extentions.WriteLine("Placing card by player " + p + " is successful.");
                        NextPlayerPosition();
                        CheckDeck();
                    }
                }
    }
    public void Shuffle(int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            UnSharedCards = [.. UnSharedCards.ToList().Shuffle()];
        }
        Extentions.WriteLine($"Shuffled cards {times} times\n" + string.Join(',',UnSharedCards.Select(x=> x.ToString())));
    }

}