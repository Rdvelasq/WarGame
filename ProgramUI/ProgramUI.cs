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

        int numberRounds = 0;

        public void Run()
        {
            bool continueGame = true;
            PrintBlankCard();
            StartGame();
            while (continueGame)
            {
                ResetGame();
                while ((player1.Deck.Cards.Count != 0) && (player2.Deck.Cards.Count != 0))
                {
                    numberRounds++;
                    DisplayGameInfo();
                    DisplayHand(player1);
                    DisplayHand(player2);
                    Player winner = _deckRepo.PlayHand(player1, player2);
                    //int cardsPlayed = 1;
                    if ((player1.Deck.Cards.Count < 10) || (player2.Deck.Cards.Count < 10))
                    {
                        
                        
                        Console.WriteLine("HI");

                    }
                    while (winner == null)
                    {                 
                        DisplayGameInfo();
                        DisplayHand(player1, false);
                        DisplayHand(player2, false);
                        for (int i = 0; i <= 10; i++)
                        {
                            _deckRepo.AddCardToPile(player1, player2);
                            if((player1.Deck.Cards.Count == 1) || (player2.Deck.Cards.Count == 1))
                            {
                                break;
                            }
                        }
                        Continue();
                        DisplayGameInfo();
                        DisplayHand(player1);
                        DisplayHand(player2);
                        winner = _deckRepo.PlayHand(player1, player2);

                    }

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
            
            Console.WriteLine($"Current Score - {player1.Name}: {player1.Deck.Cards.Count}    {player2.Name}: {player2.Deck.Cards.Count} \n" +
                              $"Current Round: {numberRounds} \n" +
                               "---------------------------------------------------------------------------------------\n");
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
                PrintCard(card);
            }
            else
            {
                PrintBlankCard();
            }
        }

        private void DisplayRoundResult(Player winner)
        {
            int winningPotCount = _deckRepo.WinningPotCount();
            _deckRepo.AwardPot(winner);
            Console.WriteLine($"{winner.Name} won {winningPotCount} cards");          
            Continue();
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
        private void PrintBlankCard()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("     ");
            Console.WriteLine(" War ");
            Console.WriteLine("     ");
            Console.ResetColor();
        }

        private void Continue()
        {
            Console.WriteLine("\nPress any key to play the next card.");
            Console.ReadKey();
            Console.Clear();
        }
    }

    
}

