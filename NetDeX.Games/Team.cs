using NetDeX.Games.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDeX.Games.Omi.Core
{
    public class TeamsData
    {
        public SimpleTeam[] Teams { get; set; } = [];
    }
    public class SimpleTeam
    {
        public string Name;
        public SimplePlayer[] Players;
        public int Wins;
        public int Draws;
        public int Loses;
        public (Card[] card, int playerwon)[] RoundsWon;
        public Card[] TradesHave;
        public Card[] TradesGiven;
        public Guid ID;
    }
    public class Team
    {
        public SimpleTeam ToSimpleTeam()
        {
            return new SimpleTeam 
            { 
                Name = Name,
                Players = Players.Select(x=> x.ToSimplePlayer()).ToArray(),
                Wins = Wins,
                Draws = Draws,
                Loses = Loses,
                RoundsWon = RoundsWon.ToArray(),
                TradesHave = TradesHave.ToArray(),
                TradesGiven = TradesGiven.ToArray(),
                ID = ID
            };
        }
        public event EventHandler? DataChanged;

        private string _Name = string.Empty;
        public string Name
        {
            get => _Name;
            internal set
            {
                _Name = value;
                ChangeData();
            }
        }

        private int _Wins;
        public int Wins
        {
            get => _Wins;
            internal set
            {
                _Wins = value;
                foreach (var player in Players)
                {
                    player.Wins = value;
                }
                ChangeData();
            }
        }

        private int _Draws;
        public int Draws
        {
            get => _Draws;
            internal set
            {
                _Draws = value;
                foreach (var player in Players)
                {
                    player.Loses = value;
                }
                ChangeData();
            }
        }

        private int _Loses;
        public int Loses
        {
            get => _Loses;
            internal set
            {
                _Loses = value;
                foreach (var player in Players)
                {
                    player.Loses = value;
                }
                ChangeData();
            }
        }

        public ObservableCollection<Player> Players { get; private set; } = new();
        public ObservableCollection<(Card[] card,int playerwon)> RoundsWon { get; private set; } = new();
        public ObservableCollection<Card> TradesHave { get; private set; } = new();
        public ObservableCollection<Card> TradesGiven { get; private set; } = new();

        public Guid ID { get; internal set; }

        public Team(Guid iD, string name, Player[] players)
        {
            ID = iD;
            Name = name;
            Players.AddRange(players);

            foreach (var item in Players)
            {
                item.DeckChanged += (_,_) => ChangeData();
                item.NameChanged += (_,_) => ChangeData();
            }
            RoundsWon.CollectionChanged += (_, _) => ChangeData();
            TradesHave.CollectionChanged += (_, _) => ChangeData();
            TradesGiven.CollectionChanged += (_, _) => ChangeData();
        }

        int dataChangeCount = 0;
        private async void ChangeData()
        {
            dataChangeCount++;
            var d = dataChangeCount;
            await Task.Delay(50);

            if (d == dataChangeCount)
                DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}