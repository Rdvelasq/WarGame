using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarClasses
{
    public class Deck
    {
        public Queue<Card> Cards = new Queue<Card>();

        public Deck()
        {

        }

        public Deck(Queue<Card> cards)
        {
            Cards = cards;
        }
    }
}
