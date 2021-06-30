using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarEnums;

namespace WarClasses
{
    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public Color Color { get; }

        public Card()
        {

        }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
            Color = ((suit == Suit.clubs || suit == Suit.spades) ? Color.black : Color.red);
        }
    }
}