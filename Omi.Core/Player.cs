using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Games.Omi.Core
{
    public class Player
    {
        public event EventHandler<(Card[] deck, int player)>? DeckChanged;
        public int Position { get; internal set; }
        public Guid ID { get; internal set; }
        public ObservableCollection<Card> Deck { get; private set; } = new();
        public int Wins { get; internal set; } = 0;
        public Guid Team { get; internal set; }
        
        public string Name { get; set; } = string.Empty;
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
