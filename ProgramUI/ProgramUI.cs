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

            while (true)
            {
                player1.Score = 0;
                player2.Score = 0;
                _deckRepo.DealCards(player1, player2);

                while (player1.Deck.Cards.Count > 0)
                {
                    Card p1Card = player1.Deck.Cards.Peek();
                    Card p2Card = player2.Deck.Cards.Peek();

                    Console.WriteLine($"{player1.Name}'s card:");
                    PrintCard(p1Card);
                    //Console.WriteLine($"{player1.Name} plays a {p1Card.Rank} of {p1Card.Suit} ({p1Card.Color})");
                    Console.WriteLine($"{player2.Name}'s card:");
                    PrintCard(p2Card);
                    //Console.WriteLine($"{player2.Name} plays a {p2Card.Rank} of {p2Card.Suit} ({p2Card.Color})");

                    Player winner = _deckRepo.PlayHand(player1, player2, playedCards);
                    if (winner == null)
                    {
                        Console.WriteLine("Tie");
                    }
                    else
                    {
                        Console.WriteLine($"{winner.Name} won this hand.");
                    }
                    Console.ReadKey();
                    Console.Clear();
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

        public void PrintCard(Card card)
        {
            string strCardRank;
            switch(card.Rank)
            {
                case Rank.two:
                case Rank.three:
                case Rank.four:
                case Rank.five:
                case Rank.six:
                case Rank.seven:
                case Rank.eight:
                case Rank.nine:
                case Rank.ten:
                    int rankIndex = (int)card.Rank;
                    strCardRank = rankIndex.ToString();

                    break;
                case Rank.jack:
                case Rank.queen:
                case Rank.king:
                case Rank.ace:
                    strCardRank = card.Rank.ToString().ToUpper().Substring(0, 1);
                    break;
                default:
                    strCardRank = " ";
                    break;
            }

            Console.BackgroundColor = ConsoleColor.White;
            if (card.Color == Color.red)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write($"{(strCardRank.Length > 1 ? strCardRank : strCardRank + " ")}   \n");
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.Write("\n");
            //Console.BackgroundColor = ConsoleColor.White;
            switch (card.Suit)
            {
                case Suit.clubs:
                    Console.Write("  \u2663  \n");
                    break;
                case Suit.spades:
                    Console.Write("  \u2660  \n");
                    break;
                case Suit.hearts:
                    Console.Write("  \u2665  \n");
                    break;
                case Suit.diamonds:
                    Console.Write("  \u2666  \n");
                    break;
                default:
                    Console.Write("     \n");
                    break;
            }
            Console.Write($"   {(strCardRank.Length > 1 ? strCardRank : " " + strCardRank)}\n\n");
            Console.ResetColor();
        }
    }
}

