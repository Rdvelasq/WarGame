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
        int cardsLaidWhenTied = 10;
        private Player player1 = new Player();
        private Player player2 = new Player("Computer");
        int numberRounds = 0;

        public void Run()
        {
            bool continueGame = true;
            Player winner = default;
            PrintBlankCard(1);
            StartGame();
            while (continueGame)
            {
                ResetGame(winner);
                while ((player1.Deck.Cards.Count != 0) && (player2.Deck.Cards.Count != 0))
                {
                    numberRounds++;
                    DisplayGameInfo();
                    DisplayHand(player1);
                    DisplayHand(player2);
                    winner = _deckRepo.PlayHand(player1, player2);
                    while (winner == null)
                    {
                        if ((player1.Deck.Cards.Count == 0) || (player2.Deck.Cards.Count == 0))
                        {
                            _deckRepo.GetWinner(player1, player2);
                            break;
                        }
                        DisplayRoundResult(winner);
                        DisplayGameInfo();
                        for (int i = 0; i < cardsLaidWhenTied; i++)
                        {
                            _deckRepo.AddCardToPile(player1, player2);
                            if ((player1.Deck.Cards.Count == 1) || (player2.Deck.Cards.Count == 1))
                            {
                                break;
                            }
                        }
                        DisplayHand(player1, false);
                        DisplayHand(player2, false);
                        DisplayNumCardsInEachPile();
                        Continue();
                        DisplayGameInfo();
                        DisplayHand(player1);
                        DisplayHand(player2);
                        winner = _deckRepo.PlayHand(player1, player2);
                    }
                    DisplayRoundResult(winner);
                }
                winner = _deckRepo.GetWinner(player1, player2);
                DisplayMatchWinner(winner);
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

        private void ResetGame(Player winner)
        {
            if (winner != default)
            {
                List<Card> unshuffledDeck = new List<Card>();
                for (int i = winner.Deck.Cards.Count - 1; i >= 0; i--)
                {
                    unshuffledDeck.Add(winner.Deck.Cards.Dequeue());
                }
                _deckRepo.AddCards(_deckRepo.ShuffleDeck(unshuffledDeck));
            }
            _deckRepo.DealCards(player1, player2);
            numberRounds = 0;
        }

        private void DisplayGameInfo()
        {
            Console.Clear();
            string info = $"Cards Remaining - {player1.Name}: {player1.Deck.Cards.Count - 1}    {player2.Name}: {player2.Deck.Cards.Count - 1}";
            string rounds = $"Current Round: {numberRounds}";
            string separator = "---------------------------------------------------------------------------------------";
            Console.WriteLine(info + rounds.PadLeft(separator.Length - info.Length) + "\n" +
                              separator + "\n");
        }

        private void DisplayHand(Player player)
        {
            DisplayHand(player, true);
        }
        
        private void DisplayHand(Player player, bool faceUp)
        {
            Card card = player.Deck.Cards.Peek();
            Console.WriteLine($"{player.Name}'s card:");
            if (faceUp)
            {
                PrintCard(card, _deckRepo.WinningPotCount() / 2 + 1);
            }
            else
            {
                PrintBlankCard(_deckRepo.WinningPotCount() / 2);
            }
        }

        private void DisplayRoundResult(Player winner)
        {
            if (winner == null)
            {
                int maxCards = cardsLaidWhenTied;
                maxCards = Math.Min(maxCards, player1.Deck.Cards.Count - 1);
                maxCards = Math.Min(maxCards, player2.Deck.Cards.Count - 1);
                Console.WriteLine($"We have a tie!  Each player will now play {maxCards} cards face down.");
            }
            else
            {
                int winningPotCount = _deckRepo.WinningPotCount();
                _deckRepo.AwardPot(winner);
                Console.WriteLine($"{winner.Name} won {winningPotCount} cards.");
            }
            Continue();
        }

        private void DisplayMatchWinner(Player winner)
        {
            Console.WriteLine($"----------------------------------------------------------------------------------------\n");
            if (winner == player1)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
            Console.WriteLine($"\n----------------------------------------------------------------------------------------");
        }

        private void DisplayNumCardsInEachPile()
        {
            Console.WriteLine($"Each player adds {(_deckRepo.WinningPotCount() / 2) - 1} cards to their pile.");
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

        private void PrintCard(Card card, int numberOfCards)
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
            if (numberOfCards > 1)
            {
                Console.Write($" x{numberOfCards}");
            }
            Console.Write("\n\n");
        }
        private void PrintBlankCard(int numberOfCards)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("     ");
            Console.WriteLine(" War ");
            Console.Write("     ");
            Console.ResetColor();
            if (numberOfCards > 1)
            {
                Console.Write($" x{numberOfCards}");
            }
            Console.Write("\n\n");
        }

        private void Continue()
        {
            Console.WriteLine("\nPress any key to play the next card.");
            Console.ReadKey();
            Console.Clear();
        }
    }

    
}

