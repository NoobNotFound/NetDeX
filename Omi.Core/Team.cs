using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Games.Omi.Core
{
    public class Team
    {
        public string Name { get; set; } = string.Empty;
        public ObservableCollection<Player> Players { get; private set; } = new();

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
            }
        }
        public ObservableCollection<(Card[] card,int playerwon)> RoundsWon { get; private set; } = new();
        public ObservableCollection<Card> TradesHave { get; private set; } = new();
        public ObservableCollection<Card> TradesGiven { get; private set; } = new();
        public Guid ID { get; internal set; }

        public Team(Guid iD, string name, Player[] players)
        {
            ID = iD;
            Name = name;
            foreach (var item in players)
            {
                item.Team = iD;
                Players.Add(item);
            }
        }
    }
}