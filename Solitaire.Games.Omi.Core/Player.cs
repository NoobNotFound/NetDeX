using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Games.Omi.Core
{
    public class SimplePlayer
    {
        public int Position;
        public Guid ID;
        public Card[] Deck;
        public int Wins;
        public Guid Team;
        public string Name;
        public int Loses;
    }
    public class Player
    {
        public SimplePlayer ToSimplePlayer()
        {
            return new SimplePlayer()
            {
                Position = Position,
                ID = ID,
                Deck = Deck.ToArray(),
                Wins = Wins,
                Team = Team,
                Name = Name,
                Loses = Loses
            };
        }
        public event EventHandler<(Card[] deck, int player)>? DeckChanged;
        public event EventHandler? NameChanged;
        public int Position { get; internal set; }
        public Guid ID { get; internal set; }
        public ObservableCollection<Card> Deck { get; private set; } = new();
        public int Wins { get; internal set; } = 0;
        public Guid Team { get; internal set; }

        string _Name;
        public string Name
        {
            get=> _Name;
            set
            {
                _Name = string.IsNullOrWhiteSpace(value) ? _Name : value;
                NameChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public int Loses { get; internal set; }

        public Player(string name, Guid iD,int position)
        {
            Name = name;
            Position = position;
            ID = iD;
            Deck.CollectionChanged += Deck_Changed;
        }

        private void Deck_Changed(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DeckChanged?.Invoke(this,(Deck.ToArray(),Position));
        }
    }
}
