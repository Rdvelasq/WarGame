using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarClasses;
using WarEnums;

namespace WarRepoNameSpace
{
    public class WarRepo
    {
        private Queue<Card> _cards = new Queue<Card>();

        // Create
        public Queue<Card> MakeDeck()
        {
            List<Card> unshuffledDeck = new List<Card>();

            //First prepare an unshuffled deck
            //For loops thorugh the 4 suits
            for(int suit = 1; suit <= 4; suit++)
            {
                //Second For Loop to loop through the 13 cards and giving it the suit from the previous loop 
                for(int rank = 2; rank <= 14; rank++)
                {
                    Rank rankEnum = (Rank)rank;
                    Suit suitEnum = (Suit)suit;
                    Card card = new Card(rankEnum, suitEnum);
                    unshuffledDeck.Add(card);
                }
            }

            //Then shuffle the unshuffled deck
            _cards = ShuffleDeck(unshuffledDeck);

            return GetDeck();
        }

        public Queue<Card> ShuffleDeck(List<Card> deck)
        {
            Random rand = new Random();
            Queue<Card> shuffledDeck = new Queue<Card>();

            while (deck.Count > 0)
            {
                int randomCardIndex = rand.Next(deck.Count);
                shuffledDeck.Enqueue(deck[randomCardIndex]);
                deck.RemoveAt(randomCardIndex);
            }

            return shuffledDeck;
        }

        // Read
        public Queue<Card> GetDeck() => _cards;

        // Add/Update
        public void AddCards(Queue<Card> deck)
        {
            foreach (Card card in deck)
            {
                _cards.Enqueue(card);
            }
        }

        // Delete
        public void DealCards(Player player1, Player player2)
        {
            bool playerToggle = false;

            // Deal cards 1 by 1 to simulate actual play
            for (int i = _cards.Count - 1; i >= 0; i--)
            {
                playerToggle = !playerToggle;
                if (playerToggle)
                {
                    player1.Deck.Cards.Enqueue(_cards.Dequeue());
                }
                else
                {
                    player2.Deck.Cards.Enqueue(_cards.Dequeue());
                }
            }
        }

        public Player PlayHand(Player player1, Player player2, List<Card> playedCards)
        {
            Card p1Card = player1.Deck.Cards.Dequeue();
            Card p2Card = player2.Deck.Cards.Dequeue();
            playedCards.Add(p1Card);
            playedCards.Add(p2Card);

            if (p1Card.Rank == p2Card.Rank)
            {
                // Tie
                return null;
            }
            else if (p1Card.Rank > p2Card.Rank)
            {
                // Player 1 wins
                player1.Score++;
                return player1;
            }
            else
            {
                // Player 2 wins
                player2.Score++;
                return player2;
            }
        }

        public Player GetWinner(Player player1, Player player2)
        {
            if (player1.Score == player2.Score)
            {
                return null;
            }
            else if (player1.Score > player2.Score)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
    }
}