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
        private Player player1 = new Player();
        private Player player2 = new Player("Computer");
        private List<Card> playedCards = new List<Card>();

        public void Run()
        {
            bool continueGame = true;
            StartGame();
            while (continueGame)
            {
                ResetGame();
                while (player1.Deck.Cards.Count > 0)
                {
                    DisplayGameInfo();
                    DisplayHand(player1);
                    DisplayHand(player2);
                    Player winner = _deckRepo.PlayHand(player1, player2, playedCards);
                    DisplayRoundResult(winner);
                }
                DisplayMatchWinner();
                continueGame = DisplayContinue();
            }
        }

        private void StartGame()
        {
            string playerName = "";
            _deckRepo.MakeDeck();
            while (playerName == "")
            {
                Console.WriteLine("Welcome to WAR!\n\n" +
                                  "What is your name?");
                playerName = Console.ReadLine();
                Console.Clear();
            }
            player1.Name = playerName;
        }

        private void ResetGame()
        {
            if (playedCards.Count > 0)
            {
                _deckRepo.AddCards(_deckRepo.ShuffleDeck(playedCards));
            }
            playedCards = new List<Card>();
            player1.Score = 0;
            player2.Score = 0;
            _deckRepo.DealCards(player1, player2);
        }

        private void DisplayGameInfo()
        {
            Console.WriteLine($"Current Score - {player1.Name}: {player1.Score}    {player2.Name}: {player2.Score}                         Rounds remaining: {player1.Deck.Cards.Count - 1}\n" +
                               "---------------------------------------------------------------------------------------\n");
        }

        private void DisplayHand(Player player)
        {
            Card card = player.Deck.Cards.Peek();
            Console.WriteLine($"{player.Name}'s card:");
            PrintCard(card);
        }

        private void DisplayRoundResult(Player winner)
        {
            if (winner == null)
            {
                Console.WriteLine("Tie");
            }
            else
            {
                Console.WriteLine($"{winner.Name} won this hand.");
            }
            Console.WriteLine("\nPress any key to play the next hand.");
            Console.ReadKey();
            Console.Clear();
        }

        private void DisplayMatchWinner()
        {
            Player winner = _deckRepo.GetWinner(player1, player2);
            Console.WriteLine($"----------------------------------------------------------------------------------------\n" +
                              $"The final score is {player1.Name} {player1.Score}, {player2.Name} {player2.Score}.\n");
            if (winner == null)
            {
                Console.WriteLine("The game resulted in a tie.");
            }
            else if (winner == player1)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
            Console.WriteLine($"----------------------------------------------------------------------------------------");
        }

        private bool DisplayContinue()
        {
            Console.WriteLine("Press Escape to exit or any other key to continue.");
            ConsoleKey input = Console.ReadKey().Key;
            Console.Clear();
            if (input == ConsoleKey.Escape)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void PrintCard(Card card) //, int numberOfCards)
        {
            // Get one character rank.
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

            // Set card color.
            Console.BackgroundColor = ConsoleColor.White;
            if (card.Color == Color.red)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }

            // Write top line of card.
            Console.Write($"{(strCardRank.Length > 1 ? strCardRank : strCardRank + " ")}   \n");

            // Write middle line of card.
            Console.BackgroundColor = ConsoleColor.White;
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

            // Write bottom line of card.
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write($"   {(strCardRank.Length > 1 ? strCardRank : " " + strCardRank)}");
            Console.ResetColor();
            //Console.Write($"x{numberOfCards}");
            Console.Write("\n\n");
        }
    }
}

