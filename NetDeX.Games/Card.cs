using NetDeX.Games.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetDeX.Games.Omi.Core
{
    public class Card
    {
        public Types Type { get; set; }
        public Values Value { get; set; }
        public Types TrumpType { get; set; }
        public int Owner { get; set; }
        public Card() : this(Types.Undefined,Values.Undefined) { }
        public Card(Types type, Values value) : this(type, value, Types.Undefined) { }
        public Card(Types type, Values value,Types trump)
        {
            Type = type;
            Value = value; 
            TrumpType = trump;
        }

        public static Card[] AllDiamond => Enum.GetValues<Values>().Where(x=> x != Values.Joker && x != Values.Undefined).Select(x => new Card(Types.Diamond, x)).ToArray();
        public static Card[] AllHeart => Enum.GetValues<Values>().Where(x => x != Values.Joker && x != Values.Undefined).Select(x => new Card(Types.Heart, x)).ToArray();
        public static Card[] AllScope => Enum.GetValues<Values>().Where(x => x != Values.Joker && x != Values.Undefined).Select(x => new Card(Types.Spade, x)).ToArray();
        public static Card[] AllCalibri => Enum.GetValues<Values>().Where(x => x != Values.Joker && x != Values.Undefined).Select(x => new Card(Types.Club, x)).ToArray();
        public static Card[] AllCards
        {
            get
            {
                var l = new List<Card>();

                l.AddRange(AllDiamond);
                l.AddRange(AllHeart);
                l.AddRange(AllScope);
                l.AddRange(AllCalibri);

                return l.ToArray();
            }
        }

        public static bool operator >(Card c1, Card c2)
        {
            if (c1.Type == c2.Type)
                return c1.Value > c2.Value;
            else
            {
                if (c1.TrumpType == c2.TrumpType)
                {
                    if (c1.Type == c1.TrumpType)
                        return true;
                    else if (c2.Type == c2.TrumpType)
                        return false;
                    else
                        return true;
                }
                else
                    return true;
            }
        }
        public static bool operator <(Card c1, Card c2)
            => !(c1 > c2);

        public override string ToString()
        {
            return Type + " " + Value;
        }
    }
}
