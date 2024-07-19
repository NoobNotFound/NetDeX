using NetDeX.Games.Enums;
using NetDeX.Games.Helpers;
using NetDeX.Games.Omi.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security;

namespace NetDeX.Games.Omi.Core;
public class Engine(Players PlayersCount)
{
    public event EventHandler<(bool isDraw, Guid TeamWon)>? GameEnded;
    public event EventHandler<Guid>? MainGameEnded;
    public event EventHandler<TeamsData>? TeamDataChanged;

    public EngineData Data { get; internal set; } = new(PlayersCount);
    public Values MinValue => Data.PlayersCount == Enums.Players.Four ? Values.Seven : Values.Eight;
    public Team[] Teams { get; internal set; } = [];

    public bool IsInitialized { get; private set; } = false;
    public bool IsInGame { get; private set; } = false;
    public void Initialize()
    {
        Data.CurrentRound.Clear();
        Data.OldRounds.Clear();
        if (Data.PlayersCount == Enums.Players.Four)
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

            foreach (var item in Teams)
                item.DataChanged += (_, _) => ChangeTeamData();
            
        }
        else
        {
            throw new NotImplementedException();
        }
        IsInitialized = true;
    }
    int TeamsdataChangeCount = 0;
    //waits 50 ms to get more data changes then invoke
    private async void ChangeTeamData()
    {
        TeamsdataChangeCount++;
        var d = TeamsdataChangeCount;
        await Task.Delay(50);

        if (d == TeamsdataChangeCount)
            TeamDataChanged?.Invoke(this, new TeamsData() { Teams = Teams.Select(x=> x.ToSimpleTeam()).ToArray()});
    }
    public void NewGame()
    {
        IsInGame = true;
        Data.CurrentPlayerPosition = Data.WhoSaidTrump;
        Data.CurrentRound.Clear();
        Data.OldRounds.Clear();
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

        Data.Trump = Types.Undefined;
        Extentions.WriteLine("Clear trump");

        if (Data.PlayersCount == Enums.Players.Four)
            Data.UnSharedCards = Card.AllCards.Where(x => x.Value >= MinValue && x.Value != Values.Undefined).ToArray();
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
            if (PositionShared > ((int)Data.PlayersCount))
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
            Data.UnSharedCards.Split(4, out cardsToshare, out var r);
            Data.UnSharedCards = r.ToArray();
        }


        if (Data.PlayersCount == Enums.Players.Four)
        {
            if (Data.UnSharedCards.Count() == 32)
            {
                PositionShared = Data.WhoSaidTrump;
                SplitCards();
                addCards();
            }
            if (Data.Trump == Types.Undefined)
            {
                Extentions.WriteLine("Waiting for trump");
                return;
            }
            while (Data.UnSharedCards.Length != 0)
            {
                nextPosistion();
                SplitCards();
                addCards();
            }

            foreach (var t in Teams)
                foreach (var p in t.Players)
                    foreach (var c in p.Deck)
                        c.TrumpType = Data.Trump;
            Data.CurrentPlayerPosition = Data.WhoSaidTrump;
        }
        else
            throw new NotImplementedException();
    }
    public void CheckDeck()
    {
        Extentions.WriteLine("Checking current round");
        if(Data.PlayersCount == Players.Four)
        {
            if (Data.CurrentRound.Count != 4)
                return;

            Extentions.WriteLine("Current round's cards are full");
            var cardwon = Data.CurrentRound[0];
            Extentions.WriteLine("Checking which card won");
            foreach (var item in Data.CurrentRound.Skip(1))
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
                        t.RoundsWon.Add((Data.CurrentRound.Select(x => x.card).ToArray(), cardwon.player));
                        winGuid = t.ID;
                        Extentions.WriteLine("Team with '" + t.ID + "' won.");
                    }

            Extentions.WriteLine("Clearing current rounds");
            Data.OldRounds.Add((Data.CurrentRound.Select(x => x.card).ToArray(), winGuid, cardwon.player));
            Data.CurrentRound.Clear();
            Data.CurrentPlayerPosition = cardwon.player;
            CheckEnd();
        }
    }
    public void CheckEnd()
    {
        Extentions.WriteLine("Checking whether game ends");
        if (Data.PlayersCount == Players.Four)
        {
            if(Data.OldRounds.Count != 8) 
                return;

            Extentions.WriteLine("Game ended, checking which team won the round");
            if (Teams[0].RoundsWon.Count == Teams[1].RoundsWon.Count)
            {
                Extentions.WriteLine("Game is a draw.");
                Teams[0].Draws++;
                Teams[1].Draws++;
                Data.OldGames.Add((Data.OldRounds.Select(x => (x.round, x.PlayerWon)).ToArray(), Guid.Empty));
                GameEnded?.Invoke(this,(true,Guid.Empty));
            }else if (Teams[0].RoundsWon.Count > Teams[1].RoundsWon.Count)
            {
                Extentions.WriteLine("Team 0 won.");
                Teams[0].Wins++;
                Teams[1].Loses++;
                Data.OldGames.Add((Data.OldRounds.Select(x => (x.round, x.PlayerWon)).ToArray(), Teams[0].ID));
                GameEnded?.Invoke(this, (false, Teams[0].ID));
            }
            else
            {
                Extentions.WriteLine("Team 1 won.");
                Teams[1].Wins++;
                Teams[0].Loses++;
                Data.OldGames.Add((Data.OldRounds.Select(x => (x.round, x.PlayerWon)).ToArray(), Teams[1].ID));
                GameEnded?.Invoke(this, (false, Teams[1].ID));
            }

            Data.WhoSaidTrump++;
            if (Data.WhoSaidTrump > ((int)Data.PlayersCount))
                Data.WhoSaidTrump = 1;
        }
    }
    public void ShareTrades()
    {
        Extentions.WriteLine("Sharing trades,will return if it's a draw");
        if (Data.OldGames.Last().TeamWon == Guid.Empty)
            return;

        Extentions.WriteLine("Last game is not a draw");
        Extentions.WriteLine("Checking draws for double trade");
        int drawsbefore = 0;
        bool isCurrentDraw = false;
        int index = Data.OldGames.Count - 2;
        do
        {
            if (index >= 0)
            {
                if (Data.OldGames[index].TeamWon == Guid.Empty)
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
            Extentions.WriteLine("Double trading to team with '" + Data.OldGames.Last().TeamWon.ToString() + "' guid.");
            Card[] tradesgiving = [];

            foreach (var team in Teams)
                if (team.ID != Data.OldGames.Last().TeamWon)
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
                if (team.ID == Data.OldGames.Last().TeamWon)
                {
                    foreach (var item in tradesgiving)
                        team.TradesGiven.Add(item);
                }

            Extentions.WriteLine("Double trading done");

        }
        else
        {
            Extentions.WriteLine("Single trading to team with '" + Data.OldGames.Last().TeamWon.ToString() + "' guid.");
            Card[] tradesgiving = [];

            foreach (var team in Teams)
                if (team.ID != Data.OldGames.Last().TeamWon)
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
                if (team.ID == Data.OldGames.Last().TeamWon)
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
        Data.CurrentPlayerPosition++;
        if (Data.CurrentPlayerPosition > ((int)Data.PlayersCount))
            Data.CurrentPlayerPosition = 1;

        Extentions.WriteLine("Player" + Data.CurrentPlayerPosition + " has to place a card");
    }
    public void PlaceCard(Card card,int p)
    {
        Extentions.WriteLine("Player " + p + " is trying to place a card");
        if (Data.CurrentPlayerPosition != p)
            return;

        if (Data.CurrentRound.Any(x => x.player == p))
            return;

        if (card == null)
            return;

        foreach (var t in Teams)
            foreach (var pl in t.Players)

                if (pl.Position == p)
                {
                    if(pl.Deck.Any(x=> x.Value == card.Value && x.Type == card.Type))
                    {
                        if (Data.CurrentRound.Any())                        
                            if(Data.CurrentRoundType != card.Type)                            
                                if(pl.Deck.Any(x=> x.Type == Data.CurrentRoundType))
                                {
                                    Extentions.WriteLine("Player " + p + " has " + Data.CurrentRoundType + " cards.");
                                    return;
                                }
                            
                       

                        if (!Data.CurrentRound.Any())
                            Data.CurrentRoundType = card.Type;

                        Data.CurrentRound.Add((pl.Deck.First(x => x.Value == card.Value && x.Type == card.Type), p));
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
            Data.UnSharedCards = [.. Data.UnSharedCards.ToList().Shuffle()];
        }
        Extentions.WriteLine($"Shuffled cards {times} times\n" + string.Join(',',Data.UnSharedCards.Select(x=> x.ToString())));
    }

}