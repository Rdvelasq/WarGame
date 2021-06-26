using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarClasses;

namespace ProgramUINameSpace
{
    public class ProgramUI
    {
        private Queue<Deck> _decks = new Queue<Deck>();

        public void Run()
        {
            seedData();
            Run();
        }

        
        public void seedData()
        {
            Card card1 = new Card(WarEnums.Rank.one, WarEnums.Suit.clubs, WarEnums.Color.black);
            Card card2 = new Card(WarEnums.Rank.two, WarEnums.Suit.diamonds, WarEnums.Color.red);

            Card card3 = new Card(WarEnums.Rank.three, WarEnums.Suit.spades, WarEnums.Color.black);
            Card card4 = new Card(WarEnums.Rank.ace, WarEnums.Suit.hearts, WarEnums.Color.black);

            Queue<Card> myQueue = new Queue<Card>();
            myQueue.Enqueue(card1);
            myQueue.Enqueue(card2);
            myQueue.Enqueue(card3);
            myQueue.Enqueue(card4);

            Deck deck = new Deck(myQueue);
            

        }
        }
}

