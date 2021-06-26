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
        public Color Color { get; set; }
        public Card()
        {

        }
        public Card(Rank rank, Suit suit, Color color)
        {
            Rank = rank;
            Suit = suit;
            Color = color;
        }
    }

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

    public class Player
    {
        public Deck Deck { get; set; }
        public Player()
        {

        }
        public Player(Deck deck)
        {
            Deck = deck;
        }
    }
}