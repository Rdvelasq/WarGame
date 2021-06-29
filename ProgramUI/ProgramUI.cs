using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarClasses;
using WarEnums;
using WarRepoNameSpace;

namespace ProgramUINameSpace
{
    public class ProgramUI
    {
        private readonly WarRepo _deckRepo = new WarRepo();

        public void Run()
        {
            _deckRepo.MakeDeck();
            Player player1 = new Player("You");
            Player player2 = new Player("Computer");
            List<Card> playedCards = new List<Card>();

            //Console.WriteLine($"{card.Rank} of {card.Suit} ({card.Color})");

            while (true)
            {
                player1.Score = 0;
                player2.Score = 0;
                _deckRepo.DealCards(player1, player2);

                while (player1.Deck.Cards.Count > 0)
                {
                    Card p1Card = player1.Deck.Cards.Peek();
                    Card p2Card = player2.Deck.Cards.Peek();
                    Console.WriteLine($"{player1.Name} plays a {p1Card.Rank} of {p1Card.Suit} ({p1Card.Color})");
                    Console.WriteLine($"{player2.Name} plays a {p2Card.Rank} of {p2Card.Suit} ({p2Card.Color})");

                    Player winner = _deckRepo.PlayHand(player1, player2, playedCards);
                    if (winner == null)
                    {
                        Console.WriteLine("Tie");
                    }
                    else
                    {
                        Console.WriteLine($"{winner.Name} won this hand.");
                    }
                }

                Console.WriteLine("----------------------");
                Console.WriteLine($"The final score is {player1.Name} {player1.Score}, {player2.Name} {player2.Score}.");
                Console.WriteLine(player1.Score > player2.Score ? "You win!" : "You lose!");
                Console.WriteLine("Press Escape to exit or any other key to continue.");

                ConsoleKey input = Console.ReadKey().Key;
                if (input == ConsoleKey.Escape)
                {
                    break;
                }
                Console.Clear();

                _deckRepo.AddCards(_deckRepo.ShuffleDeck(playedCards));
            }
        }
    }
}

